using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour 
{
	public GameObject Character;
	public Transform combo1, combo2, combo3;
	public float combo1Timer, combo2Timer, combo3Timer;
	float combo1Duration = 0.35f, combo2Duration = 0.35f, combo3Duration = 0.45f;

	public void Start()
	{
		Character = GameObject.Find("Character");
		combo1 = Character.transform.Find("Gauntlet_Effect/Combo 1");
		combo2 = Character.transform.Find("Gauntlet_Effect/Combo 2");
		combo3 = Character.transform.Find("Gauntlet_Effect/Combo 3");
	}

	void Update ()
	{
		if (combo1.gameObject.activeSelf)
		{
			if (combo1Timer > combo1Duration)
			{
				combo1.gameObject.SetActive (false);
			}
			else
			{
				combo1Timer += Time.deltaTime;
			}
		}

		if (combo2.gameObject.activeSelf)
		{
			if (combo2Timer > combo2Duration)
			{
				combo2.gameObject.SetActive (false);
			}
			else
			{
				combo2Timer += Time.deltaTime;
			}
		}

		if (combo3.gameObject.activeSelf)
		{
			if (combo3Timer > combo3Duration)
			{
				combo3.gameObject.SetActive (false);
			}
			else
			{
				combo3Timer += Time.deltaTime;
			}
		}
	}

}
