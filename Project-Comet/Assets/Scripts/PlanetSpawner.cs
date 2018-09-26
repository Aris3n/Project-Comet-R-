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
    [SerializeField]
    private float baseOrbitSpeed;
    [SerializeField]
    private float baseOrbitDecayRate;
    [SerializeField]
    private float speedIncreaseValue;
    [SerializeField]
    private float speedDecayIncreaseValue;

    public Vector3 GetSpawnPointPosition(int index)
    {
        return spawnPoints[index].position;
    }
    private void Start()
    {
        GameManager.DiffcultyUp += ChangePlanetSpeedBehaviour;
        xSpawnOffset = xSpawnOffsets[Random.Range(0, xSpawnOffsets.Length)];
    }
    public void SpawnPlanets()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            float randomSpawnOffset = 0.0f;
            do
                randomSpawnOffset = xSpawnOffsets[Random.Range(0, xSpawnOffsets.Length)];
            while (xSpawnOffset == randomSpawnOffset);
            xSpawnOffset = randomSpawnOffset;
            Vector3 spawnPosition = new Vector3(xSpawnOffset, spawnPoint.position.y, spawnPoint.position.z);
            GameObject planet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
            planet.GetComponent<Planet>().SetMaxOrbitSpeed(baseOrbitSpeed);
            planet.GetComponent<Planet>().SetMaxOrbitSpeedDecayRate(baseOrbitDecayRate);
        }
    }
    private void ChangePlanetSpeedBehaviour()
    {
        baseOrbitSpeed += speedIncreaseValue;
        baseOrbitDecayRate += speedDecayIncreaseValue;
    }
}
