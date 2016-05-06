using UnityEngine;
using System.Collections;

public class EnvironmentManager : MonoBehaviour {

	public GameObject[] obstacles;
	public Transform obstaclesContainer;


	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Transform t in obstaclesContainer)
		{
			Vector3 obstaclePosition = t.position;
			obstaclePosition.x -= 0.4f;
			t.position = obstaclePosition;
			if(t.position.x > 3.5f && t.position.x <= 3.9f)
			{
				Spawn();
			}
		}
	}

	void Spawn()
	{
		int randomObstacle = Random.Range(0, obstacles.Length);
		GameObject obstacle = (GameObject) Instantiate (obstacles[randomObstacle]);
		obstacle.transform.SetParent(obstaclesContainer, false);
	}
}
