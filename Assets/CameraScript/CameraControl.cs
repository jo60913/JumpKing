using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject[] Cam;
	public int Num;
	public GameObject UI;
	void Start () {

		Num = 0;
		for(int i = 0;i < Cam.Length;i++)
		{
			Cam[i].gameObject.SetActive(false);
		}
		Cam[0].gameObject.SetActive(true);
		Debug.Log(Cam.Length);
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.F2))
		{
			Num++;
			if(Num <=Cam.Length -1)
			{

				Cam[Num-1].gameObject.SetActive(false);
				Debug.Log(Num);
				Cam[Num].gameObject.SetActive(true);
			}else if(Num >= Cam.Length)
			{
				Num = 0;
				Cam[Cam.Length-1].gameObject.SetActive(false);
				Cam[0].gameObject.SetActive(true);
			}
		}
		switch (Num)
		{
		case 0:
			UI.gameObject.SetActive(true);
			Screen.showCursor = true;
			break;
		case 1:
			UI.gameObject.SetActive(false);
			Screen.showCursor = true;
			break;
			
		case 2:
			UI.gameObject.SetActive(false);
			Screen.showCursor = false;
			break;
		default:
			
			break;
		}



	}
}

