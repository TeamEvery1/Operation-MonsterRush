using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour 
{
	public GameObject firstScene;
	public FadeInFadeOut objectFaderScript;

	private float timer = 0.0f, duration = 0.5f;
	public int sequence = 0;

	// Use this for initialization
	void Awake()
	{
		
	}

	void Start () 
	{
		objectFaderScript = firstScene.GetComponent<FadeInFadeOut>();
		firstScene.SetActive(true);
		objectFaderScript.FadeIn();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(sequence == 0)
		{
			if(timer > duration)
			{
				objectFaderScript.FadeOut();
				StartCoroutine ("AddSequence", 3.0f);
			}
			else 
			{
				timer += Time.deltaTime;
			}
		}
		else if(sequence == 1)
		{
			SceneManager.LoadScene("Main Menu");
		}
	}

	IEnumerator AddSequence(float t)
	{
		yield return new WaitForSeconds (t);
		sequence++;
	}
}
