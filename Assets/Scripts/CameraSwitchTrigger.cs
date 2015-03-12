using UnityEngine;
using System.Collections;

public class CameraSwitchTrigger : MonoBehaviour {

	public Camera newCamera;
	public Camera previousCamera;
	
	public CameraController switcher;
	
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		newCamera.enabled = true;
		previousCamera.enabled = false;
		switcher.currentCamera = newCamera;
	}
}
