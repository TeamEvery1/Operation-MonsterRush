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
		scene = sceneName;
		Invoke ("ChangeSceneDelay",1.0f);
	}

	public void ChangePosition(GameObject obj)
	{
		gameObj = obj;
		Invoke ("ChangePositionDelay", 1.0f);
	}

	public void Exit()
	{
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
}
