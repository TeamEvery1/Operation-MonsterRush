using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour
{
	public string name;
	public int timer_saved;
	/*public int minutes;
	public int seconds;*/
	public List <LeaderboardEntry> entriesList;
	public Transform leaderboardEntryObject;
	public Transform contentPanel;
	Timer timerScript;

	private List<GameObject> tempEntries;

	void Start()
	{
		timerScript = FindObjectOfType<Timer>();
		tempEntries = new List<GameObject>();
		timer_saved = (int)timerScript.timer;
		/*for (int e = 0; e < 10; e ++)
		{
			PlayerPrefs.DeleteKey ("LeaderboardEntry" + e);
		}*/
		/*timerScript = GameObject.FindGameObjectWithTag ("GUIManager").transform.Find ("Timer").GetComponent <Timer>();
		tempEntries = new List <GameObject> ();*/
		/*SaveEntry();
		LoadEntry();*/
	}

	private void Update()
	{
		timer_saved = (int)timerScript.timer;
		if(Input.GetKeyDown(KeyCode.V))
		{
			PlayerPrefs.DeleteAll();
		}

		if(Input.GetKeyDown(KeyCode.B))
		{
			SaveEntry();
		}

		if(Input.GetKeyDown(KeyCode.N))
		{
			LoadEntry();
		}
	}

	public void SaveEntry()
	{
		//Reinitialize list
		entriesList = new List <LeaderboardEntry> ();

		//Populate list of entries from PlayerPrefs
		for (int e = 0; e < 10; e++) 
		{
			string name;
			int timer;
			/*int minutes;
			int seconds;*/

			// Extract the data from Playerprefs
			if (PlayerPrefs.HasKey ("LeaderboardEntry" + e))
			{
				string data = PlayerPrefs.GetString ("LeaderboardEntry" + e);
				name = data.Substring (0, data.IndexOf ("/"));
				timer = int.Parse(data.Substring(data.IndexOf("/") + 1));
				/*string remaining = data.Substring (data.IndexOf ("/") + 1);
				minutes = (int) timerScript.timer / 60;
				seconds = (int) timerScript.timer % 60;*/
			}
			else
			{
				name = "ANONYMOUS";
				timer = 0;
				/*minutes = 0;
				seconds = 0;*/
			}

			// Add data to LeaderboardEntry object
			entriesList.Add (new LeaderboardEntry (name, timer));
		}

		entriesList.Sort ();	

		// Save new Leaderboard entry
		for (int e = 0; e < 10; e ++)
		{
			if(timer_saved >= entriesList[e].timer)
			{
				entriesList.Insert(e, new LeaderboardEntry(name, (int)timer_saved));
				break;
			}
			/*if (minutes > entriesList[e].minutes)
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
			}*/
		}


		// Save to playerprefs
		for (int e = 0; e < 10; e ++)
		{
			PlayerPrefs.SetString("LeaderboardEntry" + e, entriesList[e].name + "/" + entriesList[e].timer);
			//Debug.Log ( entriesList [e].seconds);
		}
		// Save player leaderboard entry
		//PlayerPrefs.SetString ("LeaderboardEntry0", name + "/" + score.ToString());

		//Debug and check sorting
		string debug = string.Empty;
		for(int e = 0; e < 10; e++)
		{
			debug += entriesList[e].name + " " + entriesList[e].timer + "\n";
		}
		Debug.Log(debug);
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
			int timer;
			/*int minutes;
			int seconds;*/

			// Extract the data from Playerprefs
			if (PlayerPrefs.HasKey ("LeaderboardEntry" + e))
			{
				string data = PlayerPrefs.GetString("LeaderboardEntry" + e);
				name = data.Substring( 0, data.IndexOf("/"));
				timer = int.Parse(data.Substring(data.IndexOf("/") + 1));
			}
			else
			{
				name = "ANNOYMOUS";
				timer = 0;
			}

			// Add data to LeaderboardEntry object
			entriesList.Add(new LeaderboardEntry(name, timer));
		}

		entriesList.Sort ();

		for (int e = 0; e < 10; e ++)
		{
			Transform newLeaderboardEntryobject = Instantiate(leaderboardEntryObject) as Transform;
			newLeaderboardEntryobject.SetParent (contentPanel);

			string _name = entriesList[e].name;
			int _timer = entriesList[e].timer;

			newLeaderboardEntryobject.GetComponent<LeaderboardEntryRenderer>().RenderLeaderboard(e + 1, _name, _timer);

			tempEntries.Add(newLeaderboardEntryobject.gameObject);
		}
	}
}


