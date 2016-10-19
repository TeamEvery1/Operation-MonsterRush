using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour 
{
	Transform mainMenu;

	void Start()
	{
		mainMenu = GameObject.Find("Canvas").transform.FindChild("Main Menu");
	}

	public void ChangeScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
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

}
