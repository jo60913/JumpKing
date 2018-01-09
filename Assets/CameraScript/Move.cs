using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed;
	public int UpDown;
	public int LeftRight;
	void Start () {
	
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.F10))
		{
			UpDown = 1;
		}else if(Input.GetKeyUp(KeyCode.F10)){
			UpDown = 0;
		}

		if(Input.GetKeyDown(KeyCode.F12))
		{
			UpDown = -1;
		}else if(Input.GetKeyUp(KeyCode.F12)){
			UpDown = 0;
		}

		if(Input.GetKeyDown(KeyCode.F9))
		{
			LeftRight = -1;
		}else if(Input.GetKeyUp(KeyCode.F9)){
			LeftRight = 0;
		}
		
		if(Input.GetKeyDown(KeyCode.F11))
		{
			LeftRight = 1;
		}else if(Input.GetKeyUp(KeyCode.F11)){
			LeftRight = 0;
		}


		transform.Translate(LeftRight*speed * Time.deltaTime,0,UpDown*speed * Time.deltaTime);
	}
}
