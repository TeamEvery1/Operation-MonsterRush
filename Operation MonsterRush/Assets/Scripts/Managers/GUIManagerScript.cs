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

	public CaptureCollider captureScript = null;
	public Player.Combat playerCombatScript;
	public Player.Movement playerMovementScript;
	public Enemies.Collision enemyCollisionScript;

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
		captureScript = GameObject.FindObjectOfType <CaptureCollider>();
		playerCombatScript = GameObject.FindObjectOfType <Player.Combat>();
		playerMovementScript = GameObject.FindObjectOfType <Player.Movement>();
		enemyCollisionScript = GameObject.FindObjectOfType <Enemies.Collision>();
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

		if(Input.GetKeyDown (KeyCode.J))
		{
			JumpButton();
		}
	}
		
	void CaptureUI()
	{
		if(enemyCollisionScript.isCollided)
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
	}

	public void CaptureButton()
	{
		if(! playerCombatScript.Gauntlet_02.gameObject.activeSelf)
		{
			playerCombatScript.Gauntlet_02.gameObject.SetActive (true);
			playerCombatScript.Gauntlet.gameObject.SetActive (false);
			playerCombatScript.Radar.gameObject.SetActive (false);
			playerCombatScript.catchDelayed = true;
		}

		if(playerCombatScript.catchDelayed)
		{
			playerCombatScript.Perform();
		}
	}

	public void JumpButton()
	{
		if(!playerMovementScript.canJump && playerMovementScript.Grounded())
		playerMovementScript.canJump = true;
	}

	public void HitButton()
	{
		if(! playerCombatScript.Gauntlet.gameObject.activeSelf)
		{
			playerCombatScript.Gauntlet.gameObject.SetActive (true);
			playerCombatScript.Gauntlet_02.gameObject.SetActive (false);
			playerCombatScript.Radar.gameObject.SetActive (false);
			playerCombatScript.isDelayed = true;
		}

		if(playerCombatScript.isDelayed)
		{
			playerCombatScript.Perform();
		}
	}

	public void RadarButton()
	{
		if(! playerCombatScript.Radar.gameObject.activeSelf)
		{
			playerCombatScript.Radar.gameObject.SetActive (true);
			playerCombatScript.Gauntlet.gameObject.SetActive (false);
			playerCombatScript.Gauntlet_02.gameObject.SetActive (false);
		}

		if(playerCombatScript.isDelayed)
		{
			playerCombatScript.Perform();
		}
	}

	public void RestartButton()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
