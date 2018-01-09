using UnityEngine;
using System.Collections;

public class Click1 : MonoBehaviour {
	
	public GameObject Start1;	//開始的場景
	public GameObject People2;	//選人數的介面

	public GameObject Panel3;	//選角色的介面
	public GameObject Stage6;	//選場景的介面

	public string CurrenSense;	//記錄當前所出現的介面 start為最一開始 people2為選角介面 Panel3為選角 stage6為選場景

	private People2 people2;	//記錄People2腳本
	private Panel3 panel3;		//記錄Panel3腳本
	public Stage6 stage6;		//記錄Stage6腳本
	public int select;			//記錄當前的玩家數
	public float Panel3Interval; //所有玩家選角後過多少秒進入下個頁面
	private float _Panel3Interval;//記錄Panel3Interval的秒數
	public float Stage6Interval; //進入選場景介面後經過多少秒才讓玩家開始選擇場景 避免玩家按太快就進入遊戲
	private float _Stage6Interval;///記錄Stage6Interval的秒數
	public AudioSource ButtonVoice;	//玩家按鍵的聲音
	void Start () {
		Screen.showCursor = false;	//設定滑鼠關閉

		CurrenSense = "start";		//目前出現的介面為Start
		people2 = transform.GetComponent<People2>();	//載入People2腳本
		panel3 = transform.GetComponent<Panel3>();		//載入Panel3腳本
		stage6 = GetComponent<Stage6>();	//載入Stage6腳本

		Start1.SetActive(true);		//開啟start1的介面
		People2.SetActive(false);	//關閉選人數的介面
		Panel3.SetActive(false);	//關閉選角介面
		Stage6.SetActive(false);	//關閉場景介面
		stage6.enabled = false;		//關閉選場景介面
		_Stage6Interval = Stage6Interval;
		_Panel3Interval = Panel3Interval;	//讓_Panel3Interval載入Panel3Interval數字
	}
	

	void Update () {

		//下行程式是各個搖桿按下按鍵4就會從新載入場景
		if(Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Joystick2Button5)|| Input.GetKeyDown(KeyCode.Joystick3Button5)|| Input.GetKeyDown(KeyCode.Joystick4Button5) || Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel("Character");
		}




		switch (CurrenSense)						//依照目前的介面做該場景可以做的事
		{
			case "start":		//目前為start介面
			if(Input.GetButtonDown("Hor1Jump"))	//如果P1按下確認鍵(小跳鍵)後
			{
				CurrenSense = "people2";	//改變為選人數介面
				Start1.SetActive(false);	//關閉start介面
				People2.SetActive(true);	//開啟選人數介面
			}
				
			break;
			case "people2":		//目前為選人數介面
			if(Input.GetButtonDown("Hor1Jump"))	//如果P1按下確認鍵(小跳鍵)鍵後
			{
				panel3.ChoseCha = true;		//開啟panel3的ChoseCha參數 這樣玩家才就可以開始選角 避免玩家在亂按左右鍵動到所選的選角
				People2.SetActive(false);	//選人數介面關閉
				Panel3.SetActive(true);		//開啟選角戒面
				people2.enabled = false;	//關閉選人數的腳本 避免玩家選角時也動到所選擇的玩家人數
				CurrenSense = "panel3";		//改變當前介面為選角
				SendMessage("select");		//記錄當前玩家數
			}
				
			break;
				case "panel3":	//目前為選角介面
			if(panel3.AllCheck == true)	//如果大家都選擇完後
			{
				Panel3Interval -=Time.deltaTime;	//開始倒數要進入下個介面的時間
				if(Panel3Interval <=0)			//時間到數完後
				{
					Panel3.SetActive(false);	//關閉選角介面
					panel3.ChoseCha = false;	//關閉選角的控制 避免因為下個場景的控制而擾亂所選擇好的角色
					CurrenSense = "stage6";		//記錄當前選角為選介面
					Stage6.SetActive(true);		//選場景介面開啟
					//StartCoroutine(OpenStage6());	//開啟選場景的控制 避免玩家按太快而進入遊戲
				}
			}else{
				Panel3Interval = _Panel3Interval;	//如果大家還沒選擇完前 就讓倒數的秒數一直維持著不要倒數
			}
				
			break;
			case "stage6":		//如果為選場景介面 不做任何事
			Stage6Interval-=Time.deltaTime;		//倒數stage6的選場景時間空檔 倒數完後才可以選場景 避免玩家按太快而進入遊戲
			if(Stage6Interval <=0)	//倒數完後
			{
				stage6.enabled = true;	//腳本開啟 開始讓玩家選擇場景
			}
				break;
			default:	//如果場景超出預期時
				print ("finish");	//Debug出finish
			break;
			}


