using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera[] cameras;
	public Camera currentCamera;
	
	
	void Start () {
		cameras[0].enabled = true;
		for(int i=1; i < cameras.Length; i++)
		{
			cameras[i].enabled = false;
		}
	}
	
	void Update () {
		
		
	}
}
