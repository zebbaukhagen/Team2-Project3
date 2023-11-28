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
    //public Transform rotationPiece;
    public CharacterController characterController;
    public bool playerCanMove = true;
    [SerializeField] private UILevelController levelController;
    private Timer timer;
    public bool playerBeatLevel = false;
    [SerializeField] private Animator wheelAnim;
    [SerializeField] private Animator seatAnim;

    




    //movement direction local to the holders direction

    public bool playerHasFallen = false;


    //Velocity gained per second. Applies midairMovementMultiplier when we are not grounded:


    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateUnicycle();
        Tilt();
        PlayerFallsOver();
        AnimationControl();
    }

    void Movement()
    {
        if (playerCanMove && SceneManager.GetActiveScene().name == "Level_3")
        {
            Debug.Log("I am on Level 3, the Moon Level ");
            float downforce = -0.25f;
            //bool increasingGravity = false;
            Vector3 forwardDirection = modelHolder.forward;
            forwardDirection.y = 0.0f;
            float moveMoonSpeed = 5.0f;

            Vector3 movement = forwardDirection * Input.GetAxis("Vertical");
            movement.y = downforce;
            characterController.Move(movement * Time.deltaTime * moveMoonSpeed);

            if (!characterController.isGrounded)
            {
                if (characterController.velocity.y < 0)
                {
                    downforce -= 5.0f * Time.deltaTime;
                    Debug.Log(downforce);
                    //increasingGravity = true;
                }
            }
            else
            {
                // If the player is grounded, reset the downforce and the flag
                downforce = -0.75f;
                //increasingGravity = false;
            }
        }
        else if (playerCanMove && SceneManager.GetActiveScene().name == "Level_1" || SceneManager.GetActiveScene().name == "Level_2")
        {
            Debug.Log("Not Level 3");
            float moveEarthSpeed = 2.0f;
            float downforce = -0.75f;
            //bool increasingGravity = false;

            Vector3 forwardDirection = modelHolder.forward;
            forwardDirection.y = 0.0f;

            Vector3 movement = forwardDirection * Input.GetAxis("Vertical");
            movement.y = downforce;

            characterController.Move(movement * Time.deltaTime * moveEarthSpeed);

        }

    }

    public void Tilt()
    {
        float tiltMoonPower = -10.0f;
        float counterMoonTilt = 2.5f;
        float steadyMoonForce = 20.0f;
        float tiltEarthPower = -10.0f;
        float counterEarthTilt = 5.0f;
        float steadyEarthForce = 40.0f;

        if (playerCanMove && SceneManager.GetActiveScene().name == "Level_3")
        {
            
            modelHolder.Rotate(0.0f, 0.0f, Input.GetAxis("Horizontal") * tiltMoonPower * Time.deltaTime * counterMoonTilt, Space.Self);
            //modelHolder.Rotate(0.0f, Input.GetAxis("Horizontal"), 0.0f * tiltMoonPower * Time.deltaTime * counterMoonTilt, Space.Self);

            if (Input.GetKey(KeyCode.Keypad4))
            {
                modelHolder.Rotate(0.0f, 0.0f, steadyMoonForce * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                modelHolder.Rotate(0.0f, 0.0f, -steadyMoonForce * Time.deltaTime, Space.Self);
            }


            //if (Input.GetAxis("Vertical") == 0)
            //{
            if (modelHolder.localRotation.z >= 0)
            {
                modelHolder.Rotate(0.0f, 0.0f, -tiltMoonPower * Time.deltaTime, Space.Self);
            }
            else
            {
                modelHolder.Rotate(0.0f, 0.0f, tiltMoonPower * Time.deltaTime, Space.Self);
            }
        }
        else if (playerCanMove && SceneManager.GetActiveScene().name == "Level_1" || SceneManager.GetActiveScene().name == "Level_2")
        {
            
            modelHolder.Rotate(0.0f, 0.0f, Input.GetAxis("Horizontal") * tiltEarthPower * Time.deltaTime * counterEarthTilt, Space.Self);
            //modelHolder.Rotate(0.0f, Input.GetAxis("Horizontal"), 0.0f * tiltEarthPower * Time.deltaTime * counterEarthTilt, Space.Self);

            if (Input.GetKey(KeyCode.Keypad4))
            {
                modelHolder.Rotate(0.0f, 0.0f, steadyEarthForce * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                modelHolder.Rotate(0.0f, 0.0f, -steadyEarthForce * Time.deltaTime, Space.Self);
            }
            


            //if (Input.GetAxis("Vertical") == 0)
            //{
            if (modelHolder.localRotation.z >= 0)
            {
                modelHolder.Rotate(0.0f, 0.0f, -tiltEarthPower * Time.deltaTime, Space.Self);
            }
            else
            {
                modelHolder.Rotate(0.0f, 0.0f, tiltEarthPower * Time.deltaTime, Space.Self);
            }
        }
    }
    
 
    public void PlayerFallsOver()
    {
        if(modelHolder.eulerAngles.z >= 50 && modelHolder.eulerAngles.z <= 300)
        {
            playerCanMove = false;
            playerHasFallen = true;
            levelController.ActivateLosePanel();
            timer.SetLoseTime();
            
        }
    }

    public void RotateUnicycle()
    {
        float rotationSpeed = 100.0f; // Adjust this to control the rotation speed

        if (playerCanMove)
        {
            float rotationChange = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationChange, Space.Self);    
        }
    }

    private void AnimationControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wheelAnim.SetBool("Forward", true);
            seatAnim.SetBool("SeatForward", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            wheelAnim.SetBool("Forward", false);
            seatAnim.SetBool("SeatForward", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            wheelAnim.SetBool("Backward", true);
            seatAnim.SetBool("SeatBackward", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            wheelAnim.SetBool("Backward", false);
            seatAnim.SetBool("SeatBackward", false);
        }
    }
}



