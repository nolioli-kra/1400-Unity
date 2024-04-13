using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody playerRb;

    public int playerHealth = 10;
    public int healthBar;
    public TextMeshProUGUI healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //calculate movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        //move the player
        playerRb.MovePosition(playerRb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);


        UpdateHealth(healthBar);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 1;

            if (playerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateHealth(int healthUpdate)
    {
        healthBar = playerHealth;
        healthDisplay.text = "Health: " + healthBar;
    }
}
