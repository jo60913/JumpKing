using UnityEngine;
using System.Collections;

public class Role : MonoBehaviour {
	public float blood;		//血量
	public float bloodMax;	//血量最大值
	public float speed;		//移動速度
	protected float _speed;	//記錄血量
	protected UISlider bloodHUD;	//頭頂血量的介面
	protected UISlider bloodGui;	//角色角落的血量
	protected UISlider AngryHUD;	//頭頂憤怒值的介面
	public float Angry;		//憤怒值
	public float AngryMax;	//憤怒值的最大值
	public bool AngryCheck;		//確定憤怒值是否完成因為animation腳本要讀取 所以無法設定為protected
	public float BloodFlashTime;	//血條低於三分之一時 血條閃爍的時間
	protected float _BloodFlashTime;	//記錄血條閃爍的時間
	protected bool BloodFlashCheck;	//確定血條是否閃爍
	public Vector2 BloodAd;			//頭頂血條位置的調整
	
	public Transform AttRangePos;	//攻擊的地板產生的位置
	public GameObject AttRange;		//小跳攻擊的物件
	public GameObject BigAttRange;	//大跳攻擊的物件
	public GameObject SkillObj;		//大跳的附加特效 像蝴蝶的就是毒物
	protected bool SkillObjCheck;		//確認大絕招附加特效是否開啟
	
	protected int LeftRight;		//記錄移動方向為左右走
	protected int UpDown;			//記錄移動方向為前後
	protected int Slop;			//記錄方向為斜的方向
	
	public string plysit;		//記錄玩家狀態 因為animation腳本要讀取 所以無法設定為protected
	protected float CDTime;		//角色僵直的時間
	protected float MoveDelayTime;	//角色大跳小跳落地後僵直的時間
	
	protected GameObject Panel;	//角色介面的總成物件
	protected UISprite HeadImg;	//角色遊戲畫面角落的頭標圖示介面
	protected UISprite JumpImg;	//角色遊戲畫面角落的跳躍圖式介面
	protected UISprite BigJumpImg;	//角色遊戲畫面角落的大跳躍圖式介面
	protected UISprite SkillImg;	//角色遊戲畫面角落的突進圖示介面
	protected UISprite JumpCDImg;	//角色遊戲畫面角落的跳躍CD遮罩圖式介面
	protected UISprite BigJumpCDImg;	//角色遊戲畫面角落的大跳躍CD遮罩圖式介面
	protected UISprite SkillCDImg;	//角色遊戲畫面角落的突進CD遮罩圖式介面
	
	protected GameObject Damage;	//角色頭頂傷害介面總成
	protected UISprite DamageOneImg;	//角色頭頂傷害數字的個位數
	protected UISprite DamageTenImg;	//角色頭頂傷害數字的十位數
	protected UISprite DamageHundImg;		//角色頭頂傷害數字的百位數
	protected UISprite Negative;	//角色頭頂傷害的符號 扣血的話會顯示- 加血的話會顯示+
	public float JumpCD;	//角色小跳躍CD時間
	protected float _JumpCD;	//記錄角色小跳躍CD時間
	public float BigJumpCD;	//角色大跳躍CD時間
	protected float _BigJumpCD;	//記錄角色大跳躍CD時間
	public float SkillCD;	//角色突進CD時間
	protected float _SkillCD;	//記錄角色突進CD時間
	
	protected bool SkillDamCheck;	//確認現在角色是否可以被突進攻擊並扣血 (避免Unity對角色重複扣血)
	protected bool JumpCheck;		//確認現在角色是否可以釋放小跳絕招
	protected bool BigJumpCheck;	//確認現在角色是否可以釋放大跳絕招
	protected bool SkillCheck;	//確認是否可以釋放突進攻擊
	
	protected bool DieCheck;		//死亡確認 用來確認死亡音效的播放 避免重複播放
	
	public int PlyNum;		//記錄角色的編號 (1P-> p 2P-> p)
	public string PlyHor;	//取得角色左右按鍵控制的按鈕名稱 請參考Edit->Project Setting->input
	public string PlyVer;	//取得角色上下按鍵控制的按鈕名稱 請參考Edit->Project Setting->input
	
	public string JumpBtn;			//用來記錄普通跳躍按鈕的名稱  請參考Edit->Project Setting->input
	public string JumpSkillBtn;		//取得角色大跳的按鈕	 請參考Edit->Project Setting->input
	public string SkillBtn;			//取得角色突進按鈕	 請參考Edit->Project Setting->input
	
	protected string AttName;			//記錄被攻擊的敵人編號 角色死亡後會依這個變數記錄的數字 把資料傳送給GameManager記錄殺敵數
	protected GameManager _GameManager;	//宣告用來呼叫GameManager腳本
	public GameObject gamemamager;		//宣告用來呼叫GameManager物件
	protected GameObject HUD;		//角色頭頂介面總成
	
	public GameObject WaAtt;	//受到傷害的粒子特效
	public GameObject DieEffect;	//死亡後會產生的特效
	
	public int EleLeft;		//角色麻痺時左鍵所要按的次數
	protected int _EleLeft;	//記錄左鍵所要按的次數
	public int EleRight;	//角色麻痺時右鍵所要按的次數
	protected int _EleRight;	//記錄右鍵所要按的次數
	protected bool EleCheck;	//確認現在是該按下左鍵還是右鍵 避免玩家抓到Bug只要按同一個按鍵就可以
	
	public float JumpTime;	//跳躍的時間 從離開地面到降落地面
	protected float _JumpTime;	//記錄跳躍的時間
	public float Heigh;		//跳躍的高度 1個單位的話等於Unity一個Cube的高度 (Cube的Scale X Y Z 都要等於1)
	protected float _Heigh;	//記錄跳躍的高度
	public float JumpDis;	//跳躍的距離 1個單位等於Unity一個Cube的長度 (Cube的Scale X Y Z 都要等於1)
	protected float _JumpDis;	//記錄跳躍的距離
	protected float Gravity;	//地吸引力 因為跳躍的控制是方程式控制 所以要寫一個地吸引力做為模擬
	
	public float BigJumpTime;	//大跳躍的時間 從離開地面到降落地面
	protected float _BigJumpTime;	//記錄大跳躍的時間
	public float BigJumpHeigh;		//大跳躍的高度 1個單位的話等於Unity一個Cube的高度 (Cube的Scale X Y Z 都要等於1)
	protected float _BigJumpHeigh;	//記錄大跳躍的高度
	public float BigJumpDis;	//大跳躍的距離 1個單位等於Unity一個Cube的長度  (Cube的Scale X Y Z 都要等於1)
	protected float _BigJumpDis;	//記錄大跳躍的距離
	protected float BigJumpGravity;	//地吸引力 因為跳躍的控制是方程式控制 所以要寫一個地吸引力做為模擬
	
	public float SkillDis;	//突進的距離 1個單位的話等於Unity一個Cube的高度 (Cube的Scale X Y Z 都要等於1)
	public float SkillTime;	//突進的時間
	protected float _SkillTime;	//記錄突進的時間
	
