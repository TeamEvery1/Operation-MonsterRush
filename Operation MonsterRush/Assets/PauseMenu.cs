using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour 
{
	public GameObject PauseUI;
	//public GameObject InstructionUI;

	private bool paused = false;



	// Use this for initialization
	void Start () 
	{
		PauseUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(paused)
		{
			PauseUI.SetActive(true);
			Time.timeScale = 0;
		}

		if(!paused)
		{
			PauseUI.SetActive(false);
			//InstructionUI.SetActive(false);
			Time.timeScale = 1;
		}
	}
	public void PauseButton()
	{
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		paused = !paused;
	}

	public void Resume()
	{
		paused = false;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
	public void MenuReturn()
	{
		SoundManagerScript.Instance.StopBGM();
		SceneManager.LoadScene("Main Menu");
	}
}
