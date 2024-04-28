using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    public float fallMultiplier;
    private bool isFalling = false;
    public float jumpApex;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftControl;
    public KeyCode abilityOne = KeyCode.Q;
    public KeyCode abilityTwo = KeyCode.E;
    public KeyCode abilityThree = KeyCode.R;
    public KeyCode styleMove = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody playerRb;

    public MovementState state;
    public enum MovementState
    {
        Walking,
        Sprinting,
        air
    }

    //debug info
    private Vector3 currentVelocity;
    private bool onSlope;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
    }

    void Update()
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        //falling check
        if (!isGrounded && playerRb.velocity.y < jumpApex)
        {
            isFalling = true;
        } 
        else if (isGrounded || onSlope || playerRb.velocity.y > jumpApex)
        {
            isFalling = false;
        }

        MyInput();
        StateHandler();
        SpeedControl();

        //apply drag when grounded
        if (isGrounded)
        {
            playerRb.drag = groundDrag;
        } else
        {
            playerRb.drag = 0;
        }

        //debugging
        currentVelocity = playerRb.velocity;
        Debug.Log("Current velocity: " + currentVelocity);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void StateHandler()
    {
        //sprinting
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            state = MovementState.Sprinting;
            moveSpeed = sprintSpeed;
        }
        //walking
        else if (isGrounded)
        {
            state = MovementState.Walking;
            moveSpeed = walkSpeed;
        }
        //air
        else
        {
            state = MovementState.air;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope())
        {
            playerRb.AddForce(GetSlopeMoveDirection() * moveSpeed * 10f, ForceMode.Force);

            onSlope = true;
        }
        else
        {
            onSlope = false;
        }

        //on ground
        if (isGrounded)
        {
            playerRb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } //in air
        else if (!isGrounded)
        {
            if (isFalling)
            {
                playerRb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
            }
            playerRb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        //limit velocity if needed
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            playerRb.velocity = new Vector3(limitedVelocity.x, playerRb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        //reset y velocity to ensure consistent jump height
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
