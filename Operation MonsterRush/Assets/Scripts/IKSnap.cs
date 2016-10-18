using UnityEngine;
using System.Collections;

public class IKSnap : MonoBehaviour 
{
	public bool useIK = true;

	public bool leftHandIK = false;
	public bool rightHandIK = false;
	public bool leftFootIK = false;
	public bool rightFootIK = false;

	public bool isClimbing = false;

	public Vector3 leftHandPos;
	public Vector3 rightHandPos;
	public Vector3 leftFootPos;
	public Vector3 rightFootPos;

	public Vector3 leftHandOffset;
	public Vector3 rightHandOffset;
	public Vector3 leftFootOffset;
	public Vector3 rightFootOffset;

	public Vector3 leftHandOriginalPos;
	public Vector3 rightHandOriginalPos;

	public Quaternion leftHandRotation;
	public Quaternion rightHandRotation;
	public Quaternion leftFootRotation;
	public Quaternion rightFootRotation;

	public Quaternion leftHandRotationOffset;
	public Quaternion rightHandRotationOffset;
	public Quaternion leftFootRotationOffset;
	public Quaternion rightFootRotationOffset;

	private Animator myAnim;
	public float normalizedTime;

	public float leftHandWeight = 1.0f;
	public float rightHandWeight = 1.0f;

	void Awake()
	{
		myAnim = GetComponent <Animator> ();
	}

	void FixedUpdate()
	{
		RaycastHit leftHandtHitInfo;
		RaycastHit rightHandHitInfo;

		RaycastHit leftFootHitInfo;
		RaycastHit rightFootHitInfo;

		// Left Hand IK Check
		if (Physics.Raycast (transform.position + transform.TransformDirection (new Vector3 (0.0f, 1.4f, 0.4f)), transform.TransformDirection (new Vector3 (-0.3f, -1.0f, 0.0f)), out leftHandtHitInfo, 0.65f))
		{
			Vector3 lookAt = Vector3.Cross (-leftHandtHitInfo.normal, transform.right);
			lookAt = lookAt.y < 0 ? -lookAt : lookAt;

			leftHandIK = true;
			leftHandPos = leftHandtHitInfo.point - transform.TransformDirection(leftHandOffset);
			//leftHandPos.x = leftHandOriginalPos.x - leftHandOffset.x;
			//leftHandPos.z = leftFootPos.z - leftHandOffset.z;
			//leftHandPos.z = transform.TransformDirection (leftFootPos).x;
			leftHandRotation = Quaternion.FromToRotation(Vector3.forward, leftHandtHitInfo.normal);
			//leftHandRotation = Quaternion.LookRotation(leftHandtHitInfo.point + lookAt, leftHandtHitInfo.normal);
		}
		else
		{
			leftHandIK = false;
		}

		// Right Hand IK Check
		if (Physics.Raycast (transform.position + transform.TransformDirection (new Vector3 (0.0f, 1.4f, 0.4f)), transform.TransformDirection (new Vector3 (0.3f, -1.0f, 0.0f)), out rightHandHitInfo, 0.65f))
		{
			Vector3 lookAt = Vector3.Cross (-rightHandHitInfo.normal, transform.right);
			lookAt = lookAt.y < 0 ? -lookAt : lookAt;

			rightHandIK = true;
			rightHandPos = rightHandHitInfo.point - transform.TransformDirection(rightHandOffset);
			//rightHandPos.x = rightHandOriginalPos.x - rightHandOffset.x;
			//rightHandPos.z = rightFootPos.z - rightHandOffset.z;
			rightHandRotation = Quaternion.FromToRotation(Vector3.forward, rightHandHitInfo.normal);
			//rightHandRotation = Quaternion.LookRotation(rightHandHitInfo.point + lookAt, rightHandHitInfo.normal);
		}
		else
		{
			rightHandIK = false;
		}

		if(leftHandIK == true && rightHandIK == true)
		{
			// Left Foot IK Check
			if (Physics.Raycast (transform.position + transform.TransformDirection (new Vector3 (-0.35f, 0.5f, 0.0f)), transform.forward, out leftFootHitInfo, 1.0f))
			{
				leftFootIK = true;
				leftFootPos = leftFootHitInfo.point - leftFootOffset;
				leftFootRotation = (Quaternion.FromToRotation (Vector3.up, leftFootHitInfo.normal)) * leftFootRotationOffset;
			}
			else
			{
				leftFootIK = false;
			}

			// Right Foot IK Check
			if (Physics.Raycast (transform.position + transform.TransformDirection (new Vector3 (0.35f, 0.5f, 0.0f)), transform.forward, out rightFootHitInfo, 1.0f))
			{
				rightFootIK = true;
				rightFootPos = rightFootHitInfo.point - rightFootOffset;
				rightFootRotation = (Quaternion.FromToRotation (Vector3.up, rightFootHitInfo.normal)) * rightFootRotationOffset;
			}
			else
			{
				rightFootIK = false;
			}
		}
		else
		{
			leftFootIK = false;
			rightFootIK = false;
		}

		normalizedTime = myAnim.GetCurrentAnimatorStateInfo (0).normalizedTime % 1;

		if(myAnim)
		{
			if(myAnim.GetCurrentAnimatorStateInfo(0).IsName ("Grounded Movement"))
			{
				float velocity = 0.0f;

				if (normalizedTime < 0.25f)
				{
					leftHandWeight = Mathf.SmoothDamp (leftHandWeight, 0.0f, ref velocity, 2 * Time.deltaTime);
					rightHandWeight = Mathf.SmoothDamp (rightHandWeight, 1.0f, ref velocity, 8 * Time.deltaTime);
				}
				else if (normalizedTime > 0.25f && normalizedTime < 0.5f)
				{
					leftHandWeight = Mathf.SmoothDamp (leftHandWeight, 1.0f, ref velocity, 8 * Time.deltaTime);
					rightHandWeight = Mathf.SmoothDamp (rightHandWeight, 0.0f, ref velocity, 2 * Time.deltaTime);
				}
			}
			else
			{
				leftHandWeight = 1.0f;
				rightHandWeight = 1.0f;
			}
		}
	}

