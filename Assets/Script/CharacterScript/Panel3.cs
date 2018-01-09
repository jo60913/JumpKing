using UnityEngine;
using System.Collections;

public class Panel3 : MonoBehaviour {
	public int OnePreCha;	//P1現在所選的角色 1的話就是蝴蝶 2->炎龍 3->烏龜 4->狼人 5->烏賊
	public int TwoPreCha;	//P2現在所選的角色 1的話就是蝴蝶 2->炎龍 3->烏龜 4->狼人 5->烏賊
	public int ThreePreCha;	//P3現在所選的角色 1的話就是蝴蝶 2->炎龍 3->烏龜 4->狼人 5->烏賊
	public int FourPreCha;	//P4現在所選的角色 1的話就是蝴蝶 2->炎龍 3->烏龜 4->狼人 5->烏賊
	public Texture[] P1RenderTexture;	//P1選角色時 所要對應出的角色貼圖	
	//每個角色都會有4隻分別給P1 P2 P3 P4 (如果5種角色只放一隻的話，假設P1選到蝴蝶的時候蝴蝶會呈現選擇狀態而P2也選到蝴蝶時卻會看到蝴蝶也在選擇狀態)
	public Texture[] P2RenderTexture;	//P2選角色時 所要對應出的角色貼圖	
	public Texture[] P3RenderTexture;	//P3選角色時 所要對應出的角色貼圖	
	public Texture[] P4RenderTexture;	//P4選角色時 所要對應出的角色貼圖	
	public UISprite[] PlyCheck;		//用來顯示玩家準備完成的介面圖
	public UISprite[] PlyDetail;	//用來顯示玩家所選的角色能力的介面圖
	private ChoseAnim[] P1RoleScript = new ChoseAnim[5];	//P1的的5隻角色的P1RoleScript腳本
	private ChoseAnim[] P2RoleScript = new ChoseAnim[5];	//P2的的5隻角色的P1RoleScript腳本
	private ChoseAnim[] P3RoleScript = new ChoseAnim[5];	//P3的的5隻角色的P1RoleScript腳本
	private ChoseAnim[] P4RoleScript = new ChoseAnim[5];	//P4的的5隻角色的P1RoleScript腳本
	public GameObject[] P1Role = new GameObject[5];		//P1的的5隻角色的物件
	public GameObject[] P2Role = new GameObject[5];		//P2的的5隻角色的物件
	public GameObject[] P3Role = new GameObject[5];		//P3的的5隻角色的物件
	public GameObject[] P4Role = new GameObject[5];		//P4的的5隻角色的物件
	public GameObject P1ImgShow;	//P1用來呈現玩家所選角色的物件
	public GameObject P2ImgShow;	//P2用來呈現玩家所選角色的物件
	public GameObject P3ImgShow;	//P3用來呈現玩家所選角色的物件
	public GameObject P4ImgShow;	//P4用來呈現玩家所選角色的物件
	public GameObject black3;	//P3沒有人玩時所要顯示柵欄的物件
	public GameObject black4;	//P4沒有人玩時所要顯示柵欄的物件
	public bool ChoseCha;	//用來控制玩家是否可以選角色的判斷 (避免再選擇人數或場地的時候干擾到)
	
	public float AddTime;	//各個搖桿左右按鍵的延遲 避免搖桿反應過快 沒辦法選到玩家所選的

	private bool Stick1Check;	//確認P1是否可以按左右來選擇 會搭配上面AddTime做使用
	private bool Stick2Check;	//確認P2是否可以按左右來選擇 會搭配上面AddTime做使用
	private bool Stick3Check;	//確認P3是否可以按左右來選擇 會搭配上面AddTime做使用
	private bool Stick4Check;	//確認P4是否可以按左右來選擇 會搭配上面AddTime做使用
	private Click1 _main;	//宣告Click腳本

