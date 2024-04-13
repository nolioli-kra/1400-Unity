using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPowerUp : MonoBehaviour
{
    public float launchForce = 100f;

    public ParticleSystem collectedBomb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ParticleSystem effect = Instantiate(collectedBomb, transform.position, transform.rotation);
            effect.Play();
            

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    enemyRb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
                }
            }

            Destroy(gameObject);
        }
    }
}
