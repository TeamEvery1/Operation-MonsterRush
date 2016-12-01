using UnityEngine;
using System.Collections;

public class LusoRockScirpts : MonoBehaviour 
{
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.transform.position -= new Vector3(0.0f, -50.0f, 50.0f) * Time.deltaTime * 1.0f;
			other.transform.GetComponent<Player.Controller>().health -= 1;
			Destroy(this.gameObject);
		}
	}
}