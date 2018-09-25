using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 pointForSpawn;
    public float smoothSpeed = 0.125f;
    public float zOffset;
    public PlanetSpawner planetSpawner;

    private void Start()
    {
        UpdateSpawner();
    }

    private void Update()
    {
        if (transform.position.y >= pointForSpawn.y)
            UpdateSpawner();
    }

    private void LateUpdate()
    {
        if (target.position.y > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(0, target.transform.position.y, zOffset);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
    }

    private void UpdateSpawner()
    {
        pointForSpawn = planetSpawner.GetSpawnPointPosition(1);
        planetSpawner.SpawnPlanet();
    }

}
