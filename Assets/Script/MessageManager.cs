using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour {

	public GameObject Tank;	//要控制的坦克
	public int SendPlynum;	//記錄角色的編號
	void Start () {
	
	}
	

	void Update () {
	
	}

	public void SendPlyHor(string mes)
	{
		Debug.Log("GetPlyHor"+mes);
		Tank.SendMessage("GetPlyHor",mes);	//傳送mes給Tank物件中的GetPlyHor方程式
	}

	public void SendSkillBtn(string mes)
	{
		Debug.Log("GetSkillBtn"+mes);
		Tank.SendMessage("GetSkillBtn",mes);	//傳送mes給Tank物件中的GetSkillBtn方程式
	}

	public void SendPlyNum(int num)
	{
		Debug.Log("GetPlyNum"+num);
		SendPlynum = num;
		Tank.SendMessage("GetPlyNum",num);	//傳送mes給Tank物件中的GetPlyNum方程式
	}
}
