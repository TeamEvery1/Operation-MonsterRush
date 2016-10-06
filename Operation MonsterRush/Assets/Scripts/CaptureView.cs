using UnityEngine;
using System.Collections;

public class CaptureView : MonoBehaviour 
{
	Cameras.AutoCam autoCamScript;
	Enemies.Collision[] enemyCollisionScripts;
	Enemies.Pathfinding[] enemyPathfindingScripts;
	public Player.Controller playerControllerScript;
	GUIManagerScript guiManagerScript;
	GameManager gameManagaerScript;

	public GameObject pivotPoint;
	private GameObject target;
	public GameObject player;
	private GameObject[] obstacles;
	public GameObject camera;

	private Vector3 pivotStartingPoint;
	private Quaternion pivotStartingRotation;
	private Vector3 catchingView;
	private Vector3 catchingRotation;
	private Vector3 lookAtCamera;

	private bool catchMode;
	public bool isCollided;
	private bool changed;
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
		guiManagerScript = FindObjectOfType <GUIManagerScript>();
		gameManagaerScript = FindObjectOfType <GameManager>();

		//pivotPoint = transform.FindChild ("Pivot").gameObject;

		pivotStartingPoint = new Vector3 (0.0f, 1.66f, -0.1f);
		pivotStartingRotation = Quaternion.Euler(new Vector3 (2.4f  ,0.0f, 0.0f));

		obstacles = GameObject.FindGameObjectsWithTag ("Obstacles");

		camera = pivotPoint.transform.parent.gameObject;
	}

	void Update()
	{
		if(!isCollided)
		{
			NormalView();
		}
	}

	void LateUpdate()
	{
		foreach (Enemies.Collision enemyCollisionScript in enemyCollisionScripts)
		{
			if(enemyCollisionScript.isCollided)
			{
				isCollided = true;
				autoCamScript.enabled = false;
				target = enemyCollisionScript.gameObject;

				lookAtCamera = new Vector3 (pivotPoint.transform.position.x, target.transform.position.y, pivotPoint.transform.position.z);
				target.transform.LookAt (lookAtCamera);

				playerControllerScript.isMovable = false;
				playerControllerScript.myAnim.Play ("Grounded Movement");
				player.transform.position = target.transform.position - new Vector3 (0, 0, 4);

				if(!catchMode)
				{
					CatchingView();
				}

				if(guiManagerScript.fillUpMetre >= GUIManagerScript.fillUpMetreMax)
				{
					enemyCollisionScript.isCollided = false;
					guiManagerScript.enemyCollided = false;
					guiManagerScript.fillUpMetre = 0;
					guiManagerScript.maxTime = 10.0f;
					playerControllerScript.myAnim.Play ("Grounded Movement");
					isCollided = false;
					changed = false;
					enemyCounter ++;
					gameManagaerScript.enemyCounter --;
					target.gameObject.SetActive (false);

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
		catchingView = new Vector3 (target.transform.position.x + 0.377f, target.transform.position.y + 0.7f, target.transform.position.z - 0.2f);
		catchingRotation = new Vector3 (pivotPoint.transform.rotation.x, pivotPoint.transform.rotation.y - 40.0f, pivotPoint.transform.rotation.z);

		pivotPoint.transform.position = catchingView;
		pivotPoint.transform.rotation = Quaternion.Euler(catchingRotation);


		foreach (Enemies.Pathfinding enemyPathfindingScript in enemyPathfindingScripts)
		{
			enemyPathfindingScript.GPS.speed = 0;
		}

		foreach (GameObject obstacle in obstacles)
		{
			obstacle.SetActive (false);
		}

		catchMode = true;
	}

	void NormalView()
	{
		if(!changed && enemyCounter >= 1)
		{
			pivotPoint.transform.localPosition = pivotStartingPoint;
			pivotPoint.transform.localRotation = pivotStartingRotation;


			autoCamScript.enabled = true;
			changed = true;
		}

		foreach (Enemies.Pathfinding enemyPathfindingScript in enemyPathfindingScripts)
		{
			enemyPathfindingScript.GPS.speed = 1.0f;
		}

		foreach (GameObject obstacle in obstacles)
		{
			obstacle.SetActive (true);
		}

		catchMode = false;
}
}

