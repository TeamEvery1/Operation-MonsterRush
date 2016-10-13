using UnityEngine;
using System.Collections;

public class CaptureCollider : MonoBehaviour 
{
	public bool enemyCollided = false;
	public bool fillUpMode = false;
	public float enemyExhaustInfo;
	public float timeLimit = 30;
	public float timeLimitModifier;
	// Use this for initialization
	void Start () 
	{
		enemyExhaustInfo = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	/*public void Capture()
	{
		if (enemyCollided) 
		{
			
		} 
		else 
		{

		}
	}*/


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy") 
		{
			fillUpMode = true;
			enemyExhaustInfo = other.GetComponent<Enemies.Pathfinding>().enemyExhaustion;
			timeLimitModifier = (100 - enemyExhaustInfo)/10;
			timeLimit = timeLimit + timeLimitModifier;
			if (timeLimit>20)
			{
				timeLimit = 20;
			}
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
