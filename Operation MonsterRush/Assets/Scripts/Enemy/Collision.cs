using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class Collision : MonoBehaviour 
	{
		Pathfinding enemyPathfindingScript;
		CaptureCollider captureScript;
		GUIManagerScript guiScript;

		[HideInInspector] public bool isCollided;

		void Awake()
		{
			enemyPathfindingScript = GetComponent <Enemies.Pathfinding>();
			captureScript = FindObjectOfType <CaptureCollider>();
			guiScript = FindObjectOfType <GUIManagerScript>();
		}
	
		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("PlayerDamageCollider"))
			{
				enemyPathfindingScript.enemyExhaustion -= 10;

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
