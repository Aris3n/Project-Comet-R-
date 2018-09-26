using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class Player : MonoBehaviour
{

    public enum States { Flying, Orbiting }
    public States state;
    PlayerController controller;
    //Planet Related Properties.
    public Planet currentPlanet;
    private float orbitSpeed;
    public float orbitToSpeedRatio;
    //Movespeed Related Properties.
    private float moveSpeed;
    private float launchSpeed;
    public float moveSpeedDecayRate;
    public ParticleSystem shipTrail;
    public GameObject deathParticlePrefab;
    CanvasManager canvasManager;
    public CanvasGroup gameOverCanvas;
    //Delegates.
    public delegate void UpdateScore(int value);
    public delegate void PlayerDeath();
    //Events.
    public static event UpdateScore ScoreUp;
    public static event PlayerDeath FinalizeScore;

    public void SetShipTrailSize(float size)
    {
        shipTrail.startLifetime = size * 1;
    }

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        canvasManager = GetComponent<CanvasManager>();
        controller.EnterOrbit(currentPlanet);
        orbitSpeed = currentPlanet.GetMaxOrbitSpeed();
        state = States.Orbiting;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && state == States.Orbiting || Input.GetKeyDown(KeyCode.Space) && state == States.Orbiting)
            {
                currentPlanet = null;
                controller.ExitOrbit();
                state = States.Flying;
            }
            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {
        switch (state)
        {
            case States.Orbiting:
                orbitSpeed -= currentPlanet.GetOrbitSpeedDecayRate();
                controller.Orbit(currentPlanet, orbitSpeed);
                launchSpeed = orbitSpeed / orbitToSpeedRatio;
                SetShipTrailSize(currentPlanet.GetOrbitSpeedPercantage(orbitSpeed));
                break;
            case States.Flying:
                moveSpeed = (launchSpeed -= moveSpeedDecayRate) / 4;
                SetShipTrailSize((moveSpeed / launchSpeed));
                if (moveSpeed > 0)
                    controller.Fly(moveSpeed -= moveSpeedDecayRate);
                else
                {
                    enabled = false;
                    Death();
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {

            case "Planet":
                currentPlanet = collider.GetComponent<Planet>();
                controller.SetRotationDirection(currentPlanet.transform.position.x);
                controller.EnterOrbit(currentPlanet);
                ScoreUp(1);
                state = States.Orbiting;
                orbitSpeed = currentPlanet.GetMaxOrbitSpeed();
                break;
            case "Coin":
                ScoreUp(3);
                break;
            case "Boundary":
                Death();
                break;
            case "Asteroid":
                Death();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        FinalizeScore();
        CanvasGroup gameCanvasGroup = GameObject.Find("GameCanvas").GetComponent<CanvasGroup>();
        canvasManager.ShowCanvas(gameCanvasGroup, gameOverCanvas);
    }
}
