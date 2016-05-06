using UnityEngine;
using System.Collections;

public class Rewards : MonoBehaviour {
	
	public GameObject[] rewards;

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform t in transform) {
			Vector3 pos = t.position;
			if (pos.y < 0)
				pos.y += 0.4f;
			else {
				pos.y -= 0.4f;
			}
			t.position = pos;
		}
	}

	void Spawn() {
		int rand = Random.Range(0, rewards.Length);
		GameObject temp = (GameObject) Instantiate (rewards[rand]);
		temp.transform.SetParent(transform, true);
	}
}