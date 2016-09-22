using UnityEngine;
using System.Collections;

namespace Player
{
	public class Combat : MonoBehaviour
	{
		public Equipment gauntlet, gauntlet02, radar;
		EquipmentInfo gaunletInfo, gaunlet02Info, radarInfo;
		public Effect effectScript;
		
		public bool onCombat;
		[HideInInspector]public bool isDelayed;

		GameObject Equipment;
		public Transform Gauntlet;
		public Transform Gauntlet_02;
		public Transform Radar;

		string currentEquipment;



		void Awake()
		{
			gauntlet = new Equipment ("Gaunlet", 0, 3); 
			gaunletInfo = new EquipmentInfo ("Ifrit", "Flame Gauntlet", "My name is Ifrit. The fool who awakens me shall pay dearly with fires of hell.", 10);
			gauntlet02 = new Equipment ("Gauntlet_02", 0, 0);
			gaunlet02Info = new EquipmentInfo ("Death Saucer", "Gauntlet", "Energy that able to compress monster and capture them.", 0);
			radar = new Equipment ("Radar", 0, 0);
			radarInfo = new EquipmentInfo ("Radar", "Radar", "Capable to sense the monster that within certain range." , 0);
		}

		void Start()
		{
			Equipment = GameObject.FindGameObjectWithTag("Equipment");

			currentEquipment = "Gauntlet";

			isDelayed = true;

			effectScript = GetComponent<Effect>();
			//LeftHandPos = transform.Find("Character/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_LeftShoulder/Character1_LeftArm/Character1_LeftForeArm/Character1_LeftHand");
			//RightHandPos = transform.Find("Character/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/Gauntlet");
		}

		public void Perform()
		{
			isDelayed = false;
			onCombat = true;
			if(currentEquipment == "Gauntlet")
			{
				if(gauntlet.AttackCounter < gauntlet.TotalAttackCounter)
				{
					gauntlet.AttackCounter ++;
					if(gauntlet.AttackCounter == 1)
					{
						StartCoroutine ("attackDelayTimer", 0.15f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.4f);
					}
					else if(gauntlet.AttackCounter == 2 )
					{
						StartCoroutine ("attackDelayTimer", 0.15f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.4f);
					}
					else if(gauntlet.AttackCounter == 3)
					{
						StartCoroutine ("attackDelayTimer", 0.3f);

						StopCoroutine ("revertTimer");
						StartCoroutine ("revertTimer" , 0.6f);
					}
				}
				else
				{
					isDelayed = true;
					gauntlet.AttackCounter = 0;
				}
			}
		}

		public void SwitchWeapon (int weaponState)
		{
			if(weaponState == 0 )
			{
				if(Gauntlet_02.gameObject.activeSelf == true)
				{
					Gauntlet.gameObject.SetActive(true);
					Gauntlet_02.gameObject.SetActive(false);
				}
			}
			else if(weaponState == 1)
			{
				if(Gauntlet.gameObject.activeSelf == true)
				{
					Gauntlet.gameObject.SetActive(false);
					Gauntlet_02.gameObject.SetActive(true);
				}
			}
			else if(weaponState ==2)
			{
				
			}
		}

		IEnumerator attackDelayTimer (float t)
		{
			isDelayed = false;
			yield return new WaitForSeconds (t);
			isDelayed = true;
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
