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

		public float health;
		public int damage;
		private float maxHealth;
		//private string name;
		public bool isMovable;
		public bool isMoving;

		public Transform mainCam;

		private Vector3 cameraForward;
		private Vector3 originalPosition;
		public Vector3 direction;

		public Player.Combat playerCombatScript;

		public VirtualJoyStickScripts moveJoyStick;
		public KeyCode attackKey, primaryWeaponKey, secondaryWeaponKey, tertiaryWeaponKey; 

		public Animator myAnim;
		Rigidbody myRB;
		CapsuleCollider myCollider;

		private Player.Movement playerMovement;
		private Rigidbody controller;
		private bool jump;

		void Awake()
		{
			Player.Character playerCharacter = new Player.Character ( movementSpeed, health, damage, name);
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

			originalPosition = this.transform.position;
			maxHealth = health;

			controller.maxAngularVelocity = terminalRotationSpeed;
			controller.drag = drag;
		}

		private void Update()
		{
			//! Animation
			if(health <= 0)
			{
				this.transform.position = originalPosition;
					
				myAnim.Play ("LOSE00");
				StartCoroutine ("reviveTimer", 1.5f);
			}


			myAnim.SetInteger("attackCounter", playerCombatScript.gauntlet.AttackCounter);
			myAnim.SetBool ("onCombat", playerCombatScript.onCombat);
			myAnim.SetBool ("onCatch", playerCombatScript.onCatch);
		
			myAnim.applyRootMotion = false;
		    direction = Vector3.zero;

			direction.x = Input.GetAxis("Horizontal");
			direction.z = Input.GetAxis("Vertical");

			if(mainCam != null)
			{
				cameraForward = Vector3.Scale (mainCam.forward, new Vector3(1, 0, 1)).normalized;
				direction = direction.z * cameraForward + direction.x * mainCam.right ;
			}
			else
			{
				direction = direction.z * Vector3.forward + direction.x * Vector3.right;
			}

			if(direction.magnitude > 1)
			{
				direction.Normalize();
				isMoving = true;
			}
			else
				isMoving = false;

			//Phone Input
			if(moveJoyStick.InputDirection != Vector3.zero)
			{
				if(mainCam != null)
				{
					cameraForward = Vector3.Scale (mainCam.forward, new Vector3(1, 0, 1)).normalized;
					direction = moveJoyStick.InputDirection.z * cameraForward + moveJoyStick.InputDirection.x * mainCam.right ;
				}
				else
				{
					direction = moveJoyStick.InputDirection.z * Vector3.forward + moveJoyStick.InputDirection.x * Vector3.right;
				}
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

		IEnumerator reviveTimer(float t)
		{
			yield return new WaitForSeconds (t);
			health = maxHealth;

			myAnim.Play ("Grounded Movement");

		}
	}
}	
