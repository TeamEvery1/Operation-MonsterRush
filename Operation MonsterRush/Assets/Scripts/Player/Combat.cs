using UnityEngine;
using System.Collections;

namespace Player
{
	public class Combat : MonoBehaviour
	{
		public PlayerBullet.Statistic bulletStatistics;
		public Equipment gauntlet, gauntlet02, radar;
		EquipmentInfo gaunletInfo, gaunlet02Info, radarInfo;
		private Effect effectScript;
		public PlayerBullet.Movement bulletMovementScript;
		public Player.Controller playerControllerScript;
		
		public bool onCombat;
		[HideInInspector]public bool isDelayed;
		private bool isShoot;

		GameObject Equipment;
		//public GameObject bulletPrefab;
		public Transform Gauntlet;
		public Transform Gauntlet_02;
		public Transform Radar;

		public float rechargeTime;

	
		void Awake()
		{
			bulletStatistics = new PlayerBullet.Statistic (10, 20, 3, 0, "Catcher", "Shooting a projectile towards enemy and catch them while they're exhausted.");
			gauntlet = new Equipment ("Gaunlet", 0, 3); 
			gaunletInfo = new EquipmentInfo ("Ifrit", "Flame Gauntlet", "My name is Ifrit. The fool who awakens me shall pay dearly with fires of hell.", 10);
			gauntlet02 = new Equipment ("Gauntlet_02", 0, 0);
			gaunlet02Info = new EquipmentInfo ("Death Saucer", "Gauntlet", "Energy that able to compress monster and capture them.", 0);
			radar = new Equipment ("Radar", 0, 0);
			radarInfo = new EquipmentInfo ("Radar", "Radar", "Capable to sense the monster that within certain range." , 0);

			isDelayed = true;
			rechargeTime = 0;
		}

		void Start()
		{
			Equipment = GameObject.FindGameObjectWithTag("Equipment");
			//bulletPrefab = (GameObject) Resources.Load ("Prefabs/Bullets");

			effectScript = GetComponent<Effect>();
			//bulletMovementScript = bulletPrefab.GetComponent <PlayerBullet.Movement> ();
			playerControllerScript = GetComponent <Player.Controller>();
			//LeftHandPos = transform.Find("Character/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm/Character1_LeftHand");
			//RightHandPos = transform.Find("Character/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Gauntlet");
		}

		private void Update()
		{
			if(isShoot)
			{
				if( rechargeTime > bulletStatistics.RechargeTime )
				{
					rechargeTime = 0f;
					isDelayed = true;
					isShoot = false;
				}
				else
				{
					rechargeTime += Time.deltaTime;
				}
			}
		}

		public void Perform()
		{
			isDelayed = false;
			onCombat = true;

			if(Gauntlet.gameObject.activeSelf)
			{
				if(gauntlet.AttackCounter < gauntlet.TotalAttackCounter)
				{
					gauntlet.AttackCounter ++;
					if(gauntlet.AttackCounter == 1)
					{
						StartCoroutine ("attackDelayTimer", 0.25f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.5f);
					}
					else if(gauntlet.AttackCounter == 2 )
					{
						StartCoroutine ("attackDelayTimer", 0.25f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.5f);
					}
					else if(gauntlet.AttackCounter == 3)
					{
						StartCoroutine ("attackDelayTimer", 0.4f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.7f);
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
				isShoot = true;
			}
			else if(Radar.gameObject.activeSelf)
			{
				GameObject[] nearestEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

			}
		}

		public void SwitchWeapon (int weaponState)
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
					Gauntlet.gameObject.SetActive(false);
					Gauntlet_02.gameObject.SetActive(true);
					Radar.gameObject.SetActive(false);
				}
			}
			else if(weaponState ==2)
			{
				if(Gauntlet.gameObject.activeSelf || Gauntlet_02.gameObject.activeSelf)
				{
					Gauntlet.gameObject.SetActive(false);
					Gauntlet_02.gameObject.SetActive(false);
					Radar.gameObject.SetActive(true);
				}
			}
		}

		IEnumerator attackDelayTimer (float t)
		{
			isDelayed = false;
			playerControllerScript.isMovable = false;
			yield return new WaitForSeconds (t);
			isDelayed = true;
			playerControllerScript.isMovable = true;
		}

		IEnumerator revertTimer (float t)
		{
			yield return new WaitForSeconds (t);
			gauntlet.AttackCounter = 0;
			onCombat = false;
			effectScript.Combo1End();
			effectScript.Combo2End();
			effectScript.Combo3End();
		}
	}
}
