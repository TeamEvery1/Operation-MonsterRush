namespace Player
{
	public class Character
	{
		private float movementSpeed;
		private float health;
		private string name;

		public Character() {}
		public Character( float mS, float h, string n )
		{
			movementSpeed = mS;
			health = h;
			name = n;
		}

		#region Basic Getter and Setter
		public float MovementSpeed
		{
			get { return movementSpeed; }
			set { movementSpeed = value; }
		}
		public float Health
		{
			get { return health; }
			set { health = value; }
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion
	}
}
