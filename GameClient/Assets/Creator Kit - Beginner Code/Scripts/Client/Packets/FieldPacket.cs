using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FieldPacket : MonoBehaviour
{
	public static OutPacket movePlayer(int x, int y)
	{
		OutPacket outPacket = new OutPacket((int) OutHeader.MOVE_PLAYER);

		outPacket.WriteInt(x);
		outPacket.WriteInt(y);

		return outPacket;
	}

	public static OutPacket requestEnterField(int id)
	{
		OutPacket outPacket = new OutPacket((int)OutHeader.REQUEST_ENTER_FIELD);

		outPacket.WriteInt(id);

		return outPacket;
	}
}