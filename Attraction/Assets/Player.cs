using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxVelocity;
	public float velocityStep;
	public float velocity;
	public int direction = -1;
	float maxHeight = 5.0f;
	float minHeight = -5.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;

		velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);

		currentPosition.y = Mathf.Clamp(currentPosition.y + velocity*Time.deltaTime, minHeight, maxHeight);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			direction = -direction;
		}

		transform.position = currentPosition;
	}
}
