using UnityEngine;
using System.Collections;

public class ButterFlyAnim : MonoBehaviour {
	
	private Animator anim;				//角色身上的的控制器
	private ButterFlyMove _ButterFlyMove;	//取得身上Move移動的腳本
	public GameObject mat;				//角色3D模型的物件
	public GameObject Start_Eff;		//起跳的霧鍵
	public GameObject Mid_Eff;			//跳躍1的物件
	public GameObject Mid2_Eff;			//跳躍2的物件
	private float LastBlood;			//記錄最後的血量 這會用來判對角色是否扣血 因為主要記錄血量的腳本在Move移動腳本上 這個腳本另外記錄
	private float CurrentBlood;			//記錄當前血量
	public float DamageShowTime;		//角色損血時會亮紅色Rim light的時間
	private float _DamageShowTime;		//記錄角色損血時會亮紅色Rim light的時間
	public float JumpCD;				//小跳前的起跳動作的停滯時間		
	public float BigJumpCD;				//大跳前的起跳動作的停滯時間
	public GameObject Skill_Ready;		//角色起跳動作的特效物件
	private bool SkillReCheck;			//確認角色起跳特效物件是否產生 避免重複產生

	public bool CDtimeCheck;			//記錄現在角色是否為時間暫停的狀態
	private float CDTime;				//時間暫停的時間
	private bool SkillCheck;			//確認現在是否為腳色突進的狀態
	private float SkillTime;			//突進的時間
	public GameObject AngryEffect;		//憤怒值滿時的特效物件
	public Texture StatuDirty;			//石化1的貼圖
	public Texture Statu;				//石化2的貼圖
	public GameObject InvincibleObj;	//出場無敵的特效
	public GameObject SpeedDownObj;		//緩術的特效物件
	//public GameObject DirtyObj;
	void Start () {
		AngryEffect.SetActive(false);	//關閉憤怒的特效

		SkillReCheck = false;			//設定不要讓角色起跳特效開啟
		CDtimeCheck = false;			//設定不要讓角色時間暫停特效開啟
		CDTime = 3;						//設定時間暫停3秒鐘 (等等時間暫停特效會維持這個時間)
		SkillTime = 0.7f;				//設定突進時間 (等等突進特效會維持這個時間)
		anim = transform.Find("Roles_01_1008_TPossSkin").GetComponent<Animator> ();	//取得角色身上的動畫控制器
		_ButterFlyMove = transform.GetComponent<ButterFlyMove>();					//取得角色身上的Move移動腳本
		LastBlood = _ButterFlyMove.blood;	//記錄當前的血量
		CurrentBlood = _ButterFlyMove.blood;//記錄當前的血量
		_DamageShowTime = DamageShowTime;	//設定損血特效的時間
		Start_Eff.SetActive(false);			//關閉起跳特效
		Mid_Eff.SetActive(false);			//關閉跳躍1特效
		Mid2_Eff.SetActive(false);			//關閉跳躍2特效
		InvincibleObj.SetActive(false);		//關閉無敵特效
		StartCoroutine(InvincibleTime());	//執行無敵特效的程式碼
		SpeedDownObj.SetActive(false);		//關閉緩速特效
	}
	
