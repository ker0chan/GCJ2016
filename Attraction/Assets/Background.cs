using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x <= -17.0f) {
			Destroy (transform.gameObject);
		} 
	}
}
