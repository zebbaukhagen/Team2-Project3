using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player object.
    public Vector3 offset = new Vector3(0, 2, -5); // Offset from the player's position.
    public float smoothSpeed = 5.0f; // The smoothness of the camera movement.

    void LateUpdate()
    {
        if (player == null)
        {
            // If the player object is not assigned, exit the function.
            return;
        }

        // Calculate the desired position for the camera.
        Vector3 desiredPosition = player.position + offset;

        // Smoothly move the camera towards the desired position.
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
