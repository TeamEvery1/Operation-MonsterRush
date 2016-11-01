using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (LevelEditor))]
public class CustomLevelEditor : Editor 
{
	enum displayFieldType {DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields}

	displayFieldType DisplayFieldType;

	LevelEditor t; 
	SerializedObject GetTarget;
	SerializedProperty ThisList;
	int ListSize;

	void OnEnable ()
	{
		t = (LevelEditor) target;
		GetTarget = new SerializedObject (t);
		ThisList = GetTarget.FindProperty ("monsterList");
	}

	public override void OnInspectorGUI()
	{
		GetTarget.Update();

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		DisplayFieldType = (displayFieldType) EditorGUILayout.EnumPopup ("", DisplayFieldType);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("Define the size of the list with a number");

		ListSize = ThisList.arraySize;
		ListSize = EditorGUILayout.IntField ("List Size", ListSize);

		if (ListSize != ThisList.arraySize)
		{
			while (ListSize > ThisList.arraySize)
			{
				ThisList.InsertArrayElementAtIndex (ThisList.arraySize);
			}
			while (ListSize < ThisList.arraySize)
			{
				ThisList.DeleteArrayElementAtIndex (ThisList.arraySize - 1);
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField ("\t\t            Or");
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		// Add a new item to the list with a button
		EditorGUILayout.LabelField ("Add a new list with a button");

		if (GUILayout.Button ("Add"))
		{
			t.monsterList.Add (new LevelEditor.MonsterSpawnOrder ());
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		// Display our list to the inspector window
		for (int i = 0; i < ThisList.arraySize; i++)
		{
			SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex (i);
			SerializedProperty monsterID = MyListRef.FindPropertyRelative ("monsterID");
			SerializedProperty monsterType = MyListRef.FindPropertyRelative ("monsterType");
			SerializedProperty monsterName = MyListRef.FindPropertyRelative ("monsterName");
			SerializedProperty monsterPrefab = MyListRef.FindPropertyRelative ("monsterPrefab");
			SerializedProperty monsterPosition = MyListRef.FindPropertyRelative ("monsterPosition");

			// Display property fields in two ways
			if (DisplayFieldType == 0) // Automatic, no customization 
			{
				EditorGUILayout.LabelField ("Automatic Field By Property Type");
				EditorGUILayout.PropertyField (monsterID);
				EditorGUILayout.PropertyField (monsterType);
				EditorGUILayout.PropertyField (monsterPrefab);
				EditorGUILayout.PropertyField (monsterPosition);

				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.LabelField ("Monster Name List");

				if (GUILayout.Button ("Add New Index", GUILayout.MaxWidth (130), GUILayout.MaxHeight (20)))
				{
					monsterName.InsertArrayElementAtIndex (monsterName.arraySize);
					monsterName.GetArrayElementAtIndex (monsterName.arraySize - 1).stringValue = "";
				}

				for (int j = 0; j < monsterName.arraySize; j++)
				{
					EditorGUILayout.PropertyField (monsterName.GetArrayElementAtIndex (j));

					if (GUILayout.Button ("Remove (" + j.ToString() + ")", GUILayout.MaxWidth (100), GUILayout.MaxHeight (15)))
					{
						monsterName.DeleteArrayElementAtIndex (j);
					}
				}
			}
			else // Full custom GUI Layout 
			{
				EditorGUILayout.LabelField ("Customizable Field With GUI");
				monsterPrefab.objectReferenceValue = EditorGUILayout.ObjectField ("My Custom Prefab", monsterPrefab.objectReferenceValue, typeof (GameObject), true);
				//monsterType.enumValueIndex = EditorGUILayout.EnumMaskField ();
				monsterID.intValue = EditorGUILayout.IntField (monsterID.intValue);
				monsterPosition.vector3Value = EditorGUILayout.Vector3Field ("My Custom Vector 3", monsterPosition.vector3Value);

				EditorGUILayout.Space ();
				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ("Array Fields");

				if (GUILayout.Button ("Add New Index", GUILayout.MaxWidth (130), GUILayout.MaxHeight (20)))
				{
					monsterName.InsertArrayElementAtIndex (monsterName.arraySize);
					monsterName.GetArrayElementAtIndex (monsterName.arraySize - 1).stringValue = "";
				}

				for (int j = 0; j < monsterName.arraySize; j++)
				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField ("My Custom Name (" + j.ToString() + ")", GUILayout.MaxWidth (120));
					monsterName.GetArrayElementAtIndex (j).stringValue = EditorGUILayout.TextField("", monsterName.GetArrayElementAtIndex (j).stringValue, GUILayout.MaxWidth (100));

					if (GUILayout.Button ("-", GUILayout.MaxWidth (15), GUILayout.MaxHeight (15)))
					{
						monsterName.DeleteArrayElementAtIndex (j);
					}
					EditorGUILayout.EndHorizontal();
				}
			}

			EditorGUILayout.Space();

			if (GUILayout.Button ("Remove This Index"))
			{
				ThisList.DeleteArrayElementAtIndex (i);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			GetTarget.ApplyModifiedProperties();
		}
	}
}
