using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private static GameManager instance;

	public static GameManager Instance
	{
		get
		{
			if(GameObject.FindWithTag("GameManager") != null)
			{
				instance = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
			}
			else 
			{
				GameObject obj = new GameObject("GameManager");
				instance = obj.AddComponent<GameManager>();
			}
			return instance;
		}
	}

	private GameObject player;
	public Player.Movement playerMovementScript;
	public Player.Controller playerControllerScript;
	public Player.Combat playerCombatScript;
	public Cameras.AutoCam autoCamScript;
	//Cameras.CaptureView captureViewScript;
	//GUIManagerScript guiManagerScript;
	//CatchManager catchManagerScript;

	public GameObject[] enemies;
	public GameObject[] slimes;
	public GameObject luso;


	public bool winCondition = false;
	public bool loseCondition = false;
	public float timeMax = 900;
	public int enemyCounter = 0;
	public int maxEnemyCounter;
	public Player.Collision playerCollision;

	public GUIManagerScript guiManager;

	void Awake()
	{
		player = GameObject.Find("Character");
		playerMovementScript = player.GetComponent <Player.Movement>(); 
		playerControllerScript = player.GetComponent <Player.Controller>();
		playerCombatScript = player.GetComponent <Player.Combat> ();
		autoCamScript = FindObjectOfType<Cameras.AutoCam>();
		//captureViewScript = FindObjectOfType <Cameras.CaptureView> ();
		//guiManagerScript = FindObjectOfType <GUIManagerScript> ();
		//catchManagerScript = FindObjectOfType <CatchManager> ();
		guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();
		playerCollision = FindObjectOfType<Player.Collision>();

	}

	void Start()
	{
		//coinCounterText.text = playerCollision.coinCounter + " / 10";

		SoundManagerScript.Instance.PlayLoopingBGM (AudioClipID.BGM_BATTLE);


		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		luso = GameObject.FindGameObjectWithTag ("Luso");
		slimes = GameObject.FindGameObjectsWithTag ("Slime");

		for (int i = 0; i < enemies.Length; i++)
		{
			enemyCounter++;
		}

		if (luso.activeSelf)
		{
			enemyCounter ++;
		}

		for (int i = 0; i < slimes.Length; i++)
		{
			enemyCounter++;
		}

		maxEnemyCounter = enemyCounter;
	}

	void Update()
	{
		if(enemyCounter < maxEnemyCounter)
		{
			guiManager.canDisplayTutorialHighlight = false;
		}

	}

	void FixedUpdate()
	{
		
		//enemyCounterText.text = enemyCounter.ToString();
		if (enemyCounter <= 0) 
		{
			winCondition = true;
			autoCamScript.autoCam = false;
		}



		/*if (guiManagerScript.maxTime <= 0) 
		{
			//guiManagerScript.GetComponent <Canvas>().enabled = true;
			catchManagerScript.GetComponent <Canvas>().enabled = false;
			//loseCondition = true;
		}*/
	}

	public static float GetSqrDist (Vector3 a, Vector3 b)
	{
		Vector3 vector = new Vector3 (a.x - b.x, 0, a.z - b.z);
		return vector.sqrMagnitude;
	}
}
