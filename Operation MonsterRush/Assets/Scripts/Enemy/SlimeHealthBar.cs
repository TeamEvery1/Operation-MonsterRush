using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Enemies
{
	public class SlimeHealthBar : MonoBehaviour 
	{
		Image health;
		Enemies.SlimeBehaviour enemyPathfindingScript;
		private GUIManagerScript guiManager;
		private int firstTimeCapture;

		void Start()
		{
			health = GetComponent <Image>();
			enemyPathfindingScript = transform.parent.parent.GetComponent <Enemies.SlimeBehaviour>();
			guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();
			firstTimeCapture = 0;
		}

		void Update()
		{
			if (enemyPathfindingScript)
			{
				health.fillAmount = enemyPathfindingScript.enemyInfo.enemyExhaustion / enemyPathfindingScript.enemyInfo.enemyMaxExhaustion;
				if(health.fillAmount == 0 && firstTimeCapture == 0)
				{
					firstTimeCapture = 1;
					guiManager.canCapture = true;
					guiManager.canDisplayTutorialBlackScreen = true;
				}
			}
		}


	}
}
