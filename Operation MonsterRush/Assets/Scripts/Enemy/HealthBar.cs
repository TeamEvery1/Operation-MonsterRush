using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Enemies
{
	public class HealthBar : MonoBehaviour 
	{
		Image health;
		Enemies.Pathfinding enemyPathfindingScript;

		void Start()
		{
			health = GetComponent <Image>();
			enemyPathfindingScript = transform.parent.parent.GetComponent <Enemies.Pathfinding>();
		}

		void Update()
		{
			health.fillAmount = enemyPathfindingScript.enemyExhaustion / enemyPathfindingScript.enemyMaxExhaustion;
		}


	}
}
