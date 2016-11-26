using UnityEngine;
using System.Collections;

public class BoxShakeScripts : MonoBehaviour 
{
	private MeshRenderer meshRenderer;
	[HideInInspector] public float alphaValue;

	private void Start()
	{
		meshRenderer = this.GetComponent<MeshRenderer>();
		alphaValue = 0.5f;
	}

	private void Update()
	{
		meshRenderer.material.SetFloat("_Transparency", alphaValue);
	}
}
