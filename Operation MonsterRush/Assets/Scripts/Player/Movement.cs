using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace Player
{
	[RequireComponent (typeof (Animator))]
	[RequireComponent (typeof (Rigidbody))]
	[RequireComponent (typeof (CapsuleCollider))]

	public class Movement : MonoBehaviour
	{
		[SerializeField] private float movingTurnRate = 360f; 
		[SerializeField] private float stationaryTurnRate = 180f;
		[SerializeField] private float jumpForce = 10f;
		[SerializeField] private float movementSpeedMultiplier = 1f; 
		[SerializeField] private float animationSpeedMultiplier = 1f;
		[Range (0f, 2f)][SerializeField] private float gravityMultiplier = 1f;
		[SerializeField] private float groundCheckDistance = 10f;

		private bool onGround;

		private float turnSpeed;
		private float turnRatio;
		private float forwardRatio;
		private float defGroundCheckDistance;
		private float runCycleLegOffset;

		Animator myAnim;
		Rigidbody myRB;
		CapsuleCollider myCollider;

		Vector3 groundNormal;

		private void Start()
		{
			myAnim = GetComponent<Animator>();
			myRB = GetComponent<Rigidbody>();
			myCollider = GetComponent<CapsuleCollider>();
			myRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			defGroundCheckDistance = groundCheckDistance;
		}

		public void Player_Movement(Vector3 movement, bool jump)
		{
			//! Normalize to keep the value either 1 or 0 to keep speed constant no matter where the direction is the charater moving
			if(movement.magnitude > 1)
			{
				movement.Normalize();
			}

			//Changed to local space to make everything literally rotates and moves around character
			movement = transform.InverseTransformDirection (movement);

			movement = Vector3.ProjectOnPlane (movement, groundNormal);

			turnRatio = Mathf.Atan2 (movement.x, movement.z);
			forwardRatio = movement.z * 2;

			ApplyExtraRotation();

			if(onGround)
			{
				GroundedMovement(jump);
			}
			else
			{
				AirborneMovement();
			}

			UpdateAnimator(movement);
			CheckGroundStatus();
		}

		private void ApplyExtraRotation()
		{
			turnSpeed = Mathf.Lerp (stationaryTurnRate, movingTurnRate, forwardRatio);
			transform.Rotate( 0, turnRatio * turnSpeed * Time.deltaTime, 0);
		}

		private void GroundedMovement(bool jump)
		{
			if(jump && myAnim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				myRB.velocity = new Vector3 (myRB.velocity.x * 2f, jumpForce, myRB.velocity.z * 2f);
				onGround = false;
			//	myAnim.applyRootMotion = true;
				groundCheckDistance = 0.1f;
			}
		}

		private void AirborneMovement()
		{
			Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
			myRB.AddForce (extraGravityForce);
			groundCheckDistance = myRB.velocity.y < 0 ? defGroundCheckDistance : 0.01f;
		}

		private void UpdateAnimator(Vector3 movement)
		{
			myAnim.SetFloat("forwardRatio", forwardRatio, 0.2f, Time.deltaTime);
			myAnim.SetFloat("turnRatio", turnRatio, 0.2f, Time.deltaTime);
			myAnim.SetBool("onGround", onGround);

			float runCycle = Mathf.Repeat(myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
			float jumpPosition = (runCycle < 0.5f ? 1 : -1) * forwardRatio;

			if(!onGround)
			{
				myAnim.SetFloat("jumpHeight", myRB.velocity.y);
			}
			else
			{
				myAnim.SetFloat("jumpPosition", jumpPosition);
			}

			if(onGround && movement.magnitude > 0)
			{
				myAnim.speed = animationSpeedMultiplier;
			}
			else
			{
				myAnim.speed = 1.0f;
			}
		}

		public void OnAnimatorMove()
		{
			if(onGround && Time.deltaTime > 0)
			{
				Vector3 moveForward = transform.forward * myAnim.GetFloat("motionZ") * Time.deltaTime;
				Vector3 v = ((myAnim.deltaPosition + moveForward) * movementSpeedMultiplier / Time.deltaTime);

				v.y = myRB.velocity.y;
				myRB.velocity = v;
			}
		}

		private void CheckGroundStatus()
		{
			RaycastHit hitInfo;

			if(Physics.Raycast (transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
			{
				if(!hitInfo.collider.isTrigger)
				{
					groundNormal = hitInfo.normal;
					onGround = true;
					//myAnim.applyRootMotion = true;
				}
				else
				{
					groundNormal = Vector3.up;
					onGround = false;
					//myAnim.applyRootMotion = true;
				}
			}

		}
	}
}
