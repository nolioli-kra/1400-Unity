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
    [SerializeField] float fireRate;

    public float defaultFireRate = 0.3f;
    public float rapidFireRate = 0.15f;

    public float rapidFireDuration = 5f;
    private bool isRapidFireActive = false;
    private float timeSincePowerup = 0f;
    private float canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        //check if powerup is active
        if (isRapidFireActive)
        {
            timeSincePowerup += Time.deltaTime;
            DisablePowerup();
        }

        //fire lasers
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pepper"))
        {
            //activate powerup
            isRapidFireActive = true;
            timeSincePowerup = 0f;
            fireRate = rapidFireRate;
        }
    }

    private void FireLaser()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        canFire = Time.time + fireRate;
    }

    private void DisablePowerup()
    {
        if (timeSincePowerup >= rapidFireDuration)
        {
            isRapidFireActive = false;
            fireRate = defaultFireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //calc velocity
        Vector3 instantTurn = new Vector3(horizontalInput, 0, verticalInput) * playerSpeed;
        //instantly apply direction
        playerRb.velocity = instantTurn;

        //calc movement
        Vector3 playerMovement = new Vector3(horizontalInput, 0, verticalInput) * playerSpeed * Time.deltaTime;

        //apply movement to rb
        playerRb.MovePosition(playerRb.position + playerMovement);
    }
}
