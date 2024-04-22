using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 1.5f;
    public float spawnRangeX;

    private float nextSpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        //randomly select an enemy from the array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        //random position
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);

        //instantiate
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 13);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
