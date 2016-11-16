using UnityEngine;
using System.Collections;

namespace Cameras
{
	public class CaptureView : MonoBehaviour 
	{
		Cameras.AutoCam autoCamScript;
		Enemies.Collision[] enemyCollisionScripts;
		Enemies.Pathfinding[] enemyPathfindingScripts;
		public Player.Controller playerControllerScript;
		//GUIManagerScript guiManagerScript;
		CatchManager catchManagerScript;
		GameManager gameManagerScript;

		public GameObject pivotPoint;
		private GameObject target;
		public GameObject player;
		private GameObject[] obstacles;
		public GameObject camera01;

		private Vector3 pivotStartingPoint;
		private Quaternion pivotStartingRotation;

		private Vector3 playerStartingPoint;
		private Quaternion playerStartingRotation;

		private Vector3 targetStartingPoint;
		private Quaternion targetStartingRotation;

		private Vector3 catchingView;
		private Vector3 catchingRotation;
		private Vector3 lookAtCamera;

		private bool catchMode;
		public bool isCollided;
		private bool changed;
		private bool recordPos;
		private int enemyCounter = 0;

		void Awake()
		{
			if(player == null)
			{
				player = GameObject.Find ("Character");
			}
			else
			{
				Debug.Log ("Player not found");
			}
		}

		void Start()
		{	
			autoCamScript = GetComponent <Cameras.AutoCam>();

			enemyCollisionScripts = FindObjectsOfType <Enemies.Collision>();
			enemyPathfindingScripts = FindObjectsOfType <Enemies.Pathfinding>();

			playerControllerScript = player.GetComponent <Player.Controller>();
			//guiManagerScript = FindObjectOfType <GUIManagerScript>();
			catchManagerScript = FindObjectOfType <CatchManager>();
			gameManagerScript = FindObjectOfType <GameManager>();

			//pivotPoint = transform.FindChild ("Pivot").gameObject;

			pivotStartingPoint = new Vector3 (0.0f, 1.66f, -0.1f);
			pivotStartingRotation = Quaternion.Euler(new Vector3 (2.4f  ,0.0f, 0.0f));

			obstacles = GameObject.FindGameObjectsWithTag ("Obstacles");

			camera01 = pivotPoint.transform.parent.gameObject;
		}

		void FixedUpdate()
		{
			if(!isCollided)
			{
				NormalView();
			}
		}

		void Update()
		{
			foreach (Enemies.Collision enemyCollisionScript in enemyCollisionScripts)
			{
				if(enemyCollisionScript.isCollided)
				{
					SoundManagerScript.Instance.PlayLoopingSFX (AudioClipID.SFX_PLAYERCAPTUREDURATION);

					isCollided = true;
					autoCamScript.enabled = false;
					target = enemyCollisionScript.gameObject;

					playerControllerScript.isMovable = false;

					target.GetComponent <Animator> ().Play ("Idle");

					if (!recordPos)
					{
						playerStartingPoint = player.transform.position;
						playerStartingRotation = player.transform.rotation;

						targetStartingPoint = target.transform.position;
						targetStartingRotation = target.transform.rotation;

						recordPos = true;
					}

					if(!catchMode)
					{
						CatchingView();
					}

					if(catchManagerScript.successCapture||catchManagerScript.failCapture)
					{
						SoundManagerScript.Instance.StopLoopingSFX (AudioClipID.SFX_PLAYERCAPTUREDURATION);
						if (catchManagerScript.successCapture) 
						{
							SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERCAPTURESUCCESS);
							gameManagerScript.enemyCounter --;
							target.gameObject.SetActive (false);

						}

						enemyCollisionScript.isCollided = false;

						catchManagerScript.timeLimit = 40;
						catchManagerScript.timeLimitModifier = 0;
						catchManagerScript.enemyCollided = false;
						catchManagerScript.captureMode = false;
						catchManagerScript.fillUpMetre = 0;

						//guiManagerScript.maxTime = 10.0f;
						playerControllerScript.myAnim.Play ("Grounded Movement");

						isCollided = false;
						changed = false;
						recordPos = false;

						enemyCounter ++;

						playerControllerScript.isMovable = true;
						catchManagerScript.successCapture = false;
						catchManagerScript.failCapture = false;

						break;

					}
				}
				else
				{
					continue;
				}
			}
		}

		void CatchingView()
		{
			//catchingView = new Vector3 (target.transform.position.x + 0.377f, target.transform.position.y + 0.7f, target.transform.position.z - 0.2f);
			//catchingRotation = new Vector3 (pivotPoint.transform.rotation.x, pivotPoint.transform.rotation.y - 40.0f, pivotPoint.transform.rotation.z);
			playerControllerScript.myAnim.SetBool ("onCapture" , true);

			catchingView = new Vector3 (296.7f, 15.2f, 98.52f);
			catchingRotation = new Vector3 (0.0f, 23.3f, 0.0f);

			player.transform.position = new Vector3 (297.0f, 15.40739f, 97.0f);
			player.transform.rotation = Quaternion.Euler (0.0f, 37.501f, 0.0f);

			target.transform.position = new Vector3 (297.36f, 15.33f, 100.71f);
			target.transform.LookAt (player.transform.position);

			if(target.GetComponent <NavMeshAgent> ())
			{
				target.GetComponent <Enemies.Pathfinding> ().enabled = false;
				target.GetComponent <NavMeshAgent> ().enabled = false;
			}

			camera01.transform.position = catchingView;
			camera01.transform.rotation = Quaternion.Euler(catchingRotation);


			foreach (Enemies.Pathfinding enemyPathfindingScript in enemyPathfindingScripts)
			{
				enemyPathfindingScript.GPS.speed = 0.0f;
			}

			catchMode = true;
		}

		void NormalView()
		{
			if(!changed && enemyCounter >= 1)
			{
				playerControllerScript.myAnim.SetBool ("onCapture" , false);

				camera01.transform.position = pivotStartingPoint;
				camera01.transform.rotation = pivotStartingRotation;

				player.transform.position = playerStartingPoint;
				player.transform.rotation = playerStartingRotation;

				target.transform.position = targetStartingPoint;
				target.transform.rotation = targetStartingRotation;

				if(target.GetComponent <NavMeshAgent> ())
				{
					target.GetComponent <Enemies.Pathfinding> ().enabled = true;
					target.GetComponent <NavMeshAgent> ().enabled = true;
				}

				autoCamScript.enabled = true;

				foreach (Enemies.Pathfinding enemyPathfindingScript in enemyPathfindingScripts)
				{
					enemyPathfindingScript.GPS.speed = enemyPathfindingScript.enemyInfo.enemyMovementSpeed;
				}
				changed = true;
			}


			foreach (GameObject obstacle in obstacles)
			{
				obstacle.SetActive (true);
			}

			catchMode = false;
	}
	}
}

