using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    public float topBound = 18f;
    public float bottomBound = -15f;

    void Update()
    {
        if(transform.position.z > topBound ||  transform.position.z < bottomBound)
        {
            Destroy(gameObject);
        }
    }
}
