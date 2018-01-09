using UnityEngine;
using System.Collections;

public class LoadCharactor : MonoBehaviour {
	public int PlyCount;				//玩家數量
	public int[] PlayNum;				//玩家選擇的腳色代碼

	public GameObject[] PlyCharactor;	//要產生的腳色
	public Transform[] PlyPoint;		//角色產生的位置

	public GameObject Shadow;			//影子的物件
	
	public GameObject ShowUp;			//出場的特效
	public float ShowPlayerTime;		//出場特效後角色出場的時間 通常特效出來一點點時間角色才會出現 並不會特效和角色同時出現

	private GameManager _Gamemanager;	//取得Gamemanager腳本 
	private float DelayTime;			//取得出場攝影機繞場的時間 這段時間內玩家都不能移動
	void Awake() {
		PlyCount = PlayerPrefs.GetInt("PlyCount");		//取得玩家數
		PlayNum = new int[4];							//宣告出四個空位 等等用來存放玩家所選擇的角色
	}
	void Start () {
		PlayNum[0] = PlayerPrefs.GetInt("P1");			//從PlayerPrefs.GetInt("P1")中取得P1所選擇的角色 用數字代替 1為蝴蝶 2炎龍 3烏龜 4狼人 5烏賊
		PlayNum[1] = PlayerPrefs.GetInt("P2");			//從PlayerPrefs.GetInt("P2")中取得P2所選擇的角色
		PlayNum[2] = PlayerPrefs.GetInt("P3");			//從PlayerPrefs.GetInt("P3")中取得P3所選擇的角色
		PlayNum[3] = PlayerPrefs.GetInt("P4");			//從PlayerPrefs.GetInt("P4")中取得P4所選擇的角色

		StartCoroutine(PlayerShowUp());					//腳色產生
		_Gamemanager = GetComponent<GameManager>();		//從_Gamemanager物件中取得GameManager腳本
		DelayTime = _Gamemanager.StartDelay;			//從Gamemanager裡面取得繞場的時間

	}
	

	void Update () {

	}
	IEnumerator StartDelayTime(float num)		//攝影機繞場完後 DelayTime要歸0 不然角色出場後定隔時間會加長
	{
		
		yield return new WaitForSeconds(num);
		DelayTime = 0;
	}

	IEnumerator PlayerShowUp()         //執行角色產生的程式          
	{
		for(int i = 0;i<PlyCount;i++)
		{
			Instantiate(ShowUp,PlyPoint[i].transform.position,PlyPoint[i].transform.rotation);	//出場特效產生
		}
		yield return new WaitForSeconds(ShowPlayerTime);		//延遲時間後角色再產生
		for(int i = 0;i<PlyCount;i++)					//角色產生
		{
			CharactorIns(i,PlayNum[i]);			
			Debug.Log("Point" + i);

		}
	}

