using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]

public class Player : MonoBehaviour {

	public enum States { Flying, Orbiting }
	public States currentState;
	PlayerController controller;
	public Planet planet;
	public float orbitSpeed = 220.0f;
	public float moveSpeed = 10.0f;

	void Start () {
		controller = GetComponent<PlayerController>();
		controller.EnterOrbit(planet);
	}
	
	void Update () {	
		if (Input.GetKeyDown(KeyCode.Space)) {
			planet = null;
			controller.ExitOrbit();
			currentState = States.Flying;	
			Debug.Log("Leaving current orbit");
		}
		
		switch (currentState) {
			case States.Orbiting:
				controller.Orbit(planet, orbitSpeed);
				break;
			case States.Flying:
				controller.Fly(moveSpeed);
				break; 
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == "Planet") {
			planet = collider.GetComponent<Planet>();
			controller.EnterOrbit(planet);
			currentState = States.Orbiting;

			Debug.Log("Player has entered new orbit");
		}
	}
}
