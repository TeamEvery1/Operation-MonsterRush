using UnityEngine;
using System.Collections;

public class OrbitAround : MonoBehaviour
{
	[SerializeField] private float OrbitDegrees;
	private Transform center;
	private Transform target;
	private Vector3 rotation;

	void Awake()
	{
		center = transform.parent.transform;
		target = GetComponentInParent <Player.Combat>().closest.transform;
	}

	void Update()
	{
		transform.RotateAround (center.position , Vector3.up, OrbitDegrees * Time.deltaTime);
		rotation = Quaternion.LookRotation (target.transform.position - this.transform.position).eulerAngles;

		rotation.x = Mathf.Clamp (90, -90, -90);
		rotation.z = 0;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (rotation), 1);
	}

}
