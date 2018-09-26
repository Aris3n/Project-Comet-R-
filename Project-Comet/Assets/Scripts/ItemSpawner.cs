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
    private int difficultyModifier;

    public void Start()
    {
		//Modifier is set for test purposes.
        difficultyModifier = 0;
        randomItemIndex = Random.Range(0, itemPrefabs.Length);
        randomSpawnIndex = Random.Range(0, spawnPoints.Length);
    }
    public void SpawnItems()
    {
        switch (difficultyModifier)
        {
            case 1:
                randomSpawnIndex = Random.Range(0, 2);
                Instantiate(itemPrefabs[randomItemIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
                break;
            case 0:
                Debug.Log("Should Spawn");
                int randomIndex = 0;
                for (int i = 1; i < spawnPoints.Length; i++)
                {
                    do
                    {
                        randomIndex = Random.Range(0, spawnPoints.Length);
                        randomItemIndex = Random.Range(0, itemPrefabs.Length);
                    } while (randomSpawnIndex == randomIndex);
                    randomSpawnIndex = randomIndex;
                    Instantiate(itemPrefabs[randomItemIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
                }
                break;
        }
    }
}
