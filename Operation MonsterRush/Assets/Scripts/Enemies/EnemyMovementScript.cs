using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour {

	GameObject player = null;
	GameObject Target = null;
	float dist = 0f;
	public float DetectDist;
	NavMeshAgent GPS;

	public float enemyHealth = 30;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		GPS = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		dist = Vector3.Distance(player.transform.position, this.transform.position);

		GPS.SetDestination(GameObject.Find("Endpoint").transform.position);
		if(dist <= DetectDist)
		{

			Target = FindClosestTarget();
			//GPS.SetDestination(Target.transform.position);
		}
		else if(dist > DetectDist)
		{
			
		}

	}

	GameObject FindClosestTarget() {
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
	}
}

