using UnityEngine;
using System.Collections;

public class Buff_Effect : MonoBehaviour {

	public GameObject Effect;		//Buff的粒子特效物件
	public GameObject _render;		//Buff的物件
	public float DelayTime;			//Buff的粒子特效的消失和Buff物件出現的空檔時間
	public float Disappear;			//消失的時間
	void Start () {
		Effect.SetActive (false);		//先關閉特效
		StartCoroutine(ShowBuff());		//Buff的特效和物件切換
	}
	

	void Update () {
		Disappear -=Time.deltaTime;	//消失的時間倒數 (物件刪除的部分寫在GameManager)
		if(Disappear <= 2)			//如果消失的時間如果少於2秒 就開始控制特效
		{
			Effect.SetActive(true);	//把特效開啟
			_render.SetActive(false);	//把特效物件關閉不顯示
		}
	}

	IEnumerator ShowBuff()   //特效與物件的切換 Buff產生的時候會先出現特效 之後才會出現物件 Buff被吃到或消失的時候 物件會先消失 再來是特效                 
	{
		_render.renderer.enabled = false;	//物件的特效關閉 就是消失的意思
		Effect.SetActive (true);			//開啟特效
		yield return new WaitForSeconds(DelayTime);	//等待時間 
		_render.renderer.enabled = true;	//物件特效開啟 就是出現
		Effect.SetActive (false);			//特效消失
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") //如果被玩家吃到
		{
			_render.renderer.enabled = false;	//物件的渲染關閉不顯示
			GetComponent<SphereCollider>().enabled = false;	//碰撞也關閉避免重複吃到
			Effect.SetActive(true);		//特效開啟
			Destroy(gameObject,2);		//兩秒後刪除物件
		}
	}
}
