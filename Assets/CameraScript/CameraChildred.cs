using UnityEngine;
using System.Collections;

public class CameraChildred : MonoBehaviour {


	void Start () {
	
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.F10))
		{
			transform.parent = null;
		}

	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			gameObject.transform.parent = other.gameObject.transform;
		}
	}
}