	public bool P1Check;	//確認P1現在是否完成選擇
	public bool P2Check;	//確認P2現在是否完成選擇
	public bool P3Check;	//確認P3現在是否完成選擇
	public bool P4Check;	//確認P4現在是否完成選擇
	public bool AllCheck;	//確認現在是否所有玩家都已經選擇完
	void Start () {

		PlyCheck[0].spriteName = "space";	//先將P1的準備的圖是隱藏起來
		PlyCheck[1].spriteName = "space";	//先將P2的準備的圖是隱藏起來
		PlyCheck[2].spriteName = "space";	//先將P3的準備的圖是隱藏起來
		PlyCheck[3].spriteName = "space";	//先將P4的準備的圖是隱藏起來

		P1RoleScript[0] = P1Role[0].GetComponent<ChoseAnim>();	//載入P1蝴蝶的ChoseAnim腳本
		P1RoleScript[1] = P1Role[1].GetComponent<ChoseAnim>();	//載入P1炎龍的ChoseAnim腳本
		P1RoleScript[2] = P1Role[2].GetComponent<ChoseAnim>();	//載入P1烏龜的ChoseAnim腳本
		P1RoleScript[3] = P1Role[3].GetComponent<ChoseAnim>();	//載入P1狼人的ChoseAnim腳本
		P1RoleScript[4] = P1Role[4].GetComponent<ChoseAnim>();	//載入P1烏賊的ChoseAnim腳本

		P2RoleScript[0] = P2Role[0].GetComponent<ChoseAnim>();	//載入P2蝴蝶的ChoseAnim腳本
		P2RoleScript[1] = P2Role[1].GetComponent<ChoseAnim>();	//載入P2炎龍的ChoseAnim腳本
		P2RoleScript[2] = P2Role[2].GetComponent<ChoseAnim>();	//載入P2烏龜的ChoseAnim腳本
		P2RoleScript[3] = P2Role[3].GetComponent<ChoseAnim>();	//載入P2狼人的ChoseAnim腳本
		P2RoleScript[4] = P2Role[4].GetComponent<ChoseAnim>();	//載入P2烏賊的ChoseAnim腳本

		P3RoleScript[0] = P3Role[0].GetComponent<ChoseAnim>();	//載入P3蝴蝶的ChoseAnim腳本
		P3RoleScript[1] = P3Role[1].GetComponent<ChoseAnim>();	//載入P3炎龍的ChoseAnim腳本
		P3RoleScript[2] = P3Role[2].GetComponent<ChoseAnim>();	//載入P3烏龜的ChoseAnim腳本
		P3RoleScript[3] = P3Role[3].GetComponent<ChoseAnim>();	//載入P3狼人的ChoseAnim腳本
		P3RoleScript[4] = P3Role[4].GetComponent<ChoseAnim>();	//載入P3烏賊的ChoseAnim腳本

		P4RoleScript[0] = P4Role[0].GetComponent<ChoseAnim>();	//載入P4蝴蝶的ChoseAnim腳本
		P4RoleScript[1] = P4Role[1].GetComponent<ChoseAnim>();	//載入P4炎龍的ChoseAnim腳本
		P4RoleScript[2] = P4Role[2].GetComponent<ChoseAnim>();	//載入P4烏龜的ChoseAnim腳本
		P4RoleScript[3] = P4Role[3].GetComponent<ChoseAnim>();	//載入P4狼人的ChoseAnim腳本
		P4RoleScript[4] = P4Role[4].GetComponent<ChoseAnim>();	//載入P4烏賊的ChoseAnim腳本

		OnePreCha = 1;	//P1角色選擇預設成蝴蝶
		TwoPreCha = 1;	//P1角色選擇預設成蝴蝶
		ThreePreCha = 1;	//P1角色選擇預設成蝴蝶
		FourPreCha = 1;	//P1角色選擇預設成蝴蝶
		_main = transform.GetComponent<Click1>();	//載入Click1腳本
		ChoseCha = false;	//剛進入選角場景時 設定成false表示現在還不可以選 因為一開始還有人數選擇 人數選擇完後才會是角色選擇
		Stick1Check = true;	//P1搖桿可以控制
		Stick2Check = true;	//P2搖桿可以控制
		Stick3Check = true;	//P3搖桿可以控制
		Stick4Check = true;	//P1搖桿可以控制
		P1Check = false;	//設定成false表示P1還可以選擇角色 如果維ture表示P1已準備好
		P2Check = false;	//設定成false表示P2還可以選擇角色 如果維ture表示P2已準備好
		P3Check = false;	//設定成false表示P3還可以選擇角色 如果維ture表示P3已準備好
		P4Check = false;	//設定成false表示P4還可以選擇角色 如果維ture表示P4已準備好

		AllCheck = false;	//false表示還有玩家還沒選好 所有玩家選好時會為ture 之後也會馬上進入選擇場景介面
	}
	

