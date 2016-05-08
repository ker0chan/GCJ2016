﻿using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {

	LevelManager levelManager;
	GameObject[] obstacles;
	GameObject[] backgrounds;
	public Transform obstaclesContainer;
	public Transform backgroundsContainer;
	public float obstacleSpeed;
	public float backgroundSpeed;
	public float obstacleSpawnInterval;
	float currentObstacleSpawnTimer = 0.0f;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		levelManager.Init();
		LevelManager.LevelData currentLevelData = levelManager.GetCurrentLevelData();
		obstacles = currentLevelData.obstaclesPrefabs;
		Debug.Log(obstacles.Length);
		backgrounds = currentLevelData.backgroundsPrefabs;
		Debug.Log(backgrounds.Length);
		SpawnBackground();
		SpawnBackground();
	}

	// Update is called once per frame
	void Update () {
		foreach(Transform t in obstaclesContainer)
		{
			Vector3 obstaclePosition = t.position;
			obstaclePosition.x += obstacleSpeed*Time.deltaTime*SpeedModifierManager.speedModifier;
			t.position = obstaclePosition;
		}
		foreach(Transform t in backgroundsContainer)
		{
			Vector3 backgroundPosition = t.localPosition;
			backgroundPosition.x += backgroundSpeed*Time.deltaTime*SpeedModifierManager.speedModifier;
			t.localPosition = backgroundPosition;
		}

		currentObstacleSpawnTimer += Time.deltaTime*SpeedModifierManager.speedModifier;
		if(currentObstacleSpawnTimer >= obstacleSpawnInterval)
		{
			currentObstacleSpawnTimer = 0.0f;
			SpawnObstacle();
		}

		if(backgroundsContainer.childCount < 3)
		{
			SpawnBackground();
		}
	}

	void SpawnObstacle()
	{
		int randomObstacle = Random.Range(0, obstacles.Length);
		GameObject obstacle = (GameObject) Instantiate (obstacles[randomObstacle]);
		obstacle.GetComponent<Obstacle>().SetGravityZone(Random.Range(-1, 2));
		obstacle.transform.SetParent(obstaclesContainer, false);
	}
	void SpawnBackground()
	{
		int randomBackground = Random.Range(0, backgrounds.Length);
		GameObject background = (GameObject) Instantiate (backgrounds[randomBackground]);
		if(backgroundsContainer.childCount > 0)
		{
			Transform lastBackground = backgroundsContainer.GetChild(backgroundsContainer.childCount - 1);
			background.transform.position = new Vector3(
				lastBackground.localPosition.x + lastBackground.GetComponentInChildren<SpriteRenderer>().bounds.size.x,
				0,
				0);
		}
		background.transform.SetParent(backgroundsContainer, false);
	}
}
