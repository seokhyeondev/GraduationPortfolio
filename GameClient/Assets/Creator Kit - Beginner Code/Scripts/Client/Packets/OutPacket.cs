using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OutPacket : PacketBase
	{
		private MemoryStream m_stream;
		private BinaryWriter m_writer;
		private bool m_disposed;

		public override int Length
		{
			get { return (int)m_stream.Position; }
		}
		public override int Position
		{
			get { return (int)m_stream.Position; }
			set { m_stream.Position = value; }
		}

		public bool Disposed
		{
			get
			{
				return m_disposed;
			}
		}

		public OutPacket(int protocolId = 0)
		{
			m_stream = new MemoryStream(64);
			m_writer = new BinaryWriter(m_stream);
			m_disposed = false;

			WriteUShort((ushort)protocolId);
		}


		public OutPacket(int protocolID, bool specialHeader)
			: this(protocolID)
		{
			if (specialHeader)
				WriteByte((byte)protocolID);
		}

		public void WriteBool(bool value)
		{
			ThrowIfDisposed();
			WriteByte(value ? (byte)1 : (byte)0);
		}

		public void WriteByte(byte value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}
		public void WriteSByte(sbyte value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteBytes(params byte[] value)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteShort(short value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteUShort(ushort value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteInt(int value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteFloat(float value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteUInt(uint value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteLong(long value = 0)
		{
			ThrowIfDisposed();

			m_writer.Write(value);
		}

		public void WriteULong(ulong value = 0)
		{
			ThrowIfDisposed();
		
			m_writer.Write(value);
		}

		public void WriteString(string value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			var bytes = Encoding.GetEncoding(949).GetBytes(value);

			WriteShort((short)bytes.Length);
			WriteBytes(bytes);
		}

		private void ThrowIfDisposed()
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
		}

		public override byte[] ToArray()
		{
			ThrowIfDisposed();

			return m_stream.ToArray();
		}

		#region Zlib

		[Flags]
		public enum CompressedDataFlags : byte
		{
			None = 0,
			zlib = 1,
			KartCrypto = 2
		}


		private int Adler32(byte[] bytes)
		{
			const uint a32mod = 65521;
			uint s1 = 1, s2 = 0;
			foreach (byte b in bytes)
			{
				s1 = (s1 + b) % a32mod;
				s2 = (s2 + s1) % a32mod;
			}
			return unchecked((int)((s2 << 16) + s1));
		}

		#endregion

		public void Dispose()
		{
			m_disposed = true;

			if (m_stream != null)
				m_stream.Dispose();

			m_stream = null;
		}
	}
