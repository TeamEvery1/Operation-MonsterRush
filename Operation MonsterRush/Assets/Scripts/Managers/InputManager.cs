using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour 
{
	private static InputManager instance;

	public static InputManager Instance
	{
		get
		{
			if( instance == null )
			{
				GameObject obj = new GameObject ("Input Manager");
				obj.AddComponent <InputManager> ();
			}
			return instance;
		}
	}

	#if (UNITY_ANDROID || UNITY_IPHONE)
		Vector2 lastTouchPos;
	#else 
		Vector2 lastMousePos;		
	#endif

	public delegate bool DelegateTouchEvent (int fingerID, Vector2 pos); 
	public delegate bool DelegateDragEvent (int fingerID, Vector2 pos, Vector2 delta);
	public static event DelegateTouchEvent touchUp;
	public static event DelegateTouchEvent touchDown;
	public static event DelegateDragEvent touchDrag;
	public static event DelegateDragEvent touchStop;

	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		#if UNITY_EDITOR
		if(UnityEditor.EditorApplication.isRemoteConnected)	
		{
			Debug.Log ("Remote Connected");
		}
		#endif

		#if ((UNITY_ANDROID || UNITY_IPHONE))
		{
		// DO TOUCH HERE
			foreach (Touch touch in Input.touches)
			{
				if(touch.phase == TouchPhase.Began)
				{
					foreach (DelegateTouchEvent del in touchDown.GetInvocationList())
					{
						if(del(touch.fingerId,touch.position))
						{
							break;
						}
					}
				}

				else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					foreach (DelegateTouchEvent del in touchUp.GetInvocationList())
					{
						if(del(touch.fingerId,touch.position))
						{
							break;
						}
					}
				}

				else if(touch.phase == TouchPhase.Moved)
				{
					foreach (DelegateDragEvent del in touchDrag.GetInvocationList())
					{
						if(del(touch.fingerId, touch.position, touch.deltaPosition))
						{
							break;
						}
					}
				}

				else if(touch.phase == TouchPhase.Stationary)
				{
					foreach (DelegateDragEvent del in touchStop.GetInvocationList())
					{
						if(del(touch.fingerId, touch.position, touch.deltaPosition))
						{
							break;
						}
					}
				}
			}
		}
		#else
		{
			// DO MOUSE HERE
			Vector2 pos = Input.mousePosition;
			if(Input.GetMouseButtonDown(0))
			{
				lastMousePos = pos;
				foreach (DelegateTouchEvent del in touchDown.GetInvocationList())
				{
					if(del(0,pos))
					{
						break;
					}
				}
			}

			else if (Input.GetMouseButtonUp(0))
			{
				foreach (DelegateTouchEvent del in touchUp.GetInvocationList())
				{
					if(del(0,pos)) 
					{
						break;
					}
				}           
			}

			if(Input.GetMouseButton(0))
			{
				Vector2 delta = pos - lastMousePos;

				lastMousePos = pos;

				foreach (DelegateDragEvent del in touchDrag.GetInvocationList())
				{
					if(del(0,pos,delta))
					{
						break;
					}
				}
			}

			else if(!Input.GetMouseButton(0))
			{
				Vector2 delta = pos - lastMousePos;

				lastMousePos = pos;

				foreach (DelegateDragEvent del in touchStop.GetInvocationList())
				{
					if(del(0,pos,delta))
					{
						break;
					}
				}
			}		
		

		}
		#endif
	}
}
