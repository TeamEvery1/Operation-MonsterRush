using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartImageHolderScripts : MonoBehaviour 
{
	public GameObject start01;
	public GameObject start02;
	public GameObject start03;

	private Vector3 rotationEuler;

	private GameOverManager gameOverManagerScripts;
	private GUIManagerScript guiManagerScripts;

	private void Start()
	{
		gameOverManagerScripts = GameObject.FindGameObjectWithTag("GameOverManager").GetComponent<GameOverManager>();
		guiManagerScripts = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();
	}

	private void Update()
	{
		rotationEuler += new Vector3(0.0f, 2.0f, 0.0f);
		start01.GetComponent<RectTransform>().rotation = Quaternion.Euler(rotationEuler);
		start02.GetComponent<RectTransform>().rotation = Quaternion.Euler(rotationEuler);
		start03.GetComponent<RectTransform>().rotation = Quaternion.Euler(rotationEuler);

		if(guiManagerScripts.gameoverBool == true)
		{
			if(gameOverManagerScripts.starCounter == 1)
			{
				start01.SetActive(true);
				if(start01.GetComponent<RectTransform>().localScale.x <= 1 && start01.GetComponent<RectTransform>().localScale.y <= 1)
				{
					start01.GetComponent<RectTransform>().localScale += new Vector3(0.005f, 0.005f, 0.0f);
				}
			}
			else if(gameOverManagerScripts.starCounter == 2 )
			{
				start01.SetActive(true);
				start02.SetActive(true);

				if(start01.GetComponent<RectTransform>().localScale.x <= 1 && start01.GetComponent<RectTransform>().localScale.y <= 1 && start02.GetComponent<RectTransform>().localScale.x <= 1 && start02.GetComponent<RectTransform>().localScale.y <= 1)
				{
					start01.GetComponent<RectTransform>().localScale += new Vector3(0.005f, 0.005f, 0.0f);
					start02.GetComponent<RectTransform>().localScale += new Vector3(0.005f, 0.005f, 0.0f);
				}
			}
			else if(gameOverManagerScripts.starCounter == 3)
			{
				start01.SetActive(true);
				start02.SetActive(true);
				start03.SetActive(true);


				if(start01.GetComponent<RectTransform>().localScale.x <= 1 && start01.GetComponent<RectTransform>().localScale.y <= 1 && start02.GetComponent<RectTransform>().localScale.x <= 1 && start02.GetComponent<RectTransform>().localScale.y <= 1 && start03.GetComponent<RectTransform>().localScale.x <= 1 && start03.GetComponent<RectTransform>().localScale.y <= 1)
				{
					start01.GetComponent<RectTransform>().localScale += new Vector3(0.005f, 0.005f, 0.0f);
					start02.GetComponent<RectTransform>().localScale += new Vector3(0.005f, 0.005f, 0.0f);
					start03.GetComponent<RectTransform>().localScale += new Vector3(0.005f, 0.005f, 0.0f);
				}
			}
		}
	}
}
