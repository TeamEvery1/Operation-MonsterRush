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
	public bool isClimbingUp = false;

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

	[HideInInspector] public RaycastHit leftHandtHitInfo;
	RaycastHit rightHandHitInfo;
	RaycastHit leftFootHitInfo;
	RaycastHit rightFootHitInfo;

	private Player.Movement playerMovement;

	void Awake()
	{
		myAnim = GetComponent <Animator> ();
	}

	void Start()
	{
		playerMovement = this.GetComponent<Player.Movement>();
	}

	void FixedUpdate()
	{
		if(playerMovement.onGround == false)
		{
			// Left Hand IK Check
			if (Physics.Raycast (transform.position + transform.TransformDirection (new Vector3 (0.0f, 0.9f, 0.4f)), transform.TransformDirection (new Vector3 (-0.3f, -1.0f, 0.0f)), out leftHandtHitInfo, 0.3f)
				&& (leftHandtHitInfo.transform.gameObject.tag == "Jump"))
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
			if (Physics.Raycast (transform.position + transform.TransformDirection (new Vector3 (0.0f, 0.9f, 0.4f)), transform.TransformDirection (new Vector3 (0.3f, -1.0f, 0.0f)), out rightHandHitInfo, 0.3f) 
				&& (rightHandHitInfo.transform.gameObject.tag == "Jump"))
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
				isClimbing = true;

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
				isClimbing = false;
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
	}

	void Update()
	{
		// Left Hand IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (0.0f, 0.9f, 0.4f)), transform.TransformDirection (new Vector3 (-0.3f, -1.0f, 0.0f)), Color.blue);
		// Right Hand IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (0.0f, 0.9f, 0.4f)), transform.TransformDirection (new Vector3 (0.3f, -1.0f, 0.0f)), Color.blue);
		// Left Foot IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (-0.35f, 0.5f, 0.0f)), transform.forward, Color.red);
		// Right Foot IK Visual Ray
		Debug.DrawRay (transform.position + transform.TransformDirection (new Vector3 (0.35f, 0.5f, 0.0f)), transform.forward, Color.red);

		ClimbUp();
	}

	void OnAnimatorIK ()
	{
		if (useIK)
		{
			//leftHandOriginalPos = myAnim.GetIKPosition (AvatarIKGoal.LeftHand);
			//rightHandOriginalPos = myAnim.GetIKPosition (AvatarIKGoal.RightHand);

			if(leftHandIK)
			{
				myAnim.SetIKPositionWeight (AvatarIKGoal.LeftHand,1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.LeftHand, leftHandPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.LeftHand, leftHandRotation);
			}

			if(rightHandIK)
			{
				myAnim.SetIKPositionWeight (AvatarIKGoal.RightHand , 1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.RightHand, rightHandPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.RightHand, rightHandRotation);
			}

			if(leftFootIK)
			{
				myAnim.SetIKPositionWeight (AvatarIKGoal.LeftFoot, 1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.LeftFoot, leftFootPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.LeftFoot, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.LeftFoot, leftFootRotation);
			}

			if(rightFootIK)
			{
				myAnim.SetIKPositionWeight (AvatarIKGoal.RightFoot, 1.0f);
				myAnim.SetIKPosition (AvatarIKGoal.RightFoot, rightFootPos);

				myAnim.SetIKRotationWeight (AvatarIKGoal.RightFoot, 1.0f);
				myAnim.SetIKRotation (AvatarIKGoal.RightFoot, rightFootRotation);
			}
		}
	}

	void ClimbUp()
	{
		if(isClimbing == true)
		{
			if(playerMovement.forwardRatio > 0.9f)
			{
				useIK = false;
				isClimbingUp = true;
				//isClimbingLeft = false;
				//isClimbingRight = false;
				//this.transform.position = new Vector3(leftHandtHitInfo.point.x, leftHandtHitInfo.point.y,  leftHandtHitInfo.point.z);
			}
			/*if(playerMovement.turnRatio > 0.2f)
			{
				useIK = false;
				isClimbingLeft = true;
				isClimbingUp = false;
				isClimbingRight =false;
			}
			if(playerMovement.turnRatio < -0.2f)
			{
				useIK = false;
				isClimbingRight = true;
				isClimbingUp = false;
				isClimbingLeft = false;
			}*/
		}
	}
}