	protected Vector3 lastPosition;	//記錄最後的位置 如果跑出場外的時候會把角色拉回這個位置
	protected bool AttCheck;	//記錄可以被攻擊地板攻擊 用來避免腳本從覆扣血
	public float InvincibleTime;	//無敵時間 出場後無敵的時間
	public bool Invincible;	//如果為false表示現在為無敵狀態 被攻擊時不會被扣血

	public int characterNum = 0;
	public int CharacterNum
	{
		set{characterNum = value;}
		get{return characterNum;}
	}

	protected const float JUMP_CDTIME = 0;
	protected const float BIGJUMP_CDTIME = 1;
	protected const float BESKILLCDTIME = 1;
	protected const float BEJUMPCDTIME = 1.3f;

	public AudioSource DieVoice;	//死亡音效 目前只有蝴蝶跟炎龍有而已
	void Start () {
		AttCheck = true;	//這表示現在可以被攻擊地板攻擊
		Invincible = false;	//表示現在不可以被攻擊不會扣血	(雖然現在有記錄可以被攻擊地板以及大絕招特效攻擊 但無敵狀態的權限比較高 所以還是以無敵為主)
		lastPosition = transform.position;	//已出場位置做為最後的位置
		SkillDamCheck = true;	//現在可以被大絕招特效攻擊
		_JumpCD = JumpCD;	//將_JumpCD載入為JumpCD
		_BigJumpCD = BigJumpCD;	//將_BigJumpCD載入為BigJumpCD
		_SkillCD = SkillCD;	//將_SkillCD載入為SkillCD
		_SkillTime = SkillTime;	//將_JSkillTime載入為SkillTime
		SkillObjCheck = true;	//記錄現在為true表示等等放大絕招時可以出現大絕招特效
		AngryCheck = false;		//false表示現在沒辦法出現憤怒特效
		
		JumpCheck = true;	//true表示現在可以釋放小跳躍攻擊
		BigJumpCheck = true;	//true表示現在可以釋放大跳躍攻擊
		SkillCheck = true;	//true表示現在可以釋放突進攻擊
		
		_BloodFlashTime = BloodFlashTime;	//記錄血條閃爍的時間
		BloodFlashCheck = true;		//true表示現在血條可以閃爍
		
		
		DieCheck = true;	//表示現在還沒死亡
		EleCheck = true;	//麻痺程式碼表示等等麻痺時要先按左鍵 如果為false表示要按右鍵
		_EleLeft = EleLeft;	//記錄麻痺時左鍵所需的按鍵次數
		_EleRight = EleRight;	//記錄麻痺時右鍵所需的按鍵次數
		
		Gravity = Heigh*2;		
		//Gravity要等於兩倍的Heigh 因為東西往上飛的時候會地吸引力慢慢變小 落下時地吸引力慢慢變大 往上飛時假設從9.8慢慢往下減到0 往下掉時會從0加到9.8 會加減一次 所以乘以2
		_Heigh = Heigh;	//載入_Heigh為Heigh
		_JumpTime = JumpTime;	//載入_JumpTime為JumpTime
		_JumpDis = JumpDis;	//載入_JumpDis為JumpDis
		_BigJumpDis = BigJumpDis;	//載入_BigJumpDis為BigJumpDis;
		
		
		BigJumpGravity = BigJumpHeigh*2;	//大跳躍載入 跟上列Gravity = Heigh*2一樣意思
		_BigJumpHeigh = BigJumpHeigh;	//載入_BigJumpHeigh為BigJumpHeigh
		_BigJumpTime = BigJumpTime;		//載入_BigJumpTime為BigJumpTime
		
		
		plysit = "StartDelay";	//記錄現在為攝影機繞場狀態 所以角色不能宜動
		
		_speed = speed;	//載入_speed為speed
		
		Panel = GameObject.Find("Cha"+PlyNum.ToString()).gameObject;	//找出Panel物件 這個物件是有關角色的介面
		HUD = Panel.transform.Find("HUD").gameObject;	//找出角色頭頂介面
		HUD.gameObject.SetActive(true);	//開啟頭頂介面
		bloodHUD = HUD.transform.Find("HP").GetComponent<UISlider>();	//找出頭頂血條的戒面
		bloodGui = Panel.transform.Find("Blood").GetComponent<UISlider>();	//找出遊戲畫面角落的角色血條介面
		AngryHUD = HUD.transform.Find("Angry").GetComponent<UISlider>();	//找出遊戲畫面角落的角色憤怒值介面
		HeadImg = Panel.transform.FindChild ("Head"+PlyNum.ToString()).GetComponent<UISprite> ();	//找出遊戲畫面角落的角色圖示
		HeadImg.spriteName = "roles01_head";	//將遊戲畫面角落的角色圖示改為蝴蝶
		JumpImg = Panel.transform.Find("Jump").GetComponent<UISprite>();	//找出遊戲畫面角落小跳躍的圖示
		JumpImg.spriteName = "1";	//將遊戲畫面角落小跳躍的圖示改為小跳躍的圖示
		BigJumpImg = Panel.transform.Find ("JumpSkill"+PlyNum.ToString()).GetComponent<UISprite> ();	//找出遊戲畫面角落大跳躍的圖示
		BigJumpImg .spriteName = "butterfly";	//將遊戲畫面角落大跳躍的圖示改為蝴蝶大跳躍的圖示
		SkillImg = Panel.transform.Find ("Skill" + PlyNum.ToString ()).GetComponent<UISprite> ();	//找出遊戲畫面角落突進的圖示
		SkillImg.spriteName = "2";	//將遊戲畫面角落突進的圖示改為圖進圖示
		
		JumpCDImg = Panel.transform.Find("JumpCD").GetComponent<UISprite>();		//找出遊戲畫面角落小跳躍CD的圖示
		JumpCDImg.spriteName = "space";		//將遊戲畫面角落小跳躍CD的圖示改為透明
		BigJumpCDImg = Panel.transform.Find("JumpSkill"+PlyNum.ToString()+"CD").GetComponent<UISprite>();	//找出遊戲畫面角落大跳躍CD的圖示
		BigJumpCDImg.spriteName = "space";	//將遊戲畫面角落大跳躍CD的圖示改為透明
		SkillCDImg = Panel.transform.Find("Skill" + PlyNum.ToString()+"CD").GetComponent<UISprite>();	//找出遊戲畫面角落突進CD的圖示
		SkillCDImg.spriteName = "space";	//將遊戲畫面角落突進CD的圖示改為透明
		
		Damage = HUD.transform.Find("Damage").gameObject;	//找出頭頂扣血的介面
		Negative = Damage.transform.Find("Negative").GetComponent<UISprite>();	//找出頭頂扣血的符號
		Negative.spriteName = "space";	//將頭頂扣血的符號改為透明
		DamageOneImg = Damage.transform.Find("DamageOne").GetComponent<UISprite>();	//找出頭頂扣血數字的個位數
		DamageOneImg.spriteName = "space";	//將頭頂扣血數字的個位數改為透明
		DamageTenImg = Damage.transform.Find("DamageTen").GetComponent<UISprite>();//找出頭頂扣血數字的十位數
		DamageTenImg.spriteName = "space";	//將頭頂扣血數字的十位數改為透明
		DamageHundImg = Damage.transform.Find("DamageHund").GetComponent<UISprite>();//找出頭頂扣血數字的百位數
		DamageHundImg.spriteName = "space";	//將頭頂扣血數字的百位數改為透明
		gamemamager = GameObject.Find("GameManager").gameObject;
		_GameManager = gamemamager.GetComponent<GameManager> ();// transform.Find ("GameManager").GetComponent<GameManager>();
		
		//下列程式碼是用來取得上次死亡的剩餘憤怒值
		if(PlyNum == 1)		//如果編號為1		
		{
			Angry = _GameManager.P1Angry;	//就從GameManager的P1Angry取的數字
		}else if(PlyNum == 2)	//如果編號為2
		{
			Angry = _GameManager.P2Angry;	//就從GameManager的P2Angry取的數字
		}else if(PlyNum == 3)	//如果編號為3
		{
			Angry = _GameManager.P3Angry;	//就從GameManager的P3Angry取的數字
		}else if(PlyNum == 4)	//如果編號為4
		{
			Angry = _GameManager.P4Angry;	//就從GameManager的P4Angry取的數字
		}
		HUD.SetActive(false);	//目前狀態為攝影機繞場 所以不顯示頭頂血條
		StartCoroutine(StartDelay(_GameManager.StartDelay));	//計算攝影機繞場 等這行程式跑完後就可以開始移動了
	}
	
	
	void Update () {

		
		if(Input.GetKeyDown(KeyCode.F1))	//如果按下F1後 (Debug專用)
		{
			Angry = AngryMax;	//就將憤怒值補滿
		}
		if(Vector3.Distance(transform.position,lastPosition) >=0.5f)	//如果移動的距離與上次最後的距離相差超過0.5(0.5個Unity Cube的大小)的話
		{
			lastPosition = transform.position;	//從新記錄一次最後位置 如果上面0.5的數字越小 (那麼記錄會越頻繁 而效能也會吃比較重)
		}
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);	//將現在角色的位置轉換成遊戲畫面的平面座標並記錄到pos裡面
		pos.z = 0;	//pos的z軸為0
		Vector3 pos2 = UICamera.currentCamera.ScreenToWorldPoint(pos);	//再將剛剛記錄的pos轉換成NGUI的攝影機平面座標 並再次記錄到pos2裡面
		HUD.transform.position= pos2 + new Vector3(BloodAd.x,BloodAd.y,0);	//將剛剛記錄的pos2在加上自己調整的BloodAd 把他們變成頭頂介面的位置
		
