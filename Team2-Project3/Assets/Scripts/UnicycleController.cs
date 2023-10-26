using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    public float balanceForce = 10f;

    private Rigidbody rb;
    public float leanSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input for leaning forward and backward.
        float leanInput = Input.GetAxis("Vertical");

        // Adjust the unicycle's rotation based on input.
        transform.Rotate(Vector3.right, leanInput * leanSpeed * Time.deltaTime);

        // Calculate the balance force based on the tilt of the unicycle.
        float tilt = transform.forward.y;
        Vector3 balanceTorque = Vector3.Cross(transform.up, Physics.gravity) * tilt * balanceForce;

        // Apply the balance force to the unicycle's rigidbody.
        rb.AddTorque(balanceTorque);
    }
}
