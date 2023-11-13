using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBasedMovement : MonoBehaviour
{

    /// <summary>
    /// References
    /// </summary>
    public Transform modelHolder;
    public CharacterController characterController;

  

    /// <summary>
    /// Movement
    /// </summary>
    public float rotationSpeed = 75.0f; // Adjust this to control the rotation speed
    public float moveSpeed = 10.0f;
   
    //movement direction local to the holders direction
    float tiltPower = -20.0f;
    float counterTilt = 6.0f;






    //Velocity gained per second. Applies midairMovementMultiplier when we are not grounded:

    void Movement()
    {
        float downforce = -1.5f;
        Vector3 movement = transform.forward * Input.GetAxis("Vertical");
        movement.y = downforce;
        characterController.Move(movement * Time.deltaTime * moveSpeed);
        
    }

    public void Tilt()
    {
        modelHolder.Rotate(0.0f, 0.0f, Input.GetAxis("Horizontal") * tiltPower * Time.deltaTime * counterTilt, Space.Self);
        Debug.Log(characterController.velocity);

        if (Input.GetAxis("Vertical") == 0)
        {
            if (modelHolder.localRotation.z >= 0)
            {
                modelHolder.Rotate(0.0f, 0.0f, -tiltPower * Time.deltaTime, Space.Self);
            }
            else
            {
                modelHolder.Rotate(0.0f, 0.0f, tiltPower * Time.deltaTime, Space.Self);
            }
        }
    }

    public void RotateUnicycle()
    {
        float rotationChange = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationChange, Space.Self);   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateUnicycle();
        Tilt();
    }
}