		bloodHUD.value = blood/bloodMax;	//頭頂血條的bar條比例控制
		bloodGui.value = blood/bloodMax;	//遊戲畫面血條的bar條比例控制
		AngryHUD.value = Angry/AngryMax;	//頭頂憤怒值得bar條比例控制
		if(blood >= bloodMax)	//如果角色的血條超過最大值的話
		{
			blood = bloodMax;	//就讓血條變成最大值
		}
		
		if(Angry >=AngryMax)	//如果憤怒值為最大值的話
		{
			AngryCheck = true;	//確認可以釋放大跳躍
			Angry = AngryMax;	//就讓憤怒值為最大值
		}
		
		if(blood <= bloodMax * 0.33f)	//如果血量少於總血量的1/3時
		{
			BloodFlashTime -=Time.deltaTime;	//開始減少血條閃爍時間
			if(BloodFlashTime <=0)		//如果血條閃爍時間低於0時
			{
				if(BloodFlashCheck == true)	//讓血條顏色變暗 BloodFlashCheck的true和false轉換就會產生血條閃爍的功能
				{
					bloodHUD.alpha = 0.6f;	//頭頂血條alpha值改為0.6
					bloodGui.alpha = 0.6f;	//遊戲畫面角落的角色血條改為0.6
					BloodFlashCheck = false;	//把血條確認改為false 這等等會用來變亮血條
					BloodFlashTime = _BloodFlashTime;	//記錄一次BloodFlashTime
				}else{						//讓血條變亮
					bloodHUD.alpha = 1;	//頭頂血條alpha值改為1		
					bloodGui.alpha = 1;	//遊戲畫面角落的角色血條改為1
					BloodFlashCheck = true;		//把血條確認改為false 這等等會用來變暗血條
					BloodFlashTime = _BloodFlashTime;	//記錄一次BloodFlashTime
				}
			}
		}
		if(blood > bloodMax * 0.33f)	//血量如果超過1/3的話
		{
			bloodHUD.alpha = 1;	//頭頂血條alpha值改為1
			bloodGui.alpha = 1;	//遊戲畫面角落的角色血條改為1
		}
		
