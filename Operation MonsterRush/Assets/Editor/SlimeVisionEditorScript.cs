﻿
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor (typeof (Enemies.SlimeBehaviour))]
public class NewNewBehaviourScript : Editor {

	void OnSceneGUI()
	{
		Enemies.SlimeBehaviour enemy = (Enemies.SlimeBehaviour) target;
		Handles.color = Color.black;
		Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.viewRadius);
		Vector3 ViewAngleA = enemy.dirFromAngle(-enemy.viewAngle /2, false);
		Vector3 ViewAngleB = enemy.dirFromAngle(enemy.viewAngle /2, false);

		Handles.DrawLine(enemy.transform.position, enemy.transform.position + ViewAngleA * enemy.viewRadius);
		Handles.DrawLine(enemy.transform.position, enemy.transform.position + ViewAngleB * enemy.viewRadius);

		Handles.color = Color.red;

		if(enemy.VisibleTarget != null)
		{
		 	Handles.DrawLine(enemy.transform.position, enemy.VisibleTarget.position);
		}
	}
}
