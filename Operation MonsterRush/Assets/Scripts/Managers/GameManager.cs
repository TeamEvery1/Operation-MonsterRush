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
			if(instance == null)
			{
				GameObject obj = new GameObject("Game Manager");
				obj.AddComponent <GameManager>();
			}
			return instance;
		}
	}

	private GameObject player;
	public Player.Movement playerMovementScript;
	public Player.Controller playerControllerScript;
	CaptureView captureViewScript;
	GUIManagerScript guiManagerScript;
	CatchManager catchManagerScript;

	public GameObject[] enemies;


	public bool winCondition = false;
	public bool loseCondition = false;
	public float timeMax = 900;
	public int enemyCounter = 0;
	public Text enemyCounterText;

	void Awake()
	{
		player = GameObject.Find("Character");
		playerMovementScript = player.GetComponent <Player.Movement>(); 
		playerControllerScript = player.GetComponent <Player.Controller>();
		captureViewScript = FindObjectOfType <CaptureView> ();
		guiManagerScript = FindObjectOfType <GUIManagerScript> ();
		catchManagerScript = FindObjectOfType <CatchManager> ();


	}

	void Start()
	{
		SoundManagerScript.Instance.PlayLoopingBGM (AudioClipID.BGM_MAIN_MENU);


		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemies)
		{
			enemyCounter ++;
		}
	}

	void FixedUpdate()
	{
		enemyCounterText.text = enemyCounter.ToString();
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

}
