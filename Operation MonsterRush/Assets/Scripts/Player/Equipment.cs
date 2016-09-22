using UnityEngine;
using System;
using System.Collections;

namespace Player
{
	public class Equipment : Player.EquipmentInfo
	{
		private string equipmentName;
		private int attackCounter;
		private int totalAttackCounter;

		public Equipment() {}

		public Equipment( string eN, int aC, int tAC )
		{
			equipmentName = eN;
			attackCounter = aC;
			totalAttackCounter = tAC;
		}

		#region Basic Getters and Setters
		public string EquipmentName
		{
			get { return equipmentName; }
			set { equipmentName = value; }
		}

		public int AttackCounter
		{
			get { return attackCounter; }
			set { attackCounter = value; }
		}

		public int TotalAttackCounter
		{
			get { return totalAttackCounter; }
			set { totalAttackCounter = value; }
		}
		#endregion
	}
}
