using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class Initialization : MonoBehaviour
{
	private LevelEditor LevelEditorScript;
	private Enemies.Info[] enemyInfoScript;
	private Enemies.Pathfinding enemyPathfindingScript;

	void Awake()
	{
		LevelEditorScript = GameObject.Find ("Level").GetComponent <LevelEditor>();
		Debug.Log (LevelEditorScript.monsterList.Count);
	}

	void Start()
	{
		for (int i = 0; i < LevelEditorScript.monsterList.Count; i++)
		{
			LevelEditorScript.monsterList[i].monsterPrefab.GetComponent <Enemies.Info>().monsterName = LevelEditorScript.monsterList[i].monsterName[Random.Range (0, LevelEditorScript.monsterList[i].monsterName.Length - 1)];

			for (int j = 0; j <  LevelEditorScript.monsterList[i].wanderPoint.Length; j++)
			{
				LevelEditorScript.monsterList[i].monsterPrefab.GetComponent <Enemies.Pathfinding>().wanderPoint[j] = LevelEditorScript.monsterList[i].wanderPoint[j];
			}
		
			for (int j = 0; j <  LevelEditorScript.monsterList[i].desPoint.Length; j++)
			{
				LevelEditorScript.monsterList[i].monsterPrefab.GetComponent <Enemies.Pathfinding>().desPoint[j] = LevelEditorScript.monsterList[i].desPoint[j];
			}
		}
	}

	[ContextMenu ("Spawn Enemy")]
	void SpawnEnemies()
	{
		for (int i = 0; i < LevelEditorScript.monsterList.Count; i++)
		{
			if(Application.isEditor && !Application.isPlaying)
			{
				Instantiate (LevelEditorScript.monsterList[i].monsterPrefab, LevelEditorScript.monsterList[i].monsterPosition, Quaternion.identity);
			}
		}
	}
}

