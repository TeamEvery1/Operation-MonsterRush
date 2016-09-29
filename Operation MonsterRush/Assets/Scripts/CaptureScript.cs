using UnityEngine;
using System.Collections;

public class CaptureScript : MonoBehaviour {

	public bool enemyCollided = false;
	public bool fillUpMode = false;
	public float enemyHealthInfo;
	public float timeLimit = 10;
	public float timeLimitModifier;
	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fillUpMode) 
		{
			
		}

	}

	public void Capture()
	{
		if (enemyCollided) 
		{
			Debug.Log ("Wake Me Up");
			fillUpMode = true;
		} 
		else 
		{
			Debug.Log ("Wake Me Up Inside");

		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			enemyCollided = true;
			enemyHealthInfo = other.GetComponent<EnemyMovementScript>().enemyHealth;
			timeLimitModifier = (100 - enemyHealthInfo)/10;
			timeLimit = timeLimit + timeLimitModifier;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			fillUpMode = false;
		}
	}
}
