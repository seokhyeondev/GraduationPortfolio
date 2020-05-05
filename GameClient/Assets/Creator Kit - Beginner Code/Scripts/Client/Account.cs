using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Account : MonoBehaviour
{
	private int id;
	private string name, pw;

	public Account(int id, string name, string pw)
	{
		this.id = id;
		this.name = name;
		this.pw = pw;
	}

}