﻿using UnityEngine;
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
		// 1% = 2.88 units. C'est comme ça.
		// (Ca dépend de la taille de l'image, en fait)
		barImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, amount * 2.88f);
	}
}
