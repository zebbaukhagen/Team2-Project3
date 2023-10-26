using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicycleMovement : MonoBehaviour
{
    public float forwardForce = 10.0f; // Force to move the unicycle forward.
    public float turnForce = 5.0f; // Force to turn the unicycle.
    public Transform balancePoint; // A transform representing the balance point on the unicycle.

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply forward force to move the unicycle.
        rb.AddForce(transform.forward * forwardForce);

        // Get input for turning.
        float turnInput = Input.GetAxis("Horizontal");

        // Calculate torque to turn the unicycle.
        Vector3 turnTorque = balancePoint.up * turnInput * turnForce;

        // Apply the turning torque to the unicycle's rigidbody.
        rb.AddTorque(turnTorque);
    }
}

