using UnityEngine;
using System.Collections;
using System;

public class LeaderboardEntry : IComparable <LeaderboardEntry>
{
	public string name;
	public int timer;
	/*public int minutes;
	public int seconds;*/

	public LeaderboardEntry (string _name, int _timer)
	{
		name = _name;
		timer = _timer;
		/*minutes = _min;
		seconds = _sec;*/
	}

	public int CompareTo (LeaderboardEntry other)
	{
		if (other == null) 
		{
			return -1;
		}

		return other.timer - timer;
	}


}
