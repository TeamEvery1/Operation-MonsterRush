using UnityEngine;
using System.Collections;
using System;

public class GUIColorBlock : IDisposable
{
	private Color prevColour;

	public GUIColorBlock Begin (Color newColor)
	{
		prevColour = GUI.color;
		GUI.color = newColor;
		return this;
	}

	public void Dispose ()
	{
		GUI.color = prevColour;
	}
}

public static class guiLib // for GUILibrary
{
	private static GUIColorBlock colorBlock = new GUIColorBlock ();

	public static GUIColorBlock ColorBlock(Color c)
	{
		return colorBlock.Begin (c);
	}
}

