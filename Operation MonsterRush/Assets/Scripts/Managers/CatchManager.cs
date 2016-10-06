using UnityEngine;
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
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (guiScript.enemyCollided) 
		{
			catchManager.enabled = true;
			guiManager.enabled = false;

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
