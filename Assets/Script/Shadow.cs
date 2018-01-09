using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	public GameObject Player;		//影子要持續追蹤的對象
	void Start () {
	
	}
	
	
	void Update () {
		if(Player == null)			//如果Player為null時 (表示持續追蹤的對象死亡時，如果沒有加這行Unity會一直出紅字)
		{
			Destroy(this.gameObject);	//消除這個物件
		}
		if(Player != null)			//如果Player有存在時 
			transform.position = new Vector3(Player.transform.position.x,0.1f,Player.transform.position.z);	//把影子這個物件的Y軸0.1 X和Z軸則跟Player一樣 這樣就可以追蹤角色了

	}
}
