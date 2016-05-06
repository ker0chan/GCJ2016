using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	float gravity = 0.15f;
	int direction = -1;
	float maxHeight = 5.0f;
	float minHeight = -5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;


		if(transform.position.y >= minHeight && transform.position.y <= maxHeight)
		{
			currentPosition.y += gravity * direction;
		} else 
		{
			currentPosition.y = Mathf.Clamp(currentPosition.y, minHeight, maxHeight);
			direction = -direction;
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			direction = -direction;
		}

		transform.position = currentPosition;
	}
}
