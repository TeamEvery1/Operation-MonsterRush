using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (EnemyScript))]
public class NewBehaviourScript : Editor {

	void OnSceneGUI()
	{
		EnemyScript enemy = (EnemyScript)target;
		Handles.color = Color.black;
		Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.ViewRadius);
		Vector3 ViewAngleA = enemy.DirFromAngle(-enemy.ViewAngle /2, false);
		Vector3 ViewAngleB = enemy.DirFromAngle(enemy.ViewAngle /2, false);

		Handles.DrawLine(enemy.transform.position, enemy.transform.position + ViewAngleA * enemy.ViewRadius);
		Handles.DrawLine(enemy.transform.position, enemy.transform.position + ViewAngleB * enemy.ViewRadius);

		Handles.color = Color.red;
		if(enemy.VisibleTarget != null)
		{
		 	Handles.DrawLine(enemy.transform.position, enemy.VisibleTarget.position);
		}
	}
}
