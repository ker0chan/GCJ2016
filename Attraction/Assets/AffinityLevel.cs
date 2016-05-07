using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AffinityLevel : MonoBehaviour {

	public Image barImage;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetAmount(int amount)
	{
		// 1% = 3 pixels. C'est comme ça.
		barImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, amount * 3.0f);
	}
}
