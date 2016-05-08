using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	public RectTransform cursor;
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
		Debug.Log(amount);
		Debug.Log(parentWidth);
		cursor.localPosition = new Vector3(parentWidth * amount - parentWidth/2, 0, 0) ;
	}
}
