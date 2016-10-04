using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class Collision : MonoBehaviour 
	{
		Pathfinding enemyPathfindingScript;

		void Awake()
		{
			enemyPathfindingScript = GetComponent <Enemies.Pathfinding>();
		}
	
		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("PlayerDamageCollider"))
			{
				enemyPathfindingScript.enemyHealth -= 10;
			}
		}
	}
}
