using UnityEngine;
using System.Collections;

public class CaptureView : MonoBehaviour 
{
	Cameras.AutoCam autoCamScript;
	Enemies.Collision[] enemyCollisionScripts;
	Enemies.Pathfinding[] enemyPathfindingScripts;
	Player.Movement playerMovementScript;

	GameObject pivotPoint;

	private Vector3 catchingView;
	private Vector3 catchingRotation;

	private bool catchMode;

	void Start()
	{	
		autoCamScript = GetComponent <Cameras.AutoCam>();

		enemyCollisionScripts = FindObjectsOfType <Enemies.Collision>();
		enemyPathfindingScripts = FindObjectsOfType <Enemies.Pathfinding>();

		playerMovementScript = FindObjectOfType <Player.Movement>();
		pivotPoint = transform.FindChild ("Pivot").gameObject;
	}

	void Update()
	{
	}

	void LateUpdate()
	{
		foreach (Enemies.Collision enemyCollisionScript in enemyCollisionScripts)
		{
			if(enemyCollisionScript.isCollided)
			{
				autoCamScript.enabled = false;

				if(!catchMode)
				{
					CatchingView();
				}
			}
			else
			{
				autoCamScript.enabled = true;
			}
		}
	}

	void CatchingView()
	{
		catchingView = new Vector3 (pivotPoint.transform.position.x + 1.0f, pivotPoint.transform.position.y, pivotPoint.transform.position.z + 1.0f);
		catchingRotation = new Vector3 (pivotPoint.transform.rotation.x, pivotPoint.transform.rotation.y - 40.0f, pivotPoint.transform.rotation.z);

		pivotPoint.transform.position = catchingView;
		pivotPoint.transform.rotation = Quaternion.Euler(catchingRotation);

		foreach (Enemies.Pathfinding enemyPathfindingScript in enemyPathfindingScripts)
		{
			enemyPathfindingScript.GPS.acceleration = 0;
		}

		catchMode = true;

	}
}
