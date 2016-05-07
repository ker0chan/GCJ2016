using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= -17.0f) {
			Destroy (transform.gameObject);
		} 
	}

	public void PickUp()
	{
		Destroy(transform.gameObject);
	}
}
