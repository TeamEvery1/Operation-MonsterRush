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

	void Awake()
	{

		player = GameObject.FindGameObjectWithTag("Player");
		playerMovementScript = player.GetComponent <Player.Movement>(); 
	}

	void Update()
	{
		
	}

}
