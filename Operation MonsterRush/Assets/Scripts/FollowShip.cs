using UnityEngine;
using System.Collections;

public class FollowShip : MonoBehaviour 
{
	public LayerMask ground;
	RaycastHit hitInfo;

	void FixedUpdate()
	{
		
		if (Physics.Raycast (this.transform.position, Vector3.down, out hitInfo, ground))
		{
			if (hitInfo.collider.tag == "Ship" || hitInfo.collider.tag == "Surfboard")
			{
				this.transform.position = new Vector3 (hitInfo.transform.position.x - 0.5f, hitInfo.transform.position.y - 0.5f , hitInfo.transform.position.z + 0.5f);
			}
		}
	}

}
