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
	public bool reached = true;
	public float oriMoveSpeed;
	public float oriRotateSpeed;

	void Start()
	{
		transform.position = patrolPoint[0].position;
		currentPoint = 0;
		oriMoveSpeed = rowStat.movementSpeed;
		oriRotateSpeed = rowStat.rotateSpeed;
	}

	void FixedUpdate()
	{
		if ( GameManager.GetSqrDist (transform.position, patrolPoint[currentPoint].position) < 0.5f)
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
