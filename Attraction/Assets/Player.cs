using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxVelocity;
	public float velocityStep;
	public float velocity;
	public int direction = -1;
	float maxHeight = 5.0f;
	float minHeight = -5.0f;

	Vector3 initialPosition;

	public float invicibilityDuration;
	float currentInvincibility = 0.0f;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Variable temporaire
		Vector3 currentPosition = transform.position;

		if(isInvincible())
		{
			currentInvincibility -= Time.deltaTime;

			//TODO : Ramener le vaisseau à sa position initiale progressivement ?
			//TODO : Ajouter un effet visuel pour indiquer qu'on est invincible
		}
		//Vitesse
		velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);
		//Position
		currentPosition.y = Mathf.Clamp(currentPosition.y + velocity*Time.deltaTime, minHeight, maxHeight);


		//Changement de direction
		if(Input.GetKeyDown(KeyCode.Space))
		{
			direction = -direction;
		}

		transform.position = currentPosition;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.GetComponent<Obstacle>() != null)
		{
			Hit();
		}
		if (c.GetComponent<Rewards>() != null)
		{
			//TODO
		}
	}

	bool isInvincible()
	{
		return currentInvincibility > 0.0f;
	}

	void Hit()
	{
		Debug.Log("Hit");
		if(isInvincible()) return;


		currentInvincibility = invicibilityDuration;
		transform.position = initialPosition;
		direction = -1;
		velocity = 0.0f;

	}
}
