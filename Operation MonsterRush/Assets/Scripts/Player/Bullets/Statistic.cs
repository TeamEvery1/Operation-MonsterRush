namespace PlayerBullet
{
	public class Statistic
	{
		private float movementSpeed;
		private float damage;
		private float rechargeTime;
		private float radius;
		private string name;
		private string description;

		public Statistic (float mS, float d, float rT, float r, string n, string des)
		{
			movementSpeed = mS;
			damage = d;
			rechargeTime = rT;
			radius = r;
			name = n;
			description = des;
		}

		#region Basic Getters and Setters
		public float MovementSpeed
		{
			get { return movementSpeed; }
			set { movementSpeed = value; }
		}

		public float Damage
		{
			get { return damage; }
			set { damage = value; }
		}

		public float RechargeTime 
		{
			get { return rechargeTime; }
			set { rechargeTime = value; }
		}

		public float Radius 
		{
			get { return radius; }
			set { radius = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion
	}
}
