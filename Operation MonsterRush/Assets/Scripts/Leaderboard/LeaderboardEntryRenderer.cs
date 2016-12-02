using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeaderboardEntryRenderer : MonoBehaviour 
{
	public Text rankText; 
	public Text nameText;
	public Text minText;
	public Text secText;
	public Timer timerScript;

	void Start()
	{
		timerScript = GameObject.FindGameObjectWithTag ("GUIManager").transform.Find ("Timer").GetComponent <Timer>();
	}


	//REDO: name have to take from user Input, can't save min and sec individually, it refresh tgt.
	public void RenderLeaderboard (int rank, string name, int min, int sec)
	{
		rankText.text = rank.ToString ();
		nameText.text = name;
		minText.text = min.ToString ();
		secText.text = sec.ToString ();

		this.transform.localScale = Vector3.one; // reset scaling
	}
}
