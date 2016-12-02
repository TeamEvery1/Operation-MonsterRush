using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour 
{
	Transform mainMenu;
	private string scene;
	private GameObject gameObj;

	void Start()
	{
		mainMenu = GameObject.Find("Canvas").transform.FindChild("Main Menu");
	}

	public void ChangeScene (string sceneName)
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_BUTTONPRESSED1);
		scene = sceneName;
		Invoke ("ChangeSceneDelay",1.0f);
	}

	public void ChangePosition(GameObject obj)
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_BUTTONPRESSED1);
		gameObj = obj;
		Invoke ("ChangePositionDelay", 1.0f);
	}

	public void BackspacePosition(GameObject obj)
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_BACKSPACE);
		obj.gameObject.SetActive(true);
		obj.transform.position = mainMenu.transform.position;
		this.transform.parent.parent.gameObject.SetActive(false);
	}

	public void Exit()
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_BUTTONPRESSED1);
		Application.Quit();
	}

	void ChangeSceneDelay ()
	{
		SceneManager.LoadScene (scene);
	}

	void ChangePositionDelay()
	{
		gameObj.gameObject.SetActive(true);
		gameObj.transform.position = mainMenu.transform.position;
		this.transform.parent.parent.gameObject.SetActive(false);
	}

	public void Restart()
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_BUTTONPRESSED1);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void MainMenu()
	{
		SoundManagerScript.Instance.PlaySFX (AudioClipID.SFX_BUTTONPRESSED1);
		SceneManager.LoadScene ("Main Menu");
	}
}
