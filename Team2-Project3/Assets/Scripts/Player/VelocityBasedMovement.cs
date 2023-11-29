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
    [SerializeField] private UILevelController levelController;
    [SerializeField] private Timer timer;
    public bool playerBeatLevel = false;
    private Animator wheelAnim;
    private Animator seatAnim;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        levelController = GameObject.Find("Canvas").GetComponent<UILevelController>();
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
        if (gameManager.playerIsAbleToMove && SceneManager.GetActiveScene().name == "Level_1")
        {
            float moveEarthSpeed = 5.0f;
            float downforce = -1.5f;

            Vector3 forwardDirection = modelHolder.forward;
            forwardDirection.y = 0.0f;

            Vector3 movement = forwardDirection * Input.GetAxis("Vertical");
            movement.y = downforce;

            characterController.Move(movement * Time.deltaTime * moveEarthSpeed);
        }

        else if (gameManager.playerIsAbleToMove && SceneManager.GetActiveScene().name == "Level_2")
        {

            float moveEarthSpeed = 5.0f;
            float downforce = -1.5f;
            //bool increasingGravity = false;

            Vector3 forwardDirection = modelHolder.forward;
            forwardDirection.y = 0.0f;

            Vector3 movement = forwardDirection * Input.GetAxis("Vertical");
            movement.y = downforce;

            characterController.Move(movement * Time.deltaTime * moveEarthSpeed);
        }


        if (gameManager.playerIsAbleToMove && SceneManager.GetActiveScene().name == "Level_3")
        {
            float downforce = -0.25f;
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
                }
            }
            else
            {

                downforce = -0.75f;

            }
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

        if (gameManager.playerIsAbleToMove && SceneManager.GetActiveScene().name == "Level_3")
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

            if (modelHolder.localRotation.z >= 0)
            {
                modelHolder.Rotate(0.0f, 0.0f, -tiltMoonPower * Time.deltaTime, Space.Self);
            }
            else
            {
                modelHolder.Rotate(0.0f, 0.0f, tiltMoonPower * Time.deltaTime, Space.Self);
            }
        }
        else if (gameManager.playerIsAbleToMove && SceneManager.GetActiveScene().name == "Level_1")
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
        }
        if (gameManager.playerIsAbleToMove && SceneManager.GetActiveScene().name == "Level_2")
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
        }

        if (modelHolder.localRotation.z >= 0)
        {
            modelHolder.Rotate(0.0f, 0.0f, -tiltEarthPower * Time.deltaTime, Space.Self);
        }
        else
        {
            modelHolder.Rotate(0.0f, 0.0f, tiltEarthPower * Time.deltaTime, Space.Self);
        }
    }



    public void PlayerFallsOver()
    {
        if (modelHolder.eulerAngles.z >= 50 && modelHolder.eulerAngles.z <= 300)
        {
            gameManager.playerHasFallen = true;
            gameManager.playerIsAbleToMove = false;
            levelController.ActivateLosePanel();
            timer.SetLoseTime();
        }
    }

    public void RotateUnicycle()
    {
        float rotationSpeed = 100.0f; // Adjust this to control the rotation speed

        if (gameManager.playerIsAbleToMove)
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



