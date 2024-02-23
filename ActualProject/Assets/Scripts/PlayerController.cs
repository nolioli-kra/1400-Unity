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
    [SerializeField] private float smoothTime = 0.1f;
    private float currentVelocity;

    [SerializeField] private float speed;

    //Gravity
    private float vGravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float vVelocity;

    //Jumping
    [SerializeField] private float jumpPower;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
    }

    private void ApplyRotation()
    {
        if (conInput.sqrMagnitude == 0) return;

        var facingAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, facingAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        characterController.Move(direction * speed * Time.deltaTime);
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

        vVelocity += jumpPower;
    }

    private bool IsGrounded() => characterController.isGrounded;
}