	void Update () {
		if(ChoseCha)	//如果ChoseCha為true時 (表示現在玩家在選角介面)
		{
			if(P1Check == false)	//如果P1還沒選好時
			{
				if(Input.GetAxis("Horizontal1") <= -0.8f)		//P1按下左鍵	
				{
					if(Stick1Check == true)		//如果P1這個時候是可以按下控制時
					{
						Stick1Check = false;	//設定P1不可以控制按鍵
						StartCoroutine(StickCon1(Input.GetAxis("Horizontal1")));	//P1進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(OnePreCha <1)	//如果P1角色的選擇數小於1的時候
						{
							OnePreCha = 1;	//就讓P1角色選擇數等於1 避免出現異常
						}
						charator(1,P1ImgShow,OnePreCha);	
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
				
				
				if(Input.GetAxis("Horizontal1") >= 0.8f)		//P1按下右鍵	
				{
					
					if(Stick1Check == true)		//如果P1這個時候是可以按下控制時
					{
						Stick1Check = false;	//設定P1不可以控制按鍵
						StartCoroutine(StickCon1(Input.GetAxis("Horizontal1")));	//P1進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(OnePreCha > 5)	//如果P1角色的選擇數大於5的時候
						{
							OnePreCha = 5;	//就讓P1角色選擇數等於5 避免出現異常
						}
						charator(1,P1ImgShow,OnePreCha);	
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.X))	//如果P1按下確認鍵時
			{
				P1CheckIsTrue();	//執行方程式 判斷現在該顯示準備完成還是 顯示角色資料
			}

			if(P2Check == false)	//如果P2還沒選好時
			{
				if(Input.GetAxis("Horizontal2") <= -0.8f)		//P2按下左鍵	
				{
					if(Stick2Check == true)	//如果P2這個時候是可以按下控制時
					{
						Stick2Check = false;	//設定P2不可以控制按鍵
						StartCoroutine(StickCon2(Input.GetAxis("Horizontal2")));	//P2進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(TwoPreCha <1)	//如果P2角色的選擇數小於1的時候
						{
							TwoPreCha = 1;	//就讓P2角色選擇數等於1 避免出現異常
						}
						charator(2,P2ImgShow,TwoPreCha);	
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
				
				
				if(Input.GetAxis("Horizontal2") >= 0.8f)		//P2按下右鍵	
				{
					
					if(Stick2Check == true)	//如果P2這個時候是可以按下控制時
					{
						Stick2Check = false;	//設定P2不可以控制按鍵
						StartCoroutine(StickCon2(Input.GetAxis("Horizontal2")));	//P2進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(TwoPreCha > 5)	//如果P2角色的選擇數大於5的時候
						{
							TwoPreCha = 5;	//就讓P2角色選擇數等於5 避免出現異常
						}
						charator(2,P2ImgShow,TwoPreCha);	
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.Joystick2Button2)  || Input.GetKeyDown(KeyCode.M))	//如果P2按下確認鍵時
			{
				P2CheckIsTrue();	//執行方程式 判斷現在該顯示準備完成還是 顯示角色資料
			}

			if(P3Check == false)	//如果P3還沒選好時
			{
				if(Input.GetAxis("Horizontal3") <= -0.8f)		//P3按下左鍵	
				{
					if(Stick3Check == true)	//如果P3這個時候是可以按下控制時
					{
						Stick3Check = false;	//設定P3不可以控制按鍵
						StartCoroutine(StickCon3(Input.GetAxis("Horizontal3")));	//P3進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(ThreePreCha <1)	//如果P3角色的選擇數小於1的時候
						{
							ThreePreCha = 1;	//就讓P3角色選擇數等於1 避免出現異常
						}
						charator(3,P3ImgShow,ThreePreCha);	
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}


				if(Input.GetAxis("Horizontal3") >= 0.8f)		//P2按下右鍵	
				{

					if(Stick3Check == true)	//如果P3這個時候是可以按下控制時
					{
						Stick3Check = false;	//設定P3不可以控制按鍵
						StartCoroutine(StickCon3(Input.GetAxis("Horizontal3")));	//P3進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(ThreePreCha > 5)	//如果P3角色的選擇數大於5的時候
						{
							ThreePreCha = 5;	//就讓P3角色選擇數等於5 避免出現異常
						}
						charator(3,P3ImgShow,ThreePreCha);	
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.Joystick3Button2))		//如果P3按下確認鍵時
			{
				P3CheckIsTrue();	//執行方程式 判斷現在該顯示準備完成還是 顯示角色資料
			}

			if(P4Check == false)	//如果P4還沒選好時
			{
				if(Input.GetAxis("Horizontal4") <= -0.8f)		//P4按下左鍵	
				{
					if(Stick4Check == true)	//如果P4這個時候是可以按下控制時
					{
						Stick4Check = false;	//設定P4不可以控制按鍵
						StartCoroutine(StickCon4(Input.GetAxis("Horizontal4")));	//P4進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色
						if(FourPreCha <1)	//如果P4角色的選擇數小於1的時候
						{
							FourPreCha = 1;	//就讓P3角色選擇數等於1 避免出現異常
						}
						charator(4,P4ImgShow,FourPreCha);
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
				
				if(Input.GetAxis("Horizontal4") >= 0.8f)		//P3按下右鍵	
				{
					if(Stick4Check == true)	//如果P4這個時候是可以按下控制時
					{
						Stick4Check = false;	//設定P4不可以控制按鍵
						StartCoroutine(StickCon4(Input.GetAxis("Horizontal4")));	//P4進入按鍵CD時間 避免搖桿速度過快 選擇不到玩家要的角色			
						if(FourPreCha > 5)	//如果P3角色的選擇數大於5的時候
						{
							FourPreCha = 5;	//就讓P3角色選擇數等於5 避免出現異常
						}
						charator(4,P4ImgShow,FourPreCha);
						//上列程式碼是切換顯示所選擇的角色 框框內第一個數字填的是玩家數 如P1->1 P2->2以此類推 第二格是所要顯示角色圖片的物件 第三個是玩家的角色選擇數
					}
				}
			}

			if(Input.GetKeyDown(KeyCode.Joystick4Button2))		//如果P4按下確認鍵時
			{
				P4CheckIsTrue();	//執行方程式 判斷現在該顯示準備完成還是 顯示角色資料
			}

			if(_main.select == 2)	//如果Click1的select為2時 表示現在玩家數為2人 (_main的宣告型態為Click1)
			{
				if(P1Check == true && P2Check == true)	//如果P1 及P2都準備完成時
				{
					AllCheck = true;	//讓AllCheck為true 表示全場玩家選擇完成
				}else{					//其他情況
					AllCheck = false;	//其他情況都是false表示還有人還沒選完
				}
			}

			if(_main.select == 3)	//如果Click1的select為3時 表示現在玩家數為3人 (_main的宣告型態為Click1)
			{
				if(P1Check == true && P2Check == true && P3Check == true)	//如果P1 P2 P3 都準備完成時
				{
					AllCheck = true;	//讓AllCheck為true 表示全場玩家選擇完成
				}else{					//其他情況
					AllCheck = false;
				}
			}

			if(_main.select == 4)	//如果Click1的select為4時 表示現在玩家數為4人 (_main的宣告型態為Click1)
			{
				if(P1Check == true && P2Check == true && P3Check == true && P4Check == true)	//如果P1 P2 P3 P4 都準備完成時
				{
					AllCheck = true;	//讓AllCheck為true 表示全場玩家選擇完成
				}else{					//其他情況
					AllCheck = false;	//其他情況都是false表示還有人還沒選完
				}
			}
		}

	}
	public void P1CheckIsTrue()		//P1 判斷現在是否該顯示準備完成還是該顯示角色資料
	{
		if(P1Check == true)		//如果為true時 表示現在要取消準備
		{
			P1Check = false;	//設定成false表示還沒選擇完成
			PlyCheck[0].spriteName = "space";	//將準備完成的圖片改成透明讓玩家看不出來
			PlyDetail[0].gameObject.SetActive(true);	//開啟角色能力介面
			charator(1,P1ImgShow,OnePreCha);	//更新一次現在所選角色的圖片
			switch (OnePreCha)	//取消剛剛選擇的角色的動畫 因為選定時角色會切換成選擇的動畫 
			{
			case 1:	//如果選擇蝴蝶
				P1RoleScript[0].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 2:	//如果選擇炎龍
				P1RoleScript[1].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 3:	//如果選擇烏龜
				P1RoleScript[2].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 4:	//如果選擇狼人
				P1RoleScript[3].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 5:	//如果選擇烏賊
				P1RoleScript[4].Check = false;	//控制腳本 切換成待機動畫
				break;
			default:
				break;
			}
		}else	//如果現在為false時 表示現在要切換成確定
		{
			P1Check = true;		//切換成現在P1選擇完成
			PlyCheck[0].spriteName = "Get Ready_01(2)";		//顯示圖片為準備
			PlyDetail[0].gameObject.SetActive(false);		//關閉角色資料
			switch (OnePreCha)	//將角色切換成準備動畫
			{
			case 1:	//如果選擇蝴蝶
				P1RoleScript[0].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 2:	//如果選擇炎龍
				P1RoleScript[1].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 3:	//如果選擇烏龜
				P1RoleScript[2].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 4:	//如果選擇狼人
				P1RoleScript[3].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 5:	//如果選擇烏賊
				P1RoleScript[4].Check = true;	//控制腳本 切換成準備動畫
				break;
			default:
				break;
			}
		}
	}
	public void P2CheckIsTrue()		//P2 判斷現在是否該顯示準備完成還是該顯示角色資料
	{
		if(P2Check == true)		//如果為true時 表示現在要取消準備
		{
			P2Check = false;	//設定成false表示還沒選擇完成
			PlyCheck[1].spriteName = "space";	//將準備完成的圖片改成透明讓玩家看不出來
			PlyDetail[1].gameObject.SetActive(true);	//開啟角色能力介面
			charator(2,P2ImgShow,TwoPreCha);	//更新一次現在所選角色的圖片
			switch (TwoPreCha)	//取消剛剛選擇的角色的動畫 因為選定時角色會切換成選擇的動畫 
			{
			case 1:	//如果選擇蝴蝶
				P2RoleScript[0].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 2:	//如果選擇炎龍
				P2RoleScript[1].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 3:	//如果選擇烏龜
				P2RoleScript[2].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 4:	//如果選擇狼人
				P2RoleScript[3].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 5:	//如果選擇烏賊
				P2RoleScript[4].Check = false;	//控制腳本 切換成待機動畫
				break;
			default:
				break;
			}
		}else	//如果現在為false時 表示現在要切換成確定
		{
			P2Check = true;		//切換成現在P2選擇完成
			PlyCheck[1].spriteName = "Get Ready_01(2)";		//顯示圖片為準備
			PlyDetail[1].gameObject.SetActive(false);		//關閉角色資料
			switch (TwoPreCha)	//將角色切換成準備動畫
			{
			case 1:	//如果選擇蝴蝶
				P2RoleScript[0].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 2:	//如果選擇炎龍
				P2RoleScript[1].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 3:	//如果選擇烏龜
				P2RoleScript[2].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 4:	//如果選擇狼人
				P2RoleScript[3].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 5:	//如果選擇烏賊
				P2RoleScript[4].Check = true;	//控制腳本 切換成準備動畫
				break;
			default:
				break;
			}
		}
	}
	public void P3CheckIsTrue()		//P3 判斷現在是否該顯示準備完成還是該顯示角色資料
	{
		if(P3Check == true)		//如果為true時 表示現在要取消準備
		{
			P3Check = false;	//設定成false表示還沒選擇完成
			PlyCheck[2].spriteName = "space";	//將準備完成的圖片改成透明讓玩家看不出來
			PlyDetail[2].gameObject.SetActive(true);	//開啟角色能力介面
			charator(3,P3ImgShow,ThreePreCha);	//更新一次現在所選角色的圖片
			switch (ThreePreCha)	//取消剛剛選擇的角色的動畫 因為選定時角色會切換成選擇的動畫 
			{
			case 1:	//如果選擇蝴蝶
				P3RoleScript[0].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 2:	//如果選擇炎龍
				P3RoleScript[1].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 3:	//如果選擇烏龜
				P3RoleScript[2].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 4:	//如果選擇狼人
				P3RoleScript[3].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 5:	//如果選擇烏賊
				P3RoleScript[4].Check = false;	//控制腳本 切換成待機動畫
				break;
			default:
				break;
			}
		}else	//如果現在為false時 表示現在要切換成確定
		{
			P3Check = true;		//切換成現在P3選擇完成
			PlyCheck[2].spriteName = "Get Ready_01(2)";		//顯示圖片為準備
			PlyDetail[2].gameObject.SetActive(false);		//關閉角色資料
			switch (ThreePreCha)	//將角色切換成準備動畫
			{
			case 1:	//如果選擇蝴蝶
				P3RoleScript[0].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 2:	//如果選擇炎龍
				P3RoleScript[1].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 3:	//如果選擇烏龜
				P3RoleScript[2].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 4:	//如果選擇狼人
				P3RoleScript[3].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 5:	//如果選擇烏賊
				P3RoleScript[4].Check = true;	//控制腳本 切換成準備動畫
				break;
			default:
				break;
			}
		}
	}
	public void P4CheckIsTrue()		//P4 判斷現在是否該顯示準備完成還是該顯示角色資料
	{
		if(P4Check == true)	//如果為true時 表示現在要取消準備
		{
			P4Check = false;	//設定成false表示還沒選擇完成
			PlyCheck[3].spriteName = "space";	//將準備完成的圖片改成透明讓玩家看不出來
			PlyDetail[3].gameObject.SetActive(true);	//開啟角色能力介面
			charator(4,P4ImgShow,FourPreCha);	//更新一次現在所選角色的圖片
			switch (FourPreCha)	//取消剛剛選擇的角色的動畫 因為選定時角色會切換成選擇的動畫 
			{
			case 1:	//如果選擇蝴蝶
				P4RoleScript[0].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 2:	//如果選擇炎龍
				P4RoleScript[1].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 3:	//如果選擇烏龜
				P4RoleScript[2].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 4:	//如果選擇狼人
				P4RoleScript[3].Check = false;	//控制腳本 切換成待機動畫
				break;
			case 5:	//如果選擇烏賊
				P4RoleScript[4].Check = false;	//控制腳本 切換成待機動畫
				break;
			default:
				break;
			}
		}else{	//如果現在為false時 表示現在要切換成確定
			P4Check = true;		//切換成現在P3選擇完成
			PlyCheck[3].spriteName = "Get Ready_01(2)";		//顯示圖片為準備
			PlyDetail[3].gameObject.SetActive(false);		//關閉角色資料
			switch (FourPreCha)	//將角色切換成準備動畫
			{
			case 1:	//如果選擇蝴蝶
				P4RoleScript[0].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 2:	//如果選擇炎龍
				P4RoleScript[1].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 3:	//如果選擇烏龜
				P4RoleScript[2].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 4:	//如果選擇狼人
				P4RoleScript[3].Check = true;	//控制腳本 切換成準備動畫
				break;
			case 5:	//如果選擇烏賊
				P4RoleScript[4].Check = true;	//控制腳本 切換成準備動畫
				break;
			default:
				break;
			}
		}
	}



	void charator(int Pnum,GameObject PerNum,int num)	//角色呈現的切換
	{
		switch(Pnum)
		{
		case 1:		//P1的部分
			switch (num)
			{
			case 1:
				PerNum.renderer.material.mainTexture = P1RenderTexture[0];	//將圖片改為蝴蝶	(P1RenderTexture[0]代表蝴蝶)
				PlyDetail[0].spriteName = "butterfly";		//將下面的角色資料切換為蝴蝶的
				break;
			case 2:
				PerNum.renderer.material.mainTexture = P1RenderTexture[1];	//將圖片改為炎龍	(P1RenderTexture[1]代表炎龍)
				PlyDetail[0].spriteName = "dragon";		//將下面的角色資料切換為炎龍的
				break;
			case 3:
				PerNum.renderer.material.mainTexture = P1RenderTexture[2];	//將圖片改為烏龜	(P1RenderTexture[2]代表烏龜)
				PlyDetail[0].spriteName = "LFT";		//將下面的角色資料切換為烏龜的
				break;
			case 4:
				PerNum.renderer.material.mainTexture = P1RenderTexture[3];	//將圖片改為狼人	(P1RenderTexture[3]代表狼人)
				PlyDetail[0].spriteName = "wolf";		//將下面的角色資料切換為狼人的
				break;
			case 5:
				PerNum.renderer.material.mainTexture = P1RenderTexture[4];	//將圖片改為烏賊	(P1RenderTexture[4]代表烏賊)
				PlyDetail[0].spriteName = "clown";		//將下面的角色資料切換為烏賊的
				break;
			default:
				print ("charator choose overflow");	//如果超出預期Unity就顯示charator choose overflow
				break;
			}	
		break;
		case 2:		//P2的部分
			switch (num)
			{
			case 1:
				PerNum.renderer.material.mainTexture = P2RenderTexture[0];	//將圖片改為蝴蝶	(P2RenderTexture[0]代表蝴蝶)
				PlyDetail[1].spriteName = "butterfly";		//將下面的角色資料切換為蝴蝶的
				break;
			case 2:
				PerNum.renderer.material.mainTexture = P2RenderTexture[1];	//將圖片改為炎龍	(P2RenderTexture[1]代表炎龍)
				PlyDetail[1].spriteName = "dragon";		//將下面的角色資料切換為炎龍的
				break;
			case 3:
				PerNum.renderer.material.mainTexture = P2RenderTexture[2];	//將圖片改為烏龜	(P2RenderTexture[2]代表烏龜)
				PlyDetail[1].spriteName = "LFT";		//將下面的角色資料切換為烏龜的
				break;
			case 4:
				PerNum.renderer.material.mainTexture = P2RenderTexture[3];	//將圖片改為狼人	(P2RenderTexture[3]代表狼人)
				PlyDetail[1].spriteName = "wolf";		//將下面的角色資料切換為狼人的
				break;
			case 5:
				PerNum.renderer.material.mainTexture = P2RenderTexture[4];	//將圖片改為狼人	(P2RenderTexture[3]代表狼人)
				PlyDetail[1].spriteName = "clown";		//將下面的角色資料切換為烏賊的
				break;
			default:
				print ("charator choose overflow");	//如果超出預期Unity就顯示charator choose overflow
				break;
			}	
		break;
		case 3:		//P3的部分
			switch (num)
			{
			case 1:
				PerNum.renderer.material.mainTexture = P3RenderTexture[0];	//將圖片改為蝴蝶	(P3RenderTexture[0]代表蝴蝶)
				PlyDetail[2].spriteName = "butterfly";		//將下面的角色資料切換為蝴蝶的
				break;
			case 2:
				PerNum.renderer.material.mainTexture = P3RenderTexture[1];	//將圖片改為炎龍	(P3RenderTexture[1]代表炎龍)
				PlyDetail[2].spriteName = "dragon";		//將下面的角色資料切換為炎龍的
				break;
			case 3:
				PerNum.renderer.material.mainTexture = P3RenderTexture[2];	//將圖片改為烏龜	(P3RenderTexture[2]代表烏龜)
				PlyDetail[2].spriteName = "LFT";		//將下面的角色資料切換為烏龜的
				break;
			case 4:
				PerNum.renderer.material.mainTexture = P3RenderTexture[3];	//將圖片改為狼人	(P3RenderTexture[3]代表狼人)
				PlyDetail[2].spriteName = "wolf";		//將下面的角色資料切換為狼人的
				break;
			case 5:
				PerNum.renderer.material.mainTexture = P3RenderTexture[4];	//將圖片改為狼人	(P3RenderTexture[3]代表狼人)
				PlyDetail[2].spriteName = "clown";		//將下面的角色資料切換為烏賊的
				break;
			default:
				print ("charator choose overflow");	//如果超出預期Unity就顯示charator choose overflow
				break;
			}	
			break;
		case 4:		//P4的部分
			switch (num)
			{
			case 1:
				PerNum.renderer.material.mainTexture = P4RenderTexture[0];	//將圖片改為蝴蝶	(P4RenderTexture[0]代表蝴蝶)
				PlyDetail[3].spriteName = "butterfly";		//將下面的角色資料切換為蝴蝶的
				break;
			case 2:
				PerNum.renderer.material.mainTexture = P4RenderTexture[1];	//將圖片改為炎龍	(P4RenderTexture[1]代表炎龍)
				PlyDetail[3].spriteName = "dragon";		//將下面的角色資料切換為炎龍的
				break;
			case 3:
				PerNum.renderer.material.mainTexture = P4RenderTexture[2];	//將圖片改為烏龜	(P4RenderTexture[2]代表烏龜)
				PlyDetail[3].spriteName = "LFT";		//將下面的角色資料切換為烏龜的
				break;
			case 4:
				PerNum.renderer.material.mainTexture = P4RenderTexture[3];	//將圖片改為狼人	(P4RenderTexture[3]代表狼人)
				PlyDetail[3].spriteName = "wolf";		//將下面的角色資料切換為狼人的
				break;
			case 5:
				PerNum.renderer.material.mainTexture = P4RenderTexture[4];	//將圖片改為狼人	(P4RenderTexture[3]代表狼人)
				PlyDetail[3].spriteName = "clown";		//將下面的角色資料切換為烏賊的
				break;
			default:
				print ("charator choose overflow");	//如果超出預期Unity就顯示charator choose overflow
				break;
			}	
			break;
		}

	}

	void select(){				//選角介面介面 
		switch(_main.select){
		case 2:		//如果人數為2人時 
			P1ImgShow.gameObject.SetActive(true);	//P1選角介面開啟
			P2ImgShow.gameObject.SetActive(true);	//P2選角介面開啟
			P3ImgShow.gameObject.SetActive(false);	//P3選角介面關閉
			P4ImgShow.gameObject.SetActive(false);	//P4選角介面關閉
			black3.gameObject.SetActive(true);		//P3的的柵欄開啟
			black4.gameObject.SetActive(true);		//P4的的柵欄開啟

			break;
		case 3:		//如果人數為3人時 
			P1ImgShow.gameObject.SetActive(true);	//P1選角介面開啟
			P2ImgShow.gameObject.SetActive(true);	//P2選角介面開啟
			P3ImgShow.gameObject.SetActive(true);	//P3選角介面開啟
			P4ImgShow.gameObject.SetActive(false);	//P4選角介面關閉
			black3.gameObject.SetActive(false);		//P4的的柵欄關閉
			black4.gameObject.SetActive(true);		//P4的的柵欄開啟
			break;
		case 4:		//如果人數為4人時 
			P1ImgShow.gameObject.SetActive(true);	//P1選角介面開啟
			P2ImgShow.gameObject.SetActive(true);	//P2選角介面開啟
			P3ImgShow.gameObject.SetActive(true);	//P3選角介面開啟
			P4ImgShow.gameObject.SetActive(true);	//P4選角介面開啟
			black3.gameObject.SetActive(false);		//P3的的柵欄關閉
			black4.gameObject.SetActive(false);		//P4的的柵欄關閉
			break;
		}
	}

	IEnumerator StickCon1(float Direct)		//P1搖桿延遲 避免搖桿運作過快 沒辦法選到玩家所選的	
	{
		if(Direct >= 0.5f)	//按下右鍵時
		{
			OnePreCha++;	//角色選擇數增一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick1Check = true;		//可以再按按鍵控制
		}else if(Direct <= -0.5f)	//按下左鍵時
		{
			OnePreCha--;	//角色選擇數減一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick1Check = true;		//可以再按按鍵控制
		}

	}

	IEnumerator StickCon2(float Direct)		//P2搖桿延遲 避免搖桿運作過快 沒辦法選到玩家所選的	
	{
		if(Direct >= 0.5f)	//按下右鍵時
		{
			TwoPreCha++;	//角色選擇數增一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick2Check = true;		//可以再按按鍵控制
		}else if(Direct <= -0.5f)	//按下左鍵時
		{
			TwoPreCha--;	//角色選擇數減一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick2Check = true;		//可以再按按鍵控制
		}
		
	}
	
	IEnumerator StickCon3(float Direct)		//P3搖桿延遲 避免搖桿運作過快 沒辦法選到玩家所選的				
	{
		if(Direct >= 0.5f)	//按下右鍵時
		{
			ThreePreCha++;	//角色選擇數增一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick3Check = true;		//可以再按按鍵控制
		}else if(Direct <= -0.5f)	//按下左鍵時
		{
			ThreePreCha--;	//角色選擇數減一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick3Check = true;		//可以再按按鍵控制
		}
		
	}
	
	IEnumerator StickCon4(float Direct)		//P4搖桿延遲 避免搖桿運作過快 沒辦法選到玩家所選的				
	{
		if(Direct >= 0.5f)	//按下右鍵時
		{
			FourPreCha++;	//角色選擇數增一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick4Check = true;		//可以再按按鍵控制
		}else if(Direct <= -0.5f)	//按下左鍵時
		{
			FourPreCha--;	//角色選擇數減一
			yield return new WaitForSeconds(AddTime);	//延遲AddTime秒後
			Stick4Check = true;		//可以再按按鍵控制
		}
		
	}


}
