using UnityEngine;
using System.Collections;

public class Rewards : MonoBehaviour {
	
	public GameObject[] rewards;
	public float minWidth = -13.0f;
	public float maxHeight = 5.0f;
	public float minHeight = -5.0f;

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform t in transform) {
			Vector3 pos = t.position;
			pos.x -= 0.4f;
			t.position = pos;
			if (t.position.x <= minWidth) {
				Destroy (t.gameObject);
				Spawn ();
			}
		}
	}

	void Spawn() {
		int rand = Random.Range(0, rewards.Length);	
		GameObject temp = (GameObject) Instantiate (rewards[rand]);
		temp.transform.SetParent(transform, false);
		Vector3 pos = temp.transform.position;
		pos.y -= Random.Range (minHeight, maxHeight);
		temp.transform.position = pos;
	}
}