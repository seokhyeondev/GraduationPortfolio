using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : Client
{
	private JobEnum nowJob;
	private bool isGuiShow;
	private byte renderType;
	private string guiMsg = "";

	public GameObject loginPanel;
	public GameObject registerPanel;
	public InputField idTxt, pwTxt;
	public Button warriorImg, mageImg, archerImg, assassinImg;

	// Start is called before the first frame update
	void Awake()
    {
		base.connect("127.0.0.1", 9418);
		registerPanel.SetActive(false);
	}

	public override void handleData(int numBytesRecv)
	{
		InPacket inPacket = new InPacket(m_readBuffer);
		InHeader opCode = (InHeader)inPacket.ReadShort();
		Debug.Log("opcode" + opCode.ToString());
		switch (opCode)
		{
			case InHeader.LOGIN_RESULT:
				handleLogin(inPacket);
				break;
		}
	}

    // Update is called once per frame
    void Update()
	{
		if (renderType == 1)
			SceneManager.LoadScene("ExampleScene");
		else if (renderType == 2)
			showLogin();

		renderType = 0;
	}

	public void onLogin()
	{
		Write(LoginPacket.sendLogin(LoginResultType.REQ_LOGIN, idTxt.text, pwTxt.text));
	}

	public void showRegister()
	{
		loginPanel.SetActive(false);
		registerPanel.SetActive(true);
	}

	public void showLogin()
	{
		registerPanel.SetActive(false);
		loginPanel.SetActive(true);
		
	}

	public void onRegister()
	{
		Write(LoginPacket.sendLogin(LoginResultType.REQ_REGISTER, idTxt.text, pwTxt.text, (int) nowJob));
	}

	void OnGUI()
	{
		if (isGuiShow)
		{
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), guiMsg))
			{
				isGuiShow = false;
			}
		}
	}


	private void handleLogin(InPacket inPacket)
	{
		LoginResultType loginResult = (LoginResultType)inPacket.ReadByte();
		Debug.Log(loginResult.ToString());

		switch (loginResult)
		{
			case LoginResultType.INVAILD_PASSWORD:
				guiMsg = "비밀번호를 확인해주세요.";
				break;
			case LoginResultType.NOT_REGISTER:
				guiMsg = "가입되지 않은 아이디입니다.";
				break;
			case LoginResultType.BANNED:
				guiMsg = "게임을 이용할 수 없는 아이디입니다.";
				break;
			case LoginResultType.SUCCESS_REGISTER:
				guiMsg = "회원가입에 성공했습니다.";
				renderType = 2;
				break;
			case LoginResultType.ALREADY_EXIST_ID:
				guiMsg = "이미 가입된 아이디 입니다.";
				break;
			case LoginResultType.ALREADY_LOGGINED:
				guiMsg = "이미 로그인중인 아이디입니다.";
				break;
			case LoginResultType.SUCCESS_LOGIN:
				//MainScript.accountNo = inPacket.ReadInt();
				renderType = 1;
				return;
		}

		if (!guiMsg.Equals(""))
		{
			isGuiShow = true;
		}
	}
}
