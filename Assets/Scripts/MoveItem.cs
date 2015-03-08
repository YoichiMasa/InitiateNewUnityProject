using UnityEngine;
using System.Collections;

public class MoveItem : MonoBehaviour {

	public float moveSpeed;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			this.rigidbody2D.gravityScale = 0;
			Vector3 moveVector = new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime, 0);
			Vector3 desiredPosition = transform.position + moveVector;
			if (desiredPosition.x > -2.3f &&desiredPosition.x < 2.5f)
			{
				transform.position = desiredPosition;
			}
		}
		else
		{
			this.rigidbody2D.gravityScale = 1;
		}
	}
}
