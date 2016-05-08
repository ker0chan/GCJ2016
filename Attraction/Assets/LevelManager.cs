using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	float levelDuration = 60.0f;
	float currentTime = 0.0f;
	bool running = false;
	SceneManager sceneManager;

	public string currentLevel;

	Dictionary<string, LevelData> levelDataDictionary = new Dictionary<string, LevelData>();

	[System.Serializable]
	public struct LevelData
	{
		public string name;
		public GameObject[] obstaclesPrefabs;
		public GameObject[] backgroundsPrefabs;
	}

	public LevelData[] levelDataArray;

	bool inited = false;

	// Use this for initialization
	void Start () {
		
		GameObject sceneManagerGO = GameObject.Find("SceneManager");
		if(sceneManagerGO != null)
		{
			sceneManager = sceneManagerGO.GetComponent<SceneManager>();
		}

		Init();
	}

	public void Init()
	{
		if(inited) return;
		inited = true;
		foreach(LevelData ld in levelDataArray)
		{
			levelDataDictionary.Add(ld.name, ld);
		}
	}

	public void Run()
	{
		running = true;
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
	public LevelData GetCurrentLevelData()
	{
		return levelDataDictionary[currentLevel];
	}
}
