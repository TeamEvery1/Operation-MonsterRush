using UnityEngine;
using System.Collections;

namespace Player
{
	public class Combat : MonoBehaviour
	{
		//public PlayerBullet.Statistic bulletStatistics;
		public Equipment gauntlet, gauntlet02, radar;
		//EquipmentInfo gaunletInfo, gaunlet02Info, radarInfo;
		private Effect effectScript;
		//public PlayerBullet.Movement bulletMovementScript;
		public Player.Controller playerControllerScript;
		
		public bool onCombat;
		[HideInInspector] public bool isDelayed;
		public bool catchDelayed;
		//private bool isShoot;
		public bool onCatch;
		private bool onScan;

		//GameObject Equipment;
		//public GameObject bulletPrefab;
		public GameObject closest;
		private GameObject damageCollider, damageCollider2, damageCollider3;
		private GameObject catchCollider;

		public Transform Gauntlet;
		public Transform Gauntlet_02;
		public Transform Radar;
		public Transform radarIndicator;

		public float rechargeTime;

	
		void Awake()
		{
			//bulletStatistics = new PlayerBullet.Statistic (10, 20, 3, 0, "Catcher", "Shooting a projectile towards enemy and catch them while they're exhausted.");
			gauntlet = new Equipment ("Gaunlet", 0, 3); 
			//gaunletInfo = new EquipmentInfo ("Ifrit", "Flame Gauntlet", "My name is Ifrit. The fool who awakens me shall pay dearly with fires of hell.", 10);
			gauntlet02 = new Equipment ("Gauntlet_02", 0, 0);
			//gaunlet02Info = new EquipmentInfo ("Death Saucer", "Gauntlet", "Energy that able to compress monster and capture them.", 0);
			radar = new Equipment ("Radar", 0, 0);
			//radarInfo = new EquipmentInfo ("Radar", "Radar", "Capable to sense the monster that within certain range." , 0);

			radarIndicator = GameObject.Find ("Camera/Pivot/MainCamera/Indicator").transform;

			damageCollider = transform.Find ("Gauntlet_DamageCollider/Combo 1").gameObject;
			damageCollider2 = transform.Find ("Gauntlet_DamageCollider/Combo 2").gameObject;
			damageCollider3 = transform.Find ("Gauntlet_DamageCollider/Combo 3").gameObject;
			catchCollider = transform.Find ("Gauntlet_DamageCollider/Catch Collider").gameObject;

			isDelayed = true;
			catchDelayed = true;
			rechargeTime = 0;
		}

		void Start()
		{
			//Equipment = GameObject.FindGameObjectWithTag("Equipment");
			//bulletPrefab = (GameObject) Resources.Load ("Prefabs/Bullets");

			effectScript = GetComponent<Effect>();
			//bulletMovementScript = bulletPrefab.GetComponent <PlayerBullet.Movement> ();
			playerControllerScript = GetComponent <Player.Controller>();
			//LeftHandPos = transform.Find("Character/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm/Character1_LeftHand");
			//RightHandPos = transform.Find("Character/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Gauntlet");
		}

		private void Update()
		{
			if(!catchDelayed)
			{
				if(rechargeTime > 0.7f)
				{
					onCatch = false;
					playerControllerScript.isMovable = true;
				}
	
				if( rechargeTime > 1.5f )
				{
					catchDelayed = true;
					rechargeTime = 0f;
				}
				else
				{
					rechargeTime += Time.deltaTime;
					playerControllerScript.isMovable = false;
				}
			}

			if (onScan)
			{
				GameObject[] nearestEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
				closest = null;
				float closestDist = 5000; 

				foreach (GameObject nearestEnemy in nearestEnemies)
				{
					if(nearestEnemy == null)
					{
						closest = nearestEnemy;
						closestDist = (nearestEnemy.transform.position - this.transform.position).magnitude;
					}
					else
					{
						float distance = (nearestEnemy.transform.position - this.transform.position).magnitude;

						if(distance < closestDist)
						{
							closest = nearestEnemy;
							closestDist = distance;
						}
					}
				}
			}
		}

