using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LoginPacket : MonoBehaviour
{
	public static OutPacket sendLogin(LoginResultType type, string id, string pw, int job = -1)
	{
		OutPacket outPacket = new OutPacket((int) OutHeader.REQUEST_LOGIN);

		outPacket.WriteByte((byte) type);
		outPacket.WriteString(id);
		outPacket.WriteString(pw);
		outPacket.WriteByte((byte)job);

		return outPacket;
	}

	public static OutPacket requestEnterField(int id)
	{
		OutPacket outPacket = new OutPacket((int)OutHeader.REQUEST_ENTER_FIELD);

		outPacket.WriteInt(id);

		return outPacket;
	}
}