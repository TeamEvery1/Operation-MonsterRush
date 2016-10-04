using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour 
{
	public Transform target;

	void Update()
	{
		/*Vector3 targetDirection = target.position - this.transform.position;
		targetDirection.y = 0;
		this.transform.LookAt (targetDirection);*/

		Vector3 rotation = Quaternion.LookRotation (target.transform.position - this.transform.position).eulerAngles;

		rotation.x = 0;
		rotation.z = 0;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (rotation), 1);
	}



}
