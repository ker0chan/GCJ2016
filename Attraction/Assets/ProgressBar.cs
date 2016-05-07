using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	Transform cursor;
	float parentWidth;
	LevelManager levelManager;
	// Use this for initialization
	void Start () {
		parentWidth = ((RectTransform)cursor.parent.transform).rect.width;
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		Debug.Log(levelManager);
	}
	
	// Update is called once per frame
	void Update () {
		SetProgress(levelManager.GetTimeRatio());
	}

	void SetProgress(float amount)
	{
		cursor.localPosition = new Vector3(parentWidth * amount, 0, 0);
	}
}
