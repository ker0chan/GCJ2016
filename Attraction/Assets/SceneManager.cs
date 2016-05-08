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
	public LevelManager levelManager;
	public AffinityManager affinityManager;
	public MusicManager musicManager;


	// Use this for initialization
	void Start () {
		missions.Enqueue("mission1");
		missions.Enqueue("mission2");
		missions.Enqueue("mission2");
		missions.Enqueue("mission3");
		missions.Enqueue("mission3");
		missions.Enqueue("end");

		affinityManager = GameObject.Find("AffinityManager").GetComponent<AffinityManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			LaunchMission();
		}
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

		if(nextDialogue == "end")
		{
			Application.LoadLevel("menu");
			return;
		}

		FadeInAndLoad("narrative", () => {
			musicManager.Play("quiet");
			musicManager.Stop("battle");
			narrativeBackground.gameObject.SetActive(true);
			dialogueRunner.StartDialogue(nextDialogue);
		});
	}

	public void Debrief(string currentLevel)
	{
		Player p = GameObject.Find("Player").GetComponent<Player>();
		switch(currentLevel)
		{
			case "mission1":
				if(p.score > 30)
				{
					affinityManager.AddAffinity("mecano", 1);
				} else 
				{
					affinityManager.AddAffinity("pilote", 1);
				}
			break;
			case "mission2":
				if(p.score > 20)
				{
					affinityManager.AddAffinity("bio", 1);
				} else 
				{
					affinityManager.AddAffinity("mecano", 1);
				}
			break;
			case "mission3":
				if(p.score > 30)
				{
					affinityManager.AddAffinity("bio", 1);
				} else 
				{
					affinityManager.AddAffinity("pilote", 1);
				}
			break;
		}

	}

	public void LaunchMission()
	{
		string nextLevel = missions.Dequeue();


		FadeInAndLoad("arcadeDimitri", () => {
			musicManager.Play("battle");
			musicManager.Stop("quiet");
			levelManager.Run();
			narrativeBackground.gameObject.SetActive(false);
			GameObject.Find("LevelManager").GetComponent<LevelManager>().currentLevel = nextLevel;
		});
	}
}
