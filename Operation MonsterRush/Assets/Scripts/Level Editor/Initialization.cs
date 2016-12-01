﻿
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Initialization : MonoBehaviour
{
	private LevelEditor LevelEditorScript;
	private Enemies.Info[] enemyInfoScript;
	private Enemies.Pathfinding enemyPathfindingScript;

	void Awake()
	{
		LevelEditorScript = GameObject.Find ("Level").GetComponent <LevelEditor>();
	}

	void Start()
	{
	}

	[ContextMenu ("Spawn Enemy")]
	void SpawnEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject[] fakes = GameObject.FindGameObjectsWithTag ("Fake");
		GameObject[] slimes = GameObject.FindGameObjectsWithTag ("Slime");
		GameObject luso = GameObject.FindGameObjectWithTag ("Luso");

		foreach (GameObject enemy in enemies)
		{
			SafeDestroy (enemy.gameObject);
		}

		foreach  (GameObject fake in fakes)
		{
			SafeDestroy (fake.gameObject);
		}

		foreach (GameObject slime in slimes)
		{
			SafeDestroy (slime.gameObject);
		}

		SafeDestroy (luso.gameObject);

		for (int i = 0; i < LevelEditorScript.monsterList.Count; i++)
		{
			if(Application.isEditor && !Application.isPlaying)
			{
				GameObject clone = (GameObject) Instantiate (LevelEditorScript.monsterList[i].monsterPrefab, LevelEditorScript.monsterList[i].monsterPosition, this.transform.rotation);
				clone.transform.parent = GameObject.Find ("Monsters").transform;

				if (clone.GetComponent <Enemies.Info> ())
				{
					clone.GetComponent <Enemies.Info> ().id = LevelEditorScript.monsterList[i].monsterID;
				}

				//Debug.Log (LevelEditorScript.monsterList[i].wanderPoint.Length);
				if (LevelEditorScript.monsterList[i].desPoint.Length == 0)
				{
					continue;
				}
				else
				{
					for (int j = 0; j <  LevelEditorScript.monsterList[i].desPoint.Length; j++)
					{
						clone.GetComponent <Enemies.Pathfinding>().desPoint[j] = LevelEditorScript.monsterList[i].desPoint[j];
					}
				}

				if (LevelEditorScript.monsterList[i].wanderPoint.Length == 0)
				{
					continue;
				}
				else
				{
					for (int j = 0; j < LevelEditorScript.monsterList[i].wanderPoint.Length; j++)
					{
						clone.GetComponent <Enemies.Pathfinding>().wanderPoint[j] = LevelEditorScript.monsterList[i].wanderPoint[j];
					}
				}


			}
		}
	}

	public static T SafeDestroy<T> (T obj) where T : Object
	{
		if (Application.isEditor)
		{
			Object.DestroyImmediate (obj);
		}
		else
		{
			Object.Destroy (obj);
		}

		return null;
	}

	public static T SafeDestroyGameObject<T> (T component) where T : Component
	{
		if (component != null)
		{
			SafeDestroy (component.gameObject);
		}

		return null;
	}
}