		switch (CurrenSense)						////回上一頁
		{
		case "stage6":		//目前為選場景介面
		if(Input.GetButtonDown("Hor1Skill"))	//當P1按下確認鍵(小跳鍵)後
		{
			Stage6Interval = _Stage6Interval;	//讓變數回到原本的時間 (等到stage6場景開始時再開始倒數)
			stage6.enabled = false;				//Stage6腳本關閉 不讓玩家控制
			Stage6.SetActive(false);			//選擇場景介面關閉 
			Panel3.SetActive(true);				//選擇角色介面開啟
			panel3.ChoseCha = true;				//開啟變數讓玩家開始選角
			panel3.P1CheckIsTrue();				//取消P1的確定
			panel3.P2CheckIsTrue();				//取消P2的確定
			panel3.P3CheckIsTrue();				//取消P3的確定
			panel3.P4CheckIsTrue();				//取消P4的確定

			panel3.AllCheck = false;			//取消大家都按確定
			
			CurrenSense = "panel3";				//記錄現在介面為選角介面
			}
		break;							
			
		case "panel3":	//目前為選角介面
		if(Input.GetButtonDown("Hor1Skill"))	//當P1按下確認鍵(小跳鍵)後
		{
			People2.SetActive(true);	//選人數介面開啟
			Panel3.SetActive(false);	//選角色介面關閉
			people2.enabled = true;		//選人數的腳本開啟讓玩家開始選擇
			CurrenSense = "people2";	//記錄當前介面為選人數介面
		}
		break;
		case "people2":		//目前為選人數介面
		if(Input.GetButtonDown("Hor1Skill"))	//當P1按下確認鍵(小跳鍵)後
		{
			CurrenSense = "start";		//記錄當前介面為start
			Start1.SetActive(true);		//開啟start介面
			People2.SetActive(false);	//開啟選人數介面
		}
		break;
		}

		if(Input.GetAxis("Horizontal1") <= -0.8f)		//當P1按下左鍵時
		{
			ButtonVoice.Play();		//播放音效
		}
		
		
		if(Input.GetAxis("Horizontal1") >= 0.8f)		//當P1按下右鍵時
		{
			ButtonVoice.Play();		//播放音效
		}
		
		if(Input.GetAxis("Horizontal2") <= -0.8f)		//當P2按下左鍵時	
		{
			ButtonVoice.Play();		//播放音效
		}
		
		
		if(Input.GetAxis("Horizontal2") >= 0.8f)		//當P2按下右鍵時
		{
			ButtonVoice.Play();		//播放音效
		}
		
		
		if(Input.GetAxis("Horizontal3") <= -0.8f)		//當P3按下左鍵時	
		{
			ButtonVoice.Play();		//播放音效
		}
		
		
		if(Input.GetAxis("Horizontal3") >= 0.8f)		//當P3按下右鍵時
		{
			ButtonVoice.Play();		//播放音效
		}
		
		
		
		if(Input.GetAxis("Horizontal4") <= -0.8f)		//當P4按下左鍵時
		{
			ButtonVoice.Play();		//播放音效
		}
		
		if(Input.GetAxis("Horizontal4") >= 0.8f)		//當P4按下右鍵時
		{
			ButtonVoice.Play();		//播放音效
		}
	}


}
