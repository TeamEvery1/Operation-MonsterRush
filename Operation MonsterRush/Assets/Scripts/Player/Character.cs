namespace Player
{
	public class Character
	{
		private float movementSpeed;
		private float health;
		private int damage;
		private string name;

		public Character() {}
		public Character( float mS, float h, int d, string n )
		{
			movementSpeed = mS;
			health = h;
			damage = d;
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
		public int Damage
		{
			get { return damage; }
			set { damage = value; }
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion
	}
}
