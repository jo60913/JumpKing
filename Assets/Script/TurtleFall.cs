using UnityEngine;
using System.Collections;

public class TurtleFall : MonoBehaviour {
	
	public float FallTime;			//落下的秒數
	private float _FallTime;		//記錄落下的秒數
	public float TurtleStoneBack;	//石像過多少秒後回去(如果出現bug無法自動返回的話)
	public Transform TurtlePoint;	//落下的點
	private string StoneSit;		//石像目前的狀態
	private float FallCount;		//避免沒打到東西 所以要拉回的時間

	public float Shake;				//攝影機晃動的大小
	private float SetShake;			//記錄攝影機晃動的大小
	private bool ShakeSwitch = false;	//設定攝影機是否為搖晃
	public GameObject Cam;			//要晃動的攝影機物件
	private Vector3 CamPos;			//設定攝影機要搖晃的位置
	public GameObject SmokeEffect;	//石像降落到地板後的煙霧物件
	private bool SmokeEffectCheck;	//確定是否要產生煙霧物件
	private float SmokeEffectTime;	//煙霧維持的時間
	private float _SmokeEffectTime;	//記錄煙霧維持的時間
	public GameObject TrutleShadow;	//石像地板的影子
	private float ShadowRange;		//石像地板的影子顏色參數 會用這個參數來影響影子的alpha值
	public GameObject Ground;		//地板物件 會用這個物件來計算石像的高度差 進而影響影子的alpha
	void Start () {
		transform.position = TurtlePoint.transform.position;	//記錄石像的位置
		_FallTime = FallTime;		//記錄落下的秒數
		SetShake = Shake;			//記錄攝影機晃動的大小
		CamPos = Cam.transform.position;	//記錄攝影機晃動的位置
		SmokeEffectTime = 1f;		//設定煙霧持續的秒數
		_SmokeEffectTime = SmokeEffectTime;	//記錄煙霧持續的秒數
	}
	

	void Update () {
		FallTime-=Time.deltaTime;		//倒數石像落下的秒數
		FallCount += Time.deltaTime;	//增加石像拉回的秒數
		if(FallTime <=0)	//如果落下的秒倒數結束的話
		{
			transform.position = TurtlePoint.transform.position;	//將石像拉到掉落的位置
			rigidbody.useGravity = true;	//開啟重力 讓時像自己掉落
			StoneSit = "fall";	//將石像狀態設定為fall 表示現在在落下
			FallTime = _FallTime;	//記錄FallTime參數 方便下次絡下的秒數倒數
			FallCount = 0;		//讓石像拉回的秒數歸0
		}

		if(StoneSit == "fall" && FallCount >=8)			//如果狀態為落下 而且拉回的秒數超過8秒(表示石像都一直沒回去落下的位置)
		{
			transform.position = TurtlePoint.transform.position;	//將石像拉回落下的位置
			StoneSit = "back";	//將狀態改為back 表示現在已回到落下的位置
		}

		if(ShakeSwitch == true)	//如果ShakeSwitch為true的話 (表示現在要開始攝影機搖晃)
		{
			Cam.transform.position = new Vector3 (Random.Range(CamPos.x,CamPos.x+(Shake * 2)) -Shake,Random.Range(CamPos.y,CamPos.y+( Shake * 1)) -Shake,CamPos.z);
			//上面程式碼是用來搖晃攝影機的程式碼 隨機取亂數來控制攝影機的xyz軸
			Shake = Shake/1.05f;	//Shake參數除以1.05f 表示Shake參數會越來越小 模擬攝影機晃動後搖晃會越來越小
			if(Shake < 0.05f)		//一但晃動小於0.05的程度的話就停止 否則會無法停止
			{
				Shake = 0;			//Shake搖晃為0 停止搖晃
				ShakeSwitch = false;	//關閉搖晃
			}
		}
		if (SmokeEffectCheck == false) //如果SmokeEffectCheck為true的話(表示煙霧開始出現)
		{
			SmokeEffectTime -=Time.deltaTime;	//倒數煙霧出現的時間
			if(SmokeEffectTime <=0)		//如果煙霧出現的時間結束的話
			{
				SmokeEffectTime = _SmokeEffectTime;	//記錄煙霧持續的時間
				SmokeEffectCheck = true;	//關閉煙霧的出現
			}
		}
		TrutleShadow.renderer.material.color = new Color(0,0,0,(1 - ShadowRange));	//這邊是石像影子的alpha值改變
		ShadowRange = Vector3.Distance(Ground.transform.position,transform.position)/36;	//石像影子的參數計算 利用石像與地面的高度差來成為參數
	}
	void OnCollisionEnter(Collision other) 		//如果石像撞到任何東西的話
	{
		rigidbody.useGravity = false;	//關閉重力 讓她停止掉落
		StoneSit = "back";				//將狀態設定為back 表示準備掉落
		StartCoroutine(control(TurtleStoneBack));	//降落後的延遲 撞到東西後要延遲多久後才拉回空中
		FallTime = _FallTime;			//記錄掉落的時間
		Shake = SetShake;		//設定攝影機晃動參數
		ShakeSwitch = true;		//關閉攝影機晃動
		if (SmokeEffectCheck == true) //烏龜落地後煙霧產生 限制只產生一次
		{
			Instantiate (SmokeEffect,TrutleShadow.transform.position+new Vector3(0,0.5f,0), new Quaternion (0, 90, 0, 0));	//產生煙霧
			SmokeEffectCheck = false;	//關閉煙霧的產生
		}

	}
	IEnumerator control(float Waitime)		//石像延遲的程式碼
	{

		yield return new WaitForSeconds(Waitime);	//經過Waitime秒後
		transform.position = TurtlePoint.transform.position;	//將石像拉回掉落的位置

	}
}
