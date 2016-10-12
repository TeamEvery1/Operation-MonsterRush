using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	float timer, realTime;
	int minutes, seconds, fraction;

	public Text timerText;

	void Awake()
	{
		timer = 0.0f;
		realTime = Time.time;

		timerText = this.transform.GetComponent <Text>();
	}

	void FixedUpdate()
	{
		timer = Time.time - realTime;

		minutes = (int) timer / 60;
		seconds = (int) timer % 60;

		timerText.text = string.Format ( "{0:00} : {1:00}", minutes, seconds);
	}
}
