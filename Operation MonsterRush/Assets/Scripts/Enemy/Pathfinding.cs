using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class Pathfinding : MonoBehaviour 
	{
		public NavMeshAgent GPS;

		public Transform target;

		public LayerMask ground;

		private Vector3 rotation;

		Enemies.Character penguin, bird, bean, disgustingThing; //slime
		Enemies.Info monsterSelection;

		public Transform[] desPoint;
		public Transform[] wanderPoint;

		int RandomDes = 0;
		int turn = 0;

		[System.Serializable]
		public struct EnemyInfo
		{
			public float enemyHealth;
			public float enemyMaxHealth;
			public float enemyExhaustion;
			public float enemyMaxExhaustion;
			public float staminaRcvrSpeed;
			public float enemyStamina;
			public float enemyMaxStamina;
			public float enemyMovementSpeed;
		}
		public EnemyInfo enemyInfo;

		public float safeToBackTimer;
		float changeDesTimer = 5.5f;

		public float viewRadius;
		[Range(0,360)]	public float viewAngle;

		float timer;
		float timer2 = 0.0f;
		float startX;
		float startY;
		float startZ;

		[HideInInspector] public bool sawPlayer = false;
		bool isWander = false;
		public bool recovering = false;
		public bool isShit = false;
		private bool isAttacking = false;
		private bool isWalking = false;
		private bool isAlerting = false;
		private bool isTiring = false;

		public LayerMask PlayerLayer;
		public LayerMask ObstacleLayer;

		private Player.Movement playerMovementScript;

		GameObject player;
		float Dist;
		bool InsideRange = false;
		public bool IsKnockBacking;

		Animator anim;

		public GameObject alert;

		//public GameObject bullet;
		//public float gooFireRate;


		[HideInInspector] public Transform VisibleTarget = null;

		void Awake()
		{
			// eA = exhaustion amount
			// mE = maximum exhaustion amount
			// h = health (catching mode)
			// mH = maximum health (catching mode)
			// s = stamina 
			// rR = recovery rate - Stamina recovery rate after monster exhausted
			// mS = movement Speed

			// monster Type					  eA   mE    h   mH    s  rR  mS
			penguin = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, 
				enemyInfo.enemyStamina, enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			/*slime = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, 
				enemyInfo.enemyStamina, enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);*/
			bird = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, 
				enemyInfo.enemyStamina, enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			bean = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, 
				enemyInfo.enemyStamina, enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			disgustingThing = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, 
				enemyInfo.enemyStamina, enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);

			monsterSelection = GetComponent <Enemies.Info> ();
		}

		void Start()
		{
			playerMovementScript = FindObjectOfType <Player.Movement> ();
			anim = this.gameObject.GetComponent <Animator> ();
			StartCoroutine("FindTargetsWithDelay", .06f);
			player = GameObject.FindGameObjectWithTag("Player");
			GPS = this.gameObject.GetComponent <NavMeshAgent> ();
			GPS.autoBraking = true;

			startX = this.transform.position.x;
			startY = this.transform.position.y;
			startZ = this.transform.position.z;

			if(wanderPoint.Length > 1)
			{
				isWander = true;
			}

			if(monsterSelection.monsterType != "disgusting" && monsterSelection.monsterType != "slime")
			{
				Move();
			}
			/*else if(monsterSelection.monsterType == "slime")
			{
				GPS.speed = 0.0f;
			}*/

			if(monsterSelection.monsterType == "penguin")
			{
				penguin.MovementSpeed = enemyInfo.enemyMovementSpeed;
				GPS.speed = enemyInfo.enemyMovementSpeed;
				penguin.MaxStamina = enemyInfo.enemyMaxStamina;
				penguin.Stamina = enemyInfo.enemyStamina;
				penguin.MaxHealth = enemyInfo.enemyMaxHealth;
				penguin.Health = enemyInfo.enemyHealth;
				penguin.MaxExhaustion = enemyInfo.enemyMaxExhaustion;
				penguin.ExhaustionAmount = enemyInfo.enemyExhaustion;
			}
			/*else if(monsterSelection.monsterType == "slime")
			{
				slime.MovementSpeed = enemyInfo.enemyMovementSpeed;
				slime.MaxStamina = enemyInfo.enemyMaxStamina;
				slime.Stamina = enemyInfo.enemyStamina;
				slime.MaxHealth = enemyInfo.enemyMaxHealth;
				slime.Health = enemyInfo.enemyHealth;
				slime.MaxExhaustion = enemyInfo.enemyMaxExhaustion;
				slime.ExhaustionAmount = enemyInfo.enemyExhaustion;
			}*/
			else if(monsterSelection.monsterType == "bird")
			{
				bird.MovementSpeed = enemyInfo.enemyMovementSpeed;
				GPS.speed = enemyInfo.enemyMovementSpeed;
				bird.MaxStamina = enemyInfo.enemyMaxStamina;
				bird.Stamina = enemyInfo.enemyStamina;
				bird.MaxHealth = enemyInfo.enemyMaxHealth;
				bird.Health = enemyInfo.enemyHealth;
				bird.MaxExhaustion = enemyInfo.enemyMaxExhaustion;
				bird.ExhaustionAmount = enemyInfo.enemyExhaustion;
			}
			else if(monsterSelection.monsterType == "bean")
			{
				bean.MovementSpeed = enemyInfo.enemyMovementSpeed;
				GPS.speed = enemyInfo.enemyMovementSpeed;
				bean.MaxStamina = enemyInfo.enemyMaxStamina;
				bean.Stamina = enemyInfo.enemyStamina;
				bean.MaxHealth = enemyInfo.enemyMaxHealth;
				bean.Health = enemyInfo.enemyHealth;
				bean.MaxExhaustion = enemyInfo.enemyMaxExhaustion;
				bean.ExhaustionAmount = enemyInfo.enemyExhaustion;
			}
			else if(monsterSelection.monsterType == "disgusting")
			{
				disgustingThing.MovementSpeed = enemyInfo.enemyMovementSpeed;
				GPS.speed = enemyInfo.enemyMovementSpeed;
				disgustingThing.MaxStamina = enemyInfo.enemyMaxStamina;
				disgustingThing.Stamina = enemyInfo.enemyStamina;
				disgustingThing.MaxHealth = enemyInfo.enemyMaxHealth;
				disgustingThing.Health = enemyInfo.enemyHealth;
				disgustingThing.MaxExhaustion = enemyInfo.enemyMaxExhaustion;
				disgustingThing.ExhaustionAmount = enemyInfo.enemyExhaustion;
			}

		}

		IEnumerator FindTargetsWithDelay(float delay)
		{
			while(true)
			{
				yield return new WaitForSeconds(delay);
				FindVisibleTarget();
			}
		}

		void FindVisibleTarget()
		{
			VisibleTarget = null;
			Collider[] TargetInVisibleRadius = Physics.OverlapSphere(transform.position, viewRadius, PlayerLayer);

			for(int i = 0; i< TargetInVisibleRadius.Length; i++)
			{
				target = TargetInVisibleRadius[i].transform;
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				if(Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
				{
					float DstToTarget = Vector3.Distance(transform.position,target.position);

					if(!Physics.Raycast(transform.position, dirToTarget, DstToTarget, ObstacleLayer))
					{
						if( transform.position.y <= target.transform.position.y - 1.0f && monsterSelection.monsterType != "disgusting")
						{
							VisibleTarget = null;

						}
						else
						{
							VisibleTarget = target;
						}

					}
				}
			}
		}

		public Vector3 dirFromAngle(float AngleInDegree , bool AngleIsGlobal)
		{
			if(!AngleIsGlobal)
			{
				AngleInDegree += transform.eulerAngles.y;
			}
			return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0 , Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
		}

		void Move()
		{
			if(isWander == false)
			{
				if(turn == 0)
				{
					//Debug.Log("ss");
					rotation = new Vector3(wanderPoint[0].position.x, wanderPoint[0].position.y , wanderPoint[0].position.z);
					GPS.SetDestination(rotation);
					turn = 1;
				}
				else if(turn == 1)
				{
					//Debug.Log("s2s");
					//GPS.destination = PatrolToPos.position;
					rotation = new Vector3(startX, startY, startZ);
					GPS.SetDestination(rotation);
					turn = 0;
				}
			}

			if(isWander == true)
			{
				RandomDes = Random.Range(0, wanderPoint.Length);

				if(RandomDes == wanderPoint.Length)
				{
					rotation = new Vector3(startX, startY, startZ);
					GPS.SetDestination(rotation);
				}
				else
				{
					rotation = new Vector3(wanderPoint[RandomDes].position.x , wanderPoint[RandomDes].position.y , wanderPoint[RandomDes].position.z);
					GPS.SetDestination(rotation);
				}
			}
		}

		public void CloseUp()
		{
			playerMovementScript.beingKnockBack = true;
			IsKnockBacking = false;
			sawPlayer = true;
			timer = 0.0f;
			RandomDes = Random.Range(0,3);
			GPS.destination = desPoint[RandomDes].position;
			isAttacking = false;

			/*else if(monsterSelection.monsterType == "slime")
			{
				timer = gooFireRate;
			}*/
		}

		public void Alert()
		{
			//if(monsterSelection.monsterType != "slime")
			//{
			if(monsterSelection.monsterType == "penguin")
			{
				GPS.speed = enemyInfo.enemyMovementSpeed;
			}
			else if(monsterSelection.monsterType == "bird")
			{
				GPS.speed = enemyInfo.enemyMovementSpeed;
			}
			else if(monsterSelection.monsterType == "bean")
			{
				GPS.speed = enemyInfo.enemyMovementSpeed;
			}
			else if(monsterSelection.monsterType == "disgusting")
			{
				GPS.speed = enemyInfo.enemyMovementSpeed;
			}






			IsKnockBacking = true;

			anim.speed = 1.0f;
			//}
			/*else
					{
						sawPlayer = true;
						timer = 0.0f;
						if(monsterSelection.monsterType == "penguin")
						{
							RandomDes = Random.Range(0,3);
							GPS.destination = desPoint[RandomDes].position;
						}
					}*/
		}

		// Update is called once per frame
		void Update () 
		{
			if(this.gameObject.tag == "Slime")
			{
				if (GetComponent <Enemies.SlimeCollision> ())
				{
					anim.SetBool ("beingHit", GetComponent<Enemies.SlimeCollision> ().beingHit);
				}
			}
			else
			{
				if (GetComponent <Enemies.Collision> ())
				{
					anim.SetBool ("beingHit", GetComponent<Enemies.Collision> ().beingHit);
				}
			}
			anim.SetBool ("isAttacking", isAttacking);
			anim.SetBool ("isWalking", isWalking);
			anim.SetBool ("isAlerting", isAlerting);
			anim.SetBool ("isTiring", isTiring);

			//rotation.y = this.transform.position.y;

			//transform.LookAt (rotation);

			if(VisibleTarget != null)
			{
				this.alert.SetActive(true);
			}

			if (isAlerting)
			{
				isWalking = false;
				if(VisibleTarget = null)
				{
					isAlerting = false;
				}
			}

			if(this.gameObject.tag == "Slime")
			{
				if (GetComponent <Enemies.SlimeCollision> ())
				{
					if (GetComponent<Enemies.SlimeCollision> ().beingHit)
					{
						GPS.speed = 0.0f;
						isAttacking = false;
						isWalking = false;
						isTiring = false;
						isAlerting = false;
					}

				}
			}
			else
			{
				if (GetComponent <Enemies.Collision> ())
				{
					if (GetComponent<Enemies.Collision> ().beingHit)
					{
						GPS.speed = 0.0f;
						isAttacking = false;
						isWalking = false;
						isTiring = false;
						isAlerting = false;
					}
					else
					{
						GPS.speed = enemyInfo.enemyMovementSpeed;
					}
				}
			}

			if(recovering == false)
			{

				//! First action when face target
				if(VisibleTarget != null && sawPlayer == false && IsKnockBacking == false)
				{
					if(monsterSelection.monsterType == "disgusting")
					{
						GPS.baseOffset = -0.26f;
					}
					anim.speed = 2.5f;
					isAlerting = true;
				}

				//!proceed Close-Up action
				if(IsKnockBacking == true)
				{
					GPS.speed = enemyInfo.enemyMovementSpeed * 3.5f;
					anim.speed = 1.0f * 3.5f;
					Dist = Vector3.Distance(new Vector3(startX, startY, startZ), new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));

					// if player too for give up knock back and escape
					if(GPS.remainingDistance + 20.0f  <  Dist )
					{
						Debug.Log("gv up");
						IsKnockBacking = false;
						sawPlayer = true;
						timer = 0.0f;
						RandomDes = Random.Range(0,3);
						//GPS.destination = desPoint[RandomDes].position;
						GPS.SetDestination(desPoint[RandomDes].position);
						isAttacking = false;
						isAlerting = false;
					}
					else if(GPS.remainingDistance <= 0.8f) // knock back
					{
						isAlerting = false;
						isAttacking = true;
						isWalking = false;
					}
					else
					{
						GPS.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
					}
				}


				//!proceeed Escape cycle action
				if(sawPlayer == true && GetComponent <Enemies.Collision> ().beingHit == false)
				{
					this.alert.SetActive(false);
					GPS.speed = enemyInfo.enemyMovementSpeed * 2.5f;
					anim.speed = 1.0f * 2.5f;
					//SoundManagerScript.Instance.PlayLoopingSFX (AudioClipID.SFX_MONSTERALERT);
					viewAngle = 360.0f;
					//if(monsterSelection.monsterType != "slime")
					//{
					//Debug.Log("Escape");
					if(GPS.remainingDistance < 0.05f)
					{
						RandomDes = Random.Range(0,3);
						GPS.destination = desPoint[RandomDes].position;
					}

					if(GPS.remainingDistance > 0.05f)
					{
						if(enemyInfo.enemyStamina > 0.0f && recovering == false)
						{
							//Debug.Log("stamina: " + Stamina);
							enemyInfo.enemyStamina -= Time.deltaTime;
							isTiring = false;
							isWalking = true;
							/*else if(monsterSelection.monsterType == "bean")
							{
								anim.Play("Walk");
							}*/
							if(enemyInfo.enemyStamina <= 0.0f)
							{
								isTiring = true;
								isWalking = false;
								recovering = true;
							}
						}
					}

					if(VisibleTarget == null)
					{
						timer2 = 0.0f;
						timer += Time.deltaTime;
						if(timer >= safeToBackTimer)
						{
							timer = 0.0f;
							GPS.SetDestination(new Vector3(startX, startY, startZ));
							sawPlayer = false;
							viewAngle = 200.0f;
							//SoundManagerScript.Instance.StopLoopingSFX (AudioClipID.SFX_MONSTERALERT);

						}
					}
					else if(VisibleTarget != null)
					{
						timer = 0.0f;
						timer2 += Time.deltaTime;
						if(timer2 >= changeDesTimer)
						{
							timer2 = 0.0f;
							RandomDes = Random.Range(0,3);
							GPS.destination = desPoint[RandomDes].position;

						}

					}
				}
				/*else if(monsterSelection.monsterType == "slime") //! Split goo
					{
						//Debug.Log("bean");
						if(VisibleTarget != null)
						{
							transform.LookAt(VisibleTarget);
							timer += Time.deltaTime;
							if (timer >= gooFireRate) {
								timer = 0.0f;
								//Audio.PlayOneShot(ShootSound, 1f);
								GameObject goo = (GameObject)Instantiate (bullet, this.transform.position, bullet.transform.rotation);
								goo.GetComponent<Bullet> ().targetPos = VisibleTarget.position;
							}
						}
						else if(VisibleTarget == null)
						{
							//timer = 0.0f;
							sawPlayer = false;
						}
					}
				}*/

				//!When no visible target and wandering
				if(VisibleTarget == null && sawPlayer == false)
				{

					GPS.speed = enemyInfo.enemyMovementSpeed;
					anim.speed = 1.0f;

					Dist = Vector3.Distance(new Vector3(startX, startY, startZ), new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));

					if(Dist <= viewRadius && InsideRange == false)
					{
						InsideRange = true;
						if(Random.Range(0.0f, 1.0f) >= 0.55f)
						{
							GPS.speed = 0.0f;
							transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
						}
					}
					else if( Dist > viewRadius)
					{
						InsideRange = false;
					}

					if(monsterSelection.monsterType == "disgusting")
					{
						if(GPS.remainingDistance <= 0.05f)
						{
							isTiring = false;
							isWalking = false;
							isAlerting = false;
							isAttacking = false;

							anim.speed = 1.3f;
							GPS.baseOffset = -1.46f;
						}
					}
					/*else if(monsterSelection.monsterType == "slime")
					{
						RaycastHit hitInfo;

						if (Physics.Raycast (this.transform.position, Vector3.down, out hitInfo, ground))
						{
							if (hitInfo.collider.tag == "Ship" || hitInfo.collider.tag == "Surfboard")
							{
								this.transform.position = hitInfo.transform.position;
							}
						}
					}*/
					else if(monsterSelection.monsterType != "disgusting") //! temporary luso cant move  && monsterSelection.monsterType != "slime"
					{
						//Debug.Log("walk");
						if(GPS.remainingDistance < 0.05f)
						{
							Move();
						}

						if(GPS.remainingDistance > 0.05f)
						{
							if(enemyInfo.enemyStamina > 0.0f && recovering == false && !isAlerting)
							{
								//Debug.Log("stamina: " + Stamina);
								isTiring = false;
								isWalking = true;

								enemyInfo.enemyStamina -= Time.deltaTime/*3.0f*/;

								//!Monster Tired Condition (after 50% stamina consume)
								if(monsterSelection.monsterType == "penguin")
								{
									GPS.speed = penguin.MovementSpeed;
									anim.speed = 2.0f;
									if(enemyInfo.enemyStamina <= penguin.MaxStamina * 0.5f)
									{
										recovering = true;
									}
								}
								/*else if(monsterSelection.monsterType == "slime")
								{
									if(enemyInfo.enemyStamina  <= slime.MaxStamina * 0.5f)
									{
										recovering = true;
									}
								}*/
								else if(monsterSelection.monsterType == "bird")
								{
									GPS.speed = bird.MovementSpeed;
									anim.speed = 2.0f;
									if(enemyInfo.enemyStamina  <= bird.MaxStamina * 0.5f)
									{
										recovering = true;
									}
								}
								else if(monsterSelection.monsterType == "bean")
								{
									GPS.speed = bean.MovementSpeed;
									if(enemyInfo.enemyStamina  <= bean.MaxStamina * 0.5f)
									{

										recovering = true;
									}
								}
							}
						}
					}


				}
			}
				//! Recovering stamina
			if(recovering == true)
			{
				isWalking = false;
				isTiring = true;
				GPS.speed = 0.0f;
				enemyInfo.enemyStamina += Time.deltaTime * enemyInfo.staminaRcvrSpeed ;

				if(enemyInfo.enemyStamina >= enemyInfo.enemyMaxStamina)
				{
					if(monsterSelection.monsterType == "penguin")
					{
						if(enemyInfo.enemyStamina >= penguin.Stamina)
						{
							GPS.speed = penguin.MovementSpeed;
							recovering = false;
						}
					}
					/*else if(monsterSelection.monsterType == "slime")
					{
						if(enemyInfo.enemyStamina >= slime.Stamina)
						{
							GPS.speed = slime.MovementSpeed;
							recovering = false;
						}
					}*/
					else if(monsterSelection.monsterType == "bird")
					{
						if(enemyInfo.enemyStamina >= bird.Stamina)
						{
							GPS.speed = bird.MovementSpeed;
							recovering = false;
						}
					}
					else if(monsterSelection.monsterType == "bean")
					{
						if(enemyInfo.enemyStamina >= bean.Stamina)
						{
							GPS.speed = bean.MovementSpeed;
							recovering = false;
						}
					}
					else if(monsterSelection.monsterType == "disgusting")
					{
						if(enemyInfo.enemyStamina >= disgustingThing.Stamina)
						{
							GPS.speed = disgustingThing.MovementSpeed;
							recovering = false;
						}
					}
				}
				else if(enemyInfo.enemyStamina > 10.0f && VisibleTarget != null) // found by player when recovering 
				{
					recovering = false;
				}
			}
			
			else if(enemyInfo.enemyExhaustion <= 0f)
			{
				isWalking = false;
				isTiring = true;
				GPS.Stop();
			}
		}

		/*GameObject FindClosestTarget() {
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag("EscapePoint");
			GameObject closest = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject go in gos)
			{
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance)
				{
					closest = go;
					distance = curDistance;
				}
			}
			return closest;
		}*/

		public void EndAnimation()
		{
			if(gameObject.tag == "Slime")
			{
				GetComponent <Enemies.SlimeCollision> ().beingHit = false;
			}
			else
			{
				GetComponent <Enemies.Collision> ().beingHit = false;
			}
		}

		public void EndAttack()
		{
			isAttacking = false;
		}
	}
}

