using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


public class Client : MonoBehaviour
{
	protected Socket m_clientSocket;
	protected byte[] m_readBuffer;

	public void connect(string ip, short port)
	{
		m_readBuffer = new byte[1024];
		m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			System.IAsyncResult result = m_clientSocket.BeginConnect(ip, port, EndConnect, null);
			bool connectSuccess = result.AsyncWaitHandle.WaitOne(System.TimeSpan.FromSeconds(10));
			if (!connectSuccess)
			{
				m_clientSocket.Close();
				Debug.LogError(string.Format("Client unable to connect. Failed"));
			}
		}
		catch (System.Exception ex)
		{
			Debug.LogError(string.Format("Client exception on beginconnect: {0}", ex.Message));
		}
	}

	void EndConnect(System.IAsyncResult iar)
	{
		m_clientSocket.EndConnect(iar);
		m_clientSocket.NoDelay = true;
		BeginReceiveData();
		Debug.Log("Client connected");
	}
	void OnDestroy()
	{
		if (m_clientSocket != null)
		{
			m_clientSocket.Close();
			m_clientSocket = null;
		}
	}
	void BeginReceiveData()
	{
		m_clientSocket.BeginReceive(m_readBuffer, 0, m_readBuffer.Length, SocketFlags.None, EndReceiveData, null);
	}
	void EndReceiveData(System.IAsyncResult iar)
	{
		int numBytesReceived = m_clientSocket.EndReceive(iar);
		handleData(numBytesReceived);
		BeginReceiveData();
	}

	virtual public void handleData(int numBytesRecv)
	{
	
	}

	public void Write(OutPacket outPacket)
	{
		m_clientSocket.BeginSend(outPacket.ToArray(), 0, outPacket.Length, SocketFlags.None, EndSend, outPacket.ToArray());
	}

	void EndSend(System.IAsyncResult iar)
	{
		m_clientSocket.EndSend(iar);
		byte[] msg = (iar.AsyncState as byte[]);

		System.Array.Clear(msg, 0, msg.Length);
		msg = null;
	}
}