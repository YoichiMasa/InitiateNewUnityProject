using UnityEngine;
using System.Collections;

public class TemporaryCharControl : MonoBehaviour {

	public float moveSpeed = 15f;

	private Vector3 input;
	public Rigidbody movable;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		movable.AddForce(input*moveSpeed);
	}
}
