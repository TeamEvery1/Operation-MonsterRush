namespace Enemies
{
	public class Character
	{
		private float exhaustionAmount;
		private float maxExhaustion;
		private float health;
		private float maxHealth;
		private float stamina;
		private float maxStamina;
		private float recoveryRate;
		private float movementSpeed;

		public Character(float eA, float mE, float h, float mH, float s, float mST, float rR, float mS)
		{
			exhaustionAmount = eA;
			maxExhaustion = mE;
			health = h;
			maxHealth = mH;
			stamina = s;
			maxStamina = mST;
			recoveryRate = rR;
			movementSpeed = mS;
		}

		#region Basic Getters and Setters
		public float ExhaustionAmount
		{
			get {return exhaustionAmount;}
			set {exhaustionAmount = value;}
		}
		public float MaxExhaustion
		{
			get {return maxExhaustion;}
			set {maxExhaustion = value;}
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
		public float MaxStamina
		{
			get {return maxStamina;}
			set {maxStamina = value;}
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
