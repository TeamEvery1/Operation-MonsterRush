using UnityEngine;
using System;
using System.Collections;


namespace Enemies
{
	public class Info : MonoBehaviour
	{
		//public MonsterDictionary monsterDictionary1;
		public string monsterName;

		[SerializeField()] private MonsterDictionary _dictionary;

		public MonsterDictionary Dictionary
		{
			get { return _dictionary; }
		}

		[System.Serializable()]
		public class MonsterDictionary : SerializableDictionary <string, int>
		{
			
		}

		void Start()
		{
			/*int rnd = UnityEngine.Random.Range (1, 4);
			int index = 0;

			foreach (var kvp in monsterDictionary1)
			{
				index++; 

				if(index == rnd)
				{
					monsterName = kvp.Value;

				}
			}*/
		}

		void Update()
		{
		}

	}

}
