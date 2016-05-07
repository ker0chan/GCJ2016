using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public float fadeDuration = 1.0f;
	public CanvasGroup fadeTarget;
	float currentFadeTime = 0.0f;
	int fadingDirection = 0;
	string sceneToLoad;

	public Transform narrativeBackground;
	public Transform topicChoice;

	public delegate void AfterLoadCallback();
	protected AfterLoadCallback afterLoad;

	// Use this for initialization
	void Start () {
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
		
		FadeInAndLoad("narrative", () => {
			topicChoice.gameObject.SetActive(true);
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
