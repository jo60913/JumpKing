using UnityEngine;
using System.Collections;

public class Lighting_flower02 : MonoBehaviour {

	public float LightSpeed;
	private bool check;
	void Start () {
		check = true;
		renderer.material.SetFloat("_EmissionLM",0) ;
	}

	void Update () {

		renderer.material.SetFloat("_EmissionLM",LightSpeed) ;

		if(check == true)
		{
			LightSpeed +=Time.deltaTime;
		}

		if(check == false)
		{
			LightSpeed -=Time.deltaTime;
		}
		if(LightSpeed >=2)
		{
			check = false;
		}

		if(LightSpeed <=0.2)
		{
			check = true;
		}
	}
}
