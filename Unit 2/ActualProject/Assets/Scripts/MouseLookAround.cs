using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    //VARIABLES ------
    //Camera follow player
    [SerializeField] private Transform target;
    private float distanceToPlayer;


    private Vector2 mouseInput;

    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;

    private CameraRotation cameraRotation;

    //VARIABLES ------

    private void Awake()
    {
        //Offsets the camera's position based on where the player is
        //Gives distance to player as a float
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    public void Look (InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        cameraRotation.Yaw += mouseInput.x * mouseSensitivity.horizontal * Time.deltaTime;
        cameraRotation.Pitch += mouseInput.y * mouseSensitivity.vertical * Time.deltaTime;
        cameraRotation.Pitch = Mathf.Clamp(cameraRotation.Pitch, cameraAngle.minimum, cameraAngle.maximum);
    }

    private void LateUpdate()
    {
        //Rotates the Camera
        transform.eulerAngles = new Vector3(cameraRotation.Pitch, cameraRotation.Yaw, 0.0f);
        //Set Camera position
        transform.position = target.position - transform.forward * distanceToPlayer;
    }
}

[Serializable]
public struct MouseSensitivity
{
    public float horizontal;
    public float vertical;
}

public struct CameraRotation
{
    public float Pitch;
    public float Yaw;
}

[Serializable]

public struct CameraAngle
{
    public float minimum;
    public float maximum;
}
