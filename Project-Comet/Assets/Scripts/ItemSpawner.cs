using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject[] itemPrefabs;
    private int randomSpawnIndex;
    private int randomItemIndex;
    private int spawnBehaviorModifier;

    public void Start()
    {
        spawnBehaviorModifier = 0;
        GameManager.DiffcultyUp += ChangeSpawnBehavior;
        randomItemIndex = Random.Range(0, itemPrefabs.Length);
        randomSpawnIndex = Random.Range(0, spawnPoints.Length);
    }
    public void SpawnItems()
    {
        switch (spawnBehaviorModifier)
        {
            case 1:
                randomSpawnIndex = Random.Range(0, 2);
                Instantiate(itemPrefabs[randomItemIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
                break;
            case 2:
                int randomIndex = 0;
                for (int i = 1; i < spawnPoints.Length; i++)
                {
                    do
                        randomIndex = Random.Range(0, spawnPoints.Length);
                    while (randomSpawnIndex == randomIndex);
                    randomItemIndex = Random.Range(0, itemPrefabs.Length);
                    randomSpawnIndex = randomIndex;
                    Instantiate(itemPrefabs[randomItemIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
                }
                break;
        }
    }
    private void ChangeSpawnBehavior()
    {
        spawnBehaviorModifier++;
    }
}
