using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour 
{
	[System.Serializable]
	public class MonsterSpawnOrder
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

		public int monsterID;
		public MONSTER_TYPE monsterType;
		public string[] monsterName;
		public GameObject monsterPrefab;
		public Transform[] desPoint;
		public Transform[] wanderPoint;
		public Vector3 monsterPosition;
	}

	public List <MonsterSpawnOrder> monsterList = new List <MonsterSpawnOrder> (1);

	void Add()
	{
		monsterList.Add (new MonsterSpawnOrder());
	}

	void Remove (int index)
	{
		monsterList.RemoveAt (index);
	}
}
