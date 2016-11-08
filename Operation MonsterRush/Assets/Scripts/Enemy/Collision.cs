using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class Collision : MonoBehaviour 
	{
		Pathfinding enemyPathfindingScript;
		SlimeBehaviour slimePathfindingScript;
		Player.Controller playerControllerScript;
		//CaptureCollider captureScript;
		//GUIManagerScript guiScript;
		CatchManager catchScript;

		public bool isCollided;

		public float enemyExhaustInfo;

		void Awake()
		{
			slimePathfindingScript = GetComponent <Enemies.SlimeBehaviour>();
			enemyPathfindingScript = GetComponent <Enemies.Pathfinding>();
			playerControllerScript = FindObjectOfType <Player.Controller>();
			//captureScript = FindObjectOfType <CaptureCollider>();
			//guiScript = FindObjectOfType <GUIManagerScript>();
			catchScript = FindObjectOfType <CatchManager>();

		}

		void Update()
		{
			if (isCollided == true) 
			{
				catchScript.enemyCollided = true;
			}
		}

	
		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("PlayerDamageCollider"))
			{
				if (gameObject.tag == "Slime") 
				{
					slimePathfindingScript.enemyInfo.enemyExhaustion -= playerControllerScript.damage;
				} 
				else 
				{
					enemyPathfindingScript.enemyInfo.enemyExhaustion -= playerControllerScript.damage;
				}


				if (gameObject.tag == "Slime") 
				{
					if (slimePathfindingScript.enemyInfo.enemyExhaustion <= 0) 
					{
						slimePathfindingScript.enemyInfo.enemyExhaustion = 0;
					}
				}
				else if(enemyPathfindingScript.enemyInfo.enemyExhaustion <= 0)
				{
					enemyPathfindingScript.enemyInfo.enemyExhaustion = 0;
				}
			}

			else if (other.CompareTag ("PlayerCatchCollider")) 
			{
				//if (enemyPathfindingScript.enemyExhaustion <= 0) 
				if (gameObject.tag == "Slime") 
				{
					enemyExhaustInfo = slimePathfindingScript.enemyInfo.enemyExhaustion;
					slimePathfindingScript.enemyInfo.enemyMovementSpeed = 0;
				} 
				else 
				{
					enemyExhaustInfo = enemyPathfindingScript.enemyInfo.enemyExhaustion;
					enemyPathfindingScript.GPS.speed = 0;
				}
				isCollided = true;
				catchScript.fillUpMetre += 1;

			} 
			else 
			{
				isCollided = false;
			}

		}
	}
}
