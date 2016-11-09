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
		[Range (0f, 10f)][SerializeField] private float fallingMultiplier = 1f;
		[SerializeField] private float movementSpeedMultiplier = 1f; 
		[SerializeField] private float animationSpeedMultiplier = 1f;
		[Range (0f, 2f)][SerializeField] private float gravityMultiplier = 1f;
		[SerializeField] private float groundCheckDistance = -0.1f;

		public LayerMask ground;
		public LayerMask upperGround;
		//private LayerMask allGround;


		public bool onGround = false;
		public bool onHigherGround = false;
		[HideInInspector] public bool canJump;
		public bool isSwimming;

		public float objectVelocity = 1.0f;
		private float turnSpeed;
		private float turnRatio;
		private float forwardRatio;
		private float jumpTimer;
		public float climbRatio;
		//private float defGroundCheckDistance;
		private float runCycleLegOffset = 0.0f;

		public VirtualJoyStickScripts moveJoyStick;
		private IKSnap iKSnapScript;
		private Player.Controller playerControllerScript;

		[HideInInspector] public Animator myAnim;
		Rigidbody myRB;
		//CapsuleCollider myCollider;

		//Vector3 groundNormal;
		private Vector3 jumpMovement;
		private Vector3 v;

		private void Start()
		{
			//allGround = ~ (( 1 << ground.value) | ( 1 << upperGround.value));
			myAnim = GetComponent<Animator>();
			myRB = GetComponent<Rigidbody>();
			//myCollider = GetComponent<CapsuleCollider>();
			myRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

			iKSnapScript = GetComponent <IKSnap> ();
			playerControllerScript = GetComponent <Player.Controller>();

		}

		private void FixedUpdate()
		{
			Jump();

			if(moveJoyStick.canMove == false)
			{
				myAnim.Play("DamageDown");
			}

			/*if(canJump)
				myRB.velocity = transform.TransformDirection(v);*/
			
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

			//movement = Vector3.ProjectOnPlane (movement, groundNormal);

			turnRatio = Mathf.Atan2 (movement.x, movement.z);
			forwardRatio = movement.z;

			ApplyExtraRotation();

			/*if(onGround)
			{
				GroundedMovement(jump);
			}
			else
			{
				AirborneMovement();
			}*/

			UpdateAnimator(movement);
		}

		private void ApplyExtraRotation()
		{
			turnSpeed = Mathf.Lerp (stationaryTurnRate, movingTurnRate, forwardRatio);
			transform.Rotate( 0, turnRatio * turnSpeed * Time.deltaTime, 0);
		}

		/*private void GroundedMovement(bool jump)
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
		}*/

		private void UpdateAnimator(Vector3 movement)
		{
			myAnim.SetFloat("forwardRatio", forwardRatio, 0.1f, Time.deltaTime);
			myAnim.SetFloat("turnRatio", turnRatio, 0.1f, Time.deltaTime);
			myAnim.SetBool("onGround", onGround);
			myAnim.SetBool("isSwimming", isSwimming);
			myAnim.SetFloat("climbRatio", climbRatio, 0.1f, Time.deltaTime);

			float runCycle = Mathf.Repeat(myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
			float jumpPosition = (runCycle < 0.5f ? 1 : -1) * forwardRatio;

			if(!onGround)
			{
				jumpTimer += Time.deltaTime;	
				myAnim.SetFloat("jumpHeight", jumpTimer);
			}
			else
			{
				jumpTimer = 0.0f;
				myAnim.SetFloat("jumpPosition", jumpPosition);
			}

			if(onGround && movement.magnitude > 0)
			{
				myAnim.speed = animationSpeedMultiplier;
				SoundManagerScript.Instance.PlayLoopingSFX (AudioClipID.SFX_PLAYERWALK);
			}
			else
			{
				myAnim.speed = 1.0f;
				SoundManagerScript.Instance.StopLoopingSFX (AudioClipID.SFX_PLAYERWALK);
			}
		}

		public void OnAnimatorMove()
		{
			if(moveJoyStick.canMove)
			{
				if((Grounded() || UpperGrounded()) && Time.deltaTime > 0 && !isSwimming)
				{
					Vector3 moveForward = transform.forward * myAnim.GetFloat("motionZ") * objectVelocity * Time.deltaTime;
					v = ((myAnim.deltaPosition + moveForward) * movementSpeedMultiplier * 1.3f / Time.deltaTime);
					 
					myRB.velocity = v;
				}
				else if(isSwimming && Time.deltaTime > 0)
				{
					Vector3 moveForward = transform.forward * myAnim.GetFloat("motionZ") * Time.deltaTime;
					v = ((myAnim.deltaPosition + moveForward) * movementSpeedMultiplier * 1.3f / Time.deltaTime);

					myRB.velocity = v;
				}
			}
		}

		public bool Grounded()
		{
			return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, ground);
		}

		public bool UpperGrounded()
		{
			return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, upperGround);
		}

		void Jump()
		{
			if(UpperGrounded())
			{
				onHigherGround = true;
				onGround = true;
			}

			//Jump
			if((Grounded() || UpperGrounded()) && onGround && !isSwimming)
			{
				if(canJump)
				{
					canJump = false;
					onGround = false;
					onHigherGround = false;
					GetComponent <Player.Combat> ().targetLock = false;
					myRB.velocity = new Vector3 (0, jumpForce, 0);
					myRB.useGravity = false;
					SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERJUMP);
				}
				else
				{
					onGround = true;
				}
			}
			else if(!Grounded() && !onGround && !isSwimming && !iKSnapScript.isClimbing)
			{
				Vector3 extraGravityForce = new Vector3 (0, -jumpForce * fallingMultiplier * gravityMultiplier * 1.3f , 0);

				myRB.velocity = new Vector3 (playerControllerScript.direction.x * jumpForce / 1.5f, myRB.velocity.y , playerControllerScript.direction.z * jumpForce / 1.5f);

				myRB.AddForce (extraGravityForce);

				myRB.useGravity = false;
			}
			else if(!Grounded() && onGround && !isSwimming && !iKSnapScript.isClimbing)
			{
				//myRB.velocity = new Vector3 (playerControllerScript.direction.x * jumpForce / 1.5f, -fallingMultiplier * gravityMultiplier / 3.35f , playerControllerScript.direction.z * jumpForce / 1.5f);
				myRB.useGravity = true;
			}
			else if(iKSnapScript.isClimbing)
			{
				myAnim.Play("Climbing");
				myRB.velocity = new Vector3 (0, 0, 0);
				myRB.useGravity = false;
				if(iKSnapScript.isClimbing == true && iKSnapScript.isClimbingUp == false)
				{
					climbRatio = 0.5f;
				}
				else if(iKSnapScript.isClimbing == true && iKSnapScript.isClimbingUp == true)
				{
					climbRatio = 1.5f;
				}

				//Invoke ("ClimbingPosition", 0.5f);
			}
			else
			{
				onGround = true;
				iKSnapScript.useIK = true;
				//myAnim.Play("Grounded Movement");
				v.y = 0;
			}

			if(!Grounded() && onHigherGround)
			{
				myRB.velocity = new Vector3 (playerControllerScript.direction.x * jumpForce / 1.5f, -fallingMultiplier * gravityMultiplier / 1.3f, playerControllerScript.direction.z * jumpForce / 1.5f);
			}

			if(Grounded())
			{
				onHigherGround = false;
			}
				
		}

		/*void ClimbingPosition()
		{
			this.transform.position = new Vector3 (this.transform.position.x , this.transform.position.y - 0.01f, this.transform.position.z);
		}*/

		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("Water"))
			{
				isSwimming = true;
				this.transform.position = new Vector3 (this.transform.position.x , 6.825f, this.transform.position.z);
				onHigherGround = false;
				myRB.useGravity = false;
			}
		}

		void OnTriggerExit (Collider other)
		{
			if(other.CompareTag ("Water"))
			{
				isSwimming = false;
			}
		}

		void EndAnimation()
		{
			iKSnapScript.isClimbing = false;
			iKSnapScript.isClimbingUp = false;
			myAnim.Play("Grounded Movement");
		}

		/*void AnimationEnd()
		{
			moveJoyStick.canMove = false;
			myAnim.Play("Grounded Movement");
		}*/
	}
}
