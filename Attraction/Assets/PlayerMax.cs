using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	//Vitesse verticale maximale du joueur
	public float maxVelocity;
	//Incréments de vitesse
	public float velocityStep;
	//Vitesse actuelle
	float velocity;
	//Direction (1 = haut, -1 = bas)
	int direction = -1;
	//Limites de mouvement verticales du joueur
	float maxHeight = 5.0f;
	float minHeight = -5.0f;

	//Position d'origine du vaisseau
	Vector3 initialPosition;
	//Position à laquelle le vaisseau a heurté un obstacle
	Vector3 hitPosition;

	//Durée de direction
	public float directionDuration;
	//Temps avant le changement de direction
	float	currentDuration = 0.0f;

	//Durée d'invincibilité
	public float invicibilityDuration;
	//Temps d'invincibilité restant
	float currentInvincibility = 0.0f;

	int score = 0;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		//Mémorise la position de départ du joueur
		initialPosition = transform.position;
		//Initialise le texte du score
		UpdateScore();
	}
	
	// Update is called once per frame
	void Update () {
		//Variable temporaire
		Vector3 currentPosition = transform.position;
		Debug.Log (velocityStep);

		if (isMove ()) {
			currentDuration -= 0.05f;
		}
		if(isInvincible())
		{
			currentInvincibility -= Time.deltaTime;

			//TODO : Ramener le vaisseau à sa position initiale progressivement ?
			//TODO : Ajouter un effet visuel pour indiquer qu'on est invincible
		}

		if (velocityStep > 3.0f)
			velocityStep -= 0.3f;
		//Changement de direction

	
	/*	// Up and Down
		if(Input.GetKey(KeyCode.UpArrow))
		{
			velocityStep = 6.0f;
			direction = 1;
			velocity = 1;
			velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);
			currentPosition.y = Mathf.Clamp(currentPosition.y + velocity*Time.deltaTime, minHeight, maxHeight);
			currentDuration = directionDuration;
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			velocityStep = 6.0f;
			direction = -1;
			velocity = 1;
			velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);
			currentPosition.y = Mathf.Clamp(currentPosition.y + velocity*Time.deltaTime, minHeight, maxHeight);
			currentDuration = directionDuration;
			}
		velocity = 0;
		direction = 0;*/

		//Style JetPack JoyRide
		if (currentDuration <= 0.0f)
			direction = -1;
		velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);
		currentPosition.y = Mathf.Clamp(currentPosition.y + velocity*Time.deltaTime, minHeight, maxHeight);
		if(Input.GetKey(KeyCode.Space))
		{
			currentDuration = directionDuration;
			velocityStep = 6.0f;
			direction = 1;
		}

		//Style Flappy Bird
		/*if (currentDuration <= 0.0f)
			direction = -1;
		velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);
		currentPosition.y = Mathf.Clamp(currentPosition.y + velocity*Time.deltaTime, minHeight, maxHeight);
		if(Input.GetKeyDown(KeyCode.Space))
		{
			currentDuration = directionDuration;
			velocityStep = 6.0f;
			direction = 1;
		}*/

		transform.position = currentPosition;
	}

	//Détection des collisions
	void OnTriggerEnter2D(Collider2D c)
	{
		//Obstacle
		if (c.GetComponent<Obstacle>() != null)
		{
			Hit();
		}

		//Reward
		Reward pickedUpReward = c.GetComponent<Reward>();
		if (pickedUpReward != null)
		{
			PickUp(pickedUpReward);
		}
	}

	//Le joueur se dirige déjà vers le haut ?
	bool isMove()
	{
		return currentDuration > 0.0f;
	}

	//Le joueur est-il invincible en ce moment ?
	bool isInvincible()
	{
		return currentInvincibility > 0.0f;
	}

	//Obstacle touché
	void Hit()
	{
		//Invincibilité => on quitte la fonction
		if(isInvincible()) return;

		//On met le timer d'invincibilité au max
		currentInvincibility = invicibilityDuration;

		//On reset la position du joueur, ainsi que sa vitesse et sa direction
		transform.position = initialPosition;
		direction = -1;
		velocity = 0.0f;
	}

	//Ramassage de ressource
	void PickUp(Reward reward)
	{
		//Invincibilité => on quitte la fonction
		if(isInvincible()) return;

		//Notifie la reward qu'elle a été ramassée
		reward.PickUp();
		//Augmente le score
		score += 1;
		UpdateScore();
	}

	//Affiche le score courant dans la zone de texte "scoreText"
	void UpdateScore()
	{
		scoreText.text = score.ToString();
	}
}
