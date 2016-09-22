using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour 
{
	public GameObject Character;
	public Transform combo1, combo2, combo3;

	public void Start()
	{
		Character = GameObject.Find("Character");
		combo1 = Character.transform.Find("Gauntlet_Effect/Combo 1");
		combo2 = Character.transform.Find("Gauntlet_Effect/Combo 2");
		combo3 = Character.transform.Find("Gauntlet_Effect/Combo 3");
	}
	public void Combo1Start()
	{
		combo1.gameObject.SetActive(true);
	}

	public void Combo1End()
	{
		combo1.gameObject.SetActive(false);
	}

	public void Combo2Start()
	{
		combo2.gameObject.SetActive(true);
	}

	public void Combo2End()
	{
		combo2.gameObject.SetActive(false);
	}

	public void Combo3Start()
	{
		combo3.gameObject.SetActive(true);
	}

	public void Combo3End()
	{
		combo3.gameObject.SetActive(false);
	}

}
