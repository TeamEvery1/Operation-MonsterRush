﻿using UnityEngine;
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
	//Cameras.CaptureView captureViewScript;
	//GUIManagerScript guiManagerScript;
	//CatchManager catchManagerScript;

	public GameObject[] enemies;


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

		for (int i = 0; i < enemies.Length; i++)
		{
			enemyCounter ++;
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
