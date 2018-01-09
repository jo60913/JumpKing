using UnityEngine;
using System.Collections;

public class ChoseAnim : MonoBehaviour {

	public Animator anim;	//控制物件身上Animator元件
	public bool Check;		//選角是否被確認
	void Start () {
		anim = GetComponent<Animator>();	//取得物件身上Animator元件
		Check = false;	//角色沒有被確認
	}
	

	void Update () {
		if(Check == true)		//true為選完
		{
			anim.SetBool("Chose",true);	//Animator中的參數Chose為true 角色動作轉變為確認
		}else{					//false為沒選
			anim.SetBool("Chose",false);	//Animator中的參數Chose為false 角色動作轉變為待機
		}
	}
}
