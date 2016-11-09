using UnityEngine;
using System.Collections;

public class CoconutBehaviors : MonoBehaviour 
{
	public string coconutName;
	[HideInInspector] public bool coconut01, coconut02, coconut03, coconut04, coconut05, coconut06;
	[HideInInspector] public bool coconutC01, coconutC02, coconutC03;

	void Start()
	{
		coconut01 = true;
		coconut02 = true;
		coconut03 = true;
		coconut04 = true;
		coconut05 = true;
		coconut06 = true;
		coconutC01 = true;
		coconutC02 = true;
		coconutC03 = true;
	}
}
