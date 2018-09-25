using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]

public class Player : MonoBehaviour {

	public enum States { Flying, Orbiting }
	public States currentState;
	PlayerController controller;
	public ParticleSystem shipTrail;
	public Planet planet;
	public bool isClockwise;
	public float maxOrbitSpeed;
	public float minOrbitSpeed;
	public float orbitDecayRate;
	[SerializeField]
	private float currentOrbitSpeed;
	public float launchSpeed;
	public float moveSpeedDecayRate;
	[SerializeField]
	private float moveSpeed;
	public float orbitToSpeedRatio;
	CanvasManager canvasManager;
	public CanvasGroup gameOverCanvas;

	public delegate void UpdateScore(int value);
	public static event UpdateScore ScoreUp;

	public delegate void PlayerDeath();
	public static event PlayerDeath FinalizeScore;

	private void Start () {
		controller = GetComponent<PlayerController>();
		canvasManager = GetComponent<CanvasManager>();
		controller.EnterOrbit(planet);
		currentOrbitSpeed = maxOrbitSpeed;
	}
	
	private void Update () {	
		if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			planet = null;
			controller.ExitOrbit();
			currentState = States.Flying;	
		}

		UpdateMovement();
		if (transform.position.x > 2) {

		}
	}

	private void UpdateMovement () {
		switch (currentState) {
			case States.Orbiting:
				controller.Orbit(planet, currentOrbitSpeed);
				if (currentOrbitSpeed > minOrbitSpeed) 
					currentOrbitSpeed -= orbitDecayRate;
				launchSpeed = currentOrbitSpeed / orbitToSpeedRatio;
				planet.GravityDecay(currentOrbitSpeed / maxOrbitSpeed);
				shipTrail.startLifetime = (currentOrbitSpeed/maxOrbitSpeed) * 1;
				break;
			case States.Flying:
				moveSpeed = (launchSpeed -=moveSpeedDecayRate) / 4;
				shipTrail.startLifetime = (moveSpeed/launchSpeed) * 1;
				if (moveSpeed  > 0) {
					controller.Fly(moveSpeed -= moveSpeedDecayRate);
				}
				else {
					// Game over condition here.
				}
				break; 
		}
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Planet") {
			planet = collider.GetComponent<Planet>();
			if (transform.position.x > planet.transform.position.x) {
				controller.isClockwiseOrbit = true;
			} else {
				controller.isClockwiseOrbit = false;
			}
			controller.EnterOrbit(planet);
			ScoreUp(1);
			currentState = States.Orbiting;
			currentOrbitSpeed = maxOrbitSpeed;			
		} else if (collider.tag == "Boundary") {
			Death();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet") {	
			Death();
		}
		
    }

	private void Death() {
		FinalizeScore();
		CanvasGroup gameCanvasGroup = GameObject.Find("GameCanvas").GetComponent<CanvasGroup>();
		canvasManager.ShowCanvas(gameCanvasGroup, gameOverCanvas);
	}
}