		if(blood <=0)					//血少於0的時候死亡
		{
			if(DieCheck == true)	//死亡確認改為true 避免有些程式碼重複計算
			{
				
				plysit = "Die";		//記錄狀態為死亡
				Instantiate(DieEffect,transform.position,transform.rotation);	//產生死亡特效
				//DieVoice.Play();	//播放死亡音效
				blood = 0;	//血條歸0
				HUD.gameObject.SetActive(false);	//關閉頭頂介面
				//下列程式是判斷攻擊的玩家 並增加該玩家的殺敵數
				if(AttName == "1")		//如果攻擊的編號為1 表示是P1玩家的攻擊		
				{
					Debug.Log("was attack by 1");	//讓Unity顯示was attack by 1 確定為P1的攻擊
					_GameManager.P1Kill +=1;	//在P1Kill幫P1增加殺敵數
				}else if(AttName == "2")		//如果攻擊的編號為2 表示是P2玩家的攻擊	
				{
					Debug.Log("was attack by 2");	//讓Unity顯示was attack by 2 確定為P2的攻擊
					_GameManager.P2Kill +=1;	//在P2Kill幫P2增加殺敵數
				}else if(AttName == "3")		//如果攻擊的編號為3 表示是P3玩家的攻擊	
				{
					Debug.Log("was attack by 3");	//讓Unity顯示was attack by 3 確定為P3的攻擊
					_GameManager.P3Kill +=1;	//在P3Kill幫P2增加殺敵數
				}else if(AttName == "4")		//如果攻擊的編號為4 表示是P4玩家的攻擊	
				{
					Debug.Log("was attack by 4");	//讓Unity顯示was attack by 4 確定為P4的攻擊
					_GameManager.P4Kill +=1;	//在P4Kill幫P2增加殺敵數
				}
				
				//下列程式是增加玩家的死亡並記錄角色最後的憤怒值
				if(PlyNum == 1)		//如果玩家的編號為1
				{
					_GameManager.P1Die +=1;		//在P1Die幫P1增加殺敵數		
					_GameManager.P1Angry = Angry;	//回存一次憤怒值到P1Angry
				}else if(PlyNum == 2)		//如果玩家的編號為1
				{
					_GameManager.P2Die +=1;		//在P2Die幫P2增加殺敵數
					_GameManager.P2Angry = Angry;	//回存一次憤怒值到P2Angry
				}else if(PlyNum == 3)		//如果玩家的編號為1
				{
					_GameManager.P3Die +=1;		//在P3Die幫P3增加殺敵數
					_GameManager.P3Angry = Angry;	//回存一次憤怒值到P3Angry
				}else if(PlyNum == 4)		//如果玩家的編號為1
				{
					_GameManager.P4Die +=1;		//在P4Die幫P4增加殺敵數
					_GameManager.P4Angry = Angry;	//回存一次憤怒值到P4Angry
				}
				DieCheck = false;	//DieCheck改為false避免重複值型
			}
			Destroy(gameObject,2.5f);	//延遲2.5秒後再消除角色 (差不多是死亡動畫播完後)
		}
		
		
		
		
		if(plysit == "ground" || (plysit == "ground"&& Invincible == false))	//判斷是否在地上 是的話在移動(後面框框的意思是如果在地板上也是無敵狀態的時候也可以移動)
		{
			if(Input.GetAxis(PlyHor) >= 0.5f)		//向右
			{
				LeftRight = 1;		//LeftRight標記為1 表示是水平方向移動 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 90, 0);	//角色向右轉90度
			}
			
			if(Input.GetAxis(PlyVer) == -1)			//向下
			{
				UpDown = 1;		//UpDown標記為1 表示是垂直方向移動 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 180, 0);	//角色向右轉180度
			}
			
			if(Input.GetAxis(PlyHor) == -1)			//向左
			{
				LeftRight = 1;		//LeftRight標記為1 表示是水平方向移動 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 270, 0);	//角色向右轉270度
			}
			
			
			if(Input.GetAxis(PlyVer) == 1)			/////向上
			{
				UpDown = 1;		//UpDown標記為1 表示是垂直方向移動 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 0, 0);	//角色向右轉0度
			}
			
			if(Input.GetAxis(PlyVer) == 1 && Input.GetAxis(PlyHor) == -1)	//向左上
			{
				UpDown = 0;		//UpDown標記為0 不已垂直方向移動
				LeftRight = 0;	//LeftRight標記為0 不已水平方向移動
				Slop = 1;		//Slop標記為1 表示是以斜的方向移動(例如:東北、東南、西北、西南) 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 315, 0);	//角色向右轉315度
			}
			
			if(Input.GetAxis(PlyVer) == 1 && Input.GetAxis(PlyHor) == 1)	//向右上
			{
				UpDown = 0;		//UpDown標記為0 不已垂直方向移動
				LeftRight = 0;	//LeftRight標記為0 不已水平方向移動
				Slop = 1;		//Slop標記為1 表示是以斜的方向移動(例如:東北、東南、西北、西南) 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 45, 0);	//角色向右轉45度
			}
			
			if(Input.GetAxis(PlyHor) == 1&&Input.GetAxis(PlyVer) == -1)		///向右下
			{
				UpDown = 0;		//UpDown標記為0 不已垂直方向移動
				LeftRight = 0;	//LeftRight標記為0 不已水平方向移動
				Slop = 1;		//Slop標記為1 表示是以斜的方向移動(例如:東北、東南、西北、西南) 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 135, 0);	//角色向右轉135度
			}
			
			
			if(Input.GetAxis(PlyVer) == -1 && Input.GetAxis(PlyHor) == -1)	//向左下
			{
				UpDown = 0;		//UpDown標記為0 不已垂直方向移動
				LeftRight = 0;	//LeftRight標記為0 不已水平方向移動
				Slop = 1;		//Slop標記為1 表示是以斜的方向移動(例如:東北、東南、西北、西南) 而前進的方向會依角色的轉向移動
				transform.rotation = Quaternion.Euler(0, 225, 0);	//角色向右轉225度
			}
			
			//角色只是單純轉向 所以前進的方位始終是Z軸 程式會判斷LeftRight、UpDown以及Slop哪個數字不為0而執行該程式碼
			transform.Translate(0, 0, LeftRight * speed * Time.deltaTime);	//如果LeftRight不為0時
			transform.Translate(0, 0, UpDown * speed * Time.deltaTime);	//如果UpDown不為0時
			transform.Translate(0, 0, Slop * speed * Time.deltaTime);	//如果Slop不為0時
			if(Input.GetAxis(PlyHor) >= 0)			//如果左右鍵搖桿數值超過0
			{
				if(Input.GetAxis(PlyHor) < 1)		//如果左右鍵搖桿數值小於1 (表示左右鍵搖桿數值介在0~1之間 意思是說玩家放開左右鍵)
				{
					LeftRight = 0;		//讓LeftRight標記為0
					Slop = 0;			//讓Slop標記為0
				}
			}
			
			if(Input.GetAxis(PlyHor) <= 0)			//如果左右鍵搖桿數值超過0
			{
				if(Input.GetAxis(PlyHor) > -0.1f)	//如果左右鍵搖桿數值大於-0.1 (表示左右鍵搖桿數值介在0~-1之間 意思是說玩家放開左右鍵)
				{
					LeftRight = 0;		//讓LeftRight標記為0
					Slop = 0;			//讓Slop標記為0
				}
			}
			
			
			if(Input.GetAxis(PlyVer) <= 0)			//如果上下鍵搖桿數值小於0
			{
				if(Input.GetAxis(PlyVer) > -0.1f)	//如果上下件搖桿數值大於-0.1 (表示上下鍵搖桿數值介在0~-1之間 意思是說玩家放開上下鍵)
				{
					UpDown = 0;		//讓UpDown標記為0
					Slop = 0;		//讓Slop標記為0
				}
			}
			
			if(Input.GetAxis(PlyVer) >= 0)			//如果上下鍵搖桿數值大於0
			{
				if(Input.GetAxis(PlyVer) < 1)		//如果上下件搖桿數值小於1 (表示上下鍵搖桿數值介在0~1之間 意思是說玩家放開上下鍵)
				{
					UpDown = 0;		//讓UpDown標記為0
					Slop = 0;		//讓Slop標記為0
				}
			}
			
			if(Input.GetAxis(PlyVer) == 0)			//如果上下鍵都放開
			{
				UpDown = 0;			//讓UpDown標記為0		
				Slop = 0;			//讓Slop標記為0
			}
			
			
			
			
			if(Input.GetButtonDown(JumpBtn))		//按下小跳鍵
			{
				if(JumpCheck == true)	//JumpCheck表示現在可以釋放小跳
				{
					plysit = "jumpingpar";		//狀態改為跳躍前預備 (會先有跳躍前的預備動作)
					StartCoroutine(TransToJump(JUMP_CDTIME));	//延遲0秒後轉換成跳躍的程式碼
					rigidbody.useGravity = false;	//關閉地吸引力 因為跳躍會有自己的方程式 不能受到地吸引力的影響
					JumpCheck = false;	//改為不能釋放小跳攻擊
				}
			}
			
			if(Input.GetButtonDown(JumpSkillBtn))		//按下大跳鍵
			{
				if(AngryCheck == true)	//先判斷現在憤怒值有沒有滿
				{
					if(BigJumpCheck ==true)	//BigJumpCheck表示現在可以釋放大跳躍
					{
						plysit = "bigjumppar";		//狀態改為跳躍
						StartCoroutine(TransToJump(BIGJUMP_CDTIME));	//延遲1秒後轉換成大跳躍的程式碼
						rigidbody.useGravity = false;	//關閉地吸引力 因為跳躍會有自己的方程式 不能受到地吸引力的影響
						BigJumpCheck = false;	//改為不能釋放大跳攻擊
						AngryCheck = false;		//改成false表示現在憤怒值還沒滿
						Angry = 0;		//憤怒值歸0
					}
				}
			}
			
			if(Input.GetButtonDown(SkillBtn))		//按下突進鍵
			{
				if(SkillCheck == true)		//true表示現在可以釋放突進
				{
					plysit = "Skill";	//狀態改為突進
					SkillCheck = false;	//改為不能是放突進攻擊
				}
			}
		}
		
