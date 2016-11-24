using UnityEngine;
using System;
using System.Collections;


namespace Enemies
{
	public class Info : MonoBehaviour 
	{
		public LevelEditor.MonsterSpawnOrder.MONSTER_TYPE mT;

		[HideInInspector]public string monsterType;
		public string monsterName;
		public int id;

		void Awake()
		{
			switch(mT)
			{
			case LevelEditor.MonsterSpawnOrder.MONSTER_TYPE.Penguin:
				monsterType = "penguin";
				break;
			case LevelEditor.MonsterSpawnOrder.MONSTER_TYPE.Slime:
				monsterType = "slime";
				break;
			case LevelEditor.MonsterSpawnOrder.MONSTER_TYPE.Bird:
				monsterType = "bird";
				break;
			case LevelEditor.MonsterSpawnOrder.MONSTER_TYPE.Bean:
				monsterType = "bean";
				break;
			case LevelEditor.MonsterSpawnOrder.MONSTER_TYPE.Disgusting_Thing:
				monsterType = "disgusting";
				break;
			}
		}
	}
}
