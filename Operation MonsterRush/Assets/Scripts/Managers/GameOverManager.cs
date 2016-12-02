using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	Timer timerScript;

	public Text coinText;
	public Text monsterremainingText;
	public Text timerText;
	public Text ratingText;

	public int starCounter = 0;
	public GameObject luso;
	public bool enemycountonceBool = true;
	public bool coincountonceBool = true;
	public bool bosscountonceBool = true;
	public bool timecountonceBool = true;
	public Player.Collision playerCollision;
	public float timer;
	int minutes, seconds;

	void Awake () 
	{
		playerCollision = FindObjectOfType<Player.Collision>();
		timerScript = FindObjectOfType<Timer> ();
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*rotationEuler += new Vector3(0.0f, 2.0f, 0.0f);
		restart.GetComponent<RectTransform>().rotation = Quaternion.Euler(rotationEuler);*/
		//restart.GetComponent<RectTransform>().rotation -= new Quaternion(0.0f, -0.5f, 0.0f, 0.0f);

		timer = timerScript.timer;
		if (GameManager.Instance.enemyCounter == 0) 
		{
			if(enemycountonceBool)
			starCounter++;
			enemycountonceBool = false;
		}
		if(playerCollision.coinCounter >= 10)
		{
			if(coincountonceBool)
				starCounter++;
			coincountonceBool = false;
		}
		if(luso.activeSelf == false)
		{
			if(bosscountonceBool)
				starCounter++;
			bosscountonceBool = false;
		}
		if(timer >= 300)
		{
			if(timecountonceBool)
				starCounter++;
			timecountonceBool = false;
		}	

		coinText.text = playerCollision.coinCounter + "/" + playerCollision.maxCoinCounter;
		monsterremainingText.text = GameManager.Instance.maxEnemyCounter - GameManager.Instance.enemyCounter  + "/" + GameManager.Instance.maxEnemyCounter;
		minutes = (int) timer / 60;
		seconds = (int) timer % 60;
		timerText.text = string.Format ( "{0:00} : {1:00}", minutes, seconds);
		if (starCounter == 1) 
		{
			ratingText.text = "C";
		} 
		else if (starCounter == 2) 
		{
			ratingText.text = "B";
		} 
		else if (starCounter == 3) 
		{
			ratingText.text = "A";
		} 
		else if (starCounter == 4) 
		{
			ratingText.text = "S";
		} 
		else 
		{
			ratingText.text = "D";
		}

	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		timerScript.timer = 900.0f;


	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
