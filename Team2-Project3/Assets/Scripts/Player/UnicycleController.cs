using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    public float speed = 5.0f; // Adjust this to control the object's speed
    public float rotationSpeed = 90.0f; // Adjust this to control the rotation speed

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        MoveAndRotate(horizontalInput, verticalInput);
    }

    private void MoveAndRotate(float horizontalInput, float verticalInput)
    {
        // Calculate the rotation change
        float rotationChange = rotationSpeed * horizontalInput * Time.deltaTime;
        transform.Rotate(0, rotationChange, 0);

        // Calculate the forward movement
        Vector3 moveDirection = transform.forward * verticalInput * speed * Time.deltaTime;
        transform.position += moveDirection;
    }
}


