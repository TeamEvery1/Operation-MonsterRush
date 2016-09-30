using UnityEngine;
using System.Collections;

public class CameraScripts : MonoBehaviour 
{
	public float distanceAway;
	public float distanceUp;
	public float smooth;
	private Transform follow;
	private Vector3 targetPosition;

	void Start()
	{
		follow = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void LateUpdate()
	{
		targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

		transform.LookAt(follow);
	}
}

