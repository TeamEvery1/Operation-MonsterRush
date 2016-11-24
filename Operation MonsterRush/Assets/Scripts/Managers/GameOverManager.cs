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

	public bool enemycountonceBool = true;
	public bool coincountonceBool = true;
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
		coinText.text = playerCollision.coinCounter + "/" + playerCollision.maxCoinCounter;
		monsterremainingText.text = GameManager.Instance.enemyCounter  + "/" +GameManager.Instance.maxEnemyCounter;
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
		else if (starCounter == 3 && timer >= 300) 
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
