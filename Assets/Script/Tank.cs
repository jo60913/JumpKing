using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {
	
	public string TankHor;		//取得坦克左右按鍵
	public float RotSpeed;		//坦克的旋轉速度
	public string TankJumpBtn;	//坦克發射按鍵	
	public GameObject cube;		//發射的飛彈
	private int CubeNum;		//飛彈的名稱 (會用名稱來判斷是哪個玩家發射的攻擊)
	public Transform LunachPos;	//發射的初始位置
	void Start () {
		TankHor = "";	//先將坦克的左右按鍵清除 (避免Unity出現紅字)
	}
	
	
	void Update () {
		
		if(TankHor !="")	//如果左右按鍵不為空值的話 (表示有人控制坦克)
		{
			transform.Rotate(0,Input.GetAxis(TankHor)* Time.deltaTime * RotSpeed,0);	//坦克的旋轉方程式
		}

		if(TankJumpBtn !="")	//攻擊鍵不為空值得話	(表示有人控制坦克)
		{
			if(Input.GetButtonDown(TankJumpBtn))	//如果按下攻擊鍵的話
			{
				GameObject Cube;	//另外再宣告一個Cube 方面後面從新命名
				Cube = (GameObject)Instantiate(cube,LunachPos.transform.position,LunachPos.transform.rotation);		//產生子彈
				Cube.gameObject.name = CubeNum.ToString();	//把子彈的名稱改成CubeNum (CubeNum會是玩家的編號 玩家的編號會做為攻擊判斷的依據)
			}
		}
	}

	public void GetPlyHor(string Control)	//取得玩家左右控制的按鍵的程式碼
	{

		Debug.Log("Tank receive message"+Control);	//讓Unity列印出玩家左右控制的按鍵
		TankHor = Control;	//將取得的玩家左右控制按鍵
	}

	public void GetSkillBtn(string mes)	//取得玩家突進的按鍵的程式碼
	{
		Debug.Log("Tank"+mes);	//讓Unity列印出玩家突進的按鍵的程式碼
		TankJumpBtn = mes;	//將玩家突進的按鍵當成坦克的攻擊按鍵)
	}

	public void GetPlyNum(int num)	//取得玩家編號的程式碼 
	{
		Debug.Log("Tank's driver num"+num);	//讓Unity列印出玩家的編號
		CubeNum = num;		//記錄子彈的名稱 方便後面取代成子彈的名稱
	}


}
