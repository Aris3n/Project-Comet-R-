using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 pointForSpawn;
    public float smoothSpeed = 0.125f;
    public float zOffset;
    [SerializeField]
    private PlanetSpawner planetSpawner;
    [SerializeField]
    private ItemSpawner itemSpawner;

    public CanvasGroup gameOverCanvas;
    CanvasManager canvasManager;



    private void Start()
    {
        canvasManager = GetComponent<CanvasManager>();
        Player.FinalizeScore += ShowGameOver;
        UpdateSpawners();
    }

    private void Update()
    {
        if (transform.position.y >= pointForSpawn.y)
            UpdateSpawners();
    }

    private void LateUpdate()
    {
        if ( target != null && target.position.y > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(0, target.transform.position.y, zOffset);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
    }

    private void UpdateSpawners()
    {
        pointForSpawn = planetSpawner.GetSpawnPointPosition(1);
        planetSpawner.SpawnPlanets();
        itemSpawner.SpawnItems();
    }

    private void ShowGameOver()
    {
        CanvasGroup gameCanvasGroup = GameObject.Find("GameCanvas").GetComponent<CanvasGroup>();
        canvasManager.ShowCanvas(gameCanvasGroup, gameOverCanvas);
    }

}
