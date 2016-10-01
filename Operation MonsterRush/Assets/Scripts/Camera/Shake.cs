using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour 
{
	private float shakeDuration;
	private float shakeSpeed;
	private float magnitude;

	private Vector3 originalCameraPos;

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			shakeDuration = 0.2f;
			shakeSpeed = 1.0f;
			magnitude = 0.06f;
			Time.timeScale = 1f;
			//PlayShake ();
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			shakeDuration = 2f;
			shakeSpeed = 2.0f;
			magnitude = 0.1f;
			//PlayShake ();
		}
		else if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			shakeDuration = 0.2f;
			shakeSpeed = 1.0f;
			magnitude = 0.06f;
			Time.timeScale = 0.3f;
			//PlayShake ();
		}
			

	}

	public void PlayShake ()
	{
		StartCoroutine ("ShakeFunction");
	}
		
	IEnumerator ShakeFunction ()
	{
		float elapsed = 0.0f;

		originalCameraPos = Camera.main.transform.position;

		while (elapsed < shakeDuration) 
		{
			elapsed += Time.deltaTime; 

			float percentComplete = elapsed / shakeDuration;
			float damper = 1.0f - Mathf.Clamp (4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;

			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3 (x, y, originalCameraPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCameraPos;
	}
}
