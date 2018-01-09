using UnityEngine;
using System.Collections;

public class ButterFlyMove : Role {
	private int characternum = 1;

	void Awake(){
		base.CharacterNum = characternum;
		Debug.Log ("butterfly : "+base.CharacterNum);
	}


}
	
