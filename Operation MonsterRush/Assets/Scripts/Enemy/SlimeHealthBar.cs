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
		GameObject player;

		void Start()
		{
			health = GetComponent <Image>();
			enemyPathfindingScript = transform.parent.parent.GetComponent <Enemies.SlimeBehaviour>();
			guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();
			firstTimeCapture = 0;
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		void Update()
		{
			if (guiManager.firstCapture)
			{
				if (GameManager.GetSqrDist (this.transform.position, player.transform.position) < 3.0f)
				{
					guiManager.canDisplayTutorialHighlight = true;
				}
				/*else
				{
					guiManager.canDisplayTutorialHighlight = false;
				}*/
			}

			if (enemyPathfindingScript)
			{
				health.fillAmount = enemyPathfindingScript.enemyInfo.enemyExhaustion / enemyPathfindingScript.enemyInfo.enemyMaxExhaustion;
				if(health.fillAmount == 0 && firstTimeCapture == 0)
				{
					firstTimeCapture = 1;
					guiManager.canCapture = true;
					guiManager.canDisplayTutorialHighlight = true;
					guiManager.firstCapture = true;
				}
			}
		}


	}
}
