using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatchTimer : MonoBehaviour {
	
	public float catchTimer,seconds,minutes;

	public Text catchTimerText;
	// Use this for initialization
	void Awake () 
	{
		catchTimerText = this.transform.GetComponent <Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		catchTimer = GetComponentInParent<CatchManager> ().timeLimitF;
		minutes = (int) catchTimer / 60;
		seconds = (int) catchTimer % 60;
		catchTimerText.text = string.Format ( "{0:00} : {1:00}", minutes, seconds);
	}
}
