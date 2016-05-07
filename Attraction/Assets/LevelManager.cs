using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	float levelDuration = 10.0f;
	float currentTime = 0.0f;
	bool running = false;
	SceneManager sceneManager;

	// Use this for initialization
	void Start () {
		running = true;
		sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
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
}