		if(JumpCheck == false)		//如果JumpCheck為false時 (表示剛剛剛釋放小跳而下面為小跳的CD計算)
		{
			JumpCD -=Time.deltaTime;	//開始減少CD時間
			JumpCDImg.spriteName = "1_CD";	//將圖片改為1_CD
			JumpCDImg.fillAmount = JumpCD/_JumpCD;	//NGUI的函數用來控制CD圖片的方式 建議可以開JumpCDImg的物件出來看
			if(JumpCD <= 0)		//如果倒數結束
			{
				JumpCD = _JumpCD;	//記錄JumpCD的數字
				JumpCheck = true;	//記錄現在可以發動跳躍
				JumpCDImg.spriteName = "space";		//小跳躍CD的圖片改為透明
			}
		}
		
		if(BigJumpCheck == false)		//如果BigJumpCheck為false時 (表示剛剛剛釋放大跳躍而下面為大跳躍的CD計算)
		{
			BigJumpCD -=Time.deltaTime;	//開始減少CD時間
			BigJumpCDImg.spriteName = "butterfly_CD";	//大跳躍的CD的圖片改為butterfly_CD
			BigJumpCDImg.fillAmount = BigJumpCD/_BigJumpCD;	//NGUI的函數用來控制CD圖片的方式 建議可以開BigJumpCDImg的物件出來看
			if(BigJumpCD <=0)		//如果倒數結束
			{
				BigJumpCD = _BigJumpCD;	//記錄BigJumpCD的數字
				BigJumpCheck = true;	//記錄現在可以發動大跳躍
				BigJumpCDImg.spriteName = "space";		//大跳躍CD的圖片改為透明
			}
		}
		
		if(SkillCheck == false)		//如果SkillCheck為false時 (表示剛剛剛釋放突進而下面為突進的CD計算)
		{
			SkillCD -=Time.deltaTime;	//開始減少CD時間
			SkillCDImg.spriteName = "2_CD";	//突進的CD的圖片改為butterfly_CD
			SkillCDImg.fillAmount = SkillCD/_SkillCD;	//NGUI的函數用來控制CD圖片的方式 建議可以開BigJumpCDImg的物件出來看
			if(SkillCD <= 0)		//如果倒數結束
			{
				SkillCD = _SkillCD;	//記錄SkillCD的數字
				SkillCheck = true;	//記錄現在可以發動突進
				SkillCDImg.spriteName = "space";		//突進CD的圖片改為透明
			}
		}
		if(plysit == "Skill")		//如果為突進的狀態
		{
			gameObject.layer = 8;	//把角色的塗層改為Att (只要物件是這個塗層的就是一次性的攻擊 如果layer是9 那那個攻擊就是持續性的)
			gameObject.tag = "50";	//把tag改成50 (如果layer是8或9 那tag的指的就是他的攻擊力)
			SkillTime -=Time.deltaTime;	//持續減少突進的狀態
			gameObject.name = PlyNum.ToString();	//角色物件名稱改成玩家編號 這是用來讓其他角色記錄是被誰攻擊
			rigidbody.useGravity = false;	//關閉地吸引力 避免角色持續往下墜
			if(SkillTime >0)	//如果突進的時間還沒結束的話
			{
				transform.Translate(0, 0, SkillDis / _SkillTime * Time.deltaTime);	//角色依這個方程式一棟
			}	
			if(SkillTime <=0)	//如果突進時間節數的話
			{
				gameObject.tag = "Player";	//把tag改為Player避免在非發動突進的狀態下造成敵人損血
				gameObject.layer = 0;	//layer改為0 表示這不是攻擊
				gameObject.name = "ButterFly(Clone)";	//名稱改也回原本的名字
				SkillTime = _SkillTime;	//記錄一次突進的時間
				collider.isTrigger = false;	//改成Collision 避免被其他東西透過去
				rigidbody.useGravity = true;	//開啟地吸引力
				plysit = "ground";	//狀態也改為ground
			}
		}
		
		if(plysit == "jumping")		//狀態為跳躍
		{
			JumpTime -=Time.deltaTime;	//持續減少跳躍的時間
			Heigh -=Gravity/_JumpTime*Time.deltaTime;	//高度持續減少
			transform.Translate(0,Heigh/_JumpTime*Time.deltaTime,JumpDis/_JumpTime*Time.deltaTime);	//這是角色跳躍拋物線的方程式
			if(JumpTime<=0)	//如果時間結束的話
			{
				plysit = "ground";
				Heigh = _Heigh;
				rigidbody.useGravity = true;
				JumpTime = _JumpTime;
			}
			
		}
		
		if(plysit == "bigjump")		//狀態為大跳躍的時候
		{
			BigJumpTime -=Time.deltaTime;	//持續減少跳躍的時間
			BigJumpHeigh -=BigJumpGravity/_BigJumpTime*Time.deltaTime;	//高度持續減少
			transform.Translate(0,BigJumpHeigh/_BigJumpTime*Time.deltaTime,BigJumpDis/_BigJumpTime*Time.deltaTime);	//這是角色跳躍拋物線的方程式
		}
		
		if(_GameManager.TimeBuffPly > 0)	//如果GameManager中的TimeBuffPly數字改變的話 (這表是說有人吃到時間Buff)
		{
			if(_GameManager.TimeBuffPly != PlyNum)	//確認一次TimeBuffPly的數字 如果數字跟自己角色的不一樣 那就會進入暫停的狀態
			{
				plysit = "TimeBuff";	//狀態改為TimeBuff
			}
		}
		
		if(plysit == "TimeBuff")	//如果狀態為TimeBuff時
		{
			plysit = "CDTime";		//再把狀態改為CDTime (因為事先已經有寫好CDTime狀態了 所以只要有關暫停的狀態都會導向為CDTime)
			CDTime = 3.0f;		//暫停時間3秒
		}
		
