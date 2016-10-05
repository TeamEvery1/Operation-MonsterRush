using UnityEngine;
using System.Collections;

public class CaptureCollider : MonoBehaviour 
{
	public bool enemyCollided = false;
	public bool fillUpMode = false;
	public float enemyHealthInfo;
	public float timeLimit = 10;
	public float timeLimitModifier;
	// Use this for initialization
	void Start () 
	{
		enemyHealthInfo = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	/*public void Capture()
	{
		if (enemyCollided) 
		{
			Debug.Log ("Wake Me Up");

		} 
		else 
		{
			Debug.Log ("Wake Me Up Inside");

		}
	}*/


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			fillUpMode = true;
			enemyHealthInfo = other.GetComponent<Enemies.Pathfinding>().enemyHealth;
			timeLimitModifier = (100 - enemyHealthInfo)/10;
			timeLimit = timeLimit + timeLimitModifier;
		}
	}
	/*void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			fillUpMode = false;
		}
	}*/
}
