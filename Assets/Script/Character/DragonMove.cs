using UnityEngine;
using System.Collections;

public class DragonMove : Role {
	private int characternum = 2;
	void Awake(){
		base.CharacterNum = characternum;
		Debug.Log ("dragon : "+base.CharacterNum);
	}
	
	
}
