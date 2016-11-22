using UnityEngine;
using System.Collections;

public class Stay : MonoBehaviour 
{
	private Vector3 originalPos;
	Animation myAnim;

	void Awake () 
	{
		originalPos = this.transform.position;
		myAnim = GetComponent <Animation>();
	}

	void Update()
	{
		
	}

	void LateUpdate () 
	{
		this.transform.position = new Vector3 (originalPos.x, this.transform.position.y, originalPos.z);
	}
}