		if(plysit == "CDTime")	//暫停的狀態
		{
			if(CDTime >0)	//如果暫停的狀態大於0時
			{
				CDTime-=Time.deltaTime;	//持續減少秒數
				if(CDTime <=0)	//如果到倒數結束時
				{
					rigidbody.useGravity = true;	//把地吸引力開啟
					Heigh = _Heigh;				//記錄高度
					JumpTime = _JumpTime;		//記錄跳躍的時間
					BigJumpHeigh = _BigJumpHeigh;	//記錄大跳躍的高度
					BigJumpTime = _BigJumpTime;		//記錄大跳躍的時間
					plysit = "ground";		//把狀態改為ground
				}
			}
			
		}
		
		if(plysit == "MoveDelay")	//角色跳躍落地僵直的狀態
		{
			if(MoveDelayTime >0)	//如果暫停的狀態大於0時
			{
				MoveDelayTime-=Time.deltaTime;	//持續減少秒數
				if(MoveDelayTime <=0)	//如果到倒數結束時
				{
					rigidbody.useGravity = true;	//把地吸引力開啟
					Heigh = _Heigh;				//記錄高度
					JumpTime = _JumpTime;		//記錄跳躍的時間
					BigJumpHeigh = _BigJumpHeigh;	//記錄大跳躍的高度
					BigJumpTime = _BigJumpTime;		//記錄大跳躍的時間
					plysit = "ground";		//把狀態改為ground
				}
			}
			
		}
		
		
		
