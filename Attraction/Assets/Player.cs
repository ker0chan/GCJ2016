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

	//le player est dans une zone de gravité (0 = non)
	float		Gravity = 0;
	float		AttractionSpeed = 3f;

	//Niveau de bouclier actuel
	int shieldLevel;
	//Niveau de bouclier standard
	int defaultShieldLevel = 5;

	float teleportCooldown = 5.0f;
	float currentTeleportCooldown = 0.0f;
	bool teleporting = false;
	public Transform cooldownIndicator; 

	//Position d'origine du vaisseau
	Vector3 initialPosition;
	//Position à laquelle le vaisseau a heurté un obstacle
	Vector3 hitPosition;

	//Durée d'invincibilité
	public float invicibilityDuration;
	//Temps d'invincibilité restant
	float currentInvincibility = 0.0f;

	public ParticleSystem teleportParticleSystem;
	public Transform reticleSprite;

	int score = 0;
	public Text scoreText;
	public Text shieldText;

	// Use this for initialization
	void Start () {
		//Mémorise la position de départ du joueur
		initialPosition = transform.position;
		//Initialise le texte du score
		UpdateScore();

		shieldLevel = defaultShieldLevel;
		UpdateShield();

		cooldownIndicator.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//Variable temporaire
		Vector3 currentPosition = transform.position;

		//Invincibilité : on retourne à sa place initiale et on ne peut rien faire :(
		if(isInvincible())
		{
			//Ramene le vaisseau à sa position initiale progressivement
			currentPosition = Vector3.Lerp(currentPosition, initialPosition, 1 - currentInvincibility/invicibilityDuration);
			//TODO : Ajouter un effet visuel pour indiquer qu'on est invincible

			currentInvincibility -= Time.deltaTime;

		} else //Pas d'invincibilité
		{
			//Contrôle direct UP/DOWN
			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
			{
				//Set la velocity et le step si on appuie sur une des touches
				velocity = 1;
				velocityStep = 6.0f;
				velocity = Input.GetKey(KeyCode.UpArrow) ? 1 : -1;
				//Set la direction à 1 si c'est la touche du haut, -1 sinon
				direction = Input.GetKey(KeyCode.UpArrow) ? 1 : -1;
			} else 
			{
				velocity = 0;
				direction = 0;
			}

			velocity = Mathf.Clamp(velocity + (velocityStep * direction), -maxVelocity, maxVelocity);
			currentPosition.y = Mathf.Clamp(
				//Modifie la position verticale actuelle
				currentPosition.y +
				//En fonction de la vitesse du joueur
				(velocity*Time.deltaTime +
				//Du coefficient de gravité appliqué par les zones de gravité modifiée
				Gravity*Time.deltaTime) * 
				//Ralenti par la téléportation si elle est active
				SpeedModifierManager.speedModifier,
				//Bloque le joueur entre le haut et le bas de l'écran
				minHeight,
				maxHeight);

			//Téléportation !
			if(Input.GetMouseButtonDown(1))
			{
				if(currentTeleportCooldown == 0.0f)
				{
					teleporting = true;
					//Debug.Log("TP INIT");
					ParticleSystem.EmissionModule em = teleportParticleSystem.emission;
		 			em.enabled = true;
					reticleSprite.gameObject.SetActive(true);

					SpeedModifierManager.speedModifier = 0.05f;
				}				
			}
			if(Input.GetMouseButton(1))
			{
				if(teleporting)
				{
					Vector3 screenTeleportPosition = Input.mousePosition;
					Vector3 worldTeleportPosition = Camera.main.ScreenToWorldPoint(screenTeleportPosition);
					reticleSprite.position = new Vector3(worldTeleportPosition.x, worldTeleportPosition.y, 0);
				}
			}
			if(Input.GetMouseButtonUp(1))
			{
				if(teleporting)
				{
					//Debug.Log("TP DONE");
					ParticleSystem.EmissionModule em = teleportParticleSystem.emission;
		 			em.enabled = false;
					reticleSprite.gameObject.SetActive(false);

					SpeedModifierManager.speedModifier = 1;
					Vector3 screenTeleportPosition = Input.mousePosition;
					Vector3 worldTeleportPosition = Camera.main.ScreenToWorldPoint(screenTeleportPosition);
					currentPosition.x = worldTeleportPosition.x;
					currentPosition.y = worldTeleportPosition.y;

					velocity = 0.0f;

					teleporting = false;
					currentTeleportCooldown = teleportCooldown;
				}
			}

			//Retour à la position de base après une téléportation
			if(currentPosition.x > initialPosition.x)
			{
				currentPosition.x -= Mathf.Min(5f * Time.deltaTime * SpeedModifierManager.speedModifier, currentPosition.x - initialPosition.x);
			}
		}

		currentTeleportCooldown = Mathf.Max(currentTeleportCooldown - Time.deltaTime, 0.0f);
		if(currentTeleportCooldown == 0.0f)
		{
			cooldownIndicator.gameObject.SetActive(false);
		} else 
		{
			cooldownIndicator.gameObject.SetActive(true);
		}

		transform.position = currentPosition;
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.GetComponent<GravityZone>() != null)
			Gravity = 0.0f;
	}

	//Détection des collisions
	void OnTriggerEnter2D(Collider2D c)
	{
		//Obstacle
		if (c.GetComponent<Obstacle>() != null)
		{
			Hit();
			Debug.Log(c.name);
		}

		//Reward
		Reward pickedUpReward = c.GetComponent<Reward>();
		if (pickedUpReward != null)
		{
			PickUp(pickedUpReward);
		}

		GravityZone zone = c.GetComponent<GravityZone> ();
		if (zone != null)
		{
			Gravity += AttractionSpeed * zone.directionEffects;
		}
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

		if(shieldLevel == 0)
		{
			
		} else 
		{
			shieldLevel -= 1;
			UpdateShield();
		}

		//On met le timer d'invincibilité au max
		currentInvincibility = invicibilityDuration;
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
	//Affiche le niveau de bouclier
	void UpdateShield()
	{
		shieldText.text = shieldLevel.ToString();
	}
}
