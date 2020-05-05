using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public abstract class PacketBase
	{
		public abstract int Length { get; }
		public abstract int Position { get; set; }

		public abstract byte[] ToArray();

		public override string ToString()
		{
			var sb = new StringBuilder();

			foreach (byte b in ToArray())
			{
				sb.AppendFormat("{0:X2} ", b);
			}

			return sb.ToString();
		}
	}

