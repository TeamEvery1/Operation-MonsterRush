using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GUIManagerScript : MonoBehaviour 
{
	private static GUIManagerScript mInstance;

	public static GUIManagerScript Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject tempObject = GameObject.FindGameObjectWithTag("GUIManager");

				if(tempObject == null)
				{
					GameObject obj = new GameObject("_GUIManager");
					mInstance = obj.AddComponent<GUIManagerScript>();
					obj.tag = "GUIManager";
					//DontDestroyOnLoad(obj);
				}
				else
				{
					mInstance = tempObject.GetComponent<GUIManagerScript >();
				}
			}
			return mInstance;
		}
	}

	public CaptureScript captureScript = null;
	public Image captureBarContent;
	public Image captureBarBack;
	public Image victoryImage;
	public Image loseImage;
	public float fillUpMetre;
	public const float fillUpMetreMax = 45;
	public float enemyHealth;

	public float faintCounter = 10;

	float oneSecond = 1f;
	public float nextTime = 0;


	void Awake () 
	{

		captureScript = GameObject.FindObjectOfType <CaptureScript>();
	}
	// Use this for initialization
	void Start ()
	{
		//fillUpLove.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CaptureUI ();
	}


	void CaptureUI()
	{
		enemyHealth = captureScript.enemyHealthInfo;
		captureBarContent.fillAmount = fillUpMetre / fillUpMetreMax;
		if (captureScript.fillUpMode) 
		{
			captureBarContent.enabled = true;
			captureBarBack.enabled = true;
			if (faintCounter <= 0) 
			{
				victoryImage.enabled = true;
				Debug.Log ("Captured");
				fillUpMetre = 0;

			}
			captureScript.timeLimit -= Time.deltaTime;
			if (captureScript.timeLimit <= 0 && faintCounter > 0) 
			{
				loseImage.enabled= true;
				captureScript.timeLimit = 0;
				captureScript.fillUpMode = false;
				Debug.Log ("You Lose");
				fillUpMetre = 0;

			}
			//fillUpLove.enabled = true;
			if (( fillUpMetre <= enemyHealth + 5 ) && (fillUpMetre >= enemyHealth - 5)) 
			{
				faintCounter -= Time.deltaTime;
			}
		} 
		else 
		{
			captureBarContent.enabled = false;
			captureBarBack.enabled = false;
			//fillUpLove.enabled = false;
		}
		//battle win condition

		//update every second instead of every frame
		if (Time.time >= nextTime) 
		{
			fillUpMetre--;
			nextTime += oneSecond;
		}

		if (fillUpMetre <= 0) 
		{
			fillUpMetre = 0;
		}
	}

	public void CaptureButton()
	{
		captureScript.Capture ();
		if (captureScript.fillUpMode) 
		{
			fillUpMetre += 1;
		}
	}

	public void RestartButton()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
