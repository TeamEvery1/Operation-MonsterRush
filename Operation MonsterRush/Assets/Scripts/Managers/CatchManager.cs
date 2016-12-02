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
	//GUIManagerScript guiScript;
	Cameras.CaptureView captureViewScript;
	public Canvas guiManager;
	public Canvas catchManager;
	public GameObject[] enemies;
	public Text teachText;
	private Text monsterText;
	public bool firstTimeCollided;

	//For Capture
	//public CaptureCollider captureScript = null;
	public Image captureBarContent;
	public Image captureBarBack;
	public Image victoryImage;
	public Image loseImage;
	public float fillUpMetre;
	public const float fillUpMetreMax = 45;
	public float faintCounter = 30;
	float oneSecond = 1f;
	public float nextTime = 0;
	public float enemyHealth = 30;
	public bool enemyCollided;
	public bool captureMode;
	public bool successCapture;
	public bool failCapture;
	public float closeImageCounter = 2;

	public bool atStart =true;
	public float timeLimit = 20;
	//public float timeLimitF;
	//public float timeLimitModifier;
	private GUIManagerScript guiManagerScripts;

	public GameObject box;

	private Transform target;
	private Vector3 originalScale;
	private GameObject player;
	private RaycastHit hitInfo;
	private bool gotTarget;
	private bool useRayCast;
	private bool resetBox;


	void Awake()
	{
		//guiScript = FindObjectOfType<GUIManagerScript> ();
		captureViewScript = FindObjectOfType <Cameras.CaptureView>();
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		monsterText = this.transform.Find ("Monster Name/Monster Text").GetComponent <Text>();
	}
	// Use this for initialization
	void Start () 
	{
		catchManager.enabled = false;
		teachText.enabled = false;
		firstTimeCollided = true;
		enemyCollisionScripts = FindObjectsOfType <Enemies.Collision>();
		guiManagerScripts = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManagerScript>();

		player = GameObject.FindGameObjectWithTag("Player");
		gotTarget = false;
		useRayCast = true;
		resetBox = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ResetBox();
		CaptureUI ();

		foreach (Enemies.Collision enemyCollisionScript in enemyCollisionScripts) 
		{
			if(enemyCollisionScript.isCollided)
			{
				monsterText.text = enemyCollisionScript.gameObject.GetComponent <Enemies.Info> ().monsterName;
			}
		}

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


			for (int i = 0; i < enemies.Length; i++)
			{
				enemies[i].transform.Find ("Canvas").gameObject.SetActive (false);
			}
		}
		else
		{
			catchManager.enabled = false;
			guiManager.enabled = true;
			if(!captureViewScript.isCollided)
			{
				for (int i = 0; i < enemies.Length; i++)
				{
					enemies[i].transform.Find ("Canvas").gameObject.SetActive (true);
				}
			}
		}
	}

	void FixedUpdate()
	{
		if(captureMode)
		{
			if(useRayCast == true)
			{
				if(Physics.Raycast(player.transform.position + player.transform.TransformDirection(new Vector3(-2.0f, 0.2f, 1.0f)), player.transform.forward, out hitInfo, 5.0f))
				{	
					if(gotTarget == false)
					{
						target = hitInfo.transform;
						originalScale = target.transform.localScale;
						useRayCast = false;
						gotTarget = true;
					}
				}
			}
		}
	}

	void Reset()
	{
		faintCounter = 30;
		closeImageCounter = 2;
		atStart = true;
		enemyCollided = false;
		//enemyDestroyed = false;
	}

	void CaptureUI()
	{
		foreach (Enemies.Collision enemyCollisionScript in enemyCollisionScripts)
		{
			if (captureMode) 
			{
				if (atStart) 
				{
					//timeLimitModifier = (100 - enemyCollisionScript.enemyExhaustInfo)/10;
					//timeLimitF = timeLimit + timeLimitModifier;
					atStart = false;
				}
				//maxTime -= Time.deltaTime;
				//enemyHealth = captureScript.enemyExhaustInfo;
				captureBarContent.fillAmount = fillUpMetre / fillUpMetreMax;

				//captureBarContent.enabled = true;
				//captureBarBack.enabled = true;
				if (faintCounter <= 0) 
				{
					successCapture = true;
					victoryImage.enabled = true;
					resetBox = true;
					//Debug.Log ("Captured");
					break;
				}

				if (timeLimit <= 0 && faintCounter > 0) 
				{
					failCapture = true;
					loseImage.enabled = true;
					guiManagerScripts.canDisplayTutorialHighlight = false;
					//captureScript.timeLimit = 0;
					resetBox = true;

					//Debug.Log ("You Lose");
					break;
				}
				//fillUpLove.enabled = true;
				if ((fillUpMetre <= 35) && (fillUpMetre >= 25)) 
				{
					faintCounter -= Time.deltaTime;
				}
				timeLimit -= Time.deltaTime*0.1f;
			}

			//battle win condition

			//update every second instead of every frame
			if (Time.time >= nextTime) 
			{

				nextTime =Time.time + oneSecond;
				fillUpMetre--;

				if(captureMode == true && gotTarget == true)
				{
					if(target.transform.localScale.x >= originalScale.x && target.transform.localScale.y >= originalScale.y && target.transform.localScale.z >= originalScale.z)
					{
						target.transform.localScale = originalScale;
					}
					else
					{
						target.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
					}

					if(box.transform.localScale.x >= 6.0f && box.transform.localScale.y >= 6.0f && box.transform.localScale.z >= 6.0f)
					{
						box.transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);
					}
					else
					{
						box.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
					}

					if(box.GetComponent<BoxShakeScripts>().alphaValue <= 0.5f)
					{
						box.GetComponent<BoxShakeScripts>().alphaValue = 0.5f;
					}
					else
					{
						box.GetComponent<BoxShakeScripts>().alphaValue -= 0.05f;
					}
				}
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

		if(captureMode == true && gotTarget == true)
		{
			if(target.transform.localScale.x <= 0.3f && target.transform.localScale.y <= 0.3f && target.transform.localScale.z <= 0.3f)
			{
				target.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			}
			else
			{
				target.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
			}

			if(box.transform.localScale.x <= 1.0f && box.transform.localScale.y <= 1.0f && box.transform.localScale.z <= 1.0f)
			{
				box.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}
			else
			{
				box.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
			}

			if(box.GetComponent<BoxShakeScripts>().alphaValue >= 1.0f)
			{
				box.GetComponent<BoxShakeScripts>().alphaValue = 1.0f;
			}
			else
			{
				box.GetComponent<BoxShakeScripts>().alphaValue += 0.05f;
			}
		}
	}

	public void ResetBox()
	{
		if(resetBox == true)
		{
			box.GetComponent<BoxShakeScripts>().alphaValue = 0.5f;
			box.transform.localScale = new Vector3(6.0f, 6.0f, 6.0f);
			target.transform.localScale = originalScale;
			useRayCast = true;
			gotTarget = false;
			resetBox = false;
		}
	}
}
