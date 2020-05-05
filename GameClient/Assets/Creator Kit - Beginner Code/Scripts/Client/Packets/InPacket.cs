﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InPacket : PacketBase
	{
		private readonly byte[] _buffer;
		private int _index;

		public override int Position
		{
			get { return _index; }
			set { _index = value; }
		}
		public override int Length
		{
			get { return _buffer.Length; }
		}

		public int Available
		{
			get
			{
				return _buffer.Length - _index;
			}
		}
		public uint UdpUnknownVal { get; set; }
		public bool UdpPacket { get; set; }

		public InPacket(byte[] packet)
		{
			_buffer = packet;
			_index = 0;
		}

		private void CheckLength(int length)
		{
			if (_index + length > _buffer.Length || length < 0)
				throw new Exception("Not enough space");
		}

		public bool ReadBool()
		{
			return ReadByte() == 1;
		}

		public byte ReadByte()
		{
			CheckLength(1);
			return _buffer[_index++];
		}

		public sbyte ReadSByte()
		{
			return (sbyte)ReadByte();
		}

		public byte[] ReadBytes(int count)
		{
			CheckLength(count);
			var temp = new byte[count];
			Buffer.BlockCopy(_buffer, _index, temp, 0, count);
			_index += count;
			return temp;
		}

		public unsafe short ReadShort()
		{
			CheckLength(2);

			short value;

			fixed (byte* ptr = _buffer)
			{
				value = *(short*)(ptr + _index);
			}

			_index += 2;

			return value;
		}
		public ushort ReadUShort()
		{
			return (ushort)ReadShort();
		}

		public unsafe int ReadInt()
		{
			CheckLength(4);

			int value;

			fixed (byte* ptr = _buffer)
			{
				value = *(int*)(ptr + _index);
			}

			_index += 4;

			return value;
		}

		public unsafe float ReadFloat()
		{
			CheckLength(4);

			float value;

			fixed (byte* ptr = _buffer)
			{
				value = *(float*)(ptr + _index);
			}

			_index += 4;

			return value;
		}

		public uint ReadUInt()
		{
			return (uint)ReadInt();
		}

		public unsafe long ReadLong()
		{
			CheckLength(8);

			long value;

			fixed (byte* ptr = _buffer)
			{
				value = *(long*)(ptr + _index);
			}

			_index += 8;

			return value;
		}

		public ulong ReadULong()
		{
			return (ulong)ReadLong();
		}

		public string ReadString(bool ascii = true)
		{
			int length = ReadInt();
			if (!ascii) length *= 2;
			CheckLength(length);

			if (ascii)
				return System.Text.Encoding.GetEncoding(949).GetString(ReadBytes(length));
			else
				return System.Text.Encoding.Unicode.GetString(ReadBytes(length));
		}

		public void Skip(int count)
		{
			CheckLength(count);
			_index += count;
		}

		public override byte[] ToArray()
		{
			var final = new byte[_buffer.Length];
			Buffer.BlockCopy(_buffer, 0, final, 0, _buffer.Length);
			return final;
		}

		public string ReadString()
		{
			int length = ReadShort();
			CheckLength(length);

		return System.Text.Encoding.GetEncoding(949).GetString(ReadBytes(length));
		}
	}