	void Update () {
		CurrentBlood = _ButterFlyMove.blood;	//每次記錄當前角色的血量
		if(CurrentBlood < LastBlood)			//一但當前的血量(CurrentBlood)大於最後記錄的血量(LastBlood)那就表示腳色有損血 執行損血特效
		{
			DamageShowTime -=Time.deltaTime;	//持續減少損血特效時間
		}

		if(DamageShowTime < _DamageShowTime)	//一但DamageShowTime大於_DamageshowTime時(執行這一段程式碼時就表視角色有損寫了)
		{
			mat.renderer.material.shader = Shader.Find("Custom/RimLight");	//將角色shader切換為RimLight
			mat.renderer.material.SetColor("_RimColor",new Color(1,0,0,0));	//顏色設定為紅色 (new Color(R,G,B,alpha))
			
		}
		if(DamageShowTime < 0)		//如果損血特效時間為零時 (表示損血時間已經結束了)
		{
			mat.renderer.material.shader = Shader.Find("myUnlitAlpha");	//切換為自發光的shader
			DamageShowTime = _DamageShowTime;	//重新設定DamageShowTime
			LastBlood = CurrentBlood;			//再一次記錄角色的血量
		}

		if(_ButterFlyMove.AngryCheck == true)	//如果移動腳本內的憤怒值滿足時
		{
			AngryEffect.SetActive(true);		//開啟憤怒特效
		}else{
			AngryEffect.SetActive(false);		//關閉憤怒特效
		}



		if(_ButterFlyMove.plysit == "CDTime")	//如果角色狀態為時間暫停(CDTime)時
		{
			CDtimeCheck = true;					//CDtimeCheck就為開啟
		}

		if(CDtimeCheck == true)					//開啟CDtimeCheck時
		{
			mat.renderer.material.shader = Shader.Find("Character/StatueDiffuse"); //改變shader為石化shader
			mat.renderer.material.SetTexture("_GrungeTex",StatuDirty);				//填入石化1的貼圖
			mat.renderer.material.SetTexture("_BaseTex",Statu);						//填入石化2的貼圖
			mat.renderer.material.SetFloat("_Tighten",0.45f);						//_Tighten填寫0.45	建議可以從material內填寫數值觀看遍畫
			mat.renderer.material.SetFloat("_DiffuseAmount",0);						//_DiffuseAmount填寫0
			mat.renderer.material.SetFloat("_GrungeAmount",1.8f);					//_GrungeAmount填寫1.8f
			CDTime -=Time.deltaTime;												//持續減少時間特效時間
			if(CDTime < 0)		//如果時間特效為0時
			{
				CDTime = 1.5f;	//將時間暫停改為1.5秒 (照理說應該是要填入3但因為不知道為甚麼會執行兩次程式碼的樣子 所以時間要填維原本的一半)
				CDtimeCheck = false;	//關閉時間特效得確認
				mat.renderer.material.shader = Shader.Find("myUnlitAlpha");		//改為原本自發光的shader
			}
		}
		if(_ButterFlyMove.plysit == "Skill")	//角色狀態為衝刺時
		{
			SkillCheck = true;					//SkillCheck為開啟
		}

		if(SkillCheck == true)					//開啟SkillCheck時
		{
			mat.renderer.material.shader = Shader.Find("Custom/RimLight");		//修改shader為RimLight
			mat.renderer.material.SetColor("_RimColor",new Color(0.5f,1,0,0));	//顏色改為綠色
			anim.SetFloat("Speed",0);			//動畫控制器內的變數Speed為0，這一段程式碼是用來控制移動的動畫 需要開啟動化控制器才會看到的變數
			anim.SetBool("Sprint",true);		//動畫控制器內的變數Sprint為true，這一段程式碼是用來開啟突進的動畫 需要開啟動化控制器才會看到的變數
			SkillTime -=Time.deltaTime;			//減少突進的時間
			if(SkillTime <= 0)		//如果突進的時間為0的話
			{
				SkillTime = 0.7f;	//記錄突進時間為0.7
				SkillCheck = false;	//關閉突進的確認
				mat.renderer.material.shader = Shader.Find("myUnlitAlpha");	//修改shader為自發光
				anim.SetBool("Sprint",false);	//動畫控制器內的變數Sprint為false，這一段程式碼是用來關閉突進的動畫 需要開啟動化控制器才會看到的變數
			}
		}



		if(_ButterFlyMove.plysit == "Die")			//角色狀態為死亡後
		{
			mat.renderer.material.shader = Shader.Find("GreyScale");	//修改為灰階的shader
			anim.SetBool("Die",true);				//動畫控制器內的變數Die為true 開啟死亡動畫
			AngryEffect.SetActive(false);			//憤怒特效關閉
		}

		if(_ButterFlyMove.plysit == "jumpingpar")	//如果狀態為跳躍前的預備					
		{
			anim.SetFloat("Speed",0);				//動畫控制器內的變數Speed為0	表示關閉移動的動畫
			anim.SetBool("JumpPar",true);			//動畫控制器內的變數JumpPar為true	表示開啟跳躍前的動畫
			StartCoroutine(TranJumpPar(JumpCD));	//開啟TranJumpPar程式碼 這段程式碼是用來控制各個動畫的特效
		}


		if(_ButterFlyMove.plysit == "bigjumppar")	//如果狀態為大跳躍前的預備						
		{
			anim.SetFloat("Speed",0);		//動畫控制器內的變數Speed為0	表示關閉移動的動畫
			anim.SetBool("BigJumpPar",true);	//動畫控制器內的變數BigJumpPar為true	表示開啟跳躍前的動畫
			Start_Eff.SetActive(true);		//開啟起跳的特效
			if(SkillReCheck == true)		//確認SkillReCheck的開啟	這段程式碼是用來避免起跳特效的重複產生
			{
				Instantiate(Skill_Ready,transform.position,transform.rotation);		//產生起跳的特效
				SkillReCheck = false;		//關閉SkillReCheck
			}
			StartCoroutine(TranBigJumpPar(BigJumpCD));	//開啟TranBigJumpPar程式碼 這段程式碼是用來控制各個動畫的特效
		}		
		
		if(_ButterFlyMove.plysit == "ground")	//如果角色的狀態為ground時
		{
			if(Input.GetAxis(_ButterFlyMove.PlyHor) == 1 || Input.GetAxis(_ButterFlyMove.PlyHor) == -1)	//玩家按下做左右鍵時
			{
				anim.SetFloat("Speed",1);		//開啟移動的動畫
			}

			if(Input.GetAxis(_ButterFlyMove.PlyVer) == 1 || Input.GetAxis(_ButterFlyMove.PlyVer) == -1)	//玩家按下做上下鍵時
			{
				anim.SetFloat("Speed",1);		//開啟移動的動畫
			}
		}
		if(Input.GetAxis(_ButterFlyMove.PlyHor) == 0)			//如果玩家左右放開
		{
			if(Input.GetAxis(_ButterFlyMove.PlyVer) == 0)		//如果玩家上下放開
			{
				anim.SetFloat("Speed",0);		//關閉移動的動畫
			}
		}
				
	}
	IEnumerator InvincibleTime()    //無敵特效切換的程式碼                
	{
		InvincibleObj.SetActive(true);	//開啟無敵特效
		yield return new WaitForSeconds(_ButterFlyMove.InvincibleTime);		//延遲ButterFlyMove的InvincibleTime秒鐘後
		InvincibleObj.SetActive(false);	//關閉無敵特效
		
	}

