using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public float time;	//輸入要刪除物件的時間
	void Start () {
	
	}
	

	void Update () {
		Destroy(gameObject,time);	//幾秒後要刪除物件
	}
}
