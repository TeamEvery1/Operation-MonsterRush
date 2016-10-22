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

	GUIManagerScript guiScript;
	CaptureView captureViewScript;
	public Canvas guiManager;
	public Canvas catchManager;
	public GameObject[] enemies;
	public Text teachText;
	public bool firstTimeCollided;

	void Awake()
	{
		guiScript = FindObjectOfType<GUIManagerScript> ();
		captureViewScript = FindObjectOfType <CaptureView>();
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	// Use this for initialization
	void Start () 
	{
		catchManager.enabled = false;
		teachText.enabled = false;
		firstTimeCollided = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (guiScript.enemyCollided) 
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
}
