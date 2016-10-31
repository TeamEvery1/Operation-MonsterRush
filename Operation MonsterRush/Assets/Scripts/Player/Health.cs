using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Player
{
	public class Health : MonoBehaviour
	{
		//public Sprite HP5, HP4, HP3, HP2, HP1, HP0;
		private Player.Controller playerController;
		//private Image healthBar;
		private Image health;
		private Text value;

		void Start()
		{
			playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Controller>();
			//healthBar = this.GetComponent<Image>();
			health = this.gameObject.transform.FindChild ("Health").GetComponent <Image>();
			value = this.gameObject.transform.FindChild("Value").GetComponent<Text>();

		}


		void Update()
		{
			// Player Health Bar With Multiple Sprites
			/*if(playerController.health > 0 && playerController.health <= 20)
			{
				healthBar.overrideSprite = HP1;
			}
			else if(playerController.health > 20 && playerController.health <= 40)
			{
				healthBar.overrideSprite = HP2;
			}
			else if(playerController.health > 40 && playerController.health <= 60)
			{
				healthBar.overrideSprite = HP3;
			}
			else if(playerController.health > 60 && playerController.health <= 80)
			{
				healthBar.overrideSprite = HP4;
			}
			else if(playerController.health > 80 && playerController.health <= 100)
			{
				healthBar.overrideSprite = HP5;
			}
			else if(playerController.health <= 0)
			{
				healthBar.overrideSprite = HP0;
			}*/

			// Player Health Bar With Single Sprite

			/*if(playerController.health > 20 )
			{
				value
			}*/


			health.fillAmount = playerController.health / playerController.maxHealth;
			value.text = playerController.health + " / " + playerController.maxHealth;

		}
	}
}
