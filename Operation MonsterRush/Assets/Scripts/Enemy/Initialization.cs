using UnityEngine;
using System.Collections;

public class Initialization : MonoBehaviour
{
	public LevelEditor LevelEditorScript;
	public Enemies.Info[] enemyInfoScript;

	void Awake()
	{
		LevelEditorScript = GameObject.Find ("Level").GetComponent <LevelEditor>();
	}

	void Start()
	{
		for (int i = 0; i < LevelEditorScript.monsterList.Count; i++)
		{
			Instantiate (LevelEditorScript.monsterList[i].monsterPrefab, LevelEditorScript.monsterList[i].monsterPosition, Quaternion.identity);
			LevelEditorScript.monsterList[i].monsterPrefab.GetComponent <Enemies.Info>().monsterName = LevelEditorScript.monsterList[i].monsterName[Random.Range (0, LevelEditorScript.monsterList[i].monsterName.Length - 1)];
		}
	}
}

