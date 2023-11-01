using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform target; // The target to follow (your player's transform).
    public Vector3 offset = new Vector3(0.0f, 3.0f, -6.5f); // Offset from the target.
    public float smoothSpeed = 7.5f; // How quickly the camera follows the player.

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired camera position based on the target's position and the offset.
        Vector3 desiredPosition = target.position + offset;

        // Use SmoothDamp to interpolate the current camera position toward the desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Set the camera's position to the smoothed position.
        transform.position = smoothedPosition;

        // Make the camera look at the target.
        transform.LookAt(target);
    }
}