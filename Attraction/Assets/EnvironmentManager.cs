using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {

	public GameObject[] obstacles;
	public Transform obstaclesContainer;
	public float speed;
	public float spawnInterval;
	float currentSpawnTimer = 0.0f;

	// Use this for initialization
	void Start () {
		Spawn ();
	}

	// Update is called once per frame
	void Update () {
		foreach(Transform t in obstaclesContainer)
		{
			Vector3 obstaclePosition = t.position;
			obstaclePosition.x += speed*Time.deltaTime;
			t.position = obstaclePosition;
		}

		currentSpawnTimer += Time.deltaTime;
		if(currentSpawnTimer >= spawnInterval)
		{
			currentSpawnTimer = 0.0f;
			Spawn();
		}
	}

	void Spawn()
	{
		int randomObstacle = Random.Range(0, obstacles.Length);
		GameObject obstacle = (GameObject) Instantiate (obstacles[randomObstacle]);
		obstacle.transform.SetParent(obstaclesContainer, false);
	}
}
