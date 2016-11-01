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

	//public CaptureCollider captureScript = null;
	public Player.Combat playerCombatScript;
	public Player.Movement playerMovementScript;
	public Enemies.Collision enemyCollisionScript;
	GameManager gameManagerScript;
	Timer timerScript;

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
	public bool canCapture;
	public GameObject blackScreen;
	public bool canDisplay;
	CatchManager catchManager;

	private RectTransform text;
	private Text textImage;
	[HideInInspector ]public bool canShowCoinText;
	private float showTextTimer;
	private float showTextDuration = 0.5f;

	void Awake () 
	{
		//captureScript = GameObject.FindObjectOfType <CaptureCollider>();
		playerCombatScript = GameObject.FindObjectOfType <Player.Combat>();
		playerMovementScript = GameObject.FindObjectOfType <Player.Movement>();
		enemyCollisionScript = GameObject.FindObjectOfType <Enemies.Collision>();
		gameManagerScript = FindObjectOfType <GameManager>();
		captureImage = GameObject.Find("CaptureStart").GetComponent<Image>();
		catchManager = GameObject.FindObjectOfType<CatchManager>();
		timerScript = FindObjectOfType<Timer> ();
		text = this.gameObject.transform.Find("Coin/CoinText").GetComponent<RectTransform>();
		textImage = this.gameObject.transform.FindChild("Coin/CoinText").GetComponent<Text>();
		canShowCoinText = false;

	}
	// Use this for initialization
	void Start ()
	{
		canCapture = false;
		canDisplay = false;
		//fillUpLove.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		TutorialScene();
		ChangeUITranparentcy();
		TextMovingUP();


		if(Input.GetKeyDown (KeyCode.J))
		{
			JumpButton();
		}
			
		if (gameManagerScript.enemyCounter <= 0) 
		{
			winConImage.gameObject.SetActive(true);
		}
			
		if (winConImage.gameObject.activeSelf) 
		{
			closeImageCounter -= Time.deltaTime;
			if (closeImageCounter <= 0) 
			{
				loseConImage.gameObject.SetActive(false);
				closeImageCounter = 2;
				maxTime = 10.0f;
				SceneManager.LoadScene("Game Over");
			}
		}

		if (timerScript.timer <= 0) 
		{
			loseConImage.gameObject.SetActive(true);
		}

		if (loseConImage.gameObject.activeSelf) 
		{
			closeImageCounter -= Time.deltaTime;
			if (closeImageCounter <= 0) 
			{
				loseConImage.gameObject.SetActive(false);
				closeImageCounter = 2;
				maxTime = 10.0f;
				SceneManager.LoadScene("Game Over");
			}
		}
		/*if (maxTime <= 0.0f) 
		{
			//loseConImage.gameObject.SetActive(true);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}*/
	}

	void Reset()
	{
		
		closeImageCounter = 2;
		enemyCollided = false;
		//enemyDestroyed = false;
	}
		


	public void CaptureButton()
	{
		if(canCapture == true)
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
			catchManager.teachText.enabled = false;
		}
	}

	public void JumpButton()
	{
		if(!playerMovementScript.canJump && (playerMovementScript.Grounded() || playerMovementScript.UpperGrounded()) && playerMovementScript.myAnim.GetCurrentAnimatorStateInfo(0).IsName("Grounded Movement"))
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

	public void ChangeUITranparentcy()
	{
		if(canCapture == false)
		{
			Color temp = captureImage.color;
			temp.a = 0.65f;
			captureImage.color = temp;
		}
		else
		{
			Color temp = captureImage.color;
			temp.a = 1.0f;
			captureImage.color = temp;
		}

	}

	public void TutorialScene()
	{
		if(canDisplay == true)
		{
			blackScreen.SetActive(true);
		}
		else if(canDisplay == false)
		{
			blackScreen.SetActive(false);
		}
	}

	public void TextMovingUP()
	{
		if(canShowCoinText == true)
		{
			textImage.enabled = true;
			text.transform.Translate(Vector3.up * Time.deltaTime * 50.0f);
			showTextTimer += Time.deltaTime;
			if(showTextTimer >= showTextDuration)
			{
				showTextTimer = 0.0f;
				textImage.enabled = false;
				canShowCoinText = false;
				text.anchoredPosition = new Vector2(68, 3);
			}
		}
	}
}
