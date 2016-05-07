using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -13.0f && transform.position.x > -13.4f) {
			Destroy (transform.gameObject);
		}
	}
}
