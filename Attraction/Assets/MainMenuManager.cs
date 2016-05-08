using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public Button button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play()
	{
		button.interactable = false;
		UnityEngine.SceneManagement.SceneManager.LoadScene("narrative");
	}
}

