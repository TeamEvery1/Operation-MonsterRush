using UnityEngine;
using System.Collections;

namespace Player
{
	public class Collision : MonoBehaviour 
	{
		Player.Controller playerControllerScript;
		public float coinCounter;

		void Start()
		{
			coinCounter = 0;
			playerControllerScript = GetComponent <Player.Controller> ();
		}

		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("DeathZone"))
			{
				playerControllerScript.health = 0;
			}

			if(other.CompareTag("Coin"))
			{
				coinCounter += 1;
				Destroy(other.gameObject);
			}
		}

	}
}
