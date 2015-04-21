using UnityEngine;
using System.Collections;

public class CameraSwitchTrigger : MonoBehaviour {

	public Camera newCamera;
	public Camera previousCamera;
	
	public CameraController switcher;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			newCamera.camera.enabled = true;
			newCamera.GetComponent<AudioListener>().enabled = true;
			previousCamera.camera.enabled = false;
			previousCamera.GetComponent<AudioListener>().enabled = false;
			switcher.currentCamera = newCamera;
		}
	}
}
