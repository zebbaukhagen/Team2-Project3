using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // Adjust this to control the object's speed
    [SerializeField] private float rotationSpeed = 90.0f; // Adjust this to control the rotation speed
    [SerializeField] private UILevelController levelController;

    private float rotationPower = 5.0f;
    [SerializeField] private GameObject pivot;
    Vector3 tiltLeft = new Vector3(0, 0, 5.0f);
    Vector3 tiltRight = new Vector3(0, 0, -5.0f);
    //[SerializeField] private Timer timer;



    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        MoveAndRotate(horizontalInput, verticalInput);
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                TiltLeft();
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                TiltRight();
            }
        }
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

    public void TiltLeft()
    {
        pivot.transform.Rotate(rotationPower * tiltLeft * Time.deltaTime);
    }

    public void TiltRight()
    {
        pivot.transform.Rotate(rotationPower * tiltRight * Time.deltaTime);
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


