using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour 
{
	Transform mainMenu;
	private string scene;

	void Start()
	{
		mainMenu = GameObject.Find("Canvas").transform.FindChild("Main Menu");
	}

	public void ChangeScene (string sceneName)
	{
		scene = sceneName;
		Invoke ("ChangeSceneDelayTimer",1.0f);
	}

	public void ChangePosition(GameObject obj)
	{
		obj.gameObject.SetActive(true);
		obj.transform.position = mainMenu.transform.position;
		this.transform.parent.parent.gameObject.SetActive(false);
	}

	public void Exit()
	{
		Application.Quit();
	}

	void ChangeSceneDelayTimer ()
	{
			SceneManager.LoadScene (scene);
	}
}
