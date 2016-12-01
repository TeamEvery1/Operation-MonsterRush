using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rolling : MonoBehaviour 
{
	Rigidbody myRB;
	float gravityMultiplier;

	private GameObject boulderPointObj;
	private GameObject[] boulderPointsObj;
	public List <Transform> boulderPoints;
	public float distance;
	public int currentPoint;
	private Animation myAnim;
	public bool isCollided;


	void Start () 
	{
		myRB = GetComponent <Rigidbody> ();
		//myRB.AddForce(transform.forward * 100.0f);
		gravityMultiplier = 10.0f;

		boulderPointObj = GameObject.Find ("Boulder Points");
		boulderPointsObj = GameObject.FindGameObjectsWithTag ("Boulder Point");

		/*for (int i = boulderPointsObj.Length ; i > 0; i--)
		{
			boulderPoints.Add (boulderPointsObj[i -1].transform);
		}*/

		for (int i = 0; i < boulderPointsObj.Length; i++)
		{
			boulderPoints.Add (boulderPointsObj[i].transform);
		}

		currentPoint = 0;

		myAnim = GetComponent <Animation>();

		isCollided = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isCollided)
		{
			myAnim.Play ();
			StartCoroutine ("BreakDelay", 0.5f);
		}

		if (currentPoint < boulderPoints.Count)
		{
			distance = GameManager.GetSqrDist (this.transform.position, boulderPoints[currentPoint].position);
			this.transform.position = Vector3.MoveTowards (this.transform.position, boulderPoints[currentPoint].position, 10.0f * Time.deltaTime);
			myRB.velocity -= new Vector3 (0.0f, gravityMultiplier * Time.deltaTime, 0.0f);

			if (distance < 1.5f)
			{
				currentPoint ++;
			}
		}
		else if (currentPoint == boulderPoints.Count || isCollided)
		{
			myAnim.Play ();
			StartCoroutine ("BreakDelay", 0.5f);
		}



		//distance = GameManager.GetSqrDist (this.transform.position, boulderPoints.position);
		//if (GameManager.GetSqrDist (this.transform.position, boulderPoints.position) < 1.5f)
		//{
		//	Destroy (this.gameObject);
		//}
	}

	IEnumerator BreakDelay (float t)
	{
		yield return new WaitForSeconds (t);
		isCollided = false;
		Destroy (this.gameObject);
	}
}
