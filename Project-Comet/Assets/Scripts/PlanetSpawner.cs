using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public float[] xSpawnOffsets;
    float xSpawnOffset;
    public GameObject planetPrefab;

	private void Start () { 
		xSpawnOffset = xSpawnOffsets[Random.Range(0, xSpawnOffsets.Length)];
	}

    public void SpawnPlanet() {
        foreach (Transform spawnPoint in spawnPoints) {
			float randomSpawnOffset = 0.0f;
			do {
				randomSpawnOffset = xSpawnOffsets[Random.Range(0, xSpawnOffsets.Length)];
			} while (xSpawnOffset == randomSpawnOffset);
            xSpawnOffset = randomSpawnOffset;
            Vector3 spawnPosition = new Vector3(xSpawnOffset, spawnPoint.position.y, spawnPoint.position.z);
            Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
