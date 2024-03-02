using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTank : MonoBehaviour
{
    public GameObject playerTank;
    public Vector3 camOffset = new Vector3(5f, 4f, -9.5f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTank.transform.position + camOffset;
    }
}
