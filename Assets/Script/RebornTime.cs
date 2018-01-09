using UnityEngine;
using System.Collections;

public class RebornTime : MonoBehaviour {

	public GameObject plane;	//要產生倒數數字的物件
	public Texture space;		//全透明的貼圖
	public Texture zero;		//數字0的貼圖
	public Texture one;			//數字1的貼圖
	public Texture two;			//數字2的貼圖
	public Texture three;		//數字3的貼圖
	public GameObject Cam;		//攝影機的物件
	private bool CountCheck;	//判斷當前是否到數	
	public float CountTime;		//倒數的參數	因為倒數只能使用float的型態 會沒辦法坐後面swtich函式的判斷 所以後面會轉成int的型態
	private int CountTimeInt;	//倒數的參數 float轉換成int後
	void Start () {
		Cam = GameObject.Find("Main Camera02").gameObject;	//找到目前的攝影機
		CountCheck = false;		//還沒開始倒數
		StartCoroutine(CountDown());	//等待倒數 角色死亡後到倒數會有2.5秒的時間
		plane.renderer.material.mainTexture = space;	//還沒開始倒數 所已顯示為透明的貼圖
	}
	

	void Update () {

		if(CountCheck == true)	//倒數開始
		{
			transform.LookAt(Cam.transform.position);	//物件永遠面相攝影機 這樣不管放在什麼角度都會看得很清楚
			CountTime -=Time.deltaTime;		//倒數時間的程式碼
			if(CountTime <=0)	//倒數歸0後 刪除這個物件
			{
				Destroy(gameObject);
			}
			CountTimeInt = (int)CountTime;	//把倒數的數字轉換成從float轉成int 這樣下列switch才可以判斷
			switch (CountTimeInt)	//判斷當前該顯示的數字
			{
			case 0:
				plane.renderer.material.mainTexture = zero;	//判斷數字為0 所以顯是為0的貼圖
				break;
			case 1:
				plane.renderer.material.mainTexture = one;	//判斷數字為1 所以顯是為1的貼圖
				break;
			case 2:
				plane.renderer.material.mainTexture = two;	//判斷數字為2 所以顯是為2的貼圖
				break;
			case 3:
				plane.renderer.material.mainTexture = three;	//判斷數字為3 所以顯是為3的貼圖
				break;
			default:
				break;
			}
		}

	}
	IEnumerator CountDown()		//等待倒數
	{
		yield return new WaitForSeconds(2.5f);
		CountCheck = true;
	}
}