		public void Perform()
		{
			if(Gauntlet.gameObject.activeSelf)
			{
				isDelayed = false;
			
				if(gauntlet.AttackCounter < gauntlet.TotalAttackCounter)
				{
					onCombat = true;

					gauntlet.AttackCounter ++;
					if(gauntlet.AttackCounter == 1)
					{
						StartCoroutine ("attackDelayTimer", 0.25f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.5f);

						StopCoroutine ("enableDamageCollider");
						StartCoroutine ("enableDamageCollider", 0.0f);

						StopCoroutine ("disableDamageCollider");
						StartCoroutine ("disableDamageCollider", 0.3f);
					}
					else if(gauntlet.AttackCounter == 2 )
					{
						StartCoroutine ("attackDelayTimer", 0.25f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.5f);

						StopCoroutine ("enableDamageCollider");
						StartCoroutine ("enableDamageCollider", 0.08f);

						StopCoroutine ("disableDamageCollider");
						StartCoroutine ("disableDamageCollider", 0.3f);
					}
					else if(gauntlet.AttackCounter == 3)
					{
						StartCoroutine ("attackDelayTimer", 0.4f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.4f);

						StopCoroutine ("enableDamageCollider");
						StartCoroutine ("enableDamageCollider", 0.12f);

						StopCoroutine ("disableDamageCollider");
						StartCoroutine ("disableDamageCollider", 0.3f);
					}
				}
				else
				{
					isDelayed = true;
					gauntlet.AttackCounter = 0;
				}
			}
			else if(Gauntlet_02.gameObject.activeSelf)
			{
				//Instantiate (bulletPrefab, new Vector3 (this.transform.position.x, this.transform.position.y + 0.8f, this.transform.position.z), this.transform.rotation);
		
				onCatch = true;
				catchDelayed = false;

				StopCoroutine ("enableCatchCollider");
				StartCoroutine ("enableCatchCollider", 0.0f);

				StopCoroutine ("disableCatchCollider");
				StartCoroutine ("disableCatchCollider", 0.8f);
			}
			else if(Radar.gameObject.activeSelf)
			{
				isDelayed = false;
				onScan = true;


			}
		}

		/*public void SwitchWeapon (int weaponState)
		{
			if(weaponState == 0 )
			{
				if(Gauntlet_02.gameObject.activeSelf || Radar.gameObject.activeSelf)
				{
					gauntlet.AttackCounter = 0;
					isDelayed = true;
					Gauntlet.gameObject.SetActive(true);
					Gauntlet_02.gameObject.SetActive(false);
					Radar.gameObject.SetActive(false);
				}
			}
			else if(weaponState == 1)
			{
				if(Gauntlet.gameObject.activeSelf || Radar.gameObject.activeSelf)
				{
					isDelayed = true;
					Gauntlet.gameObject.SetActive(false);
					Gauntlet_02.gameObject.SetActive(true);
					Radar.gameObject.SetActive(false);
				}
			}
			else if(weaponState ==2)
			{
				if(Gauntlet.gameObject.activeSelf || Gauntlet_02.gameObject.activeSelf)
				{
					isDelayed = true;
					Gauntlet.gameObject.SetActive(false);
					Gauntlet_02.gameObject.SetActive(false);
					Radar.gameObject.SetActive(true);
				}
			}
		}*/

		IEnumerator attackDelayTimer (float t)
		{
			isDelayed = false;
			playerControllerScript.isMovable = false;

			yield return new WaitForSeconds (t);

			isDelayed = true;
			playerControllerScript.isMovable = true;
			effectScript.Combo1End();
			effectScript.Combo2End();
			effectScript.Combo3End();
		}

		IEnumerator revertTimer (float t)
		{
			yield return new WaitForSeconds (t);

			gauntlet.AttackCounter = 0;
			onCombat = false;
		}

		IEnumerator enableDamageCollider (float t)
		{
			yield return new WaitForSeconds (t);

			if (gauntlet.AttackCounter == 1)
			{
				damageCollider.SetActive (true);
				damageCollider2.SetActive (false);
				damageCollider3.SetActive (false);
			}
			else if (gauntlet.AttackCounter == 2)
			{
				damageCollider.SetActive (false);
				damageCollider2.SetActive (true);
				damageCollider3.SetActive (false);
			}
			else if (gauntlet.AttackCounter == 3)
			{
				damageCollider.SetActive (false);
				damageCollider2.SetActive (false);
				damageCollider3.SetActive (true);
			}
		}

		IEnumerator disableDamageCollider (float t)
		{
			yield return new WaitForSeconds (t);

			damageCollider.SetActive (false);
			damageCollider2.SetActive (false);
			damageCollider3.SetActive (false);
		}

		IEnumerator enableCatchCollider (float t)
		{
			yield return new WaitForSeconds (t);

			catchCollider.SetActive (true);
		}

		IEnumerator disableCatchCollider (float t)
		{
			yield return new WaitForSeconds (t);

			catchCollider.SetActive (false);
		}
	}
}