	IEnumerator TranJumpPar(float num)     //開始跳躍動作時特效的切換               
	{

		yield return new WaitForSeconds(num);	//延遲num秒鐘後
		anim.SetBool("JumpPar",false);			//動畫控制器JumpPar參數為false	表示關閉起跳動作動畫
		anim.SetBool("Jump",true);				//動畫控制器Jump參數為true	表示開啟跳躍動作動畫
		yield return new WaitForSeconds(0.1f);	//延遲0.1秒後
		anim.SetBool("Jump",false);				//動畫控制器Jump為false		表示關閉跳躍動作動畫
	}


	IEnumerator TranBigJumpPar(float num)      //開啟大跳躍動作特效切換              
	{
		yield return new WaitForSeconds(num);	//延遲num秒後
		anim.SetBool("BigJumpPar",false);		//動畫控制器BigJumpPar參數為false 表示關閉起跳動作動畫
		Start_Eff.SetActive(false);				//起跳特效關閉
		anim.SetBool("BigJump",true);			//動畫控制器BigJump參數為true	表示大跳躍動畫開啟
		Mid_Eff.SetActive(true);				//跳躍1特效關閉
							
		yield return new WaitForSeconds(0.35f);	//經過0.35秒後
		anim.SetBool("BigJump",false);			//動畫控制器BigJump參數為false 表示關閉跳躍動作
		Mid_Eff.SetActive(false);				//關閉跳躍1特效
		Mid2_Eff.SetActive(true);				//開啟跳躍2特效
		yield return new WaitForSeconds(0.35f);	//經過0.35秒後
		Mid2_Eff.SetActive(false);				//跳躍2特效關閉


	}
	IEnumerator messtime(float num)     //吃到時間Buff時
	{
		SpeedDownObj.SetActive(true);	//開啟緩速的特效
		yield return new WaitForSeconds(num);	//延遲num秒鐘
		SpeedDownObj.SetActive(false);	//關閉緩速的特效
	}


	void OnTriggerEnter(Collider other)	
	{
		if(other.gameObject.name == "buff_speedup(Clone)")    //吃到時間Buff時
		{
			StartCoroutine(messtime(5));	//執行messtime程式碼
		}
		
		
	}
}
