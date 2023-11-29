using UnityEngine;

public class UnicycleController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // Adjust this to control the object's speed
    [SerializeField] private float rotationSpeed = 90.0f; // Adjust this to control the rotation speed
    [SerializeField] private UILevelController levelController;

    private float rotationPower = 7.5f;
    float tiltPower = -10.0f;
    
    float horizontalInput = 0.0f;
    float verticalInput = 0.0f;
    public bool hasControl = true;
   
    
    

    //[SerializeField] private Timer timer;



    void Update()
    {
        if (hasControl)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            MoveAndRotate(horizontalInput, verticalInput);

            Tilt();
        }
    }



    private void MoveAndRotate(float horizontalInput, float verticalInput)
    {
        // Calculate the rotation change
        float rotationChange = rotationSpeed * horizontalInput * Time.deltaTime;
        transform.Rotate(0, rotationChange, 0, Space.World);

        // Calculate the forward movement
        Vector3 moveDirection = transform.forward * verticalInput * speed * Time.deltaTime;
        transform.position += moveDirection;


    }

    //public void TiltLeft()
    //{
    //    //transform.Rotate(Vector3.right,   rotationPower * tiltPower *  Time.deltaTime);
    //    //tiltAngle += rotationPower * tiltPower * Time.deltaTime;
    //    //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, tiltAngle);
    //    Debug.Log("this is left");
    //}

    //public void TiltRight()
    //{
    //    //transform.Rotate(Vector3.right,  rotationPower * -tiltPower *  Time.deltaTime);
    //    //tiltAngle += rotationPower * tiltPower * Time.deltaTime;
    //    //transform.localRotation = Quaternion.Euler(0.0f, 0.0f, tiltAngle);
    //    Debug.Log("this is right");
    //}

    public void Tilt()
    {
        transform.Rotate(Vector3.forward, horizontalInput * rotationPower * tiltPower * Time.deltaTime, Space.Self);
        
        if(verticalInput == 0)
        {
            if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 180)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * -tiltPower, Space.Self);
            }
            else
            {
                transform.Rotate(-Vector3.forward * Time.deltaTime * -tiltPower, Space.Self);
            }

        }



        if (transform.eulerAngles.z >= 45 && transform.eulerAngles.z <= 315)
        {
            levelController.ActivateLosePanel();
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,90);
            hasControl = false;
            Debug.Log("player loses");
        }
    }



    //public void PlayerLoses()
    //{
    //    if(transform.localRotation.z <= -45 || transform.localRotation.z >= 45)
    //    {
    //        levelController.ActivateLosePanel();
    //    }
    //}




    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("LevelOneComplete"))
        {
            Debug.Log("you finished!");
            levelController.ActivateWinPanel();
        }

    }
}


