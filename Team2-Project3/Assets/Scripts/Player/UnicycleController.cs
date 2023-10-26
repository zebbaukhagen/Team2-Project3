using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 180.0f; // Adjust this for the rotation speed.

    void Update()
    {
        // Get input for WASD movement.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction.
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        // Apply the movement to the object.
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
