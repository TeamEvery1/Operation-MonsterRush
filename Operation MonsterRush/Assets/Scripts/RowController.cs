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
	private int currentPoint;
	private bool rotating = false;

	void Start()
	{
		transform.position = patrolPoint[0].position;
		currentPoint = 0;
	}

	void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards (transform.position, patrolPoint[currentPoint].position, rowStat.movementSpeed * Time.deltaTime);

		if ( Vector3.Distance (transform.position, patrolPoint[currentPoint].position) < 0.5f)
		{
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
	
	}
}
