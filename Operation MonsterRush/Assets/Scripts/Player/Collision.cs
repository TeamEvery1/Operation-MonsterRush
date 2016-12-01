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

		private GameObject obj;
		private GameObject healingEffect;

		GUIManagerScript guiScript;

		private Component[] cratesPieces;

		Timer timerScript;

		private Enemies.Pathfinding pathFinding;

		public bool coinCollided;

		void Start()
		{
			timerScript = FindObjectOfType<Timer> ();

			coinCounter = 0;
			maxCoinCounter = 10;

			potion = (GameObject) Resources.Load ("Prefabs/Props/Potion");
			playerCombatScript = GetComponent <Player.Combat> ();
			playerControllerScript = GetComponent <Player.Controller> ();
			playerHealthScript = GameObject.FindGameObjectWithTag("GUIManager").transform.FindChild("Player UI").GetComponent <Player.Health> ();
			guiScript = FindObjectOfType <GUIManagerScript>();
			pathFinding = GameObject.FindObjectOfType<Enemies.Pathfinding>();

			healingEffect = this.transform.Find ("Extra Effect/Healing").gameObject;

			coinCollided = false;
		}

		void FixedUpdate()
		{
			if (healingEffect.activeSelf)
			{
				StartCoroutine ("HealingVFXTimer", 1.5f);
			}
		}

		void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag ("DeathZone"))
			{
				playerControllerScript.health = 0;
			}

			if(other.CompareTag("Coin"))
			{
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_COINCOLLECT);
				coinCounter += 1;
				guiScript.canShowCoinText = true;
				other.GetComponent <Animation> ().Play ("Coin Get");
				other.GetComponent <Animation> ().PlayQueued ("Coin");
				obj = other.gameObject;

				Invoke ("Delay", 1.3f);
			}

			if(other.CompareTag("Potion"))
			{
				playerControllerScript.health += 1;
				playerHealthScript.canShowText = true;

				healingEffect.SetActive (true);
	
				Destroy(other.gameObject);
			}

			if(other.CompareTag("Enemy"))
			{
				if (playerCombatScript.mode==0) {
					if (playerCombatScript.gauntlet.AttackCounter == 1) {
						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERATK1);
						GetComponent <Effect> ().combo1.gameObject.SetActive (true);
						GetComponent <Effect> ().combo1Timer = 0.0f;
					}
					if (playerCombatScript.gauntlet.AttackCounter == 2) {
						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERATK2);
						GetComponent <Effect> ().combo2.gameObject.SetActive (true);
						GetComponent <Effect> ().combo2Timer = 0.0f;
					}
					if (playerCombatScript.gauntlet.AttackCounter == 3) {
						SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_PLAYERATK3);
						GetComponent <Effect> ().combo3.gameObject.SetActive (true);
						GetComponent <Effect> ().combo3Timer = 0.0f;
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

				if (!other.GetComponent <Animation> ().isPlaying)
				{
					other.GetComponent <Animation> ().Play();
				}

				if(other.GetComponent <CoconutBehaviors>())
				{
					if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut01")
					{
						if(other.GetComponent<CoconutBehaviors>().coconut01 == true)
						{
							Instantiate(potion, new Vector3(other.transform.position.x, this.transform.position.y + 2.5f, other.transform.position.z - 1.0f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconut01 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut02")
					{
						if(other.GetComponent<CoconutBehaviors>().coconut02 == true)
						{
							Instantiate(potion, new Vector3(other.transform.position.x + 1.5f, this.transform.position.y + 2.5f, other.transform.position.z - 3.0f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconut02 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut03")
					{
						if(other.GetComponent<CoconutBehaviors>().coconut03 == true)
						{
							Instantiate(potion, new Vector3(other.transform.position.x + 1.0f, this.transform.position.y + 2.5f, other.transform.position.z + 2.5f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconut03 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut04")
					{
						if(other.GetComponent<CoconutBehaviors>().coconut04 == true)
						{
							Instantiate(potion, new Vector3(other.transform.position.x - 3.0f, this.transform.position.y + 2.5f, other.transform.position.z - 3.0f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconut04 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut05")
					{
						if(other.GetComponent<CoconutBehaviors>().coconut05 == true)
						{
							Instantiate(potion, new Vector3(other.transform.position.x + 3.5f, this.transform.position.y + 2.5f, other.transform.position.z), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconut05 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "Coconut06")
					{
						if(other.GetComponent<CoconutBehaviors>().coconut06 == true)
						{
							Instantiate(potion, new Vector3(other.transform.position.x - 1.5f, this.transform.position.y + 2.0f, other.transform.position.z + 1.0f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconut06 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "CoconutC01")
					{
						if(other.GetComponent<CoconutBehaviors>().coconutC01 == true)
						{
							Instantiate(coin, new Vector3(other.transform.position.x + 1.0f, this.transform.position.y + 2.5f, other.transform.position.z + 2.0f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconutC01 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "CoconutC02")
					{
						if(other.GetComponent<CoconutBehaviors>().coconutC02 == true)
						{
							Instantiate(coin, new Vector3(other.transform.position.x + 1.5f, this.transform.position.y + 4.0f, other.transform.position.z + 1.5f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconutC02 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
					if(other.GetComponent<CoconutBehaviors>().coconutName == "CoconutC03")
					{
						if(other.GetComponent<CoconutBehaviors>().coconutC03 == true)
						{
							Instantiate(coin, new Vector3(other.transform.position.x + 1.0f, this.transform.position.y + 2.5f, other.transform.position.z + 3.0f), Quaternion.identity);
							other.GetComponent<CoconutBehaviors>().coconutC03 = false;
							other.GetComponent<CoconutBehaviors>().effect.SetActive(false);
						}
					}
				}
			}
			if (other.CompareTag ("Crates"))
			{
				SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_HITCRATE);

				if(other.GetComponent <Animation>())
				{
					if (!other.GetComponent <Animation> ().isPlaying)
					{
						other.GetComponent <Animation> ().Play();
					}
				}

				other.GetComponent <BoxCollider> ().enabled = false;
			}
		}

		void Delay ()
		{
			Destroy (obj.gameObject);
		}

		IEnumerator HealingVFXTimer (float t)
		{
			yield return new WaitForSeconds (t);
			healingEffect.SetActive (false);
		}
	}
}
