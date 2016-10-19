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
	GameManager gameManagerScript;

	public Image captureBarContent;
	public Image captureBarBack;
	public Image winConImage;
	public Image loseConImage;
	public Image victoryImage;
	public Image loseImage;
	public float fillUpMetre;
	public const float fillUpMetreMax = 45;
	public float enemyHealth = 30;

	public float faintCounter = 10;
	public float closeImageCounter = 2;

	float oneSecond = 1f;
	public float nextTime = 0;
	public float maxTime = 10;
	public bool enemyCollided;

	private Image captureImage;

	void Awake () 
	{
		captureScript = GameObject.FindObjectOfType <CaptureCollider>();
		playerCombatScript = GameObject.FindObjectOfType <Player.Combat>();
		playerMovementScript = GameObject.FindObjectOfType <Player.Movement>();
		enemyCollisionScript = GameObject.FindObjectOfType <Enemies.Collision>();
		gameManagerScript = FindObjectOfType <GameManager>();
		captureImage = GameObject.Find("CaptureStart").GetComponent<Image>();
	}
	// Use this for initialization
	void Start ()
	{
		Color temp = captureImage.color;
		temp.a = 0.65f;
		captureImage.color = temp;
		//fillUpLove.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CaptureUI ();
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

		if(Input.GetKeyDown (KeyCode.J))
		{
			JumpButton();
		}
			
		if (gameManagerScript.enemyCounter <= 0) 
		{
			winConImage.gameObject.SetActive(true);
		}
			
		/*if (maxTime <= 0.0f) 
		{
			//loseConImage.gameObject.SetActive(true);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}*/

		/*if (loseConImage.gameObject.activeSelf) 
		{
			closeImageCounter -= Time.deltaTime;
			if (closeImageCounter <= 0) 
			{
				loseConImage.gameObject.SetActive(false);
				closeImageCounter = 2;
				maxTime = 10.0f;

			}
		}*/
	}

	void Reset()
	{
		faintCounter = 10;
		captureScript.timeLimit = 10;
		captureScript.fillUpMode = false;
		closeImageCounter = 2;
		enemyCollided = false;
		//enemyDestroyed = false;
	}
		
	void CaptureUI()
	{
		if(enemyCollided)
		{
			//maxTime -= Time.deltaTime;
			//enemyHealth = captureScript.enemyExhaustInfo;
			captureBarContent.fillAmount = fillUpMetre / fillUpMetreMax;
			if (captureScript.fillUpMode) 
			{
				captureBarContent.enabled = true;
				captureBarBack.enabled = true;
				if (faintCounter <= 0) 
				{
					victoryImage.enabled = true;
					Debug.Log ("Captured");


				}
				captureScript.timeLimit -= Time.deltaTime;
				if (captureScript.timeLimit <= 0 && faintCounter > 0) 
				{
					loseImage.enabled= true;
					captureScript.timeLimit = 0;

					Debug.Log ("You Lose");


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
			playerCombatScript.radarIndicator.gameObject.SetActive (false);
			playerCombatScript.Gauntlet_02.gameObject.SetActive (true);
			playerCombatScript.Gauntlet.gameObject.SetActive (false);
			playerCombatScript.Radar.gameObject.SetActive (false);
			playerCombatScript.catchDelayed = true;
		}

		if(playerCombatScript.catchDelayed)
		{
			playerCombatScript.Perform();
		}
		fillUpMetre+=1;
	}

	public void JumpButton()
	{
		if(!playerMovementScript.canJump && playerMovementScript.Grounded() && playerMovementScript.myAnim.GetCurrentAnimatorStateInfo(0).IsName("Grounded Movement"))
		playerMovementScript.canJump = true;
	}

	public void HitButton()
	{
		if(! playerCombatScript.Gauntlet.gameObject.activeSelf)
		{
			playerCombatScript.radarIndicator.gameObject.SetActive (false);
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
			playerCombatScript.radarIndicator.gameObject.SetActive (true);
			playerCombatScript.Radar.gameObject.SetActive (true);
			playerCombatScript.Gauntlet.gameObject.SetActive (false);
			playerCombatScript.Gauntlet_02.gameObject.SetActive (false);
		}

		if(playerCombatScript.isDelayed)
		{
			playerCombatScript.Perform();
		}
	}


}
