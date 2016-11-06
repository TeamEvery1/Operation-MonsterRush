using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Restart()
	{
		SceneManager.LoadScene("Level 01");


	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");


	}
}
