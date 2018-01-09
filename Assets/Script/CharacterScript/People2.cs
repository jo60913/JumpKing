using UnityEngine;
using System.Collections;

public class People2 : MonoBehaviour {

	public UISprite TwoPeoImage;		//兩個人的人數圖片
	public UISprite ThreePeoImage;		//三個人的人數圖片
	public UISprite FourPeoImage;		//四個人的人數圖片
	private int CurrenPeople;			//現在玩家所選的人數
	private Click1 _main;				//用來取得click腳本
	//☆public UISprite KeyboardImg;		//玩家介面介紹圖片 因為後面沒有放置 所以程式碼忽略 如果要用的話要把有打開頭打☆的程式碼弄出來 就是刪除// 以及開頭為/* 以及結尾為*/的程式碼
	//☆private bool KeyboardCheck;		//確認現在玩家介面介紹圖片是否開啟
	private bool Stick1Check;			//這是搖桿的CD時間 因為搖桿右鍵按住的時候會向右跑到底 並不會像鍵盤按右鍵一樣 只跳一格 所以要給搖桿按鍵CD時間
	void Start () {
		CurrenPeople = 1;				//設定CurrenPeople一開始為1 代表現在人數為2個人 CurrenPeople為2時代表3個人 CurrenPeople為3食代表4個人
		_main = transform.GetComponent<Click1>();	//取得click1的錶本
		//☆KeyboardCheck = false;			//設定KeyboardCheck代表設定現在玩家介面介紹維關閉
		Stick1Check = true;				//搖桿可以按左右控制
	}
	

	void Update () {
		/*☆if(Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick2Button4)|| Input.GetKeyDown(KeyCode.Joystick3Button4)|| Input.GetKeyDown(KeyCode.Joystick4Button4)||Input.GetKeyDown(KeyCode.Q))
		{
			if(KeyboardCheck == true)
			{
				KeyboardImg.gameObject.SetActive(true);
				KeyboardCheck = false;
				Debug.Log("true");
			}else if(KeyboardCheck == false)
				{
					KeyboardImg.gameObject.SetActive(false);
					KeyboardCheck = true;
					Debug.Log("false");
				}
		}*/
		
		if(Input.GetAxis("Horizontal1") <= -0.8f)		///P1按下左鍵
		{
			if(Stick1Check == true)//如果可以控制的話
			{
				Stick1Check = false;	//關閉P1的控制
				StartCoroutine(StickCon1(Input.GetAxis("Horizontal1")));	//P1左右的CD程式碼
				
				if(CurrenPeople <1)		//如果選擇的人數小於1的話 
				{
					CurrenPeople = 1;	//就跑1 代表現在最少只能選2個人
				}
			}
		}
		
		if(Input.GetAxis("Horizontal1") >= 0.8f)		///P1按下右鍵
		{
			if(Stick1Check == true)//如果可以控制的話
			{
				Stick1Check = false;	//關閉P1的控制
				StartCoroutine(StickCon1(Input.GetAxis("Horizontal1")));	//P1左右的CD程式碼
				
				if(CurrenPeople > 3)		//如果選擇的人數大於3的話 
				{
					CurrenPeople = 3;		//就跑3 代表現在最少只能選4個人
				}
			}
		}
		switch (CurrenPeople)		//依照現在所選的人數切換現在的人數圖片
		{
		case 1:	//如果CurrenPeople為1時 (代表現在所選人數為2個人)
			TwoPeoImage.gameObject.SetActive(true);		//開啟2PP的圖片 代表現在選擇兩個人
			ThreePeoImage.gameObject.SetActive(false);	//關閉3P的圖片
			FourPeoImage.gameObject.SetActive(false);	//關閉4P的圖片
			PlayerPrefs.SetInt("peoplemath",2);		//在本機端記錄人數為2 等進入遊戲場景的時候 讓GameManager腳本知道
			_main.select = 2;		//再click1的腳本中的select變數記錄為2 代表有兩位玩家 這變數放變後面腳本取得現在的人數
			break;
		case 2:	//如果CurrenPeople為2時 (代表現在所選人數為3個人)
			TwoPeoImage.gameObject.SetActive(false);	//關閉2PP的圖片
			ThreePeoImage.gameObject.SetActive(true);	//開啟3P的圖片 代表現在選擇三個人
			FourPeoImage.gameObject.SetActive(false);	//關閉4P的圖片
			PlayerPrefs.SetInt("peoplemath",3);		//在本機端記錄人數為3 等進入遊戲場景的時候 讓GameManager腳本知道
			_main.select = 3;		//再click1的腳本中的select變數記錄為3 代表有三位玩家 這變數放變後面腳本取得現在的人數
			break;
		case 3:	//如果CurrenPeople為3時 (代表現在所選人數為4個人)
			TwoPeoImage.gameObject.SetActive(false);	//關閉2PP的圖片 
			ThreePeoImage.gameObject.SetActive(false);	//開啟3P的圖片
			FourPeoImage.gameObject.SetActive(true);	//關閉4P的圖片	代表現在選擇四個人
			PlayerPrefs.SetInt("peoplemath",4);		//在本機端記錄人數為4 等進入遊戲場景的時候 讓GameManager腳本知道
			_main.select = 4;		//再click1的腳本中的select變數記錄為4 代表有四位玩家 這變數放變後面腳本取得現在的人數
			break;
		default:	//如果超出預期的話
			print ("peoplefinish");	//顯示peoplefinish
			break;
			
		}
	}
	IEnumerator StickCon1(float Direct)		//P1的左右按鍵的CD		
	{
		if(Direct >= 0.5f)					//如果按下的數值超過 0.5 	(搖桿按下上下左右鍵時 會有個數值從0慢慢加到1 須要以該數值做為確認
		{
			CurrenPeople++;					//現在玩家所選的人數再加一
			yield return new WaitForSeconds(0.15f);	//等待0.15秒的CD時間
			Stick1Check = true;				//CD時間結束 P1可以再按下左右控制
		}else if(Direct <= -0.5f)			//如果按下的數值小於 -0.5 
		{
			CurrenPeople--;					//現在玩家所選的人數減一
			yield return new WaitForSeconds(0.15f);	//等待0.15秒的CD時間
			Stick1Check = true;				//CD時間結束 P1可以再按下左右控制
		}
		
	}

}
