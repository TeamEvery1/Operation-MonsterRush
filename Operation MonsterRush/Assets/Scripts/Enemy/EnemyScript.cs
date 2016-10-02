using UnityEngine;
using System.Collections;


public class EnemyScript : MonoBehaviour {

	NavMeshAgent GPS;
	bool SawPlayer = false;
	public Transform[] DesPoint;
	public Transform[] WanderPoint;
	int RandomDes = 0;
	public float Speed;
	bool IsWander = false;
	int turn = 0;
	public float SafeToBackTimer;
	public float StaminaRcvrSpeed;
	float timer;
	float StartX;
	float StartY;
	float StartZ;

	public float MaxStamina;
	float Stamina;
	bool Recovering = false;

	public float enemyHealth;

	public float ViewRadius;
	[Range(0,360)]	public float ViewAngle;

	public LayerMask PlayerLayer;
	public LayerMask ObstacleLayer;

	[HideInInspector] public Transform VisibleTarget = null;

	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
		GPS = this.gameObject.GetComponent<NavMeshAgent>();
		GPS.autoBraking = false;
		GPS.speed = Speed;

		StartX = this.transform.position.x;
		StartY = this.transform.position.y;
		StartZ = this.transform.position.z;
		Stamina = MaxStamina;
		if(WanderPoint.Length > 1)
		{
			IsWander = true;
		}

		Move();
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
		Collider[] TargetInVisibleRadius = Physics.OverlapSphere(transform.position, ViewRadius, PlayerLayer);

		for(int i = 0; i< TargetInVisibleRadius.Length; i++)
		{
			Transform target = TargetInVisibleRadius[i].transform;
			Vector3 DirToTarget = (target.position - transform.position).normalized;
			if(Vector3.Angle (transform.forward, DirToTarget) < ViewAngle / 2)
			{
				float DstToTarget = Vector3.Distance(transform.position,target.position);

				if(!Physics.Raycast(transform.position, DirToTarget, DstToTarget, ObstacleLayer))
				{
					VisibleTarget = target;
				}
			}

		}
	}

	public Vector3 DirFromAngle(float AngleInDegree , bool AngleIsGlobal)
	{
		if(!AngleIsGlobal)
		{
			AngleInDegree += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0 , Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
	}

	void Move()
	{
		if(IsWander == false)
		{

			if(turn == 0)
			{
				//Debug.Log("ss");
				GPS.SetDestination(new Vector3(WanderPoint[0].position.x, WanderPoint[0].position.y, WanderPoint[0].position.z));
				turn = 1;
			}
			else if(turn == 1)
			{
				//Debug.Log("s2s");
				//GPS.destination = PatrolToPos.position;
				GPS.SetDestination(new Vector3(StartX, StartY, StartZ));
				turn = 0;
			}
		}

		if(IsWander == true)
		{
			RandomDes = Random.Range(0, WanderPoint.Length);

			if(RandomDes == WanderPoint.Length)
			{
				GPS.SetDestination(new Vector3(StartX, StartY, StartZ));
			}
			else
			{
				GPS.SetDestination(new Vector3(WanderPoint[RandomDes].position.x , WanderPoint[RandomDes].position.y, WanderPoint[RandomDes].position.z));
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(VisibleTarget != null && SawPlayer == false)
		{
			SawPlayer = true;
			RandomDes = Random.Range(0,3);
			GPS.destination = DesPoint[RandomDes].position;

		}

		if(SawPlayer == true)
		{
			if(GPS.remainingDistance < 0.5f)
			{
				RandomDes = Random.Range(0,3);
				GPS.destination = DesPoint[RandomDes].position;
			}

			if(GPS.remainingDistance > 0.5f)
			{
				if(Stamina > 0.0f && Recovering == false)
				{
					//Debug.Log("stamina: " + Stamina);
					Stamina -= Time.deltaTime;
					if(Stamina <= 0.0f)
					{
						Recovering = true;
					}
				}
			}

			if(VisibleTarget == null)
			{
				timer += Time.deltaTime;
				if(timer >= SafeToBackTimer)
				{
					GPS.SetDestination(new Vector3(StartX, StartY, StartZ));
					SawPlayer = false;
				}
			}
		}

		if(VisibleTarget == null && SawPlayer == false)
		{
			if(GPS.remainingDistance < 0.5f)
			{
				Move();
			}

			if(GPS.remainingDistance > 0.5f)
			{
				
				if(Stamina > 0.0f && Recovering == false)
				{
					//Debug.Log("stamina: " + Stamina);
					Stamina -= Time.deltaTime;
					if(Stamina <= 0.0f)
					{
						Recovering = true;
					}
				}



			}


		}

		if( Recovering == true)
		{
			GPS.speed = 0.0f;
			Stamina += Time.deltaTime * 3.0f * StaminaRcvrSpeed;
			if(Stamina >= MaxStamina)
			{
				GPS.speed = Speed;
				Recovering = false;
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

