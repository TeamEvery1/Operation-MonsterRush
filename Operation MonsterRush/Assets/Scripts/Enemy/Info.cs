using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemies
{
	public class Data
	{
		private string name;
		private string type;
		private string gender;
		private int age;
		private int yearOfBirth;
		private int monthOfBirth;
		private int dayOfBirth;
		private float weight;

		public Data()
		{
			
		}

		public Data(string n, string t, string g, int a, int yOB, int mOB, int dOB, float w)
		{
			name = n;
			type = t;
			gender = g;
			age = a;
			yearOfBirth = yOB;
			monthOfBirth = mOB;
			dayOfBirth = dOB;
			weight = w;
		}

		#region Basic Getters and Setters
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public string Gender
		{
			get { return gender; }
			set { gender = value; }
		}

		public int Age
		{
			get { return age; }
			set { age = value; }
		}

		public int YearOfBirth
		{
			get { return yearOfBirth; }
			set { yearOfBirth = value; }
		}

		public int MonthOfBirth
		{
			get { return monthOfBirth; }
			set { monthOfBirth = value; }
		}

		public int DayOfBirth
		{
			get { return dayOfBirth; }
			set { dayOfBirth = value; }
		}

		public float Weight
		{
			get { return weight; }
			set { weight = value; }
		}
		#endregion
	}

	public static class ArrayExtension 
	{
		public static T RandomItem <T> (this T[] array)
		{
			return array [Random.Range (0, array.Length)];
		}
	}

	public class Info : MonoBehaviour 
	{
		private Enemies.Selection enemySelectionScript; 

		public Data monsterData;

		private string gender;

		void Awake ()
		{
			enemySelectionScript = GetComponent <Enemies.Selection> ();
		}

		void Start ()
		{
			monsterData = new Data ("", "", "", 0, 0, 0, 0, 0.0f);

			string[] names = { "Elizabeth", "Mona Lisa", "Bob Ross", "Martin", "Monkey D Luffy", "Gordon Ramsay", "Hanji Shimada", "Teh Yong Quan.Jr" };

			string[] genders = { "Male", "Female" };

			if (enemySelectionScript.monsterType == "Bean")
			{
				monsterData.Name = "Teh Yong Quan";
			}
			else
			{
				monsterData.Name = names.RandomItem();
			}

			monsterData.Type = enemySelectionScript.monsterType;
			monsterData.Gender = genders.RandomItem();
			monsterData.Age = Random.Range (16, 71);
			monsterData.YearOfBirth = Random.Range (500, 2017);
			monsterData.MonthOfBirth = Random.Range (1, 13);
			monsterData.DayOfBirth = Random.Range (1, 32);
			monsterData.Weight = Random.Range (20,61);
		}
	}
}
