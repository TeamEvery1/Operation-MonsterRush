using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterControllerScripts : MonoBehaviour 
{
	[System.Serializable]
	public class MoveSettings
	{
		public float forwardVel = 12.0f;
		public float rotateVel = 100.0f;
		public float jumpVel = 25.0f;
		public float distToGrounded = 0.1f;
		public LayerMask ground;
	}

	[System.Serializable]
	public class PhysSettings
	{
		public float downAccel = 0.75f;
	}

	[System.Serializable]
	public class InputSettings
	{
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
	}

	public MoveSettings moveSetting = new MoveSettings();
	public PhysSettings physSetting = new PhysSettings();
	public InputSettings inputSetting = new InputSettings();

	public VirtualJoyStickScripts moveJoyStick;
	private bool canJump;
	private bool onGround;

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	Animator myAnim;
	float forwardInput, turnInput, jumpInput;
	private float turnRate, forwardRate;
	private float extraRotationSpeed;
	private float jumpHeight;

	public Quaternion TargetRotation
	{
		get{ return targetRotation;}
	}

	bool Grounded()
	{
		return Physics.Raycast(transform.position, Vector3.down, moveSetting.distToGrounded, moveSetting.ground);
	}

	void Start()
	{
		targetRotation = transform.rotation;

		if(GetComponent<Rigidbody>())
			rBody = GetComponent<Rigidbody>();
		else
			Debug.LogError("The Character needs a rigidbody.");

		if(GetComponent<Animator>())
			myAnim = GetComponent <Animator>();
		else
			Debug.LogError("The character needs an animator.");

		forwardInput = turnInput = jumpInput = 0;
	}

	void GetInput()
	{
		forwardInput = moveJoyStick.InputDirection.z;
		turnInput = moveJoyStick.InputDirection.x;
		jumpInput = Input.GetAxisRaw(inputSetting.JUMP_AXIS);
	}

	void Update()
	{
		forwardRate = velocity.z * 2;
		turnRate = Mathf.Atan2 (velocity.x, velocity.z);

		jumpHeight = Mathf.Abs(this.transform.position.y - velocity.y);

		myAnim.SetFloat ("forwardRatio", velocity.z , 0.2f, Time.deltaTime);
		myAnim.SetFloat ("turnRatio", turnRate, 0.2f, Time.deltaTime);
		myAnim.SetBool ("onGround", onGround);
		myAnim.SetFloat ("jumpHeight", jumpHeight , 0.2f, Time.deltaTime);

		GetInput();
		Turn();
	}

	void FixedUpdate()
	{
		Run();
		Jump();

		rBody.velocity = transform.TransformDirection (velocity); 
	}

	void Run()
	{
		if(Mathf.Abs(forwardInput) > inputSetting.inputDelay)
		{
			//Move
			velocity.z = moveSetting.forwardVel * forwardInput;
		}
		else
			velocity.z = 0;

		
	}
	void Turn()
	{
		if(Mathf.Abs(turnInput) > inputSetting.inputDelay)
		{
			
			targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);

		}
		transform.rotation = targetRotation;

		//transform.Rotate( 0, turnRate * extraRotationSpeed * Time.deltaTime, 0);
	}

	void Jump()
	{
		//Jump
		if(Grounded() && onGround)
		{
			if(canJump)
			{
				canJump = false;
				onGround = false;
				velocity.y = moveSetting.jumpVel;
			}
			else
				onGround = true;
		}
		else if(!Grounded())
		{
			//Gravity Down

			velocity.y -= physSetting.downAccel;
		}
		else
		{
			onGround = true;
			velocity.y = 0;
		}
	}

	public void JumpButtonClicked()
	{
		canJump = true;
	}
}
