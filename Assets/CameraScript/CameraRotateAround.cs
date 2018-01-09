using UnityEngine;
using System.Collections;

public class CameraRotateAround : MonoBehaviour {

	public GameObject Target;
	public float speed;
	public Vector3 Angle;
	void Start () {
	
	}

	void Update () {
		if (Input.GetKey(KeyCode.F3))
		{
			transform.RotateAround(Target.transform.position,Angle, speed * Time.deltaTime);
		}

		if (Input.GetKeyDown (KeyCode.F4))
		{
			Time.timeScale = 0.1f;
		}else if (Input.GetKeyUp (KeyCode.F4))
		{
			Time.timeScale = 1;
		}
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit))
			{
				Target = hit.collider.gameObject;
				//transform.position = Target.transform.position + new Vector3(0,0.5f,0.5f);
			}
		}
	}
}
