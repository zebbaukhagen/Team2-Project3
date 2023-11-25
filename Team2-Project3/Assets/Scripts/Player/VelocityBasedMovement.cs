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
    public float rotationSpeed = 200.0f; // Adjust this to control the rotation speed
    public float moveSpeed = 5.0f;
   
    //movement direction local to the holders direction
    float tiltPower = -10.0f;
    float counterTilt = 2.0f;

    public bool playerHasFallen = false;
    
    






    //Velocity gained per second. Applies midairMovementMultiplier when we are not grounded:

    void Movement()
    {
        if (playerCanMove && SceneManager.GetActiveScene().name == "Level_3")
        {
            float downforce = -0.75f;
            bool increasingGravity = false;
            //float secondaryForce = 0.75f;
            
            Vector3 movement = transform.forward * Input.GetAxis("Vertical");
            movement.y = downforce;
            //movement.x = secondaryForce;
            characterController.Move(movement * Time.deltaTime * moveSpeed);

            if(characterController.isGrounded == false)
            {
                if(characterController.velocity.y < 0)
                {
                    downforce -= 5.0f * Time.deltaTime;
                    increasingGravity = true;
                }
            }
            else
            {
                // If the player is grounded, reset the downforce and the flag
                downforce = -0.75f;
                increasingGravity = false;
            }
            if (increasingGravity)
            {
                movement.y = downforce;
                characterController.Move(movement * Time.deltaTime * moveSpeed);
            }
        }
    }
    //else if (playerCanMove && SceneManager.GetActiveScene().name == "Level_3")
    //{
    //    float moonGravity = -0.01f;

    //    Vector3 movement = transform.forward * Input.GetAxis("Vertical");
    //    movement.y = moonGravity;
    //    characterController.Move(movement * Time.deltaTime * moveSpeed);
    //}




    public void Tilt()
    {
        if (playerCanMove)
        {
            modelHolder.Rotate(0.0f, 0.0f, Input.GetAxis("Horizontal") * tiltPower * Time.deltaTime * counterTilt, Space.Self);

            //if (Input.GetAxis("Vertical") == 0)
            //{
            if (modelHolder.localRotation.z >= 0)
            {
                modelHolder.Rotate(0.0f, rotationSpeed, -tiltPower * Time.deltaTime, Space.Self);
                //modelHolder.Rotate(0.0f, rotationSpeed, 0.0f, -tiltPower * Time.delayTime, Space.Self);
            }
            else
            {
                modelHolder.Rotate(0.0f, 0.0f, tiltPower * Time.deltaTime, Space.Self);
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
        //PlayerFallsOver();
    }
}



