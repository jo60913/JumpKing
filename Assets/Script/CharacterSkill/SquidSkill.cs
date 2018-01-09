using UnityEngine;
using System.Collections;

public class SquidSkill : MonoBehaviour {
	
	public GameObject player;	//選告一個player變數 等等用來放入跑進這個物件的玩家
	public float DestroyTime;	//存在的時間

	private float Size;			//當前碰撞區的大小
	public float SizeMax;		//最終碰撞區的大小
	private BoxCollider collider;	//碰撞區的變數
	void Start () {
		collider = gameObject.GetComponent<BoxCollider>();	//取得碰撞區的元件
	}

	void Update () {
		if(player != null)		//如果player變數有人
		{
			Vector3 ply;		//選告一個vector3變數
			ply = player.transform.position;	//ply變數為角色的位置座標
			if(ply.x > this.transform.position.x)	//如果角色的位置座標在這個物件的右邊的話
			{
				player.transform.position -=Time.deltaTime *new Vector3(1,0,0);		//讓角色的位置慢慢的向左邊移動
			}

			if(ply.x < this.transform.position.x)	//如果角色的位置座標在這個物件的左邊的話
			{
				player.transform.position +=Time.deltaTime *new Vector3(1,0,0);		//讓角色的位置慢慢的向右邊移動
			}

			if(ply.z > this.transform.position.z)	//如果角色的位置座標在這個物件的上面的話
			{
				player.transform.position -=Time.deltaTime *new Vector3(0,0,1);		//讓角色的位置慢慢的向下移動
			}
			
			if(ply.z < this.transform.position.z)	//如果角色的位置座標在這個物件的下面的話
			{
				player.transform.position +=Time.deltaTime *new Vector3(0,0,1);		//讓角色的位置慢慢的向上邊移動
			}
		}

		Size +=Time.deltaTime * SizeMax/DestroyTime;	//增加這個物件的碰撞區大小的方程式
		if(Size >=SizeMax)		//如果碰撞區的大小等於設定的最大值的話
		{
			Size = SizeMax;		//就讓碰撞區大小等於最大值
		}
		collider.size =new Vector3(Size,3,Size);	//控制碰撞區的大小
		Destroy(gameObject,DestroyTime);			//消除物件 在DestroyTime秒後
	}


	void OnTriggerStay(Collider other) 			//待在這個物件裡面的話
	{
		if(other.gameObject.tag == "Player")	//如果物件的tag為Player的話(就是角色)
		{
			player = other.gameObject;			//則player這個變數就會放進跑進該碰撞區裡面的物件
		}
	}

	void OnTriggerExit(Collider other) 			//離開這個物件的話
	{
		if(other.gameObject.tag == "Player")	//如果物件的tag為Player的話(就是角色)
		{
			player = null;						//就讓player清空
		}
	}
}
