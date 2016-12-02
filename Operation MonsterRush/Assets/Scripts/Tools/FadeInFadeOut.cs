using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour 
{
	public float fadeDelay = 0.0f, fadeDuration = 0.5f;
	public bool fadeIn = false, fadeOut = false;
	private bool fadeSequence = false;

	private Color[] colors; 

	IEnumerator Start()
	{
		yield return new WaitForSeconds(fadeDelay);

		if(fadeIn)
		{
			fadeSequence = true;
			FadeIn();
		} 
		if(fadeOut)
		{
			FadeOut(fadeDuration);
		}
	}

	//! Check alpha value of the object
	float MaxAlpha()
	{
		float maxAlpha = 0.0f;
		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>();
		foreach(Renderer item in rendererObjects)
		{
			maxAlpha = Mathf.Max(maxAlpha, item.material.color.a);
		}
		return maxAlpha;
	}

	//fade sequence 
	IEnumerator FadeSequence(float fadingOutTime)
	{
		bool fadingOut = (fadingOutTime < 0.0f);
		float fadingOutSpeed = 1.0f/ fadingOutTime;

		Renderer[] rendererObjects = GetComponentsInChildren<Renderer>();

		if(colors == null)
		{
			colors = new Color[rendererObjects.Length];

			for(int i = 0; i < rendererObjects.Length; i++)
			{
				colors[i] = rendererObjects[i].material.color;
			}
		}

		for(int i = 0; i < rendererObjects.Length; i++)
			rendererObjects[i].enabled = true;

		float alphaValue = MaxAlpha();

		if(fadeSequence && !fadingOut)
		{
			alphaValue = 0.0f;
			fadeSequence = false;
		}

		while((alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut))
		{
			alphaValue += Time.deltaTime * fadingOutSpeed;

			for(int i = 0; i < rendererObjects.Length; i++)
			{
				Color newColor = (colors != null ? colors[i] : rendererObjects[i].material.color);
				newColor.a = Mathf.Min(newColor.a, alphaValue);
				newColor.a = Mathf.Clamp(newColor.a, 0.0f, 1.0f);
				rendererObjects[i].material.SetColor("_Color",newColor);
			}
			yield return null;
		}

		if(fadingOut)
		{
			for(int i = 0; i < rendererObjects.Length; i++)
			{
				rendererObjects[i].enabled = false;
			}
		}
	}

	public void FadeIn()
	{
		FadeIn (fadeDuration);
	}

	public void FadeOut()
	{
		FadeOut (fadeDuration);
	}

	void FadeIn(float newFadeTime)
	{
		StopAllCoroutines();
		StartCoroutine("FadeSequence", newFadeTime);
	}

	void FadeOut(float newFadeTime)
	{
		StopAllCoroutines();
		StartCoroutine("FadeSequence", -newFadeTime);
	}
}
