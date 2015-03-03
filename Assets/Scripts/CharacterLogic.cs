using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour {

	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float directionDampTime = .25f;
	[SerializeField]
	private float directionSpeed = 3.0f;
	[SerializeField]
	private ThirdPersonCamera gamecam;
	[SerializeField]
	private float rotationDegreePerSecond = 120f;

	private float speed = 0.0f;
	private float direction = 0f;
	private float h = 0.0f;
	private float v = 0.0f;

	private AnimatorStateInfo stateInfo;

	private int m_LocomotionID = 0;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		if(animator.layerCount >= 2)
		{
			animator.SetLayerWeight(1, 2);
		}

		m_LocomotionID = Animator.StringToHash("Base Layer.Locomotion");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(animator)
		{
			stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");

			StickToWorldspace(this.transform, gamecam.transform, ref directionSpeed, ref speed);

			animator.SetFloat("Speed", speed);
			animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);
		}
	
	}

	void FixedUpdate()
	{
		//rotate character model if stick is tilted right or left, but only if character is moving in that direction
		if(IsInLocomotion() && ((direction >= 0 && h >= 0) || (direction< 0 && h < 0)))
		{
			Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * h < 0f ? -1f : 1f, 0), Mathf.Abs(h));
			Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
			this.transform.rotation = (this.transform.rotation * deltaRotation);
		}
	}

	public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
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
	}

	public bool IsInLocomotion()
	{
		if (stateInfo.nameHash == m_LocomotionID)
			return false;
		else
			return true;
	}
}
