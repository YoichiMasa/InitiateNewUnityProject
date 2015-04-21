using UnityEngine;
using System.Collections;

public class TemporaryCharControl : MonoBehaviour {

	//elements from primary controller(experimental)
	[SerializeField]
	private float directionDampTime = .25f;
	[SerializeField]
	private float directionSpeed = 3.0f;
	[SerializeField]
	private CameraController gamecam;
	[SerializeField]
	private float rotationDegreePerSecond = 120f;
	[SerializeField]
	private float turnSmoothing = 200f;

	private float speed = 0.0f;
	private float direction = 0f;
	private float h = 0.0f;
	private float v = 0.0f;

	private bool grounded = true;
	private bool crouch = false;
	private bool sprint = false;


	//normal elements
	public float moveSpeed = 15f;
	public float jumpHeight = 1;

	private Vector3 input;
	public Rigidbody movable;
	private CapsuleCollider ratboyCollider;

	public Camera invent;

	// Use this for initialization
	void Start () 
	{
		ratboyCollider = transform.GetComponentInChildren<CapsuleCollider>();
		invent = GameObject.Find("Inventory").GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if(invent.enabled == false)
		{
			//jump
			if (Input.GetKeyDown (KeyCode.Space) && grounded) 
			{
			Jump();
			}
		
			//jump height control
			if (Input.GetKeyUp (KeyCode.Space)) 
			{
				if (movable.velocity.y > 0) 
				{
					movable.velocity = new Vector3 (movable.velocity.x, movable.velocity.y / 2, movable.velocity.z);
				}
			}

			//Check if grounded
			IsGrounded();

			if(Input.GetKeyDown (KeyCode.Z))
			{
				sprint = true;
				Sprint();
			}
		
			if(Input.GetKeyUp (KeyCode.Z))
			{
				sprint = false;
				Sprint();
			}

			if(Input.GetKeyDown (KeyCode.C))
			{
				crouch = true;
				Crouch();
			}

			if(Input.GetKeyUp (KeyCode.C))
			{
				crouch = false;
				Crouch();
			}
		



			//current
			//input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//movable.AddForce(input*moveSpeed);

			//experimental
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");

			Vector3 moveDirection = StickToWorldspace(this.transform, gamecam.currentCamera.transform, ref directionSpeed, ref speed);
			movable.AddForce (moveDirection * moveSpeed);

			if(h != 0 || v != 0)
			{
				if(sprint == true)
				{
					HUDController.instance.StaminaMovementHandling(HUDController.instance.sprint);
				}
				else
				{
					HUDController.instance.StaminaMovementHandling(HUDController.instance.move);
				}
			}


			//model rotation
			if(h != 0f || v != 0f)
			{
				Rotating(moveDirection);
			}
		}

			//if (rigidbody.velocity.magnitude < moveSpeed) 
		//{
		//	rigidbody.AddForce (moveDirection * moveSpeed);
		//}


	}

	/*void FixedUpdate()
	{
		//rotate character model if stick is tilted right or left, but only if character is moving in that direction
		if(((direction >= 0 && h >= 0) || (direction <= 0 && h < 0)))
		{
			Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (h < 0f ? -1f : 1f), 0), Mathf.Abs(h));
			Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
			this.transform.rotation = (this.transform.rotation * deltaRotation);
		}
	}*/

	//experimental methods
	public Vector3 StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{
		Vector3 rootDirection = root.forward;
		
		Vector3 stickDirection = new Vector3(h, 0, v);
		
		speedOut = stickDirection.sqrMagnitude;
		
		//get camera rotation
		Vector3 cameraDirection = camera.forward;
		cameraDirection.y = 0.0f; //kill y
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
		
		//convert stick input in worldspace coordinates
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
		
		float angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
		
		angleRootToMove /= 180f;
		
		directionOut = angleRootToMove * directionSpeed;

		return moveDirection;
	}

	//rotate character method
	public void Rotating (Vector3 moveDirection)
	{
		Vector3 targetDirection = moveDirection;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(movable.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		movable.MoveRotation(newRotation);
	}

	//jump
	void Jump()
	{
		movable.AddForce (0, jumpHeight, 0);
		HUDController.instance.RegularStaminaConsumption(HUDController.instance.jump);
	}

	//check if on the ground for jump
	public bool IsGrounded() 
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 1f)) {
			Debug.Log("Hitting: " + hit.transform.gameObject.tag);
			Debug.DrawRay(transform.position, Vector3.down);
			if(hit.transform.gameObject) 
			{
				grounded = true;
			}
			else
			{
				grounded = false;
			}
		}
		return grounded;
	}

	void Crouch()
	{
		//crouch
		if (crouch)
		{
			ratboyCollider.height = 0.1f;
			ratboyCollider.center = new Vector3(ratboyCollider.center.x, -0.03f, ratboyCollider.center.z);
			moveSpeed = 8f;
		}
		else
		{
			ratboyCollider.height = 0.17f;
			ratboyCollider.center = new Vector3(ratboyCollider.center.x, 0, ratboyCollider.center.z);
			moveSpeed = 12f;
		}
	}

	void Sprint()
	{
		//Sprint
		if (sprint)
		{
			moveSpeed = 18f;
		}
		else
		{
			moveSpeed = 12f;
		}
	}
}
