using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperScript : MonoBehaviour
{
    public float enemySpeed = 10f;
    public Vector3 enemyDirection = Vector3.forward;

    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        enemyRb.velocity = enemyDirection.normalized * enemySpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
