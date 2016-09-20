using UnityEngine;
using System.Collections;

public class CameraFollowBallScripts : MonoBehaviour 
{
	public Transform lootAt;

	private Vector3 offset;

	private float distance = 5.0f;
	private float yOffset = 3.5f;

	private void Start()
	{
		offset = new Vector3 (0, yOffset, -1f * distance);
	}

	private void Update()
	{
		transform.position = lootAt.position + offset;
	}
}

