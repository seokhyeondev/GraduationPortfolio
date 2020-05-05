using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum LoginResultType
{
	NOT_REGISTER,
	SUCCESS_LOGIN,
	INVAILD_PASSWORD,
	BANNED,
	ALREADY_LOGGINED,
	SUCCESS_REGISTER,
	ALREADY_EXIST_ID,
	REQ_LOGIN = 10,
	REQ_REGISTER = 11,
};
