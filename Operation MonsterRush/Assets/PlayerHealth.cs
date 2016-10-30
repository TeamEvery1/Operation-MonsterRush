using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public Sprite HP5, HP4, HP3, HP2, HP1, HP0;
	private Player.Controller playerController;
	private Image healthBar;

	void Start()
	{
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Controller>();
		healthBar = this.GetComponent<Image>();
	}


	void Update()
	{
		if(playerController.health > 0 && playerController.health <= 20)
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
		}
	}
}
