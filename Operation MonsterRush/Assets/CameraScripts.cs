using UnityEngine;
using System.Collections;

public class CameraScripts : MonoBehaviour 
{
	/*public float distanceAway;
	public float distanceUp;
	public float smooth;
	private Transform follow;
	private Vector3 targetPosition;

	void Start()
	{
		follow = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void LateUpdate()
	{
		targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

		transform.LookAt(follow);
	}

	public Transform target;
	public float lookSmooth = 0.01f;
	public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
	public float xTilt = 10.0f;

	Vector3 destination = Vector3.zero;
	CharacterControllerScripts charController;
	float rotateVel = 0.0f;

	void Start()
	{
		SetCameraTarget(target);
	}

	void SetCameraTarget(Transform t)
	{
		target = t;
		if(target != null)
		{
			if(target.GetComponent<CharacterControllerScripts>())
			{
				charController = target.GetComponent<CharacterControllerScripts>();
			}
			else
				Debug.LogError("The camer's target needs a character controller.");
		}
		else
			Debug.LogError("Your camera needs a target.");
	}

	void LateUpdate()
	{
		//Moving
		MoveToTarget();
		//Rotating
		LookAtTarget();
	}

	void MoveToTarget()
	{
		destination = charController.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget()
	{
		float eulerAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerAngle, 0.0f);
	}
	*/

	[SerializeField]
	private Transform target;

	[SerializeField]
	private Vector3 offsetPosition;

	[SerializeField]
	private Space offsetPositionSpace = Space.Self;

	[SerializeField]
	private bool lookAt = true;

	private void Update()
	{
		Refresh();
	}

	public void Refresh()
	{
		if(target == null)
		{
			Debug.LogWarning("Missing target ref !", this);

			return;
		}

		// compute position
		if(offsetPositionSpace == Space.Self)
		{
			transform.position = target.TransformPoint(offsetPosition);
		}
		else
		{
			transform.position = target.position + offsetPosition;
		}

		// compute rotation
		if(lookAt)
		{
			transform.LookAt(target);
		}
		else
		{
			transform.rotation = target.rotation;
		}
	}
}

