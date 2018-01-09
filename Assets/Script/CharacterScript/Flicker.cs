using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	private UISprite photo;	//要閃爍的Ngui介面
	private float al;	//閃爍的參數
	void Start () {
		photo = GetComponent<UISprite>();	//取得介面
		al = -1;	//先將參數設定為-1
	}

	void Update () {
		photo.alpha +=Time.deltaTime * 1 *al;	//介面alpha改變的方程式
		if(photo.alpha >= 1)	//如果介面的alpha超過1的話
		{
			photo.alpha =1;		//讓介面的alpha等於1就好
			al *= -1;			//讓al乘以-1 等於讓他從原本持續加改為持續減
		}

		if(photo.alpha <=0)		//如果介面的alpha超過0的話
		{
			photo.alpha =0;		//讓介面的alpha等於0就好
			al *= -1;			//讓al乘以-1 等於讓他從原本持續加改為持續減
		}
	}
}
