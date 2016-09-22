namespace Player
{
	public enum EQUIPMENT_TYPE
	{
		GAUNTLET = 0,
		GAUNTLET_2 = 1,
		RADAR = 2,

		TOTAL
	}

	public class EquipmentInfo
	{
		private string name;
		private string type;
		private string description;
		private int damage;

		public EquipmentInfo(){}

		public EquipmentInfo( string n, string t, string des, int d )
		{
			name = n;
			type = t;
			description = des;
			damage = d;
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

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public int Damage
		{
			get { return damage; }
			set { damage = value; }
		}
		#endregion
	}
}
