using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {


	public GameObject CameraMove;
	public float Speed;
	public Transform[] waypoints;
	public Transform[] cameraorbit;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private Quaternion startPointQu;
	private Quaternion endPointQu;
	private float startTime;

	private int targetwaypoint;
	private Vector3 OrgPos;
	private Quaternion OrgQu;
	private CameraOrbit _CameraOrbit;
	private GameManager _GameManager;
	//public CameraControl _CameraControl;
	//public CameraOrbit _CameraOrbit;
	void  Start (){

		_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		OrgPos = transform.position;
		OrgQu = transform.rotation;
		_CameraOrbit = GetComponent<CameraOrbit>();
		startPoint = CameraMove.transform.position;
		startPointQu = CameraMove.transform.rotation;
		startTime = Time.time;
		
		if (waypoints.Length <= 0) {
			Debug.Log("No waypoints found");
			enabled = false;
		}
		
		targetwaypoint = 0;
		endPoint = waypoints[targetwaypoint].position;
		endPointQu = waypoints[targetwaypoint].rotation;
		StartCoroutine(GetPlayerCount());
	}
	
	void  Update (){
		/*if(Input.GetKeyDown(KeyCode.F3))
		{
			_CameraControl.enabled = true;
			_CameraOrbit.enabled = false;
		}*/


		float duration = (Vector3.Distance(startPoint, endPoint) / 10* 1/Speed);
		float i= (Time.time - startTime) / duration;
		
		CameraMove.transform.position = Vector3.Lerp(startPoint, endPoint, i);
		CameraMove.transform.rotation = Quaternion.Lerp(startPointQu, endPointQu, i);
		if (CameraMove.transform.position == endPoint) {
			startTime = Time.time;
			
			// increment and wrap the target waypoint index
			targetwaypoint++;
			targetwaypoint = targetwaypoint % waypoints.Length;
			startPoint = endPoint;
			startPointQu = endPointQu;
			endPoint = waypoints[targetwaypoint].position;
			endPointQu = waypoints[targetwaypoint].rotation;
			if(transform.position == waypoints[waypoints.Length-1].transform.position)
			{
				endPoint =OrgPos;
				endPointQu = OrgQu;
				targetwaypoint = 0;
			}
		}
		if(Vector3.Distance(transform.position,OrgPos) <= 1f && targetwaypoint == 0)
		{
			_CameraOrbit.enabled = false;
		}
		//Debug.Log(Vector3.Distance(transform.position,OrgPos));
	}
	IEnumerator GetPlayerCount()
	{
		Debug.Log("get"+_GameManager.PlayerCount);
		yield return new WaitForSeconds(1);
		waypoints = new Transform[_GameManager.PlayerCount+1];
		Debug.Log("wayleng"+waypoints.Length);
		for(int i = 0;i< waypoints.Length;i++)
		{
			waypoints[i] = cameraorbit[i];
		}
	}
}