using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	[SerializeField]
	private Vector3 offset = new Vector3(0f, 1.5f, 0f);
	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	private Vector3 lookDir;
	private Vector3 targetPosition;
	private Vector3 velocityCamSmooth = Vector3.zero;



	// Use this for initialization
	void Start () {
		follow = GameObject.FindWithTag("Player").transform;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate ()
	{
		Vector3 characterOffset = follow.position + offset;

		//calculate direction from camera to player, kill y, and normalize to give a valid direction with unit magnitude
		lookDir = characterOffset - this.transform.position;
		lookDir.y = 0;
		lookDir.Normalize();

		targetPosition = characterOffset + follow.up * distanceUp - lookDir * distanceAway;

		CompensateForWalls(characterOffset, ref targetPosition);

		SmoothPosition (this.transform.position, targetPosition);

		transform.LookAt(follow);
	}

	private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		RaycastHit wallHit = new RaycastHit();
		if (Physics.Linecast(fromObject, toTarget, out wallHit))
		{
			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
		}
	}
}
