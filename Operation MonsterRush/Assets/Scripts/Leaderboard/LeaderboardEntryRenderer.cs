using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardEntryRenderer : MonoBehaviour 
{
	public Text rankText; 
	public Text nameText;
	public Text timerText;
	/*public Text minText;
	public Text secText;*/
	private Timer timerScript;

	void Start()
	{
		timerScript = FindObjectOfType<Timer>();
	}


	//REDO: name have to take from user Input, can't save min and sec individually, it refresh tgt.
	public void RenderLeaderboard (int rank, string name, int timer)
	{
		rankText.text = rank.ToString ();
		nameText.text = name;

		float temp  = timer / 60;
		float temp2 = timer % 60;
		timerText.text = temp.ToString() + " : " + temp2.ToString();
		/*minText.text = min.ToString ();
		secText.text = sec.ToString ();*/

		this.transform.localScale = Vector3.one; // reset scaling
	}
}
