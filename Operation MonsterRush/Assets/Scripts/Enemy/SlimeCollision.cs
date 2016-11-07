using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class SlimeCollision : MonoBehaviour 
	{
		SlimeBehaviour enemyPathfindingScript;
		Player.Controller playerControllerScript;
		//CaptureCollider captureScript;
		//GUIManagerScript guiScript;
		CatchManager catchScript;

		public bool isCollided;

		public float enemyExhaustInfo;

		void Awake()
		{
			enemyPathfindingScript = GetComponent <Enemies.SlimeBehaviour>();
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
				enemyPathfindingScript.enemyInfo.enemyExhaustion -= playerControllerScript.damage;

				if(enemyPathfindingScript.enemyInfo.enemyExhaustion <= 0)
				{
					enemyPathfindingScript.enemyInfo.enemyExhaustion = 0;
				}
			}

			else if (other.CompareTag ("PlayerCatchCollider")) 
			{
				//if (enemyPathfindingScript.enemyExhaustion <= 0) 

				isCollided = true;
				enemyExhaustInfo = enemyPathfindingScript.enemyInfo.enemyExhaustion;
				catchScript.fillUpMetre += 1;

			} 
			else 
			{
				isCollided = false;
			}

		}
	}
}
