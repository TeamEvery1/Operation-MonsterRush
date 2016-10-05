using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class Collision : MonoBehaviour 
	{
		Pathfinding enemyPathfindingScript;
		Player.Controller playerControllerScript;
		CaptureCollider captureScript;
		GUIManagerScript guiScript;

		public bool isCollided;

		void Awake()
		{
			enemyPathfindingScript = GetComponent <Enemies.Pathfinding>();
			playerControllerScript = FindObjectOfType <Player.Controller>();
			captureScript = FindObjectOfType <CaptureCollider>();
			guiScript = FindObjectOfType <GUIManagerScript>();
		}
	
		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("PlayerDamageCollider"))
			{
				enemyPathfindingScript.enemyExhaustion -= playerControllerScript.damage;

				if(enemyPathfindingScript.enemyExhaustion <= 0)
				{
					enemyPathfindingScript.enemyExhaustion = 0;
				}
			}

			else if (other.CompareTag ("PlayerCatchCollider"))
			{
				if(enemyPathfindingScript.enemyExhaustion <= 0)
				{
					isCollided = true;
					guiScript.fillUpMetre += 1;
				}
				else
				{
					isCollided = false;
				}
			}
		}
	}
}
