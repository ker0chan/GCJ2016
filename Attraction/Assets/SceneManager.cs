using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

	public float fadeDuration = 1.0f;
	public CanvasGroup fadeTarget;
	float currentFadeTime = 0.0f;
	int fadingDirection = 0;
	string sceneToLoad;

	public Transform narrativeBackground;

	public delegate void AfterLoadCallback();
	protected AfterLoadCallback afterLoad;

	Queue<string> missions = new Queue<string>();
	public Yarn.Unity.DialogueRunner dialogueRunner;

	// Use this for initialization
	void Start () {
		missions.Enqueue("mission1");
		missions.Enqueue("mission2");
		missions.Enqueue("mission3");
	}
	
	// Update is called once per frame
	void Update () {
		if(fadingDirection != 0)
		{
			currentFadeTime += Time.deltaTime * fadingDirection;

			fadeTarget.alpha = Mathf.Clamp01(currentFadeTime/fadeDuration);

			if(fadingDirection == 1 && currentFadeTime >= fadeDuration)
			{
				Application.LoadLevel(sceneToLoad);
				if(afterLoad != null)
				{
					Debug.Log("callback");
					afterLoad();
				}
				currentFadeTime = fadeDuration;
				fadingDirection = -1;
			} else if(fadingDirection == -1 && currentFadeTime <= 0.0f)
			{
				fadingDirection = 0;
				currentFadeTime = 0.0f;
			}
		}
	}

	void FadeInAndLoad(string scene, AfterLoadCallback callback)
	{
		fadingDirection = 1;
		sceneToLoad = scene;
		afterLoad = callback;
	}
	public bool isLoading()
	{
		return fadingDirection != 0;
	}

	public void OpenNarration()
	{
		string nextDialogue = missions.Dequeue();
		FadeInAndLoad("narrative", () => {
			narrativeBackground.gameObject.SetActive(true);
			dialogueRunner.StartDialogue(nextDialogue);
		});
	}

	public void LaunchMission()
	{
		FadeInAndLoad("arcadeDimitri", () => {
			Debug.Log("test");
			narrativeBackground.gameObject.SetActive(false);
		});
	}
}
