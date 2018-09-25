using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]

public class Player : MonoBehaviour {

	public enum States { Flying, Orbiting }
	public States currentState;
	PlayerController controller;
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

	private void Start () {
		controller = GetComponent<PlayerController>();
		controller.EnterOrbit(planet);

		currentOrbitSpeed = maxOrbitSpeed;
	}
	
	private void Update () {	
		if (Input.GetKeyDown(KeyCode.Space)) {
			planet = null;
			controller.ExitOrbit(isClockwise);
			currentState = States.Flying;	
		}

		UpdateMovement();
	}

	private void UpdateMovement () {
		switch (currentState) {
			case States.Orbiting:
				controller.Orbit(planet, currentOrbitSpeed, isClockwise);
				if (currentOrbitSpeed > minOrbitSpeed) 
					currentOrbitSpeed -= orbitDecayRate;
				launchSpeed = currentOrbitSpeed / orbitToSpeedRatio;
				planet.GravityDecay(currentOrbitSpeed / maxOrbitSpeed);
				break;
			case States.Flying:
				moveSpeed = (launchSpeed -=moveSpeedDecayRate) / 4;
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
			controller.EnterOrbit(planet);
			if (transform.position.x > planet.transform.position.x) {
				isClockwise = true;
			} else {
				isClockwise = false;
			}
			currentState = States.Orbiting;
			currentOrbitSpeed = maxOrbitSpeed;			
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet") {	
			//Debug.Log("Player collided with planet");
		}
    }
}
