namespace Player
{
	public class Character
	{
		private float movementSpeed;
		private string name;

		public Character() {}
		public Character( float mS, string n )
		{
			movementSpeed = mS;
			name = n;
		}

		#region Basic Getter and Setter
		public float MovementSpeed
		{
			get { return movementSpeed; }
			set { movementSpeed = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		#endregion
	}
}