	public void CharactorIns(int point,int ChaNum)		//角色產生的程式碼	
	{

		switch (ChaNum)		//依PlayNum來出場所選的角色
		{
		case 1:				//如果為1 蝴蝶
			GameObject ply1;
			ply1 = Instantiate(PlyCharactor[0],PlyPoint[point].transform.position,PlyPoint[point].transform.rotation) as GameObject;	//產生角色物件
			point +=1;		//下行開始point代表為玩家編號 如point == 1 玩家即為1p
			ply1.GetComponent<ButterFlyMove>().PlyHor = "Horizontal" + point.ToString();		//取得玩家的左右按鈕名稱 參考Edit->Project setting->input
			ply1.GetComponent<ButterFlyMove>().PlyVer = "Vertical" + point.ToString();			//取得玩家的上下按鈕名稱 參考Edit->Project setting->input
			ply1.GetComponent<ButterFlyMove>().PlyNum = point;									//傳送玩家代碼 如p1
			ply1.GetComponent<ButterFlyMove>().JumpBtn = "Hor"+point+"Jump";					//取得玩家的小跳躍按鈕名稱 參考Edit->Project setting->input
			ply1.GetComponent<ButterFlyMove>().JumpSkillBtn = "Hor"+point+"JumpSkill";			//取得玩家的大跳躍按鈕名稱 參考Edit->Project setting->input
			ply1.GetComponent<ButterFlyMove>().SkillBtn = "Hor"+point+"Skill";					//取得玩家的突進按鈕名稱 參考Edit->Project setting->input
			GameObject shdow1;																	//影子物件的參數
			shdow1 = Instantiate(Shadow,ply1.transform.position,transform.rotation) as GameObject;	//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow1.GetComponent<Shadow>().Player = ply1.gameObject;								//設定影子要追蹤的對象為ply1 請參考shadow腳本
			break;
		case 2:				//炎龍
			GameObject ply2;
			ply2 = Instantiate(PlyCharactor[1],PlyPoint[point].transform.position,PlyPoint[point].transform.rotation) as GameObject;	//產生角色物件
			point +=1;		//下行開始point代表為玩家編號 如point == 1 玩家即為1p
			ply2.GetComponent<DragonMove>().PlyHor = "Horizontal" + point.ToString();			//取得玩家的左右按鈕名稱 參考Edit->Project setting->input
			ply2.GetComponent<DragonMove>().PlyVer = "Vertical" + point.ToString();				//取得玩家的上下按鈕名稱 參考Edit->Project setting->input
			ply2.GetComponent<DragonMove>().PlyNum = point;										//傳送玩家代碼 如p1
			ply2.GetComponent<DragonMove>().JumpBtn = "Hor"+point+"Jump";						//取得玩家的小跳躍按鈕名稱 參考Edit->Project setting->input
			ply2.GetComponent<DragonMove>().JumpSkillBtn = "Hor"+point+"JumpSkill";				//取得玩家的大跳躍按鈕名稱 參考Edit->Project setting->input
			ply2.GetComponent<DragonMove>().SkillBtn = "Hor"+point+"Skill";						//取得玩家的突進按鈕名稱 參考Edit->Project setting->input
			GameObject shdow2;																	//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow2 = Instantiate(Shadow,ply2.transform.position,ply2.transform.rotation) as GameObject;//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow2.GetComponent<Shadow>().Player = ply2.gameObject;								//設定影子要追蹤的對象為ply1 請參考shadow腳本
			break;
		case 3:				//小咖龜
			GameObject ply3;
			ply3 = Instantiate(PlyCharactor[2],PlyPoint[point].transform.position,PlyPoint[point].transform.rotation) as GameObject;	//產生角色物件
			point +=1;		//下行開始point代表為玩家編號 如point == 1 玩家即為1p
			ply3.GetComponent<TurtleMove>().PlyHor = "Horizontal" + point.ToString();		//取得玩家的左右按鈕名稱 參考Edit->Project setting->input
			ply3.GetComponent<TurtleMove>().PlyVer = "Vertical" + point.ToString();			//取得玩家的上下按鈕名稱 參考Edit->Project setting->input
			ply3.GetComponent<TurtleMove>().PlyNum = point;									//傳送玩家代碼 如p1
			ply3.GetComponent<TurtleMove>().JumpBtn = "Hor"+point+"Jump";					//取得玩家的小跳躍按鈕名稱 參考Edit->Project setting->input
			ply3.GetComponent<TurtleMove>().JumpSkillBtn = "Hor"+point+"JumpSkill";			//取得玩家的大跳躍按鈕名稱 參考Edit->Project setting->input
			ply3.GetComponent<TurtleMove>().SkillBtn = "Hor"+point+"Skill";					//取得玩家的突進按鈕名稱 參考Edit->Project setting->input
			GameObject shdow3;																//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow3 = Instantiate(Shadow,ply3.transform.position,ply3.transform.rotation) as GameObject;//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow3.GetComponent<Shadow>().Player = ply3.gameObject;							//設定影子要追蹤的對象為ply1 請參考shadow腳本

			break;
		case 4:				//冰郎
			GameObject ply4;
			ply4 = Instantiate(PlyCharactor[3],PlyPoint[point].transform.position,PlyPoint[point].transform.rotation) as GameObject;	//產生角色物件
			point +=1;		//下行開始point代表為玩家編號 如point == 1 玩家即為1p
			ply4.GetComponent<WolveMove>().PlyHor = "Horizontal" + point.ToString();		//取得玩家的左右按鈕名稱 參考Edit->Project setting->input
			ply4.GetComponent<WolveMove>().PlyVer = "Vertical" + point.ToString();			//取得玩家的上下按鈕名稱 參考Edit->Project setting->input
			ply4.GetComponent<WolveMove>().PlyNum = point;									//傳送玩家代碼 如p1
			ply4.GetComponent<WolveMove>().JumpBtn = "Hor"+point+"Jump";					//取得玩家的小跳躍按鈕名稱 參考Edit->Project setting->input
			ply4.GetComponent<WolveMove>().JumpSkillBtn = "Hor"+point+"JumpSkill";			//取得玩家的大跳躍按鈕名稱 參考Edit->Project setting->input
			ply4.GetComponent<WolveMove>().SkillBtn = "Hor"+point+"Skill";					//取得玩家的突進按鈕名稱 參考Edit->Project setting->input
			GameObject shdow4;																//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow4 = Instantiate(Shadow,ply4.transform.position,ply4.transform.rotation) as GameObject;//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow4.GetComponent<Shadow>().Player = ply4.gameObject;							//設定影子要追蹤的對象為ply1 請參考shadow腳本

			break;
		case 5:				//烏賊
			GameObject ply5;
			ply5 = Instantiate(PlyCharactor[4],PlyPoint[point].transform.position,PlyPoint[point].transform.rotation) as GameObject;
			point +=1;		//下行開始point代表為玩家編號 如point == 1 玩家即為1p
			ply5.GetComponent<SquidMove>().PlyHor = "Horizontal" + point.ToString();		//取得玩家的左右按鈕名稱 參考Edit->Project setting->input
			ply5.GetComponent<SquidMove>().PlyVer = "Vertical" + point.ToString();			//取得玩家的上下按鈕名稱 參考Edit->Project setting->input
			ply5.GetComponent<SquidMove>().PlyNum = point;									//傳送玩家代碼 如p1
			ply5.GetComponent<SquidMove>().JumpBtn = "Hor"+point+"Jump";					//取得玩家的小跳躍按鈕名稱 參考Edit->Project setting->input
			ply5.GetComponent<SquidMove>().JumpSkillBtn = "Hor"+point+"JumpSkill";			//取得玩家的大跳躍按鈕名稱 參考Edit->Project setting->input
			ply5.GetComponent<SquidMove>().SkillBtn = "Hor"+point+"Skill";					//取得玩家的突進按鈕名稱 參考Edit->Project setting->input
			GameObject shdow5;																//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow5 = Instantiate(Shadow,ply5.transform.position,ply5.transform.rotation) as GameObject;//產生影子物件並把他編號為shdow1 以方便後面的控制
			shdow5.GetComponent<Shadow>().Player = ply5.gameObject							;//設定影子要追蹤的對象為ply1 請參考shadow腳本

			break;
		default:
			print ("charator choose overflow");
			break;
		}

	}
}
