using UnityEngine;
using System.Collections;

namespace Enemies
{
	public enum MONSTER_TYPE
	{
		Penguin = 0,
		Slime = 1,
		Bird = 2,
		Bean = 3,
		Disgusting_Thing = 4,

		TOTAL
	}

	public class Selection : MonoBehaviour
	{
		public MONSTER_TYPE mT;
		[HideInInspector]public string monsterType;

		void OnGUI()
		{
			
		}

		void Awake()
		{
			switch(mT)
			{
			case MONSTER_TYPE.Penguin:
				monsterType = "penguin";
				break;
			case MONSTER_TYPE.Slime:
				monsterType = "slime";
				break;
			case MONSTER_TYPE.Bird:
				monsterType = "bird";
				break;
			case MONSTER_TYPE.Bean:
				monsterType = "bean";
				break;
			case MONSTER_TYPE.Disgusting_Thing:
				monsterType = "disgusting";
				break;
			}
		}
	}
}