		if(plysit == "Stop")	//如果吃到食人花時的麻痺		
		{
			
			if (Input.GetAxis(PlyHor) == -1 && EleCheck == true)	//如果玩家按下左鍵且EleCheck狀態為true時
			{
				EleLeft--;	//減少一次左鍵要按的次數
				EleCheck = false;	//EleCheck改為false(表示等一下要按右鍵)
			}
			
			if (Input.GetAxis(PlyHor) >= 0.5f && EleCheck == false)	//如果玩家按下右鍵且EleCheck狀態為false時		
			{
				EleRight--;	//減少一次右鍵要按的次數
				EleCheck = true;	//EleCheck改為false(表示等一下要按左鍵)
			}
			if(EleLeft <= 0&& EleRight <= 0)	//如果左右要按的次數都為0時(表示已經按完了)
			{
				EleLeft = _EleLeft;		//記錄左鍵要按的次數
				EleRight = _EleRight;	//記錄右鍵要按的次數
				plysit = "ground";	//狀態改為ground
			}
		}
		
		
		
	}
	
	IEnumerator DelayAtt(float num,GameObject attRange)		//小跳落地後的攻擊地板延遲產生函數
	{
		yield return new WaitForSeconds(num);//經過num秒後
		GameObject clone;	//宣告一個clone的物件
		clone = Instantiate(attRange,AttRangePos.position,AttRangePos.rotation) as GameObject;	//產生攻擊地板 切將物件存入clone中
		clone.gameObject.name = PlyNum.ToString();	//將clone的名稱改為角色編號 方便玩家攻擊判斷
	}

	IEnumerator TransToJump(float num)	//小跳起跳前的預備延遲
	{
		if (num == JUMP_CDTIME) 
		{
			yield return new WaitForSeconds (num);	//延遲num秒後
			plysit = "jumping";	//將狀態改為jumping
		} else if(num == BIGJUMP_CDTIME) {
			yield return new WaitForSeconds(num);	//延遲num秒後
			plysit = "bigjump";	//將狀態改為bigjump
		}
	}
	
	IEnumerator BigJumpSkill()	//大跳躍釋放後的慢動作控制                    
	{
		Time.timeScale = 0.05f;	//將時間縮放0.05 (原本為1 表示我們現實世界一秒鐘等於遊戲的一秒鐘 如果為0.5表示現實世界一秒鐘等於遊戲的0.5秒)
		yield return new WaitForSeconds(0.1f);	//經過0.1秒後 (因為時間縮放為0.05秒 而經過0.1秒 其實是我們現實世界的2秒後)
		Time.timeScale = 1;		//將時間縮放改為1
		
		
	}
	IEnumerator SkillDamage(float num)	//被突進攻擊的延遲 如果SkillDamCheck為true表示會扣血 (被突進攻擊一次後會有一點點的時間不會被突進攻擊扣血 避免被同一個攻擊扣兩次血)
	{
		if (num == BESKILLCDTIME) 
		{
			yield return new WaitForSeconds(num);	//經過num秒後
			SkillDamCheck = true;	//將SkillDamCheck為true
			Debug.Log("be skill attacked");
		} else if (num == BEJUMPCDTIME) {
			yield return new WaitForSeconds(num);	//經過num秒後
			AttCheck = true;	//將AttCheck為true
			Debug.Log("be jump attacked");
		}
		
	}
	
	void DamageImgShow(int num)	//頭頂扣血的展示
	{
		Angry +=num;	//憤怒值增加num
		int[] number = new int[3];	//宣告三個空間 (等等用來存放傷害數字的百、十以及各位數)
		number[0] = (num%100)%10;			//個位數的取得
		number[1] = (num - (num-(num%100)) - (num-(num%100))%10)/10;    //十位數的取得	num撿百位再減個位在除以十
		number[2] = (num-(num%100))/100;	//百位數的取得
		DamageOneImg.spriteName = NumImg(number[0],"");		//將剛剛運算的個位數放到NumImg函術中轉換為圖片 再讓頭頂傷害顯示該圖片
		DamageTenImg.spriteName = NumImg(number[1],"");		//將剛剛運算的十位數放到NumImg函術中轉換為圖片 再讓頭頂傷害顯示該圖片
		DamageHundImg.spriteName = NumImg(number[2],"");	//將剛剛運算的百位數放到NumImg函術中轉換為圖片 再讓頭頂傷害顯示該圖片
		Negative.spriteName = "Negative";	//顯示扣血的符號
		StartCoroutine(CleanDamageImg(1));	//這個函數是用來清除頭頂傷害的數字 1秒後清除
	}
	
	void ADDImgShow(int num)
	{
		int[] number = new int[3];	//宣告三個空間 (等等用來存放傷害數字的百、十以及各位數)
		number[0] = (num%100)%10;					////個位數
		number[1] = (num - (num-(num%100)) - (num-(num%100))%10)/10;    //十位數的取得	num撿百位再減個位在除以十
		number[2] = (num-(num%100))/100;	//百位數的取得
		DamageOneImg.spriteName = NumImg(number[0],"");		//將剛剛運算的個位數放到NumImg函術中轉換為圖片 再讓頭頂傷害顯示該圖片
		DamageTenImg.spriteName = NumImg(number[1],"");		//將剛剛運算的十位數放到NumImg函術中轉換為圖片 再讓頭頂傷害顯示該圖片
		DamageHundImg.spriteName = NumImg(number[2],"");	//將剛剛運算的百位數放到NumImg函術中轉換為圖片 再讓頭頂傷害顯示該圖片
		Negative.spriteName = "Postive";	//顯示補血的符號
		StartCoroutine(CleanDamageImg(1));	//這個函數是用來清除頭頂傷害的數字 1秒後清除
	}
	
	
	IEnumerator CleanDamageImg(float num)	//用來清除頭頂傷害的數字
	{
		yield return new WaitForSeconds(num);	//經過num秒後
		Negative.spriteName = "space";		//符號改為透明
		DamageOneImg.spriteName = "space";	//個位數改為透明
		DamageTenImg.spriteName = "space";	//十位數改為透明
		DamageHundImg.spriteName = "space";	//百位數改為透明
	}
	
	IEnumerator messtime(float num)        //玩家吃到時間Buff
	{
		yield return new WaitForSeconds(num);	//經過num秒後
		speed =_speed;	//記錄速度
	}
	
	
	IEnumerator StartDelay(float num)	//攝影機繞場的時間的延遲
	{
		yield return new WaitForSeconds(num);	//經過num秒後
		plysit = "ground";	//狀態改為ground (表示現在角色可以移動了)
		HUD.SetActive(true);	//開啟頭頂介面
		yield return new WaitForSeconds(InvincibleTime);	//經過InvincibleTime秒後
		Invincible = true;	//把Invincible改為true (表示現在可以被攻擊了)
	}
	string NumImg(int num,string count)		//把數字輸入然後轉換為圖片名稱的函數
	{
		switch (num)
		{
		case 0:		//如果輸入數字為0時
			count = "N0";	//就將count記錄為N0
			break;
		case 1:		//如果輸入數字為1時
			count = "N1";	//就將count記錄為N1
			break;
		case 2:		//如果輸入數字為2時
			count = "N2";	//就將count記錄為N2
			break;
		case 3:		//如果輸入數字為3時
			count = "N3";	//就將count記錄為N3
			break;
		case 4:		//如果輸入數字為4時
			count = "N4";	//就將count記錄為N4
			break;
		case 5:		//如果輸入數字為5時
			count = "N5";	//就將count記錄為N5
			break;
		case 6:		//如果輸入數字為6時
			count = "N6";	//就將count記錄為N6
			break;
		case 7:		//如果輸入數字為7時
			count = "N7";	//就將count記錄為N7
			break;
		case 8:		//如果輸入數字為8時
			count = "N8";	//就將count記錄為N8
			break;
		case 9:		//如果輸入數字為9時
			count = "N9";	//就將count記錄為N9
			break;
		case 10:		//如果輸入數字為10時
			count = "N0";	//就將count記錄為N0
			break;
		default:
			break;
		}
		return count;	//回傳count數字
	}
	
	
	void OnCollisionEnter(Collision other) 			
	{
		if(other.gameObject.tag == "Out")	//如果碰到界線的物件時
		{
			transform.position = lastPosition;	//將角色拉回最後記錄的位置
			Debug.Log("Player Out");		//Unity顯示Player Out 表示角色出戒
			if(plysit == "jumping")			//如果為小跳躍狀態時 (表示跳躍的時候碰到界線物件)
			{
				StartCoroutine(DelayAtt(0f,AttRange));	//落地後延遲0秒產生攻擊地板
				plysit = "MoveDelay";	//狀態改為MoveDelay 表示落地的延遲
				MoveDelayTime = 1f;		//延遲1秒鐘
				JumpDis = _JumpDis;		//記錄JumpDis
			}
			
			if(plysit == "bigjump")				//如果為大跳躍狀態時 (表示跳躍的時候碰到界線物件)
			{
				StartCoroutine(DelayAtt(0f,BigAttRange));	//落地後延遲0秒產生攻擊地板
				
				plysit = "MoveDelay";	//狀態改為MoveDelay 表示落地的延遲
				MoveDelayTime = 2f;		//延遲2秒鐘
				BigJumpDis = _BigJumpDis;	//記錄BigJumpDis
				BigJumpHeigh = _BigJumpHeigh;	//記錄BigJumpHeigh
				rigidbody.useGravity = true;	//開啟地吸引力
				BigJumpTime = _BigJumpTime;	//記錄跳躍的時間
				SkillObjCheck = true;	//大跳躍特效可以釋放
				StartCoroutine(BigJumpSkill());		//時間變成慢動作
				if(SkillObjCheck == true)	//如果為true時(用這個變數判斷 避免特效從覆產生)
				{
					GameObject Obj;		//宣告obj
					Obj = (GameObject)Instantiate(SkillObj,AttRangePos.transform.position,AttRange.transform.rotation);	//產生大跳躍特效 並回存為Obj
					Obj.gameObject.name = PlyNum.ToString();	//將大跳躍特效名稱改為角色編號
					SkillObjCheck = false;	//改為false 這樣上列程式碼就只會產生產生一次
				}
				gamemamager.SendMessage("BigJumpSkillImgCha",characterNum);	//傳送訊息給GameManager 讓GameManager在遊戲畫面出現蝴蝶的圖片
				Debug.Log("characternum : "+ characterNum);
			}
		}
		if(Invincible == true)	//如果無敵狀態消失時
		{
			if(other.gameObject.layer == 8)		//小跳攻擊地板碰撞
			{
				DamageImgShow(int.Parse(other.gameObject.tag));		//以攻擊地板的tag轉為數字並轉為頭頂傷害的圖片
				AttName = other.gameObject.name;	//把攻擊地板的名稱存入AttName 表示剛剛受到該編號對應的玩家的攻擊
				blood -=float.Parse(other.gameObject.tag);		//以攻擊地板的tag轉為數字並當作扣血的依據
				Instantiate(WaAtt,transform.position,transform.rotation);	//產生受傷的特效
			}
		}
		if(other.gameObject.tag == "Wall")	//如果撞到為Wall的物件的時候 (tag為Wall跟tag為Out其實都是邊界的物件 而tag為Out的位置比較外圍，有時候會只碰到Out而沒碰到Wall)
		{
			transform.position = lastPosition;	//一樣回復到角色上一次記錄的位置
		}
		if(other.gameObject.tag == "ground" || other.gameObject.tag == "Wall" || other.gameObject.tag == "Player")		//踩到地板的時候
		{
			if(plysit == "jumping")		//如果為跳躍要降落後
			{
				StartCoroutine(DelayAtt(0f,AttRange));	//落地後延遲0秒產生攻擊地板
				plysit = "MoveDelay";	//狀態改為MoveDelay 表示落地的延遲
				MoveDelayTime = 1f;	//延遲1秒鐘
				JumpDis = _JumpDis;	//記錄JumpDis
			}
			
			if(plysit == "bigjump")				//如果為跳躍要降落後
			{
				StartCoroutine(DelayAtt(0f,BigAttRange));	//落地後延遲0秒產生攻擊地板				
				plysit = "MoveDelay";	//狀態改為MoveDelay 表示落地的延遲
				MoveDelayTime = 2f;	//延遲2秒鐘
				BigJumpDis = _BigJumpDis;	//記錄BigJumpDis		
				BigJumpHeigh = _BigJumpHeigh;	//記錄BigJumpHeigh
				rigidbody.useGravity = true;	//開啟地吸引力
				BigJumpTime = _BigJumpTime;	//記錄跳躍的時間
				SkillObjCheck = true;	//大跳躍特效可以釋放
				StartCoroutine(BigJumpSkill());			//時間變成慢動作
				if(SkillObjCheck == true)	//如果為true時(用這個變數判斷 避免特效從覆產生)
				{
					GameObject Obj;		//宣告obj
					Obj = (GameObject)Instantiate(SkillObj,AttRangePos.transform.position,AttRange.transform.rotation);	//產生大跳躍特效 並回存為Obj
					Obj.gameObject.name = PlyNum.ToString();	//將大跳躍特效名稱改為角色編號
					SkillObjCheck = false;	//改為false 這樣上列程式碼就只會產生產生一次
				}
				gamemamager.SendMessage("BigJumpSkillImgCha",characterNum);	//傳送訊息給GameManager 讓GameManager在遊戲畫面出現蝴蝶的圖片
				
			}
			
			
			
		}
		if(other.gameObject.name == "TurtleFall")	//如果撞到烏龜石像時
		{
			Instantiate(WaAtt,transform.position,transform.rotation);	//產生受傷特效
			AttName = "";	//清空被攻擊的物件 (但如果被石像砸死時 攻擊玩家的那個圖片會是空的)
			blood -=50;	//扣50滴血
			DamageImgShow(50);	//顯示扣50滴血的圖片
		}
	}
	
	
	
	
	
	
	
	void OnTriggerEnter(Collider other)
	{
		if(Invincible == true)		//如果為true  表示現在不是無敵狀態 可以被攻擊
		{
			if(other.gameObject.layer == 8)			//小跳和大跳攻擊碰撞
			{
				if(AttCheck == true)	//而AttCheck為true 表示現在可已損血
				{
					string Att = PlyNum.ToString();	//宣告一個Att並存放自己的玩家編號
					if(other.gameObject.name != Att)	//判斷物件名稱與自己的玩家編號是否想同(相同的話 表示是自己的攻擊 就不會扣血 不相同的話就會扣血)
					{
						AttCheck = false;	//AttCheck記錄為False 攻擊的CD 避免同一個攻擊扣血兩次
						DamageImgShow(int.Parse(other.gameObject.tag));		//依物件的tag顯示頭頂扣血
						AttName = other.gameObject.name;	//記錄被攻擊的玩家編號
						blood -=float.Parse(other.gameObject.tag);	//依物件的tag當作扣寫的依據
						Instantiate(WaAtt,transform.position,transform.rotation);	//顯示受傷特效
						StartCoroutine(SkillDamage(BEJUMPCDTIME));	//AttCheck延遲1.3秒後再改為True (表示這1.3秒內不會被大跳小跳攻擊而損血)
					}
				}
				
			}
		}
		
		if(other.gameObject.name == "buff_speedup(Clone)")      //吃到時間BUFF
		{
			speed  = speed*0.5f;	//把速度改為原本的一半
			StartCoroutine(messtime(5));	//速度回復的程式碼5秒後回覆原本的速度
		}
		
		if(other.gameObject.name == "buff_hpup(Clone)")      //吃到補血BUFF
		{
			int HpBuff;	//宣告一個HPBuff
			HpBuff = int.Parse(other.gameObject.tag);	//將物件的tag回存為HPBuff
			blood +=HpBuff;		//以剛剛存到的HPBuff做為回血的依據
			ADDImgShow(100);	//顯示補100滴血
		}
		
		if(other.gameObject.tag == "Elecric")	//碰到食人花
		{
			plysit = "Stop";	//裝態改為Stop
			rigidbody.useGravity = true;	//開啟地吸引力
			Destroy(other.gameObject,1);	//一秒後刪除食人花
		}
		
		if(other.gameObject.tag == "TimeBuff")	//碰到時間Buff
		{
			_GameManager.TimeBuffPly = PlyNum;	//把角色編號存入GameManager的TimeBuffPly中
			Destroy(other.gameObject);	//刪除時間Buff物件
		}
	}
	
	
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Tank")	//碰到坦克的時候
		{
			if(Input.GetButtonDown(JumpBtn))	//按下小跳鍵
			{
				plysit = "Tank";		//狀態改為tank
				gamemamager.SendMessage("SendPlyHor",PlyHor);	//傳送自己左右控制按鍵名稱給坦克作左右控制
				gamemamager.SendMessage("SendSkillBtn",JumpSkillBtn);	//傳送自己突進按鍵名稱給坦克作發射控制
				gamemamager.SendMessage("SendPlyNum",PlyNum);	//傳送自己的角色編號改坦克
				transform.position = other.transform.position + new Vector3(0,1,0);	//改變自己的位置
				transform.eulerAngles = other.transform.eulerAngles;	//把自己的角色的角度改的跟坦克一樣
				gameObject.transform.parent = other.gameObject.transform;	//把坦克做為角色的父物件 這樣就可以一起旋轉了
			}
			
			if(Input.GetButtonDown(SkillBtn))
			{
				int zero;	//宣告變數zero
				zero = 0;	//zero為0
				gameObject.transform.parent = null;	//移除角色的父物件
				plysit = "ground";	//狀態改成ground
				gamemamager.SendMessage("SendPlyHor","");	//坦克的左右旋轉控制都清空不控制
				gamemamager.SendMessage("SendSkillBtn","");	//坦克的發射砲彈控制清空不控制
				gamemamager.SendMessage("SendPlyNum",zero);	//坦克子彈的發射名稱清空
			}
		}
		
		if(Invincible == true)
		{
			if(other.gameObject.layer == 9)				///大絕招特效碰撞(這邊的大絕招特效碰撞是指持續性的 碰到會一直扣血的 像毒物 火焰 水龍捲 冰柱 不包括烏龜的爆炸
			{
				string Att = PlyNum.ToString();	//記錄自己的玩家編號
				if(other.gameObject.name != Att)	//如果攻擊的物件的名稱跟自己不一樣的時候(表示受到別人的攻擊)
				{
					if(SkillDamCheck == true)	//如果SkillDamCheck為true時 表示現在可以被大絕招特效扣血
					{
						AttName = other.gameObject.name;	//依照攻擊物件的名稱存入攻擊的玩家編號
						Instantiate(WaAtt,transform.position,transform.rotation);	//產生損血特效
						SkillDamCheck = false;	//改為False 避免扣血扣太快
						DamageImgShow(int.Parse(other.gameObject.tag));		//依攻擊物件的tag做為扣血顯示的依據
						blood -=float.Parse(other.gameObject.tag);		///依攻擊物件的tag做為扣血的依據
						StartCoroutine(SkillDamage(BESKILLCDTIME));	//SkillDamCheck的延遲 1秒後改為true
					}
				}
			}
		}
		
	}
	
	
	void OnTriggerExit(Collider other) //離開水流變回原本速度
	{
		if(other.gameObject.tag == "Slow")		//碰到tag為slow的物件的時候 (是一審前有說要做水流影響玩家的功能)
		{
			speed = 40;		//速度改為40
		}
		if(other.gameObject.tag == "Elecric")	//碰到麻痺的物件時
		{
			Destroy(other.gameObject);		//刪除物件
		}
	}
}
