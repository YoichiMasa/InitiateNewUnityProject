using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject[] cameras;
	public Camera currentCamera;
	
	
	void Start () {
		cameras[0].camera.enabled = true;
		for(int i=1; i < cameras.Length; i++)
		{
			cameras[i].camera.enabled = false;
			cameras[i].GetComponent<AudioListener>().enabled = false;
		}
	}
}
