using UnityEngine;
using System.Collections;

namespace Player
{
	public class Collision : MonoBehaviour 
	{
		Player.Controller playerControllerScript;

		void Start()
		{
			playerControllerScript = GetComponent <Player.Controller> ();
		}

		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("DeathZone"))
			{
				playerControllerScript.health = 0;
			}
		}

	}
}