	void Update()
	{
		// Left Hand IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (0.0f, 1.4f, 0.4f)), transform.TransformDirection (new Vector3 (-0.3f, -1.0f, 0.0f)), Color.blue);
		// Right Hand IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (0.0f, 1.4f, 0.4f)), transform.TransformDirection (new Vector3 (0.3f, -1.0f, 0.0f)), Color.blue);
		// Left Foot IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (-0.35f, 0.5f, 0.0f)), transform.forward, Color.red);
		// Right Foot IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (0.35f, 0.5f, 0.0f)), transform.forward, Color.red);
	}
		
	void OnAnimatorIK ()
	{
		if (useIK)
		{
			//leftHandOriginalPos = myAnim.GetIKPosition (AvatarIKGoal.LeftHand);
			//rightHandOriginalPos = myAnim.GetIKPosition (AvatarIKGoal.RightHand);

			if(leftHandIK)
			{
				isClimbing = true;
				myAnim.SetIKPositionWeight (AvatarIKGoal.LeftHand,1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.LeftHand, leftHandPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.LeftHand, leftHandRotation);
			}
			else
				isClimbing = false;

			if(rightHandIK)
			{
				isClimbing = true;
				myAnim.SetIKPositionWeight (AvatarIKGoal.RightHand , 1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.RightHand, rightHandPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.RightHand, rightHandRotation);
			}
			else
				isClimbing = false;

			if(leftFootIK)
			{
				isClimbing = true;
				myAnim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, 1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.LeftFoot, leftFootPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.LeftFoot, leftFootRotation);
			}
			else
				isClimbing = false;

			if(rightFootIK)
			{
				isClimbing = true;
				myAnim.SetIKPositionWeight (AvatarIKGoal.RightFoot, 1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.RightFoot, rightFootPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.RightFoot, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.RightFoot, rightFootRotation);
			}
			else 
				isClimbing = false;
		}
	}
}
