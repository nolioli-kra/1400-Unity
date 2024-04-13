using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] float playerAcceleration;

    private Rigidbody playerRb;

    public GameObject laserPrefab;
    public Transform firePoint;
    [SerializeField] float fireRate = 0.5f;
    private float canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //calc velocity
        Vector3 instantTurn = new Vector3 (horizontalInput, 0, verticalInput) * playerSpeed;
        //instantly apply direction
        playerRb.velocity = instantTurn;

        //calc movement
        Vector3 playerMovement = new Vector3 (horizontalInput, 0, verticalInput) * playerSpeed * Time.deltaTime;

        //apply movement to rb
        playerRb.MovePosition(playerRb.position + playerMovement);

        //fire lasers
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            canFire = Time.time + fireRate;
        }

    }
}
