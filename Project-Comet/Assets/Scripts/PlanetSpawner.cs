using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float[] xSpawnOffsets;
    private float xSpawnOffset;
    public GameObject planetPrefab;

    public Vector3 GetSpawnPointPosition(int index)
    {
        return spawnPoints[index].position;
    }

    private void Start()
    {
        xSpawnOffset = xSpawnOffsets[Random.Range(0, xSpawnOffsets.Length)];
    }

    public void SpawnPlanet()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            float randomSpawnOffset = 0.0f;
            do
            {
                randomSpawnOffset = xSpawnOffsets[Random.Range(0, xSpawnOffsets.Length)];
            } while (xSpawnOffset == randomSpawnOffset);
            xSpawnOffset = randomSpawnOffset;
            Vector3 spawnPosition = new Vector3(xSpawnOffset, spawnPoint.position.y, spawnPoint.position.z);
            Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
        }
    }

    //Add difficulty modifier here with planets that have harder prefabs interms of speed.
}
