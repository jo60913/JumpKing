using UnityEngine;
using System.Collections;

public class Stage6 : MonoBehaviour {

	private int StageNum;	//記錄當前選到的場景 懶咖場景為1 芝德場景為2 臼刺場景為3  以此類推
	public UISprite StageDetail;	//場景的預覽圖介面
	public UISprite Stage1;	//下面的石頭裝飾 這是第1顆 選到第1場景會亮
	public UISprite Stage2;	//下面的石頭裝飾 這是2第顆 選到第2場景會亮
	public UISprite Stage3;	//下面的石頭裝飾 這是3第顆 選到第3場景會亮
	public UISprite Stage4;	//下面的石頭裝飾 這是4第顆 選到第4場景會亮
	public UISprite Stage5;	//下面的石頭裝飾 這是5第顆 選到第5場景會亮
	private Click1 _main;	//取得main腳本
	private Panel3 panel3;	//取得Panel3腳本
	private Skill5 skill5;	//取得skill5腳本
	public UISprite LeftButton;	//左邊的石頭 當P1按左鍵的時候 石頭會亮一下
	public UISprite RightButton;	//右邊的石頭 當P1按右鍵的時候 石頭會亮一下

	private bool Stick1Check;	//這是搖桿的CD時間 因為搖桿右鍵按住的時候會向右跑到底 並不會像鍵盤按右鍵一樣 只跳一格 所以要給搖桿按鍵CD時間
	void Start () {
		Stick1Check = true;	//搖桿可以按左右控制
		StageNum = 1;		//一開始選擇的場景為第一個場景
		panel3 = transform.GetComponent<Panel3>();	//載入panel3腳本
		skill5 = transform.GetComponent<Skill5>();	//載入skill5腳本
		_main = transform.GetComponent<Click1>();	//載入click腳本
	}
	

	void Update () {
		if(Input.GetAxis("Horizontal1") <= -0.8f)		///P1按下左鍵
		{
			if(Stick1Check == true)	//如果可以控制的話
			{
				StartCoroutine(StickCon1(Input.GetAxis("Horizontal1")));	//P1左右的CD程式碼
				Stick1Check = false;	//關閉P1的控制
				if(StageNum <=1)	//如果選擇的場景小於1的話 就只能跑第一個場景
				{
					StageNum = 1;
				}
			}
		}
		
		if(Input.GetAxis("Horizontal1") >= 0.8f)		///P1按下右鍵
		{
			if(Stick1Check == true)	//如果可以控制的話
			{
				StartCoroutine(StickCon1(Input.GetAxis("Horizontal1")));	//P1左右的CD程式碼
				Stick1Check = false;	//關閉P1的控制
				if(StageNum >= 3)	//如果選擇的場景大於3的話 就只能跑第三個場景 (因為目前只有三個場景)
				{
					StageNum = 3;
				}
			}

		}

		if(Input.GetButtonDown("Hor1Jump"))		//如果P1按下確認鍵(小跳按鍵)的話
		{
			if(StageNum == 1)	//如果當前選擇的場景為1的話
			{
				Application.LoadLevel("map1");	//載入場景1
			}

			if(StageNum == 2)	//如果當前選擇的場景為2的話
			{
				Application.LoadLevel("map2");	//載入場景2
			}

			if(StageNum == 3)	//如果當前選擇的場景為3的話
			{
				Application.LoadLevel("map3");	//載入場景3
			}
		}
		



		switch (StageNum)	//以現在所選擇的場景來控制要出現的場景預覽圖、場景名稱以及下面裝飾的石頭的閃亮
		{
		case 1:		//如果選擇到的場景為1的話
			StageDetail.spriteName = "fightstage_02";	//切換第一場景的預覽圖
			Stage1.spriteName = "ball_02";	//第一顆石頭亮
			Stage2.spriteName = "ball_01";	//第二顆石頭暗
			Stage3.spriteName = "ball_01";	//第三顆石頭暗
			break;
		case 2:		//如果選擇到的場景為2的話
			StageDetail.spriteName = "fightstage_01";	//切換第二場景的預覽圖
			Stage1.spriteName = "ball_01";	//第一顆石頭暗
			Stage2.spriteName = "ball_02";	//第二顆石頭亮
			Stage3.spriteName = "ball_01";	//第三顆石頭暗
			break;
		case 3:		//如果選擇到的場景為3的話
			StageDetail.spriteName = "fightstage_03";	//切換第三場景的預覽圖
			Stage1.spriteName = "ball_01";	//第一顆石頭暗
			Stage2.spriteName = "ball_01";	//第二顆石頭暗
			Stage3.spriteName = "ball_02";	//第三顆石頭亮
			break;
		default:
			print ("charator choose overflow");//超出預期的話就Debug出charator choose overflow
			break;
		}

		PlayerPrefs.SetInt("PlyCount",_main.select);	//記錄當前的玩家數 (場景傳送數值 可以把數值存在電腦中 並可以再另外的場景中取出)

		PlayerPrefs.SetInt("P1",panel3.OnePreCha);	//P1選擇的角色 (場景傳送數值 可以把數值存在電腦中 並可以再另外的場景中取出)
		PlayerPrefs.SetInt("P2",panel3.TwoPreCha);	//P2選擇的角色 (場景傳送數值 可以把數值存在電腦中 並可以再另外的場景中取出)
		PlayerPrefs.SetInt("P3",panel3.ThreePreCha);	//P3選擇的角色 (場景傳送數值 可以把數值存在電腦中 並可以再另外的場景中取出)
		PlayerPrefs.SetInt("P4",panel3.FourPreCha);	//P4選擇的角色 (場景傳送數值 可以把數值存在電腦中 並可以再另外的場景中取出)

	}

	IEnumerator StickCon1(float Direct)		//P1的左右按鍵的CD	
	{
		if(Direct >= 0.5f)	//如果按下的數值超過 0.5 	(搖桿按下上下左右鍵時 會有個數值從0慢慢加到1 須要以該數值做為確認
		{
			StageNum++;		//當前所選的場景數加一
			yield return new WaitForSeconds(0.15f);	//等待0.15秒的CD時間
			Stick1Check = true;		//CD時間結束 P1可以再按下左右控制
		}else if(Direct <= -0.5f)	//如果按下的數值小於 0.5 
		{
			StageNum--;		//當前所選的場景數加一
			yield return new WaitForSeconds(0.15f);	//等待0.15秒的CD時間
			Stick1Check = true;		//CD時間結束 P1可以再按下左右控制
		}
		
	}
}
