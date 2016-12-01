using UnityEngine;
using System.Collections;

public class LusoSensor : MonoBehaviour {

	public Enemies.Pathfinding pathfinding;
	//GameObject[] Luso;
	private bool isExit = false;

	void Start () {

		/*Luso = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject luso in Luso)
		{
			if(luso.name == "LusoMega")
			{
				pathfinding = luso.GetComponent<Enemies.Pathfinding>(); 
				break;
			}
		}*/


	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//Debug.Log("sensed");

			if(pathfinding.playerInRange == false)
			{
				//Debug.Log("true");
				pathfinding.playerInRange = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Rock")
		{
			Destroy(other.gameObject);
		}

		if(pathfinding.playerInRange == true)
		{
			pathfinding.playerInRange = false;
		}
	}
}
