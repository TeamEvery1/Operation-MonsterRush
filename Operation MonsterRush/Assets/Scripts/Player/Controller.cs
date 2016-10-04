using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Player
{
	[RequireComponent (typeof(Animator))]
	[RequireComponent (typeof(Rigidbody))]
	[RequireComponent (typeof(CapsuleCollider))]
	public class Controller : MonoBehaviour
	{
		[SerializeField] private float movementSpeed = 20f;
		[SerializeField] private float drag = 0.5f;
		[SerializeField] private float terminalRotationSpeed = 25.0f;
		//private string name;
		public bool isMovable;
		public bool isMoving;
		private Transform mainCam;
		private Vector3 cameraForward;

		public Player.Combat playerCombatScript;

		public VirtualJoyStickScripts moveJoyStick;
		public KeyCode attackKey, primaryWeaponKey, secondaryWeaponKey, tertiaryWeaponKey; 

		Animator myAnim;
		Rigidbody myRB;
		CapsuleCollider myCollider;

		private Player.Movement playerMovement;
		private Rigidbody controller;
		private bool jump;

		void Awake()
		{
			Player.Character playerCharacter = new Player.Character ( movementSpeed, name );
			playerCombatScript = GetComponent <Player.Combat>(); 

			isMovable = true;

			myAnim = GetComponent<Animator>();
			myRB = GetComponent<Rigidbody>();
			myCollider = GetComponent<CapsuleCollider>();
			mainCam = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		}

		private void Start()
		{
			playerMovement = GetComponent <Player.Movement> ();
			controller = GetComponent<Rigidbody>();
			controller.maxAngularVelocity = terminalRotationSpeed;
			controller.drag = drag;
		}

		private void Update()
		{
			//! Animation
			myAnim.SetInteger("attackCounter", playerCombatScript.gauntlet.AttackCounter);
			myAnim.SetBool ("onCombat", playerCombatScript.onCombat);
			myAnim.SetBool ("onCatch", playerCombatScript.onCatch);
		
			//! perform attack
			if(playerCombatScript.isDelayed)
			{
				if(Input.GetKeyDown (attackKey))
				{
					playerCombatScript.Perform();
				}
			}

			//! switch weapon during runtime
			if(playerCombatScript.isDelayed)
			{
				if(Input.GetKeyDown (primaryWeaponKey))
				{
					playerCombatScript.SwitchWeapon(0);
				}
				if(Input.GetKeyDown (secondaryWeaponKey))
				{
					playerCombatScript.SwitchWeapon(1);
				}
				if(Input.GetKeyDown (tertiaryWeaponKey))
				{
					playerCombatScript.SwitchWeapon(2);
				}
			}

			Vector3 direction = Vector3.zero;

			direction.x = Input.GetAxis("Horizontal");
			direction.z = Input.GetAxis("Vertical");

			if(direction.magnitude > 1)
			{
				direction.Normalize();
				isMoving = true;
			}
			else
				isMoving = false;

			//Phone Input

			//cameraForward = Vector3.Scale (mainCam.forward, new Vector3(1, 0, 1).normalized);
			if(moveJoyStick.InputDirection != Vector3.zero)
			{
				direction = moveJoyStick.InputDirection;
			}

			if(isMovable)
			{
				playerMovement.Player_Movement(direction, jump);
			}

			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Restart();
			}

		}
		public void Restart()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}	
