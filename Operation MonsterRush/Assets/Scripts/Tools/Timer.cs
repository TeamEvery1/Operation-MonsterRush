using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public float timer, realTime;
	int minutes, seconds, fraction;

	public Text timerText;

	void Awake()
	{
		timer = 900.0f;
		//realTime = 900.0f;

		timerText = this.transform.GetComponent <Text>();
	}

	void FixedUpdate()
	{
		if (timer <= 10) 
		{

			SoundManagerScript.Instance.PlayLoopingSFX (AudioClipID.SFX_TIMERALERT);
		}
		if (timer == 0) 
		{
			SoundManagerScript.Instance.PauseLoopingSFX (AudioClipID.SFX_TIMERALERT);

		}

		timer -= Time.deltaTime;
		if (timer <= 0) 
		{
			timer = 0;
		}

		minutes = (int) timer / 60;
		seconds = (int) timer % 60;

		timerText.text = string.Format ( "{0:00} : {1:00}", minutes, seconds);
	}
}
