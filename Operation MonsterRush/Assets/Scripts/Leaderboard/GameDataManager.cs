using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
	public string name;
	public int minutes;
	public int seconds;
	public List <LeaderboardEntry> entriesList;
	public Transform leaderboardEntryObject;
	public Transform contentPanel;
	Timer timerScript;

	private List <GameObject> tempEntries;

	void Start()
	{
		/*for (int e = 0; e < 10; e ++)
		{
			PlayerPrefs.DeleteKey ("LeaderboardEntry" + e);
		}*/
		timerScript = GameObject.FindGameObjectWithTag ("GUIManager").transform.Find ("Timer").GetComponent <Timer>();
		tempEntries = new List <GameObject> ();
		SaveEntry();
		LoadEntry();
	}

	public void SaveEntry()
	{
		//Reinitialize list
		entriesList = new List <LeaderboardEntry> ();

		//Populate list of entries from PlayerPrefs
		for (int e = 0; e < 10; e++) 
		{
			string name;
			int minutes;
			int seconds;

			// Extract the data from Playerprefs
			if (PlayerPrefs.HasKey ("LeaderboardEntry" + e))
			{
				string data = PlayerPrefs.GetString ("LeaderboardEntry" + e);
				name = data.Substring (0, data.IndexOf ("/"));
				string remaining = data.Substring (data.IndexOf ("/") + 1);
				minutes = (int) timerScript.timer / 60;
				seconds = (int) timerScript.timer % 60;
			
			}
			else
			{
				name = "ANONYMOUS";
				minutes = 0;
				seconds = 0;
			}

			// Add data to LeaderboardEntry object
			entriesList.Add (new LeaderboardEntry (name, minutes, seconds));
		}

		entriesList.Sort ();	

		// Save new Leaderboard entry
		for (int e = 0; e < 10; e ++)
		{
			if (minutes > entriesList[e].minutes)
			{
				entriesList.Insert (e, new LeaderboardEntry (name, minutes, seconds));
				break;
			}
			else if (minutes == entriesList[e].minutes)
			{
				if (seconds >= entriesList[e].seconds)
				{
					entriesList.Insert (e, new LeaderboardEntry (name, minutes, seconds));
					break;
				}
			}
		}


		// Save to playerprefs
		for (int e = 0; e < 10; e ++)
		{
			PlayerPrefs.SetString ("LeaderboardEntry" + e, entriesList [e].name + "/" + entriesList [e].minutes + "." + entriesList [e].seconds);
			Debug.Log ( entriesList [e].seconds);
		}
		// Save player leaderboard entry
		//PlayerPrefs.SetString ("LeaderboardEntry0", name + "/" + score.ToString());
	}

	public void LoadEntry()
	{
		if (tempEntries.Count > 0)
		{
			for (int i = 0; i < tempEntries.Count; i++)
			{
				Destroy (tempEntries [i]);
			}
		}

		tempEntries.Clear();
		// Load player data
		/*string debug = string.Empty;
		for (int e = 0; e < 10; e ++)
		{
			debug += entriesList [e].name + " " + entriesList [e].score + "\n";
		}
		Debug.Log (debug);*/

		entriesList = new List <LeaderboardEntry> ();
		for (int e = 0; e < 10; e++) 
		{
			string name;
			int minutes;
			int seconds;

			// Extract the data from Playerprefs
			if (PlayerPrefs.HasKey ("LeaderboardEntry" + e))
			{
				string data = PlayerPrefs.GetString ("LeaderboardEntry" + e);
				name = data.Substring (0, data.IndexOf ("/"));
				string remaining = data.Substring (data.IndexOf ("/") + 1);
				minutes = (int) timerScript.timer / 60;
				seconds = (int) timerScript.timer % 60;
			}
			else
			{
				name = "ANONYMOUS";
				minutes = 0;
				seconds = 0;
			}

			// Add data to LeaderboardEntry object
			entriesList.Add (new LeaderboardEntry (name, minutes, seconds));
		}

		entriesList.Sort ();

		for (int e = 0; e < 10; e ++)
		{
			Transform newLeaderboardEntryObject = Instantiate (leaderboardEntryObject);
			newLeaderboardEntryObject.SetParent (contentPanel);

			string _name = entriesList [e].name;
			int _min = entriesList [e].minutes;
			int _sec = entriesList [e].seconds;

			newLeaderboardEntryObject.GetComponent <LeaderboardEntryRenderer> ().RenderLeaderboard (e + 1, _name, _min, _sec);

			tempEntries.Add (newLeaderboardEntryObject.gameObject);
		}
	}
}


