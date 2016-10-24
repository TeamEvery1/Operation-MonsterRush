using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour 
{
	public GameObject PauseUI;
	//public GameObject InstructionUI;
	Timer timerScript;
	private bool paused = false;

	void Awake () 
	{
		timerScript = FindObjectOfType<Timer> ();
	}
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
		timerScript.timer = 900.0f;

	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
		timerScript.timer = 900.0f;

	}
	public void MenuReturn()
	{
		SoundManagerScript.Instance.StopBGM();
		SceneManager.LoadScene("Main Menu");
	}
}
