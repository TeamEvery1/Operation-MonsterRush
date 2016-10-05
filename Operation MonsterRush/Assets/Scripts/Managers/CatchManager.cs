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
	public Canvas guiManager;
	public Canvas catchManager;
	void Awake()
	{
		guiScript = FindObjectOfType<GUIManagerScript> ();

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
		}
		else
		{
			catchManager.enabled = false;
			guiManager.enabled = true;
		}
	}
}
