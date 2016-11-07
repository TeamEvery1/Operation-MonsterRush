using UnityEngine;
using System.Collections;

namespace Enemies
{
	public class SlimeBehaviour : MonoBehaviour 
	{
		Enemies.Character slime;
		Enemies.Info monsterSelection;

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

		public Transform target;
		[HideInInspector] public Transform VisibleTarget = null;
	
		private GameObject bullet;
		public float gooFireRate;

		public float viewRadius;
		[Range(0,360)]	public float viewAngle;

		float timer;

		public LayerMask ground;
		public LayerMask PlayerLayer;
		public LayerMask ObstacleLayer;

		RaycastHit hitInfo;

		void Awake()
		{
			slime = new Enemies.Character (enemyInfo.enemyExhaustion, enemyInfo.enemyMaxExhaustion, enemyInfo.enemyHealth, enemyInfo.enemyMaxHealth, 
				enemyInfo.enemyStamina, enemyInfo.enemyMaxStamina, enemyInfo.staminaRcvrSpeed, enemyInfo.enemyMovementSpeed);

			bullet = (GameObject) Resources.Load ("Prefabs/Monsters/Goo");
		}
		void Start()
		{
			StartCoroutine("FindTargetsWithDelay", .2f);
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

		void Update()
		{
			if (Physics.Raycast (this.transform.position, Vector3.down, out hitInfo, ground))
			{
				if (hitInfo.collider.tag == "Ship")
				{
					this.transform.position = new Vector3 (hitInfo.transform.GetChild(0).transform.position.x, hitInfo.transform.position.y - 0.2f , hitInfo.transform.GetChild(0).position.z - 0.4f);
					if (VisibleTarget == null)
					{
						Quaternion rotation = Quaternion.LookRotation ( hitInfo.transform.parent.GetComponent <RowController> ().patrolPoint[hitInfo.transform.parent.GetComponent <RowController> ().currentPoint].position - this.transform.position);
						rotation.x = 0;
						rotation.z = 0;

						transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 0.5f * Time.deltaTime);
					}
					//else 
				}
			}

			if(VisibleTarget != null)
			{
				transform.LookAt(VisibleTarget);
				timer += Time.deltaTime;
				if (timer >= gooFireRate) {
					timer = 0.0f;
				//Audio.PlayOneShot(ShootSound, 1f);
				GameObject goo = (GameObject)Instantiate (bullet, new Vector3 (this.transform.position.x + 0.2f, this.transform.position.y + 0.6f, this.transform.position.z) , Quaternion.identity);
				goo.GetComponent<Bullet> ().targetPos = VisibleTarget.position;
				//goo.GetComponent<Rigidbody>().AddForce((VisibleTarget.position- goo.transform.position).normalized * 3.0f);
				}
			}

		}

		void CloseUp()
		{
			timer = gooFireRate;
		}



	}
}
