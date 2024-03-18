using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject dogPrefab;
    public float dogCoolDown = 1f;
    private float lastDogTime = 0f;

    
    void Start()
    {
        //initialize last spawn time
        lastDogTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //check if enough time has passed to spawn another dog
            if (Time.time - lastDogTime >= dogCoolDown)
            {
                Vector3 spawnPoint = transform.position + transform.forward * 4f;
                Instantiate(dogPrefab, spawnPoint, dogPrefab.transform.rotation);

                //update last dog time
                lastDogTime = Time.time;
            }
        }
    }
}
