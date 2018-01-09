using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	
	public int PlayerCount;			//玩家數量
	public float timer;				//回合時間
	public GameObject Venus;		//食人花物件
	private float VenusTimer;		//食人花每幾秒出現一次
	private GameObject ScoreBoardPanel;		//取得記分板分類 裡面包含有關記分板的物件
	private GameObject Panel1;				//P1的分類 如殺敵數、死亡數的分類
	private GameObject Panel2;				//P2的分類 如殺敵數、死亡數的分類
	private GameObject Panel3;				//P3的分類 如殺敵數、死亡數的分類
	private GameObject Panel4;				//P4的分類 如殺敵數、死亡數的分類
	public UISprite ScoreBoard;				//取得記分板物件
	private GameObject Panel;				//介面分類
	private GameObject TimerCount;			//時間倒數分類 裡面都是包含有關時間倒數的物件
	
	private float ShowPlayerTime;			//取得LoadCharactor的ShowPlayerTime 是用來延遲角色出場的時間 不要和出場特效同時產生 角色會在出場特效出現後產生
	public GameObject ShowUp;				//出場特效
	
	private UISprite[] P1Info = new UISprite[4]; 	//取得P1記分板資料 如殺敵數、死亡數的分類
	private UISprite[] P2Info = new UISprite[4];	//取得P2記分板資料 如殺敵數、死亡數的分類
	private UISprite[] P3Info = new UISprite[4];	//取得P3記分板資料 如殺敵數、死亡數的分類
	private UISprite[] P4Info = new UISprite[4];	//取得P4記分板資料 如殺敵數、死亡數的分類
	
	private LoadCharactor _LoadCharactor;			//取得LoadCharactor腳本
	
	public int P1Kill;			//記錄P1殺敵數
	public int P2Kill;			//記錄P2殺敵數
	public int P3Kill;			//記錄P3殺敵數
	public int P4Kill;			//記錄P4殺敵數
	
	public int P1Die;			//記錄P1死亡數
	public int P2Die;			//記錄P2死亡數
	public int P3Die;			//記錄P3死亡數
	public int P4Die;			//記錄P4死亡數
	
	private int _P1Die;			//這邊變數是用來比較上面的。   
	private int _P2Die;			//變數如_P1Die會跟P1Die比較，
	private int _P3Die;			//一但P1Die大於_P1Die 那就表示P1有死，
	private int _P4Die;			//亡這邊就會開始執行角色死亡後的方程式。
	
	private int _P1Kill;		//這邊變數是用來比較上面的。
	private int _P2Kill;		//變數如_P1Kill會跟P1Kill比較，
	private int _P3Kill;		//一但P1Kill大於_P1Kill 那就表示P1有死，
	private int _P4Kill;		//亡這邊就會開始執行角色死亡後的方程式。

	public float BuffDestroyTime;	//倒數Buff清除的時間 Buff待在場上多久沒吃就會清除並產生下一個
	private float _BuffDestroyTime;		//記錄倒數Buff清除的時間
	public int CurrentBuff;		//記錄現在場上出現的Buff 如果數字為1的話就是BuffObj的空格的第一格 以此類推 目前順序是 補血->緩術->時間
	public Transform[] BuffPosRange;		//控制Buff的產生位置 在這裡面得放入四個物件 Buff出場就會以這四個物件圍起來的位置隨機產生 四個位置分別代表 第一個是左上 第二個是右上 第三是是左下 第四個是右下
	private float BuffXmin;		//以上面的Buff位置挑出X軸最小的位置到哪裡
	private float BuffXMax;		//以上面的Buff位置挑出X軸最大的位置到哪裡
	private float BuffZmin;		//以上面的Buff位置挑出Z軸最小的位置到哪裡
	private float BuffZMax;		//以上面的Buff位置挑出Z軸最大的位置到哪裡
	public float BuffY;			//Buff產生的Y軸位置 因為每個場景高度都有些不同 所以需要另外填寫
	public GameObject[] BuffObj;	//填寫所要產生的Buff 填寫的順序會決定產生的順序
	private GameObject _BuffObj;	//記錄當前場上的Buff
	
	
	public float TimeBuffPly;		//確認是否有人吃到時間Buff 如果數字不一樣代表有人吃到
	public float TimeBuffPlyReset;		//用來清除TimeBuffPly變數 超過幾秒後清除為0
	private float _TimeBuffPlyReset;	//記錄用來清除TimeBuffPly變數
	
	private UISprite MinImg;		//取得回合倒數的分的介面
	private UISprite SecTenImg;		//取得回合倒數的十位數的秒的介面
	private UISprite SecOneImg;		//取得回合到薯的個位數的秒的介面

	public float P1Angry;			//記錄P1的憤怒值 P1死亡後存到這邊
	public float P2Angry;			//記錄P2的憤怒值 P1死亡後存到這邊
	public float P3Angry;			//記錄P3的憤怒值 P1死亡後存到這邊
	public float P4Angry;			//記錄P4的憤怒值 P1死亡後存到這邊

	private int Min;				//記錄現在回合倒數分的數字 會在用程式切換成介面上看到的圖片
	private int SecTen;				//記錄現在回合倒數十位數的秒的數字 會在用程式切換成介面上看到的圖片
	private int SecOne;				//記錄現在回合倒數個位數的秒的數字 會在用程式切換成介面上看到的圖片
	
	public float ReboneTime;		//角色死亡後到角色產生之間的空檔 可以讓角色死亡的玩家短暫休息
	
	private GameObject p1;			//取得P1在場上的資訊 如頭頂血條以及左上角的大血條
	private GameObject p2;			//取得P2在場上的資訊 如頭頂血條以及左上角的大血條
	private GameObject p3;			//取得P3在場上的資訊 如頭頂血條以及左上角的大血條
	private GameObject p4;			//取得P4在場上的資訊 如頭頂血條以及左上角的大血條
	
	public AudioSource Music;		//當前的背景音樂的物件
	
	public AudioClip BGM;			//當前的背景音樂
	public GameObject Win;			//勝利的音樂
	public bool finishCheck;		//確認是否時間結束 確認後只播放一次勝利音樂 避免重複產生
	
	private UISprite P1TimeCountImg;	//P1左上角的出場到數圖示
	private UISprite P2TimeCountImg;	//P2右上角的出場到數圖示
	private UISprite P3TimeCountImg;	//P3左下角的出場到數圖示
	private UISprite P4TimeCountImg;	//P4右下角的出場到數圖示
	
	private UISprite P1Head;		//P1左上角的角色圖示
	private UISprite P2Head;		//P2右上角的角色圖示
	private UISprite P3Head;		//P3左下角的角色圖示
	private UISprite P4Head;		//P4右下角的角色圖示
	
	private bool P1DeadCheck;		//確認P1的死亡
	private bool P2DeadCheck;		//確認P2的死亡
	private bool P3DeadCheck;		//確認P3的死亡
	private bool P4DeadCheck;		//確認P4的死亡
	
	private float P1DeadTimer;		//P1左上角的出場到數 會在依程式轉換成場上看到的倒數介面
	private float P2DeadTimer;		//P2右上角的出場到數
	private float P3DeadTimer;		//P3左下角的出場到數
	private float P4DeadTimer;		//P4右下角的出場到數
	
	private UISprite KillImg;		//取得殺敵的介面
	private UISprite BeKillImg;		//取得被殺的介面
	public float ShowBoardTime;		//殺敵的介面與被殺的介面過多久後清除
	private float _ShowBoardTime;	//記錄ShowBoardTime多久後清除
	public float BigJumpSkillTime;	//玩家使出大跳躍的介面時間 過多久後清除
	private float _BigJumpSkillTime;	//記錄玩家使出大跳躍的介面時間
	private UISprite BigJumpSkillImg;	//玩家使出大跳躍會出現角色圖片的那個介面

	public GameObject HeartBeat;		//倒數時心跳聲的物件
	private bool HeartBeatCheck;		//確認只讓心跳聲的音樂播放一次 避免造成重複產生
	public float StartDelay;			//開場時攝影機繞場的延遲
	private float _StartDelay;			//記錄出場時攝影機繞場的時間 時間到後就讓StartDelay歸0 避免角色出場僵持過久
	public GameObject RebornTimeObj;	//角色死亡時 場上產生的倒數物件
	void Start () {
		_StartDelay = StartDelay;		//記錄攝影機繞場的時間
		Screen.showCursor = false;		//滑鼠關閉
		HeartBeatCheck = true;			//不要讓心跳生產生
		P1Angry = 0;					//P1憤怒值為0
		P2Angry = 0;					//P2憤怒值為0
		P3Angry = 0;					//P3憤怒值為0
		P3Angry = 0;					//P4憤怒值為0
		
		_BigJumpSkillTime = BigJumpSkillTime;		//記錄玩家使出大絕招時  場上角色圖示的持續時間
		_ShowBoardTime = ShowBoardTime;				//記錄殺與被殺介面多久會被清除的時間
		Time.timeScale = 1;				//現在時間為正常 如果只有0.5那表示 時間跑的速度只有原來的一半
		Panel = GameObject.Find("UI Root").gameObject;	//取得介面物件
		ScoreBoardPanel = Panel.transform.FindChild("ScoreBoardPanel").gameObject;		//取得記分板介面總成物件
		ScoreBoard = ScoreBoardPanel.transform.FindChild("ScoreBoard").GetComponent<UISprite>();	//取得介面的UIsprite腳本
		
		
		Panel1 = ScoreBoardPanel.transform.FindChild("Panel1").gameObject;		//取得P1記分板介面 如殺敵數的顯示和死亡數的顯示
		Panel2 = ScoreBoardPanel.transform.FindChild("Panel2").gameObject;		//取得P2記分板介面 如殺敵數的顯示和死亡數的顯示
		Panel3 = ScoreBoardPanel.transform.FindChild("Panel3").gameObject;		//取得P3記分板介面 如殺敵數的顯示和死亡數的顯示
		Panel4 = ScoreBoardPanel.transform.FindChild("Panel4").gameObject;		//取得P4記分板介面 如殺敵數的顯示和死亡數的顯示
		
		P1Info[0] = Panel1.transform.FindChild("P1KillTen").GetComponent<UISprite>();	//取得P1記分板中殺敵的十位數
		P1Info[1] = Panel1.transform.FindChild("P1KillOne").GetComponent<UISprite>();	//取得P1記分板中殺敵的個位數
		P1Info[2] = Panel1.transform.FindChild("P1DieTen").GetComponent<UISprite>();	//取得P1記分板中死亡的十位數
		P1Info[3] = Panel1.transform.FindChild("P1DieOne").GetComponent<UISprite>();	//取得P1記分板中死亡的個位數
		
		P2Info[0] = Panel2.transform.FindChild("P2KillTen").GetComponent<UISprite>();	//取得P2記分板中殺敵的十位數
		P2Info[1] = Panel2.transform.FindChild("P2KillOne").GetComponent<UISprite>();	//取得P2記分板中殺敵的個位數
		P2Info[2] = Panel2.transform.FindChild("P2DieTen").GetComponent<UISprite>();	//取得P2記分板中死亡的十位數
		P2Info[3] = Panel2.transform.FindChild("P2DieOne").GetComponent<UISprite>();	//取得P2記分板中死亡的個位數
		
		P3Info[0] = Panel3.transform.FindChild("P3KillTen").GetComponent<UISprite>();	//取得P3記分板中殺敵的十位數
		P3Info[1] = Panel3.transform.FindChild("P3KillOne").GetComponent<UISprite>();	//取得P3記分板中殺敵的個位數
		P3Info[2] = Panel3.transform.FindChild("P3DieTen").GetComponent<UISprite>();	//取得P3記分板中死亡的十位數
		P3Info[3] = Panel3.transform.FindChild("P3DieOne").GetComponent<UISprite>();	//取得P3記分板中死亡的個位數
		
		P4Info[0] = Panel4.transform.FindChild("P4KillTen").GetComponent<UISprite>();	//取得P4記分板中殺敵的十位數
		P4Info[1] = Panel4.transform.FindChild("P4KillOne").GetComponent<UISprite>();	//取得P4記分板中殺敵的個位數
		P4Info[2] = Panel4.transform.FindChild("P4DieTen").GetComponent<UISprite>();	//取得P4記分板中死亡的十位數
		P4Info[3] = Panel4.transform.FindChild("P4DieOne").GetComponent<UISprite>();	//取得P4記分板中死亡的個位數
		
		BigJumpSkillImg = Panel.transform.FindChild("BigJumpSkill").GetComponent<UISprite>();	//取得玩家放大絕招時 場上出現角色圖示的介面
		
		TimerCount = Panel.transform.FindChild("Timer").gameObject;				//取得回合時間的總成
		MinImg = TimerCount.transform.FindChild("Hundred").GetComponent<UISprite>();	//取得回合時間到數的"分"介面
		SecTenImg = TimerCount.transform.FindChild("Ten").GetComponent<UISprite>();		//取得回合時間到數的"秒的十位數"介面
		SecOneImg = TimerCount.transform.FindChild("One").GetComponent<UISprite>();		//取得回合時間到數的"秒的個位數"介面
		
		KillImg = Panel.transform.FindChild("Kill").GetComponent<UISprite>();	//取得殺敵顯示的介面
		KillImg.spriteName = "space";											//殺敵顯示的介面為空白
		BeKillImg = Panel.transform.FindChild("BeKill").GetComponent<UISprite>();	//取得被敵顯示的介面
		BeKillImg.spriteName = "space";											//被敵顯示的介面為空白
		
		_LoadCharactor = GetComponent<LoadCharactor>();		//取得_LoadCharactor腳本
		ShowPlayerTime = _LoadCharactor.ShowPlayerTime;		//取得_LoadCharactor腳本中角色出場特效後角色出場的時間
		PlayerCount = _LoadCharactor.PlyCount;				//取得玩家數
		_TimeBuffPlyReset = TimeBuffPlyReset;				//記錄TimeBuffPlyReset
		TimeBuffPly = 0;									//TimeBuffPly為0代表沒有人吃到時間Buff 
		_BuffDestroyTime = BuffDestroyTime;					//記錄BuffDestroyTime
		switch (PlayerCount)		//確認現在玩家數 並決定角色繞場的時間
		{
		case 2:
			StartDelay = 4;			//兩個人的話 攝影機繞場約4秒 所以角色延遲4秒後開時移動
			break;
		case 3:
			StartDelay = 5;			//三個人的話 攝影機繞場約5秒 所以角色延遲5秒後開時移動
			break;
		case 4:
			StartDelay = 6;			//四個人的話 攝影機繞場約6秒 所以角色延遲6秒後開時移動
			break;
		default:
			break;
			Debug.Log("startcord");
		}
		CurrentBuff = 1;		//實現第一個buff
		
		P1Kill = 0;		//各個角色的殺敵數歸0
		P2Kill = 0;
		P3Kill = 0;
		P4Kill = 0;
		
		P1Die = 0;		//各個角色的死亡數歸0
		P2Die = 0;
		P3Die = 0;
		P4Die = 0;
		
		VenusTimer = 10;	//食人花每過多少秒後轉移位置
		
		
		BuffXmin = BuffPosRange[0].transform.position.x;		//分析Buff產生的範圍 
		BuffXMax = BuffPosRange[1].transform.position.x;
		BuffZmin = BuffPosRange[0].transform.position.z;
		BuffZMax = BuffPosRange[2].transform.position.z;
		
		p1 = GameObject.Find("Cha1").gameObject;		//取得P1玩家的資訊
		p2 = GameObject.Find("Cha2").gameObject;		//取得P2玩家的資訊
		p3 = GameObject.Find("Cha3").gameObject;		//取得P3玩家的資訊
		p4 = GameObject.Find("Cha4").gameObject;		//取得P4玩家的資訊
		
		P1TimeCountImg = p1.transform.Find("Head1/TimeCount").GetComponent<UISprite>();		//P1角色死亡後時間的倒數為透明不顯示
		P1TimeCountImg.spriteName = "space";
		P2TimeCountImg = p2.transform.Find("Head2/TimeCount").GetComponent<UISprite>();		//P2角色死亡後時間的倒數為透明不顯示
		P2TimeCountImg.spriteName = "space";
		P3TimeCountImg = p3.transform.Find("Head3/TimeCount").GetComponent<UISprite>();		//P3角色死亡後時間的倒數為透明不顯示
		P3TimeCountImg.spriteName = "space";
		P4TimeCountImg = p4.transform.Find("Head4/TimeCount").GetComponent<UISprite>();		//P4角色死亡後時間的倒數為透明不顯示
		P4TimeCountImg.spriteName = "space";
		
		P1Head = p1.transform.Find("Head1").GetComponent<UISprite>();	//取得P1頭上資訊
		P2Head = p2.transform.Find("Head2").GetComponent<UISprite>();	//取得P2頭上資訊
		P3Head = p3.transform.Find("Head3").GetComponent<UISprite>();	//取得P3頭上資訊
		P4Head = p4.transform.Find("Head4").GetComponent<UISprite>();	//取得P4頭上資訊
		
		P1DeadCheck = false;	//P1死亡確認
		P2DeadCheck = false;	//P2死亡確認
		P3DeadCheck = false;	//P3死亡確認
		P4DeadCheck = false;	//P4死亡確認
		
		P1DeadTimer = ReboneTime;	//P1死亡倒數 因為每個角色死亡的時間點都會不一樣 所以另外用變數倒數
		P2DeadTimer = ReboneTime;
		P3DeadTimer = ReboneTime;
		P4DeadTimer = ReboneTime;
		
		UIShutDown(PlayerCount);	//關閉沒人玩的玩家介面
		
		
		ScoreBoardPanel.gameObject.SetActive(false);	//記分版關閉
		finishCheck = true;		//時間到薯確認
		Music.clip = BGM;		//播放BGM音樂
		Music.Play();			//開始播放


		StartCoroutine(StartDelayTime(_StartDelay));	//讓StartDelay歸0
	}
	
	
	
	void Update () {



		if(timer <=10)		//如果時間少於10秒就播放心跳聲
		{
			if(HeartBeatCheck == true)
			{
				HeartBeatCheck = false;
				Instantiate(HeartBeat,transform.position,transform.rotation);
			}
		}

		if(Input.GetKeyDown(KeyCode.Joystick1Button4))	//P1搖桿按下第5個按鍵就重新開始遊戲
		{
			string loadname;
			loadname = Application.loadedLevelName;
			Application.LoadLevel(loadname);
		}

		if(Input.GetKeyDown(KeyCode.Joystick1Button5))	//P1搖桿按下第6個按鍵就重新選角
		{
			Application.LoadLevel("Character");
		}

		BuffDestroyTime -=Time.deltaTime;	//BuffDestroyTime時間到數
		if(BuffDestroyTime <=0)		//BuffDestroyTime時間少於0時就產生新的時間
		{
			BuffControl();
		}
		
		Min = (int)Mathf.Floor( timer / 60);				//把時間轉換成分秒當中的分鐘數
		SecTen =  (int)Mathf.Floor((timer % 60) / 10);		//把時間轉換成分秒當中的秒鐘數的十位數
		SecOne =  (int)Mathf.Floor((timer % 60)-SecTen * 10 / 1);	//把時間轉換成分秒當中的秒鐘數的個位數
		
		MinImg.spriteName = NumImg(Min,"");					//把上面轉換的時間轉成介面圖
		SecTenImg.spriteName = NumImg(SecTen,"");			//把上面轉換的時間轉成介面圖
		SecOneImg.spriteName = NumImg(SecOne,"");			//把上面轉換的時間轉成介面圖
		
		if(BigJumpSkillImg.spriteName != "space")		//角色放大絕招 產生的角色圖示顯示為空白
		{
			BigJumpSkillTime -=Time.deltaTime;
			if(BigJumpSkillTime <=0)
			{
				BigJumpSkillTime = _BigJumpSkillTime;
				BigJumpSkillImg.spriteName = "space";
			}
		}
		
		timer -=Time.deltaTime;							//回合時間的倒數
		if(timer <=0)			//時間結束
		{
			if(finishCheck == true)		//確認時間到播放勝利的音樂 只播放一次
			{
				Music.Stop();
				Instantiate(Win,transform.position,transform.rotation);
				finishCheck = false;
			}
			
			Time.timeScale = 0;			//時間暫停
			ScoreBoardPanel.gameObject.SetActive(true);		//打開記分板
			int P1KillTen;
			int P1KIllOne;
			int P1DieTen;
			int P1DieOne;
			
			int P2KillTen;
			int P2KIllOne;
			int P2DieTen;
			int P2DieOne;
			
			int P3KillTen;
			int P3KIllOne;
			int P3DieTen;
			int P3DieOne;
			
			int P4KillTen;
			int P4KIllOne;
			int P4DieTen;
			int P4DieOne;
			
			P1KillTen = (int)Mathf.Floor(P1Kill / 10);		//取出P1的殺敵數的十位數
			P1KIllOne = P1Kill % 10;						//取出P1的殺敵數的個位數
			P1DieTen = (int)Mathf.Floor(P1Die / 10);		//取出P1的死亡數的十位數
			P1DieOne = P1Die % 10;							//取出P1的死亡數的個位數
			
			P2KillTen = (int)Mathf.Floor(P2Kill / 10);		//取出P2的殺敵數的十位數
			P2KIllOne = P2Kill % 10;						//取出P2的殺敵數的個位數
			P2DieTen = (int)Mathf.Floor(P2Die / 10);		//取出P2的死亡數的十位數
			P2DieOne = P2Die % 10;							//取出P2的死亡數的個位數
			
			P3KillTen = (int)Mathf.Floor(P3Kill / 10);		//取出P3的殺敵數的十位數
			P3KIllOne = P3Kill % 10;						//取出P3的殺敵數的個位數
			P3DieTen = (int)Mathf.Floor(P3Die / 10);		//取出P3的死亡數的十位數
			P3DieOne = P3Die % 10;							//取出P3的死亡數的個位數
			
			P4KillTen = (int)Mathf.Floor(P4Kill / 10);		//取出P4的殺敵數的十位數
			P4KIllOne = P4Kill % 10;						//取出P4的殺敵數的個位數
			P4DieTen = (int)Mathf.Floor(P4Die / 10);		//取出P4的死亡數的十位數
			P4DieOne = P4Die % 10;							//取出P4的死亡數的個位數
			
			P1Info[0].spriteName = NumImg(P1KillTen,"");	//把P1的殺敵數的十位數轉成圖片
			P1Info[1].spriteName = NumImg(P1KIllOne,"");	//把P1的殺敵數的個位數轉成圖片
			P1Info[2].spriteName = NumImg(P1DieTen,"");		//把P1的死亡數的十位數轉成圖片
			P1Info[3].spriteName = NumImg(P1DieOne,"");		//把P1的死亡數的個位數轉成圖片
			
			P2Info[0].spriteName = NumImg(P2KillTen,"");	//把P2的殺敵數的十位數轉成圖片
			P2Info[1].spriteName = NumImg(P2KIllOne,"");	//把P2的殺敵數的個位數轉成圖片
			P2Info[2].spriteName = NumImg(P2DieTen,"");		//把P2的死亡數的十位數轉成圖片
			P2Info[3].spriteName = NumImg(P2DieOne,"");		//把P2的死亡數的個位數轉成圖片
			
			P3Info[0].spriteName = NumImg(P3KillTen,"");	//把P3的殺敵數的十位數轉成圖片
			P3Info[1].spriteName = NumImg(P3KIllOne,"");	//把P3的殺敵數的個位數轉成圖片
			P3Info[2].spriteName = NumImg(P3DieTen,"");		//把P3的死亡數的十位數轉成圖片
			P3Info[3].spriteName = NumImg(P3DieOne,"");		//把P3的死亡數的個位數轉成圖片
			
			P4Info[0].spriteName = NumImg(P4KillTen,"");	//把P4的殺敵數的十位數轉成圖片
			P4Info[1].spriteName = NumImg(P4KIllOne,"");	//把P4的殺敵數的個位數轉成圖片
			P4Info[2].spriteName = NumImg(P4DieTen,"");		//把P4的死亡數的十位數轉成圖片
			P4Info[3].spriteName = NumImg(P4DieOne,"");		//把P4的死亡數的個位數轉成圖片
			
			MinImg.gameObject.SetActive(false);				//把回合時間關閉
			SecTenImg.gameObject.SetActive(false);			//把回合時間關閉
			SecOneImg.gameObject.SetActive(false);			//把回合時間關閉
		}	
		
		if(TimeBuffPly >0)		//有人吃到時間Buff的時候
		{
			TimeBuffPlyReset -= Time.deltaTime;				//TimeBuffPlyReset倒數
			if(TimeBuffPlyReset <=0)						//TimeBuffPlyReset倒數歸0後把TimeBuffPly，
			{												//歸0角色會取這個數判斷是否有人吃到時間Buff，
				TimeBuffPly = 0;							//所以需要延遲一些時間後再歸0 如果有人吃到後，
				TimeBuffPlyReset = _TimeBuffPlyReset;		//馬上歸0那樣可能會有角色腳本讀不到此變數。
			}
		}
		
		if (Input.GetKey(KeyCode.R))		//鍵盤按R後回選角
		{
			Application.LoadLevel("Character");
		}
		
		
		if(BeKillImg.spriteName !="space")		//如果殺敵介面有顯是東西會在ShowBoardTime所填的數字後變成透明 透明表示沒有顯示
		{
			ShowBoardTime -=Time.deltaTime;
			if(ShowBoardTime <=0)
			{
				BeKillImg.spriteName = "space";
				KillImg.spriteName = "space";
				ShowBoardTime = _ShowBoardTime;
			}
		}
		
		if(P1Die > _P1Die)		//_P1Die是用來記錄原本的P1死亡數 P1Die是角色死亡後 角色會傳值過來加一 如果P1Die大於_P1Die那就表示P1死亡 就會執行下面程式	例如出場時_P1Die為0 P1Die也為0 當P1死亡後 P1Die加一變成1  那麼就會P1Die>_P1Die了
		{
			StartCoroutine(Rebone1(ReboneTime));	//延遲一些時間在開始倒數 讓玩家休息
			Instantiate(RebornTimeObj,_LoadCharactor.PlyPoint[0].transform.position,_LoadCharactor.PlyPoint[0].transform.rotation);		//產生地板的倒數
			_P1Die = P1Die;							//記下當前的死亡數
			BeKillImg.spriteName = "P1";			//被殺的介面顯示為P1
		}
		
		if(P2Die > _P2Die)		//_P2Die是用來記錄原本的P2死亡數 P2Die是角色死亡後 角色會傳值過來加一 如果P2Die大於_P2Die那就表示P2死亡 就會執行下面程式	例如出場時_P2Die為0 P2Die也為0 當P2死亡後 P2Die加一變成1  那麼就會P2Die>_P2Die了
		{
			StartCoroutine(Rebone2(ReboneTime));	//延遲一些時間在開始倒數 讓玩家休息
			Instantiate(RebornTimeObj,_LoadCharactor.PlyPoint[1].transform.position,_LoadCharactor.PlyPoint[1].transform.rotation);
			_P2Die = P2Die;							//記下當前的死亡數
			BeKillImg.spriteName = "P2";			//被殺的介面顯示為P2
		}
		
		if(P3Die > _P3Die)		//_P3Die是用來記錄原本的P3死亡數 P3Die是角色死亡後 角色會傳值過來加一 如果P3Die大於_P3Die那就表示P3死亡 就會執行下面程式	例如出場時_P3Die為0 P3Die也為0 當P2死亡後 P3Die加一變成1  那麼就會P3Die>_P3Die了
		{
			StartCoroutine(Rebone3(ReboneTime));	//延遲一些時間在開始倒數 讓玩家休息
			Instantiate(RebornTimeObj,_LoadCharactor.PlyPoint[2].transform.position,_LoadCharactor.PlyPoint[2].transform.rotation);
			_P3Die = P3Die;							//記下當前的死亡數
			BeKillImg.spriteName = "P3";			//被殺的介面顯示為P3
		}
		
		if(P4Die > _P4Die)		//_P4Die是用來記錄原本的P4死亡數 P4Die是角色死亡後 角色會傳值過來加一 如果P4Die大於_P4Die那就表示P4死亡 就會執行下面程式	例如出場時_P4Die為0 P4Die也為0 當P2死亡後 P4Die加一變成1  那麼就會P4Die>_P4Die了
		{
			StartCoroutine(Rebone4(ReboneTime));	//延遲一些時間在開始倒數 讓玩家休息
			Instantiate(RebornTimeObj,_LoadCharactor.PlyPoint[3].transform.position,_LoadCharactor.PlyPoint[3].transform.rotation);
			_P4Die = P4Die;							//記下當前的死亡數
			BeKillImg.spriteName = "P4";			//被殺的介面顯示為P4
		}
		
		
		
		if(P1Kill > _P1Kill)	//雨上面P1Die同像道理 確認P1殺敵+1 就執行下面程式碼
		{
			_P1Kill = P1Kill;	//記下當前的殺敵數
			KillImg.spriteName = "P1Kill";	//顯示殺敵數為P1
		}
		
		if(P2Kill > _P2Kill)	//雨上面P2Die同像道理 確認P2殺敵+1 就執行下面程式碼
		{
			_P2Kill = P2Kill;	//記下當前的殺敵數
			KillImg.spriteName = "P2Kill";	//顯示殺敵數為P2
		}
		
		if(P3Kill > _P3Kill)	//雨上面P3Die同像道理 確認P3殺敵+1 就執行下面程式碼
		{
			_P3Kill = P3Kill;	//記下當前的殺敵數
			KillImg.spriteName = "P3Kill";	//顯示殺敵數為P3
		}
		
		if(P4Kill > _P4Kill)	//雨上面P4Die同像道理 確認P4殺敵+1 就執行下面程式碼
		{
			_P4Kill = P4Kill;	//記下當前的殺敵數
			KillImg.spriteName = "P4Kill";	//顯示殺敵數為P4
		}
		
		if(P1DeadCheck == true)					//P1顯示在畫面左上面的死亡倒數
		{
			P1DeadTimer -=Time.deltaTime;		//倒數數字
			P1TimeCountImg.spriteName = NumImg((int)P1DeadTimer,"");	//判斷倒數時該顯示的數字圖片
			P1Head.spriteName = "space";		//把原本的角色圖片變成透明 玩家會比較好看到倒數數字
			if(P1DeadTimer <=0)					//如果倒數結束就到數數字變成透明
			{
				P1DeadCheck = false;
				P1DeadTimer = ReboneTime;
				P1TimeCountImg.spriteName = "space";
			}
		}
		
		if(P2DeadCheck == true)					//P2顯示在畫面左上面的死亡倒數
		{
			P2DeadTimer -=Time.deltaTime;		//倒數數字
			P2TimeCountImg.spriteName = NumImg((int)P2DeadTimer,"");	//判斷倒數時該顯示的數字圖片
			P2Head.spriteName = "space";		//把原本的角色圖片變成透明 玩家會比較好看到倒數數字
			if(P2DeadTimer <=0)					//如果倒數結束就到數數字變成透明
			{
				P2DeadCheck = false;
				P2DeadTimer = ReboneTime;
				P2TimeCountImg.spriteName = "space";
			}
		}
		
		if(P3DeadCheck == true)					//P3顯示在畫面左上面的死亡倒數
		{
			P3DeadTimer -=Time.deltaTime;		//倒數數字
			P3TimeCountImg.spriteName = NumImg((int)P3DeadTimer,"");	//判斷倒數時該顯示的數字圖片
			P3Head.spriteName = "space";		//把原本的角色圖片變成透明 玩家會比較好看到倒數數字
			if(P3DeadTimer <=0)					//如果倒數結束就到數數字變成透明
			{
				P3DeadCheck = false;
				P3DeadTimer = ReboneTime;
				P3TimeCountImg.spriteName = "space";
			}
		}
		
		if(P4DeadCheck == true)					//P4顯示在畫面左上面的死亡倒數
		{
			P4DeadTimer -=Time.deltaTime;		//倒數數字
			P4TimeCountImg.spriteName = NumImg((int)P4DeadTimer,"");	//判斷倒數時該顯示的數字圖片
			P4Head.spriteName = "space";		//把原本的角色圖片變成透明 玩家會比較好看到倒數數字
			if(P4DeadTimer <=0)					//如果倒數結束就到數數字變成透明
			{
				P4DeadCheck = false;
				P4DeadTimer = ReboneTime;
				P4TimeCountImg.spriteName = "space";
			}
		}
		
		VenusTimer -=Time.deltaTime; //食人花到數 因為後來沒有使用 所以程式碼得載需要再做調整
		if(VenusTimer <=0)
		{
			int x = Random.Range(-18,18);		//X軸在-18~18之間產生數字
			int y = Random.Range(-18,18);		//Y軸在-18~18之間產生數字
			if(Venus == null)		//如果食人花消失	如果沒有消失就可以忽略這行判斷
			{
				Instantiate(Venus,transform.position,transform.rotation);	//產生食人花
			}
			Venus.transform.position = new Vector3(x,0,y);	//把剛才隨機產生的數字 成為食人花的x和z座標
			
			VenusTimer = 10;	//再讓時間從10秒到數 倒數完後再從新判斷上面的程式碼
		}
		
		
		
		
		
		
	}
	IEnumerator Rebone1(float Waitime)			//P1玩家重生
	{
		yield return new WaitForSeconds(2.5f);	//延遲2.5秒
		P1DeadCheck = true;			//角色產生確認
		int __P1Die;
		__P1Die = 1 - 1;
		yield return new WaitForSeconds(Waitime);
		Instantiate(ShowUp,_LoadCharactor.PlyPoint[__P1Die].transform.position,_LoadCharactor.PlyPoint[__P1Die].transform.rotation);	//角色出場特效
		yield return new WaitForSeconds(ShowPlayerTime);				//特效完後延遲產生角色
		_LoadCharactor.CharactorIns(0,_LoadCharactor.PlayNum[0]);		//產生角色
		
	}
	
	IEnumerator Rebone2(float Waitime)			//P2玩家重生
	{
		yield return new WaitForSeconds(2.5f);	//延遲2.5秒
		P2DeadCheck = true;			//角色產生確認
		int __P2Die;
		__P2Die = 2 - 1;
		yield return new WaitForSeconds(Waitime);
		Instantiate(ShowUp,_LoadCharactor.PlyPoint[__P2Die].transform.position,_LoadCharactor.PlyPoint[__P2Die].transform.rotation);	//角色出場特效
		yield return new WaitForSeconds(ShowPlayerTime);				//特效完後延遲產生角色
		_LoadCharactor.CharactorIns(1,_LoadCharactor.PlayNum[1]);		//產生角色
		
	}
	
	IEnumerator Rebone3(float Waitime)			//P3玩家重生
	{
		yield return new WaitForSeconds(2.5f);	//延遲2.5秒
		P3DeadCheck = true;			//角色產生確認
		int __P3Die;
		__P3Die = 3 - 1;
		yield return new WaitForSeconds(Waitime);
		Instantiate(ShowUp,_LoadCharactor.PlyPoint[__P3Die].transform.position,_LoadCharactor.PlyPoint[__P3Die].transform.rotation);	//角色出場特效
		yield return new WaitForSeconds(ShowPlayerTime);				//特效完後延遲產生角色
		_LoadCharactor.CharactorIns(2,_LoadCharactor.PlayNum[2]);		//產生角色
		
	}
	
	IEnumerator Rebone4(float Waitime)			//P4玩家重生
	{
		yield return new WaitForSeconds(2.5f);	//延遲2.5秒
		P4DeadCheck = true;			//角色產生確認
		int __P4Die;
		__P4Die = 4 - 1;
		yield return new WaitForSeconds(Waitime);
		Instantiate(ShowUp,_LoadCharactor.PlyPoint[__P4Die].transform.position,_LoadCharactor.PlyPoint[__P4Die].transform.rotation);	//角色出場特效
		yield return new WaitForSeconds(ShowPlayerTime);				//特效完後延遲產生角色
		_LoadCharactor.CharactorIns(3,_LoadCharactor.PlayNum[3]);		//產生角色
		
	}

	IEnumerator StartDelayTime(float num)	//讓StartDelay清除的程式碼
	{

		yield return new WaitForSeconds(num);
		StartDelay = 0;
	}

	public void BigJumpSkillImgCha(int CharactorNum)	//依照取得的數字來顯示誰放大絕招 該顯是誰的圖示
	{
		switch (CharactorNum)
		{
		case 1:
			BigJumpSkillImg.spriteName = "roles01";
			break;
		case 2:
			BigJumpSkillImg.spriteName = "roles02";
			break;
		case 3:
			BigJumpSkillImg.spriteName = "roles03";
			break;
		case 4:
			BigJumpSkillImg.spriteName = "roles04";
			break;
		case 5:
			BigJumpSkillImg.spriteName = "roles05";
			break;
		default:
			break;
		}
	}
	
	
	string NumImg(int num,string count)		//數字顯示程式碼
	{
		switch (num)
		{
		case 0:
			count = "N0";
			break;
		case 1:
			count = "N1";
			break;
		case 2:
			count = "N2";
			break;
		case 3:
			count = "N3";
			break;
		case 4:
			count = "N4";
			break;
		case 5:
			count = "N5";
			break;
		case 6:
			count = "N6";
			break;
		case 7:
			count = "N7";
			break;
		case 8:
			count = "N8";
			break;
		case 9:
			count = "N9";
			break;
			
		default:
			break;
		}
		return count;
	}
	
	
	void BuffControl()		//Buff的控制
	{
		Destroy(_BuffObj);	//消除場上的Buff
		if(CurrentBuff < BuffObj.Length)	//記錄待會場上的Buff
		{
			CurrentBuff += 1;
		}else if(CurrentBuff >= BuffObj.Length)
		{
			CurrentBuff = 1;
		}
		BuffDestroyTime = _BuffDestroyTime;
		_BuffObj = (GameObject)Instantiate(BuffObj[CurrentBuff-1],new Vector3(Random.Range(BuffXmin,BuffXMax),BuffY,Random.Range(BuffZmin,BuffZMax)),transform.rotation);
		//產生Buff
	}
	
	void UIShutDown(int num) //依照完家人數關閉沒有人玩的玩家介面
	{
		switch(num)
		{
		case 2:
			p1.SetActive(true);
			p2.SetActive(true);
			p3.SetActive(false);
			p4.SetActive(false);
			break;
		case 3:
			p1.SetActive(true);
			p2.SetActive(true);
			p3.SetActive(true);
			p4.SetActive(false);
			break;
		case 4:
			p1.SetActive(true);
			p2.SetActive(true);
			p3.SetActive(true);
			p4.SetActive(true);
			break;
		default:
			Debug.Log("UI Shut Down overflow");
			break;
		}
		
	}
}
