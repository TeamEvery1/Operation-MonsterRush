﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Enemies
{
	public class HealthBar : MonoBehaviour 
	{
		Image health;
		Enemies.Pathfinding enemyPathfindingScript;
		private GUIManagerScript guiManager;

		void Start()
		{
			health = GetComponent <Image>();
			enemyPathfindingScript = transform.parent.parent.GetComponent <Enemies.Pathfinding>();
			guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();
		}

		void Update()
		{
			if (enemyPathfindingScript)
			{
				health.fillAmount = enemyPathfindingScript.enemyInfo.enemyExhaustion / enemyPathfindingScript.enemyInfo.enemyMaxExhaustion;
				if(health.fillAmount == 0)
				{
					guiManager.canCapture = true;
					guiManager.canDisplay = true;
				}
			}
		}


	}
}
