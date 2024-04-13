using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMove : MonoBehaviour
{
    public float fallSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
