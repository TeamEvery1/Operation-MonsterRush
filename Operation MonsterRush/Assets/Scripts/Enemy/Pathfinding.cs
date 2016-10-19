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
		public float enemyHealth;
		public float enemyMaxHealth;
		public float enemyExhaustion;
		public float enemyMaxExhaustion;

		public bool playerCanMove;

		public float safeToBackTimer;
		public float staminaRcvrSpeed;
		public float stamina;
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
			penguin = new Enemies.Character (enemyExhaustion, enemyMaxExhaustion, enemyHealth, enemyMaxHealth, stamina, staminaRcvrSpeed, 1);
			slime = new Enemies.Character (enemyExhaustion, enemyMaxExhaustion, enemyHealth, enemyMaxHealth, stamina, staminaRcvrSpeed, 1);
			bird = new Enemies.Character (enemyExhaustion, enemyMaxExhaustion, enemyHealth, enemyMaxHealth, stamina, staminaRcvrSpeed, 1);
			bean = new Enemies.Character (enemyExhaustion, enemyMaxExhaustion, enemyHealth, enemyMaxHealth, stamina, staminaRcvrSpeed, 1);
			disgustingThing = new Enemies.Character (enemyExhaustion, enemyMaxExhaustion, enemyHealth, enemyMaxHealth, stamina, staminaRcvrSpeed, 1);

			monsterSelection = GetComponent <Selection> ();
		}
	
		void Start()
		{
			vjs = GameObject.Find("VirtualJoyStickContainer").GetComponent<VirtualJoyStickScripts>();
			StartCoroutine("FindTargetsWithDelay", .2f);
			player = GameObject.FindGameObjectWithTag("Player");
			GPS = this.gameObject.GetComponent <NavMeshAgent> ();
			GPS.autoBraking = false;

			if(monsterSelection.monsterType == "penguin")
			{
				GPS.speed = penguin.MovementSpeed;
				stamina = penguin.Stamina;
				enemyMaxHealth = penguin.MaxHealth;
				enemyHealth = penguin.Health;
				enemyMaxExhaustion = penguin.MaxExhaustion;
				enemyExhaustion = penguin.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "slime")
			{
				GPS.speed = slime.MovementSpeed;
				stamina = slime.Stamina;
				enemyMaxHealth = slime.MaxHealth;
				enemyHealth = slime.Health;
				enemyMaxExhaustion = slime.MaxExhaustion;
				enemyExhaustion = slime.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "bird")
			{
				GPS.speed = bird.MovementSpeed;
				stamina = bird.Stamina;
				enemyMaxHealth = bird.MaxHealth;
				enemyHealth = bird.Health;
				enemyMaxExhaustion = bird.MaxExhaustion;
				enemyExhaustion = bird.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "bean")
			{
				GPS.speed = bean.MovementSpeed;
				stamina = bean.Stamina;
				enemyMaxHealth = bean.MaxHealth;
				enemyHealth = bean.Health;
				enemyMaxExhaustion = bean.MaxExhaustion;
				enemyExhaustion = bean.ExhaustionAmount;
			}
			else if(monsterSelection.monsterType == "disgusting")
			{
				GPS.speed = disgustingThing.MovementSpeed;
				stamina = disgustingThing.Stamina;
				enemyMaxHealth = disgustingThing.MaxHealth;
				enemyHealth = disgustingThing.Health;
				enemyMaxExhaustion = disgustingThing.MaxExhaustion;
				enemyExhaustion = disgustingThing.ExhaustionAmount;
			}
				
			startX = this.transform.position.x;
			startY = this.transform.position.y;
			startZ = this.transform.position.z;
				
			if(wanderPoint.Length > 1)
			{
				isWander = true;
			}

			if(isShit == false)
			{
				Move();
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
		
		// Update is called once per frame
		void Update () 
		{
			//rotation.y = this.transform.position.y;

			transform.LookAt (rotation);

			if(VisibleTarget != null && sawPlayer == false && IsKnockBacking == false)
			{
				GPS.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));

				IsKnockBacking = true;

			}

			if(IsKnockBacking == true)
			{
				
				if(GPS.remainingDistance <= 0.5f)
				{
					vjs.canMove = false;
					IsKnockBacking = false;
					sawPlayer = true;
					timer = 0.0f;
					RandomDes = Random.Range(0,3);
					GPS.destination = desPoint[RandomDes].position;
					if(isShit == true)
					{
						viewAngle = 120.0f;
						GPS.baseOffset = -0.26f;
					}
				}
			}

			if(sawPlayer == true)
			{
				if(GPS.remainingDistance < 0.5f)
				{
					RandomDes = Random.Range(0,3);
					GPS.destination = desPoint[RandomDes].position;
				}

				if(GPS.remainingDistance > 0.5f)
				{
					if(stamina > 0.0f && recovering == false)
					{
						//Debug.Log("stamina: " + Stamina);
						stamina -= Time.deltaTime;
						if(stamina <= 0.0f)
						{
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

				if(isShit == true)
				{
					GPS.baseOffset = -1.46f;
					viewAngle = 360.0f;
				}
				else if(isShit == false)
				{
					if(GPS.remainingDistance < 0.5f)
					{
						Move();
					}

					if(GPS.remainingDistance > 0.5f)
					{
						if(stamina > 0.0f && recovering == false)
						{
							//Debug.Log("stamina: " + Stamina);
							stamina -= Time.deltaTime;
							if(stamina <= 0.0f)
							{
								recovering = true;
							}
						}
					}
				}
				

			}

			if(recovering == true)
			{
				GPS.speed = 0.0f;
				stamina += Time.deltaTime * 3.0f * staminaRcvrSpeed;

				if(monsterSelection.monsterType == "penguin")
				{
					if(stamina >= penguin.Stamina)
					{
						GPS.speed = penguin.MovementSpeed;
						recovering = false;
					}
				}
				else if(monsterSelection.monsterType == "slime")
				{
					if(stamina >= slime.Stamina)
					{
						GPS.speed = slime.MovementSpeed;
						recovering = false;
					}
				}
				else if(monsterSelection.monsterType == "bird")
				{
					if(stamina >= bird.Stamina)
					{
						GPS.speed = bird.MovementSpeed;
						recovering = false;
					}
				}
				else if(monsterSelection.monsterType == "bean")
				{
					if(stamina >= bean.Stamina)
					{
						GPS.speed = bean.MovementSpeed;
						recovering = false;
					}
				}
				else if(monsterSelection.monsterType == "disgusting")
				{
					if(stamina >= disgustingThing.Stamina)
					{
						GPS.speed = disgustingThing.MovementSpeed;
						recovering = false;
					}
				}
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

