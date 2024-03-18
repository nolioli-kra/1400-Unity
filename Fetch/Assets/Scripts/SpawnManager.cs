using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bonePrefab;
    private float spawnRangeMax = 17;
    private float spawnRangeMin = -10;
    private float spawnPosZ = 13;

    private float startDelay = 1.5f;
    private float spawnInterval = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBones", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBones()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnRangeMin, spawnRangeMax), 22, spawnPosZ);

        Instantiate(bonePrefab, spawnPos, bonePrefab.transform.rotation);
    }
}
