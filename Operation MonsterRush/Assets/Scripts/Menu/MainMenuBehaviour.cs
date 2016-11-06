using UnityEngine;
using System.Collections;

public class MainMenuBehaviour : MonoBehaviour 
{
	Animator myAnim;
	private float delayTimer, delayDuration = 5.0f;
	private int randomCounter = 0;

	void Start () 
	{
		myAnim = GetComponent <Animator>();
	}
	

	void Update () 
	{
		SoundManagerScript.Instance.PlayLoopingBGM (AudioClipID.BGM_MAIN_MENU);
		if (delayTimer > delayDuration)
		{
			delayTimer = 0f;
			randomCounter = Random.Range (1,5);
		}
		else
		{
			delayTimer += Time.deltaTime;
		}

		if (randomCounter == 1)
		{
			myAnim.SetInteger ("Counter", 0);
		}
		else if (randomCounter == 2)
		{
			myAnim.SetInteger ("Counter", 1);
		}
		else if (randomCounter == 3)
		{
			myAnim.SetInteger ("Counter", 2);
		}
		else if (randomCounter == 4)
		{
			myAnim.SetInteger ("Counter", 3);
		}
	}
}
