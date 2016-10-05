using UnityEngine;
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
	public GameObject[] enemies;
	public bool winCondition;
	public bool loseCondition;
	public float timeMax = 900;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerMovementScript = player.GetComponent <Player.Movement>(); 
		playerControllerScript = player.GetComponent <Player.Controller>();
	}

	void Start()
	{
		SoundManagerScript.Instance.PlayLoopingBGM (AudioClipID.BGM_MAIN_MENU);
		if (enemies == null) 
		{
			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		}
	}

	void Update()
	{
		timeMax -= Time.deltaTime;
		if (enemies.Length == 0) 
		{
			winCondition = true;
		}
		if (timeMax <= 0) 
		{
			loseCondition = true;
		}
	}

}
