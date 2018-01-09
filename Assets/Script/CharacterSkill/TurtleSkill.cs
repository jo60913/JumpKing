using UnityEngine;
using System.Collections;

public class TurtleSkill : MonoBehaviour {
	private float Size;			//當前碰撞的大小
	public float SizeMax;		//碰撞的最大值
	public float ExtentTime;	//碰撞變大的時間
	void Start () {

	}
	

	void Update () {
		Size +=Time.deltaTime * SizeMax/ExtentTime;		//碰撞慢慢變大的方程式
		if(Size >=SizeMax)		//如果當前碰撞的大小超過設定的大小時
		{
			Size = SizeMax;		//就不要再繼續變大
			Destroy(gameObject);//刪除該物件
		}
		transform.localScale = new Vector3(Size,Size,Size);	//當前物件的碰撞大小

	}
}
