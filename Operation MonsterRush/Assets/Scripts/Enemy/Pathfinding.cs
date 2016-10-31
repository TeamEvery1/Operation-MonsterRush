using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class Pathfinding : MonoBehaviour 
	{
		public NavMeshAgent GPS;

		public Transform target;

		private Vector3 rotation;

		Enemies.Character penguin, slime, bird, bean, disgustingThing;
		Selection monsterSelection;

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

		public bool playerCanMove;

		public float safeToBackTimer;

		public float viewRadius;
		[Range(0,360)]	public float viewAngle;

		float timer;
		float startX;
		float startY;
		float startZ;

		bool sawPlayer = false;
		bool isWander = false;
		bool recovering = false;
		public bool isShit = false;

		public LayerMask PlayerLayer;
		public LayerMask ObstacleLayer;

		private VirtualJoyStickScripts vjs;

		GameObject player;
		float Dist;
		bool IsKnockBacking;

		Animator anim;

		public GameObject bullet;
		public float gooFireRate;


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
			penguin = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, enemyInfo.enemyStamina,
				enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			slime = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, enemyInfo.enemyStamina, 
				enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			bird = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, enemyInfo.enemyStamina, 
				enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			bean = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, enemyInfo.enemyStamina, 
				enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);
			disgustingThing = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, enemyInfo.enemyStamina,
				enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);

			monsterSelection = GetComponent <Selection> ();
		}

		void Start()
		{
			vjs = GameObject.Find("VirtualJoyStickContainer").GetComponent<VirtualJoyStickScripts>();
			anim = this.gameObject.GetComponent <Animator> ();
			StartCoroutine("FindTargetsWithDelay", .2f);
			player = GameObject.FindGameObjectWithTag("Player");
			GPS = this.gameObject.GetComponent <NavMeshAgent> ();
			GPS.autoBraking = false;

			if(monsterSelection.monsterType == "penguin")
			{
				GPS.speed = penguin.MovementSpeed;
				enemyInfo.enemyMaxStamina = penguin.MaxStamina;
				enemyInfo.enemyStamina = penguin.Stamina;
				enemyInfo.enemyMaxHealth = penguin.MaxHealth;
				enemyInfo.enemyHealth = penguin.Health;
				enemyInfo.enemyMaxExhaustion = penguin.MaxExhaustion;
				enemyInfo.enemyExhaustion = penguin.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "slime")
			{
				GPS.speed = slime.MovementSpeed;
				enemyInfo.enemyMaxStamina = slime.MaxStamina;
				enemyInfo.enemyStamina = slime.Stamina;
				enemyInfo.enemyMaxHealth = slime.MaxHealth;
				enemyInfo.enemyHealth = slime.Health;
				enemyInfo.enemyMaxExhaustion = slime.MaxExhaustion;
				enemyInfo.enemyExhaustion = slime.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "bird")
			{
				GPS.speed = bird.MovementSpeed;
				enemyInfo.enemyMaxStamina = bird.MaxStamina;
				enemyInfo.enemyStamina = bird.Stamina;
				enemyInfo.enemyMaxHealth = bird.MaxHealth;
				enemyInfo.enemyHealth = bird.Health;
				enemyInfo.enemyMaxExhaustion = bird.MaxExhaustion;
				enemyInfo.enemyExhaustion = bird.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "bean")
			{
				GPS.speed = bean.MovementSpeed;
				enemyInfo.enemyMaxStamina = bean.MaxStamina;
				enemyInfo.enemyStamina = bean.Stamina;
				enemyInfo.enemyMaxHealth = bean.MaxHealth;
				enemyInfo.enemyHealth = bean.Health;
				enemyInfo.enemyMaxExhaustion = bean.MaxExhaustion;
				enemyInfo.enemyExhaustion = bean.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "disgusting")
			{
				GPS.speed = disgustingThing.MovementSpeed;
				enemyInfo.enemyMaxStamina = disgustingThing.MaxStamina;
				enemyInfo.enemyStamina = disgustingThing.Stamina;
				enemyInfo.enemyMaxHealth = disgustingThing.MaxHealth;
				enemyInfo.enemyHealth = disgustingThing.Health;
				enemyInfo.enemyMaxExhaustion = disgustingThing.MaxExhaustion;
				enemyInfo.enemyExhaustion = disgustingThing.ExhaustionAmount;
			}

			startX = this.transform.position.x;
			startY = this.transform.position.y;
			startZ = this.transform.position.z;

			if(wanderPoint.Length > 1)
			{
				isWander = true;
			}

			if(monsterSelection.monsterType != "disgusting" && monsterSelection.monsterType != "bean")
			{
				Move();
			}
			else if(monsterSelection.monsterType == "bean")
			{
				GPS.speed = 0.0f;
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
						VisibleTarget = target;
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
			vjs.canMove = false;
			IsKnockBacking = false;
			sawPlayer = true;
			timer = 0.0f;
			RandomDes = Random.Range(0,3);
			GPS.destination = desPoint[RandomDes].position;
			if(monsterSelection.monsterType == "disgusting")
			{
				viewAngle = 120.0f;
				GPS.baseOffset = -0.26f;
			}
			else if(monsterSelection.monsterType == "bean")
			{
				timer = gooFireRate;
			}
		}

		// Update is called once per frame
		void Update () 
		{
			//rotation.y = this.transform.position.y;

			//transform.LookAt (rotation);
			if(enemyInfo.enemyExhaustion > 0f)
			{

				//! First action when face target
				if(VisibleTarget != null && sawPlayer == false && IsKnockBacking == false)
				{
					if(monsterSelection.monsterType != "slime" && monsterSelection.monsterType != "penguin" && monsterSelection.monsterType != "bean")//! luso just temporary in this condition
					{

						GPS.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));

						IsKnockBacking = true;
					}
					else
					{
						sawPlayer = true;
						timer = 0.0f;
						if(monsterSelection.monsterType == "penguin")
						{
							RandomDes = Random.Range(0,3);
							GPS.destination = desPoint[RandomDes].position;
						}
					}

				}

				//!proceed Close-Up action
				if(IsKnockBacking == true)
				{

					if(GPS.remainingDistance <= 1.5f)
					{
						if(monsterSelection.monsterType == "bird")
						{
							anim.Play("Hit");
						}
						else
						{
							CloseUp();
						}
					}
				}


				//!proceeed Escape cycle action
				if(sawPlayer == true)
				{
					if(monsterSelection.monsterType != "bean")//! temporary luso become slime
					{
						//Debug.Log("Escape");
						if(GPS.remainingDistance < 0.5f)
						{
							RandomDes = Random.Range(0,3);
							GPS.destination = desPoint[RandomDes].position;
						}

						if(GPS.remainingDistance > 0.5f)
						{
							if(enemyInfo.enemyStamina > 0.0f && recovering == false)
							{
								//Debug.Log("stamina: " + Stamina);
								enemyInfo.enemyStamina -= Time.deltaTime;
								if(monsterSelection.monsterType == "disgusting")
								{
									anim.Play("Walk");
								}
								else if(monsterSelection.monsterType == "bean")
								{
									anim.Play("Walk");
								}
								if(enemyInfo.enemyStamina <= 0.0f)
								{
									anim.Play("Tired");

									recovering = true;
								}
							}
						}

						if(VisibleTarget == null)
						{
							timer += Time.deltaTime;
							if(timer >= safeToBackTimer)
							{
								GPS.SetDestination(new Vector3(startX, startY, startZ));
								sawPlayer = false;
							}
						}
						else if(VisibleTarget != null)
						{
							timer = 0.0f;
						}
					}
					else if(monsterSelection.monsterType == "bean") //! Split goo
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

				}

				//!When no visible target and wandering
				if(VisibleTarget == null && sawPlayer == false)
				{
					Dist = Vector3.Distance(new Vector3(startX, startY, startZ), new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));

					if(Dist <= viewRadius)
					{
						timer += Time.deltaTime;
						if(timer >= 5.0f)
						{
							timer = 0.0f;
							if(Random.Range(0.0f, 1.0f) > 0.9f)
							{
								transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
							}
						}
					}

					if(monsterSelection.monsterType == "disgusting")
					{
						if(GPS.remainingDistance <= 0.5f)
						{
							anim.Play("Idle");
							GPS.baseOffset = -1.46f;
							viewAngle = 360.0f;
						}
					}
					else if(monsterSelection.monsterType == "bean")
					{
						anim.Play("Idle");
					}
					else if(monsterSelection.monsterType != "disgusting" && monsterSelection.monsterType != "bean") //! temporary luso cant move
					{
						//Debug.Log("walk");
						if(GPS.remainingDistance < 0.5f)
						{
							Move();
						}

						if(GPS.remainingDistance > 0.5f)
						{

							if(enemyInfo.enemyStamina > 0.0f && recovering == false)
							{
								//Debug.Log("stamina: " + Stamina);
								if(monsterSelection.monsterType == "bean")
								{

									anim.Play("Walk");
								}
								enemyInfo.enemyStamina -= Time.deltaTime *3.0f;

								//!Monster Tired Condition (after 50% stamina consume)
								if(monsterSelection.monsterType == "penguin")
								{
									if(enemyInfo.enemyStamina <= penguin.MaxStamina * 0.5f)
									{
										recovering = true;
									}
								}
								else if(monsterSelection.monsterType == "slime")
								{
									if(enemyInfo.enemyStamina  <= slime.MaxStamina * 0.5f)
									{
										recovering = true;
									}
								}
								else if(monsterSelection.monsterType == "bird")
								{
									if(enemyInfo.enemyStamina  <= bird.MaxStamina * 0.5f)
									{
										recovering = true;
									}
								}
								else if(monsterSelection.monsterType == "bean")
								{
									if(enemyInfo.enemyStamina  <= bean.MaxStamina * 0.5f)
									{

										recovering = true;
									}
								}
							}
						}
					}


				}

				//! Recovering stamina
				if(recovering == true)
				{
					anim.Play("Tired");
					GPS.speed = 0.0f;
					enemyInfo.enemyStamina += Time.deltaTime * enemyInfo.staminaRcvrSpeed ;

					if(monsterSelection.monsterType == "penguin")
					{
						if(enemyInfo.enemyStamina >= penguin.Stamina)
						{
							GPS.speed = penguin.MovementSpeed;
							recovering = false;
						}
					}
					else if(monsterSelection.monsterType == "slime")
					{
						if(enemyInfo.enemyStamina >= slime.Stamina)
						{
							GPS.speed = slime.MovementSpeed;
							recovering = false;
						}
					}
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
			}
			else if(enemyInfo.enemyExhaustion <= 0f)
			{
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
	}
}

