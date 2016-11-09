using UnityEngine;
using System.Collections;

namespace Player
{
	public class Collision : MonoBehaviour 
	{
		public Player.Combat playerCombatScript;
		Player.Controller playerControllerScript;
		public Player.Health playerHealthScript;
		public int coinCounter;
		public int maxCoinCounter;
		public GameObject potion;
		public GameObject coin;
		GUIManagerScript guiScript;

		void Start()
		{
			coinCounter = 0;
			maxCoinCounter = 10;
			playerCombatScript = GetComponent <Player.Combat> ();
			playerControllerScript = GetComponent <Player.Controller> ();
			playerHealthScript = GameObject.FindGameObjectWithTag("GUIManager").transform.FindChild("Player UI").GetComponent <Player.Health> ();
			guiScript = FindObjectOfType <GUIManagerScript>();
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
				guiScript.canShowCoinText = true;
				Destroy(other.gameObject);
			}

			if(other.CompareTag("Potion"))
			{
				playerControllerScript.health += 1;
				playerHealthScript.canShowText = true;
				Destroy(other.gameObject);
			}

			if(other.CompareTag("Enemy"))
			{
				if (playerCombatScript.mode==0) {
					if (playerCombatScript.gauntlet.AttackCounter == 1) {
						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERATK1);
					}
					if (playerCombatScript.gauntlet.AttackCounter == 2) {
						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERATK2);
					}
					if (playerCombatScript.gauntlet.AttackCounter == 3) {
						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERATK3);
					}
				} 
				else if (playerCombatScript.mode == 1) 
				{
					SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERCAPTURE);
				}
			}


			if(other.CompareTag("Coconut"))
			{
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_HITTREE);

				other.GetComponent <Animation> ().Play();

				if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut01")
				{
					if(other.GetComponent<CoconutBehaviors>().coconut01 == true)
					{
						Instantiate(potion, new Vector3(other.transform.position.x - 3.0f, this.transform.position.y + 3.5f, other.transform.position.z - 2.0f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconut01 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut02")
				{
					if(other.GetComponent<CoconutBehaviors>().coconut02 == true)
					{
						Instantiate(potion, new Vector3(other.transform.position.x + 1.5f, this.transform.position.y + 3.5f, other.transform.position.z - 3.0f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconut02 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut03")
				{
					if(other.GetComponent<CoconutBehaviors>().coconut03 == true)
					{
						Instantiate(potion, new Vector3(other.transform.position.x + 1.0f, this.transform.position.y + 3.5f, other.transform.position.z + 2.5f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconut03 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut04")
				{
					if(other.GetComponent<CoconutBehaviors>().coconut04 == true)
					{
						Instantiate(potion, new Vector3(other.transform.position.x - 3.0f, this.transform.position.y + 3.5f, other.transform.position.z - 3.0f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconut04 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut05")
				{
					if(other.GetComponent<CoconutBehaviors>().coconut05 == true)
					{
						Instantiate(potion, new Vector3(other.transform.position.x + 3.5f, this.transform.position.y + 3.5f, other.transform.position.z), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconut05 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut06")
				{
					if(other.GetComponent<CoconutBehaviors>().coconut06 == true)
					{
						Instantiate(potion, new Vector3(other.transform.position.x + 2.5f, this.transform.position.y + 3.5f, other.transform.position.z + 1.5f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconut06 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "CoconutC01")
				{
					if(other.GetComponent<CoconutBehaviors>().coconutC01 == true)
					{
						Instantiate(coin, new Vector3(other.transform.position.x + 1.0f, this.transform.position.y + 3.5f, other.transform.position.z + 2.0f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconutC01 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "CoconutC02")
				{
					if(other.GetComponent<CoconutBehaviors>().coconutC02 == true)
					{
						Instantiate(coin, new Vector3(other.transform.position.x + 3.0f, this.transform.position.y + 3.5f, other.transform.position.z), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconutC02 = true;
					}
				}
				if(other.GetComponent<CoconutBehaviors>().coconutName == "CoconutC03")
				{
					if(other.GetComponent<CoconutBehaviors>().coconutC03 == true)
					{
						Instantiate(coin, new Vector3(other.transform.position.x + 1.0f, this.transform.position.y + 3.5f, other.transform.position.z + 3.0f), Quaternion.identity);
						other.GetComponent<CoconutBehaviors>().coconutC03 = true;
					}
				}

				if (other.CompareTag ("Crates"))
				{
					SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_HITCRATE);

					other.GetComponent <Animation> ().Play();
					other.GetComponent <BoxCollider> ().enabled = false;
				}
			}
		}
	}
}
