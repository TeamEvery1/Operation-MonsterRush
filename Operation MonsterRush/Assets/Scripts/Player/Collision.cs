using UnityEngine;
using System.Collections;

namespace Player
{
	public class Collision : MonoBehaviour 
	{
		Player.Controller playerControllerScript;
		public float coinCounter;
		public GameObject coconut;
		public float potionCounter;

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

			if(other.CompareTag("Coconut"))
			{
				if(potionCounter < 1)
				{
					Instantiate(coconut, new Vector3(other.transform.position.x , this.transform.position.y + 2.5f, other.transform.position.z + 1.5f), Quaternion.identity);
					potionCounter += 1;
				}
			}
		}

	}
}
