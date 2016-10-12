using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour 
{
	public Transform target;

	void Update()
	{
		transform.LookAt (this.transform.position + target.transform.rotation * Vector3.back, target.transform.rotation * Vector3.up);

		/*Vector3 rotation = Quaternion.LookRotation (target.transform.position - this.transform.position).eulerAngles;

		rotation.x = 0;
		rotation.z = 0;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (rotation), 1);*/
	}



}
