using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallMovementScripts : MonoBehaviour 
{
	public float moveSpeed = 5.0f;
	public float drag = 0.5f;
	public float terminalRotationSpeed = 25.0f;
	public VirtualJoyStickScripts moveJoyStick;

	private Player.Movement playerMovement;
	private Rigidbody controller;
	private bool jump;

	private void Start()
	{
		playerMovement = GetComponent <Player.Movement> ();
		controller = GetComponent<Rigidbody>();
		controller.maxAngularVelocity = terminalRotationSpeed;
		controller.drag = drag;
	}

	private void Update()
	{
		//KeyboardInput
		Vector3 direction = Vector3.zero;

		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");

		if(direction.magnitude > 1)
		{
			direction.Normalize();
		}

		//Phone Input
		if(moveJoyStick.InputDirection != Vector3.zero)
		{
			direction = moveJoyStick.InputDirection;
		}

		playerMovement.Player_Movement(direction, jump);

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Restart();
		}

	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
