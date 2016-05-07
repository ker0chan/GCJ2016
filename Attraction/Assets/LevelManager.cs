using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	float levelDuration = 10.0f;
	float currentTime = 0.0f;
	bool running = false;
	SceneManager sceneManager;

	Dictionary<string, LevelData> levelDataDictionary = new Dictionary<string, LevelData>();

	[System.Serializable]
	public struct LevelData
	{
		public string name;
		public GameObject[] obstaclesPrefabs;
		public GameObject[] backgroundsPrefabs;
	}

	public LevelData[] levelDataArray;

	// Use this for initialization
	void Start () {
		running = true;
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
		foreach(LevelData ld in levelDataArray)
		{
			levelDataDictionary.Add(ld.name, ld);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(running)
		{
			currentTime += Time.deltaTime;
			if (currentTime >= levelDuration)
			{
				running = false;
				sceneManager.OpenNarration();
			}
		}
	}
	public float GetTimeRatio()
	{
		return currentTime/levelDuration;
	}
	public LevelData GetLevelData(string levelName)
	{
		return levelDataDictionary[levelName];
	}
}
