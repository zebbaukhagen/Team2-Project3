using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VelocityBasedMovement : MonoBehaviour
{

    /// <summary>
    /// References
    /// </summary>
    public Transform modelHolder;
    public CharacterController characterController;
    public bool playerCanMove = true;
    [SerializeField] private UILevelController levelController;

  

    /// <summary>
    /// Movement
    /// </summary>
    public float rotationSpeed = 50.0f; // Adjust this to control the rotation speed
    public float moveSpeed = 5.0f;
   
    //movement direction local to the holders direction
    float tiltPower = -20.0f;
    float counterTilt = 4.0f;

    public bool playerHasFallen = false;
    public bool isGrounded = true;






    //Velocity gained per second. Applies midairMovementMultiplier when we are not grounded:

    void Movement()
    {
        if (playerCanMove)
        {
            float downforce = -0.75f;
            //float secondaryForce = 0.75f;
            
            Vector3 movement = transform.forward * Input.GetAxis("Vertical");
            movement.y = downforce;
            //movement.x = secondaryForce;
            characterController.Move(movement * Time.deltaTime * moveSpeed);

            if(isGrounded == false)
            {
                Debug.Log("Gravity is increasing");
                downforce += .1f;
            }
        }
        //else if (playerCanMove && SceneManager.GetActiveScene().name == "Level_3")
        //{
        //    float moonGravity = -0.01f;

        //    Vector3 movement = transform.forward * Input.GetAxis("Vertical");
        //    movement.y = moonGravity;
        //    characterController.Move(movement * Time.deltaTime * moveSpeed);
        //}
    }



    public void Tilt()
    {
        if (playerCanMove)
        {
            modelHolder.Rotate(0.0f, 0.0f, Input.GetAxis("Horizontal") * tiltPower * Time.deltaTime * counterTilt, Space.Self);

            if (Input.GetAxis("Vertical") == 0)
            {
                if (modelHolder.localRotation.z >= 0)
                {
                    modelHolder.Rotate(0.0f, 0.0f, -tiltPower * Time.deltaTime, Space.Self);
                }
                else
                {
                    modelHolder.Rotate(0.0f, 0.0f, tiltPower * Time.deltaTime, Space.Self);
                }
            }
        }
    }

    public void PlayerFallsOver()
    {
        if(modelHolder.eulerAngles.z >= 50 && modelHolder.eulerAngles.z <= 300)
        {
            playerCanMove = false;
            levelController.ActivateLosePanel();
            playerHasFallen = true;
        }
    }





    public void RotateUnicycle()
    {
        if (playerCanMove)
        {
            float rotationChange = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationChange, Space.Self);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateUnicycle();
        Tilt();
        PlayerFallsOver();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the trigger involves a specific tag
        if (other.gameObject.tag == "Ground")
        {
            playerCanMove = true;
            isGrounded = true;
            
        }
    }
}



