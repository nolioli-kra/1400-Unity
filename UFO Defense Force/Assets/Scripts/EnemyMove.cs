using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float enemySpeed = 10f;
    public Vector3 enemyDirection = Vector3.forward;

    private Rigidbody enemyRb;

    public float passPlayer = -14.8f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        enemyRb.velocity = enemyDirection.normalized * enemySpeed;

        if (transform.position.z <= passPlayer)
        {
            Debug.Log("Game Over");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
   
}
