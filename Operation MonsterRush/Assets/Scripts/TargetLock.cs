using UnityEngine;
using System.Collections;

public class TargetLock : MonoBehaviour
{
	Player.Combat playerCombatScript;

	void Awake()
	{
		playerCombatScript = FindObjectOfType <Player.Combat> ();
	}

	void Update()
	{
		//transform.position = playerCombatScript.closest.transform.position;

		Vector3 pos = Camera.main.WorldToScreenPoint (new Vector3 (playerCombatScript.closest.transform.position.x - 0.5f, playerCombatScript.closest.transform.position.y + 0.3f, playerCombatScript.closest.transform.position.z));
		//RectTransformUtility.ScreenPointToLocalPointInRectangle (this.transform.parent.transform as RectTransform, playerCombatScript.closest.transform.position);
		this.transform.position = pos;
	}
}
