using UnityEngine;
using System.Collections;

public class Forwarding : MonoBehaviour {

	public float speed;
	void Start () {
	
	}

	void Update () {
		transform.Translate(0,0,Time.deltaTime*speed);
		Destroy(gameObject,1);
	}
}
