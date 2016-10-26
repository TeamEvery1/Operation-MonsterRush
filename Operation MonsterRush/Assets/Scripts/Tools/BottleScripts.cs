using UnityEngine;
using System.Collections;

public class BottleScripts : MonoBehaviour 
{
	Rigidbody myRB;
	public bool onGround;
	RaycastHit land;

	private void Start()
	{
		myRB = this.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Physics.Raycast (transform.position, Vector3.down, out land, 0.4f))
		{
			onGround = true;
		}
		else
		{
			onGround = false;
		}

		if(onGround)
		{
			myRB.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
		else
		{
			Vector3 gravity = new Vector3 (0.0f, -10.0f * -1.0f * -1.0f, 0.0f);
			//myRB.velocity = new Vector3(myRB.position.x, myRB.position.y, myRB.position.z);
			myRB.AddForce(gravity);
		}
	}
}
