using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public float speed = 1.0f;//how fast it shakes
	public float amount = 1.0f;// how much it shakes

	public bool shake = false;
	float shakeObj;
	public GameObject shakeIt;


	void Update()
	{
		if(shake)
		{
			shakeIt.transform.position = new Vector3 (Mathf.Sin ((Time.time * speed)*.01f), shakeIt.transform.position.y, shakeIt.transform.position.z);

			//ShakeIt ();
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player") 
		{
			shake = !shake;
		}
	}

//	IEnumerator ShakeIt()
//	{
//		shakeIt.transform.position = new Vector3 (Mathf.Sin (Time.time * speed), shakeIt.transform.position.y, shakeIt.transform.position.z);
//		//yield return new WaitForSeconds (5);
//	}

}
