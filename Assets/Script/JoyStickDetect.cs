using UnityEngine;
using System.Collections;

public class JoyStickDetect : MonoBehaviour {

	public GUIText Detect;	//要顯示的GuiText
	public int Num;			//判斷狀態 狀態如果為1表示隱藏 如果是2就是顯示各個玩家的左右鍵是否正常 如果是3就是顯示各個角色的上下建是否正常
	void Start () {
		Num = 1;	//狀態為1隱藏GuiText不顯示
		Detect.guiText.pixelOffset = new Vector2(-Screen.width/2*0.9965f,-Screen.height/2+75);	//位置調整
	}


	void Update () {

		if(Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick2Button4)|| Input.GetKeyDown(KeyCode.Joystick3Button4)|| Input.GetKeyDown(KeyCode.Joystick4Button4))
		//各個搖桿如果按下按鍵3的時候就切換GuiText的狀態
		{
			Num++;	//切換狀態
			if(Num > 3)	//狀態如果超過三的時候
			{
				Num = 1;	//就把狀態拉回1
			}
		}



		switch (Num)	//狀態的轉換函數
		{
		case 1:		//狀態為1時
			Detect.color = new Color(1,1,1,0);	//alpha為0 所以隱藏GuiText
			break;
		case 2:
			Detect.color = new Color(1,1,1,1);	//alpha為1 所以GuiText顯示
			DetectH();	//顯示各個搖桿的左右鍵狀況
			break;
		case 3:
			Detect.color = new Color(1,1,1,1);	//alpha為1 所以GuiText顯示
			DetectV();	//顯示各個搖桿的上下鍵狀況
			break;
		default:
			break;
		}



	}

	void DetectH ()
	{
		switch (Input.GetJoystickNames().Length)
		{
		case 2:	//如果搖桿為2枝時
			Detect.text = "Horizontal"+"\n" +"No.1"+Input.GetJoystickNames()[0]+Input.GetAxis("Horizontal1")+"\n" +"No.2"+ Input.GetJoystickNames()[1]+Input.GetAxis("Horizontal2")+"\n" ;
			//顯示兩枝的搖桿名稱及左右鍵按鍵的數值
			break;
		case 3:	//如果搖桿為3枝時
			Detect.text = "Horizontal"+"\n" +"No.1"+Input.GetJoystickNames()[0]+Input.GetAxis("Horizontal1")+"\n" +"No.2"+ Input.GetJoystickNames()[1]+Input.GetAxis("Horizontal2")+"\n" +"No.3"+Input.GetJoystickNames()[2]+Input.GetAxis("Horizontal3")+"\n" ;
			//顯示三枝的搖桿名稱及左右鍵按鍵的數值
			break;
		case 4:	//如果搖桿為4枝時
			Detect.text = "Horizontal"+"\n" +"No.1"+Input.GetJoystickNames()[0]+Input.GetAxis("Horizontal1")+"\n" +"No.2"+ Input.GetJoystickNames()[1]+"\n"+"No.3"+Input.GetJoystickNames()[2]+Input.GetAxis("Horizontal3")+"\n" + "No.4"+Input.GetJoystickNames()[3]+Input.GetAxis("Horizontal4")+"\n" ;
			//顯示四枝的搖桿名稱及左右鍵按鍵的數值
			break;
		default :
			break;
		}
	}
	void DetectV()
	{
		switch (Input.GetJoystickNames().Length)
		{
		case 2:	//如果搖桿為2枝時
			Detect.text = "Vertical"+"\n" +"No.1"+Input.GetJoystickNames()[0]+Input.GetAxis("Vertical1")+"\n" +"No.2"+ Input.GetJoystickNames()[1]+Input.GetAxis("Vertical2")+"\n" ;
			//顯示兩枝的搖桿名稱及左右鍵按鍵的數值
			break;
		case 3:	//如果搖桿為3枝時
			Detect.text = "Vertical"+"\n" +"No.1"+Input.GetJoystickNames()[0]+Input.GetAxis("Vertical1")+"\n" +"No.2"+ Input.GetJoystickNames()[1]+Input.GetAxis("Vertical2")+"\n" +"No.3"+Input.GetJoystickNames()[2]+Input.GetAxis("Vertical3")+"\n" ;
			//顯示三枝的搖桿名稱及左右鍵按鍵的數值
			break;
		case 4:	//如果搖桿為4枝時
			Detect.text = "Vertical"+"\n" +"No.1"+Input.GetJoystickNames()[0]+Input.GetAxis("Vertical1")+"\n" +"No.2"+ Input.GetJoystickNames()[1]+Input.GetAxis("Vertical2")+"\n"+"No.3"+Input.GetJoystickNames()[2]+Input.GetAxis("Vertical3")+"\n" + "No.4"+Input.GetJoystickNames()[3]+Input.GetAxis("Vertical4")+"\n" ;
			//顯示四枝的搖桿名稱及左右鍵按鍵的數值
			break;
		default :
			break;
		}
	}
}
