using UnityEngine;
using System.Collections;

public class VenusAnim : MonoBehaviour {


	private Animator anim;	//宣告一個動化控制器
	void Start () {
		anim = GetComponent<Animator>();	//取得物件上的動畫控制器
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")	//如果碰到物件tag為Player時
		{
			transform.LookAt(other.transform.position);		//將物件面對角色
			anim.SetBool("Catch",true);		//把動畫控制器中的Catch改為true (表示轉換動化為咬住)
		}
	}

}
