using UnityEngine;
using System.Collections;

public class EnemyMovementScript : MonoBehaviour {

	GameObject player = null;
	GameObject Target = null;
	public Transform target;
	float dist = 0f;
	public float DetectDist;
	NavMeshAgent GPS;

	public float enemyHealth = 30;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag("Player");
		GPS = this.gameObject.GetComponent<NavMeshAgent>();
		GPS.SetDestination(new Vector3(target.position.x,target.position.y,target.position.z));
	}
	
	// Update is called once per frame
	void Update () {
		

	}



}

