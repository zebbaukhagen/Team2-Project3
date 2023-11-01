using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // Adjust this to control the object's speed
    [SerializeField] private float rotationSpeed = 90.0f; // Adjust this to control the rotation speed
    [SerializeField] private UILevelController levelController;
    //[SerializeField] private Timer timer;



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


    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("LevelOneComplete"))
    //    {
    //        Debug.Log("you finished!");
    //        levelController.ActivateWinPanel();
    //    }

    //}
}


