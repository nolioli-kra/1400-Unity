using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOOB : MonoBehaviour
{
    private float lowBound = -1;
    private float highBound = 40;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowBound)
        {
            Destroy(gameObject);
        } else if (transform.position.y > highBound) {
            Destroy(gameObject);
        }
    }
}
