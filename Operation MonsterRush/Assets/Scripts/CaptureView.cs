using UnityEngine;
using System.Collections;

public class CaptureView : MonoBehaviour 
{
	Cameras.AutoCam autoCamScript;
	Enemies.Collision enemyCollisionScript;

	GameObject pivotPoint;

	private Vector3 catchingView;
	private Vector3 catchingRotation;

	void Start()
	{	
		autoCamScript = GetComponent <Cameras.AutoCam>();
		enemyCollisionScript = FindObjectOfType <Enemies.Collision>();
		pivotPoint = transform.FindChild ("Pivot").gameObject;
	}

	void LateUpdate()
	{
		if(enemyCollisionScript.isCollided)
		{
			autoCamScript.enabled = false;

			catchingView = new Vector3 (pivotPoint.transform.position.x + 1.0f, pivotPoint.transform.position.y, pivotPoint.transform.position.z + 1.0f);
			catchingRotation = new Vector3 (pivotPoint.transform.rotation.x, pivotPoint.transform.rotation.y - 40.0f, pivotPoint.transform.rotation.z);

			pivotPoint.transform.position = catchingView;
			pivotPoint.transform.rotation = Quaternion.Euler(catchingRotation);
		}
		else
		{
			autoCamScript.enabled = true;
		}
	}

}
