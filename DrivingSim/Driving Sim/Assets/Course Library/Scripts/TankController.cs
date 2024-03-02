using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Tank movement variables
    public float tankSpeed = 5.0f;
    public float turnSpeed;
    public float horizontalInput;
    public float forwardInput;

    void Update()
    {
        //Move the tank
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * tankSpeed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
    }
}
