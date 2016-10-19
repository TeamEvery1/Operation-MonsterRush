using UnityEngine;
using System.Collections;

public class OrbitAround : MonoBehaviour
{
	[SerializeField] private float OrbitDegrees;

	private GameObject player;
	//private Transform center;
	private Transform target;
	public Vector3 rotation;

	void Awake()
	{
		player =  GameObject.Find ("Character");
		//center = transform;

	}

	void Update()
	{
		target = player.GetComponent<Player.Combat>().closest.transform;
		//transform.RotateAround (center.position , Vector3.up, OrbitDegrees * Time.deltaTime);

		rotation = Quaternion.LookRotation (target.transform.position - this.transform.position).eulerAngles;

		rotation.x = 50;
		rotation.z = 50;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (rotation), 1);
	}

}
