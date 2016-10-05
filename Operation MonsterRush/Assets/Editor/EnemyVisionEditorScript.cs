using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (Enemies.Pathfinding))]
public class NewBehaviourScript : Editor {

	void OnSceneGUI()
	{
		Enemies.Pathfinding enemy = (Enemies.Pathfinding) target;
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
