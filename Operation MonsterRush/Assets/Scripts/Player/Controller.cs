using UnityEngine;
using System.Collections;

namespace Player
{
	[RequireComponent (typeof(Animator))]
	[RequireComponent (typeof(Rigidbody))]
	[RequireComponent (typeof(CapsuleCollider))]
	public class Controller : MonoBehaviour
	{
		private float movementSpeed;
		private string name;

		public Player.Combat playerCombatScript;

		public KeyCode attackKey, primaryWeaponKey, secondaryWeaponKey, tertiaryWeaponKey; 

		Animator myAnim;
		Rigidbody myRB;
		CapsuleCollider myCollider;

		void Awake()
		{
			Player.Character playerCharacter = new Player.Character ( movementSpeed, name );
			playerCombatScript = GetComponent <Player.Combat>(); 

			myAnim = GetComponent<Animator>();
			myRB = GetComponent<Rigidbody>();
			myCollider = GetComponent<CapsuleCollider>();
		}

		void Update()
		{
			//! Animation
			myAnim.SetInteger("attackCounter", playerCombatScript.gauntlet.AttackCounter);
			myAnim.SetBool ("onCombat", playerCombatScript.onCombat);
		
			//! perform attack
			if(playerCombatScript.isDelayed)
			{
				if(Input.GetKeyDown (attackKey))
				{
					playerCombatScript.Perform();
				}
			}

			//! switch weapon during runtime
			if(Input.GetKeyDown (primaryWeaponKey))
			{
				playerCombatScript.SwitchWeapon(0);
			}
			if(Input.GetKeyDown (secondaryWeaponKey))
			{
				playerCombatScript.SwitchWeapon(1);
			}
			if(Input.GetKeyDown (tertiaryWeaponKey))
			{
				playerCombatScript.SwitchWeapon(2);
			}


		}
	}
}	
