using UnityEngine;
using System.Collections;

public class Stay : MonoBehaviour 
{
	private Vector3 originalPos;
	Animation myAnim;

	[HideInInspector] public bool getCoin = false;
	bool onShip = false;
	Rigidbody rigid;
	BottleScripts bottleScripts;

	void Awake () 
	{
		originalPos = this.transform.position;
		myAnim = GetComponent <Animation>();
		rigid = GetComponent<Rigidbody>();

		bottleScripts = GetComponent<BottleScripts>();
	}

	void Update()
	{
		if(getCoin == true)
		{
			//Debug.Log("up");
			rigid.AddForce(Vector3.up * 100.0f);
			getCoin = false;
		}

	}

	void LateUpdate () 
	{
		if(onShip == false && bottleScripts == null)
		{
			bottleScripts.enabled = false;
			this.transform.position = new Vector3 (originalPos.x, this.transform.position.y, originalPos.z);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Ship")
		{
			onShip = true;
		}
	}
}