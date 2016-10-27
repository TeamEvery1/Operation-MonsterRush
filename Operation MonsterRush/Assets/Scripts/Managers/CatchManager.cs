using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatchManager : MonoBehaviour {

	private static CatchManager mInstance;

	public static CatchManager Instance
	{
		get
		{
			if(mInstance == null)
			{
				GameObject tempObject = GameObject.FindGameObjectWithTag("CatchManager");

				if(tempObject == null)
				{
					GameObject obj = new GameObject("_CatchManager");
					mInstance = obj.AddComponent<CatchManager>();
					obj.tag = "CatchManager";
					//DontDestroyOnLoad(obj);
				}
				else
				{
					mInstance = tempObject.GetComponent<CatchManager >();
				}
			}
			return mInstance;
		}
	}
	Enemies.Collision[] enemyCollisionScripts;
	GUIManagerScript guiScript;
	Cameras.CaptureView captureViewScript;
	public Canvas guiManager;
	public Canvas catchManager;
	public GameObject[] enemies;
	public Text teachText;
	public bool firstTimeCollided;

	//For Capture
	//public CaptureCollider captureScript = null;
	public Image captureBarContent;
	public Image captureBarBack;
	public Image victoryImage;
	public Image loseImage;
	public float fillUpMetre;
	public const float fillUpMetreMax = 45;
	public float faintCounter = 10;
	float oneSecond = 1f;
	public float nextTime = 0;
	public float enemyHealth = 30;
	public bool enemyCollided;
	public bool captureMode;
	public bool successCapture;
	public bool failCapture;
	public float closeImageCounter = 2;

	void Awake()
	{
		guiScript = FindObjectOfType<GUIManagerScript> ();
		captureViewScript = FindObjectOfType <Cameras.CaptureView>();
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	// Use this for initialization
	void Start () 
	{
		catchManager.enabled = false;
		teachText.enabled = false;
		firstTimeCollided = true;
		enemyCollisionScripts = FindObjectsOfType <Enemies.Collision>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CaptureUI ();
		if (enemyCollided) 
		{
			captureMode = true;
		}
		if (victoryImage.enabled == true) 
		{
			closeImageCounter -= Time.deltaTime;
			if (closeImageCounter <= 0) 
			{
				victoryImage.enabled = false;
				Reset();
			}
		}
		if (loseImage.enabled == true) 
		{
			closeImageCounter -= Time.deltaTime;
			if (closeImageCounter <= 0) 
			{
				loseImage.enabled = false;
				Reset();

			}
		}
		if (enemyCollided) 
			 
		{
			catchManager.enabled = true;
			guiManager.enabled = false;
			if(firstTimeCollided == true)
			{
				teachText.enabled = true;
				firstTimeCollided = false;
			}


			foreach (GameObject enemy in enemies)
			{
				enemy.transform.Find ("Canvas").gameObject.SetActive (false);
			}
		}
		else
		{
			catchManager.enabled = false;
			guiManager.enabled = true;

			if(!captureViewScript.isCollided)
			{
				foreach (GameObject enemy in enemies)
				{
					enemy.transform.Find ("Canvas").gameObject.SetActive (true);
				}
			}
		}
	}

	void Reset()
	{
		faintCounter = 10;
		closeImageCounter = 2;
		enemyCollided = false;
		//enemyDestroyed = false;
	}

	void CaptureUI()
	{
		foreach (Enemies.Collision enemyCollisionScript in enemyCollisionScripts)
		{
			if (captureMode) 
			{
				//maxTime -= Time.deltaTime;
				//enemyHealth = captureScript.enemyExhaustInfo;
				captureBarContent.fillAmount = fillUpMetre / fillUpMetreMax;

				//captureBarContent.enabled = true;
				//captureBarBack.enabled = true;
				if (faintCounter <= 0) 
				{
					successCapture = true;
					victoryImage.enabled = true;
					Debug.Log ("Captured");
				}
				enemyCollisionScript.timeLimit -= Time.deltaTime;
				if (enemyCollisionScript.timeLimit <= 0 && faintCounter > 0) 
				{
					failCapture = true;
					loseImage.enabled = true;
					//captureScript.timeLimit = 0;

					Debug.Log ("You Lose");


				}
				//fillUpLove.enabled = true;
				if ((fillUpMetre <= 35) && (fillUpMetre >= 25)) 
				{
					faintCounter -= Time.deltaTime;
				}

			}

			//battle win condition

			//update every second instead of every frame
			if (Time.time >= nextTime) 
			{

				nextTime =Time.time + oneSecond;
				fillUpMetre--;
			}

			if (fillUpMetre <= 0) 
			{
				fillUpMetre = 0;
			}
		}
	}

	public void CaptureButton()
	{
		fillUpMetre++;
	}


}
