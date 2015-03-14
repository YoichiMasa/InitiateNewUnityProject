using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera[] cameras;
	public Camera currentCamera;
	
	
	void Start () {
		cameras[0].camera.active = true;
		for(int i=1; i < cameras.Length; i++)
		{
			cameras[i].camera.active = false;
		}
	}
	
	void Update () {
		
		
	}
}
