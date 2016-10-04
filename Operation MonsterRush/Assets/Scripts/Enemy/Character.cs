namespace Enemies
{
	public class Character
	{
		private int exhaustionAmount;
		private float health;
		private float maxHealth;
		private float stamina;
		private float recoveryRate;
		private float movementSpeed;

		public Character(int eA, float h, float mH, float s, float rR, float mS)
		{
			exhaustionAmount = eA;
			health = h;
			maxHealth = mH;
			stamina = s;
			recoveryRate = rR;
			movementSpeed = mS;
		}

		#region Basic Getters and Setters
		public int ExhaustionAmount
		{
			get {return exhaustionAmount;}
			set {exhaustionAmount = value;}
		}
		public float Health
		{
			get {return health;}
			set {health = value;}
		}
		public float MaxHealth
		{
			get {return maxHealth;}
			set {maxHealth = value;}
		}
		public float Stamina
		{
			get {return stamina;}
			set {stamina = value;}
		}
		public float RecoveryRate
		{
			get {return recoveryRate;}
			set {recoveryRate = value;}
		}
		public float MovementSpeed
		{
			get {return movementSpeed;}
			set {movementSpeed = value;}
		}
		#endregion
	}
}
