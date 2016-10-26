﻿using UnityEngine;
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

		void Update()
		{
			if (isCollided == true) 
			{
				guiScript.enemyCollided = true;
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
				if(enemyPathfindingScript.enemyInfo.enemyExhaustion <= 0)
				{
					isCollided = true;
					enemyPathfindingScript.GPS.speed = 0;
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
