using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct RowStat
{
	public float movementSpeed;
	public float rotateSpeed;
}

public class RowController : MonoBehaviour 
{
	public RowStat rowStat;
	public Transform[] patrolPoint;
	public int currentPoint;
	private bool rotating = false;
	public bool reached = true;

	void Start()
	{
		transform.position = patrolPoint[0].position;
		currentPoint = 0;
	}

	void FixedUpdate()
	{
		if ( Vector3.Distance (transform.position, patrolPoint[currentPoint].position) < 0.5f)
		{
			reached = true;
			currentPoint ++;
		}

		if (currentPoint >= patrolPoint.Length)
		{
			currentPoint = 0;
		}
			
		Quaternion rotation = Quaternion.LookRotation ( patrolPoint[currentPoint].position - transform.position);
		rotation.x = 0;
		rotation.z = 0;

		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rowStat.rotateSpeed * Time.deltaTime);

		if (reached)
		{
			StartCoroutine ("StopTime", 3.0f);
		}
		else
		{
			transform.position = Vector3.MoveTowards (transform.position, patrolPoint[currentPoint].position, rowStat.movementSpeed * Time.deltaTime);
		}
	}

	IEnumerator StopTime (float t)
	{
		yield return new WaitForSeconds (t);

		reached = false;
	}
}
