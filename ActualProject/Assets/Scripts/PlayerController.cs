using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Movement
    private Vector2 conInput;
    private CharacterController characterController;
    private Vector3 direction;

    //Rotation
    [SerializeField] private float rotationSpeed = 500f;
    private Camera mainCam;
    [SerializeField] private Movement movement;

    //Speed
    [SerializeField] private float speed;

    //Gravity
    private float vGravity = -5f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float vVelocity;

    //Jumping
    [SerializeField] private float jumpPower;

    //Crouching
    private float startingHeight;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCam = Camera.main;
        startingHeight = characterController.height;
    }

    private void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyRotation()
    {
        if (conInput.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0.0f, mainCam.transform.eulerAngles.y, 0.0f) * new Vector3(conInput.x, 0.0f, conInput.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        float targetSpeed;

        if(movement.isSprinting)
        {
            targetSpeed = movement.speed * movement.multiplier; //sprint speed
        } else if (movement.isCrouching)
        {
            targetSpeed = movement.speed * movement.crouchMultiplier; //crouch speed
            //height of the character while crouching
            float targetHeight = startingHeight * 0.75f;
            characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * 5f);
            //characterController.center = new Vector3(characterController.center.x, targetHeight * 0.5f, characterController.center.z);
        } else
        {
            targetSpeed = movement.speed; //normal walk speed
        }

        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.acceleration * Time.deltaTime);
        //applies basic movement
        characterController.Move(direction * movement.currentSpeed * Time.deltaTime);

        //Revert to normal height
        if (!movement.isCrouching)
        {
            float originalHeight = startingHeight;
            characterController.height = Mathf.Lerp(characterController.height, originalHeight, Time.deltaTime * 5f);
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && vVelocity < 0.0f)
        {
            vVelocity = -1.0f;
        }
        else
        {
            vVelocity += vGravity * gravityMultiplier * Time.deltaTime;
        }
        
        direction.y = vVelocity;
    }
    public void Move(InputAction.CallbackContext context)
    {
        conInput = context.ReadValue<Vector2>();
        direction = new Vector3(conInput.x, 0.0f, conInput.y);
    }

    public void Jump(InputAction.CallbackContext context) 
    {
        if (!context.started) return;
        if (!IsGrounded() ) return;
        if (movement.isCrouching == true) return;

        vVelocity += jumpPower;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        //Sprinting returned as true when context is started (key pressed) and when context is performed (key held down)
        movement.isSprinting = context.started || context.performed;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        movement.isCrouching = context.started || context.performed;
    }

    private bool IsGrounded() => characterController.isGrounded;
}

[Serializable]
public struct Movement
{
    public float speed;
    public float multiplier;
    public float crouchMultiplier;
    public float acceleration;

    //variables not necessary to see in the inspector are hidden
    [HideInInspector] public bool isSprinting;
    [HideInInspector] public bool isCrouching;
    [HideInInspector] public float currentSpeed;
}