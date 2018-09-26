using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float zOffset;
    [SerializeField]
    private PlanetSpawner planetSpawner;
    [SerializeField]
    private ItemSpawner itemSpawner;
    public Vector3 pointForSpawn;

    private void Start()
    {
        InitiateSpawn();
    }
    private void Update()
    {
        if (transform.position.y >= pointForSpawn.y)
            InitiateSpawn();
    }
    private void LateUpdate()
    {
        if (target != null && target.position.y > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(0, target.transform.position.y, zOffset);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
    }
    private void InitiateSpawn()
    {
        pointForSpawn = planetSpawner.GetSpawnPointPosition(1);
        planetSpawner.SpawnPlanets();
        itemSpawner.SpawnItems();
    }
}
