using UnityEngine;

public class TireMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 60.0f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the forward movement based on the local forward vector.
        float movement = verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * movement);

        // Calculate the turn based on horizontal input.
        float turn = horizontalInput * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right * turn);
    }
}

