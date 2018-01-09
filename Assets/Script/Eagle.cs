using UnityEngine;
using System.Collections;

public class Eagle : MonoBehaviour {
	public Transform CatchPoint;	//角色被抓住的位置
	public float Speed;				//移動速度
	void Start () {
	}
	
	void Update () {
		transform.Translate(0,Time.deltaTime*Speed*-1,0);	//移動的方程式
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")	//如果抓到玩家的話
		{
			other.transform.position = CatchPoint.transform.position;	//把玩家移動到抓住的點
			other.gameObject.rigidbody.useGravity = false;	//關閉玩家的地吸引力 避免玩家下墜
			other.transform.parent = this.transform;		//改成子物件 會隨著老鷹移動
		}
	}

}
