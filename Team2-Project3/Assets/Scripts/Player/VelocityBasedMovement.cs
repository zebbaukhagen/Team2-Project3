using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityBasedMovement : MonoBehaviour
{

    /// <summary>
    /// References
    /// </summary>
    public Transform trans;
    public Transform modelHolder;
    public CharacterController characterController;

    /// <summary>
    /// Gravity
    /// </summary>
    //public float maxGravity = 92.0f;
    //public float timeToMaxGravity = 0.6f;

    //Property that gets the downward momentum per second to apply as gravity:
    //public float GravityPerSecond
    //{
    //    get
    //    {
    //        return maxGravity / timeToMaxGravity;
    //    }
    //}

    private float yVelocity = 0.0f;

    /// <summary>
    /// Movement
    /// </summary>
    public float rotationSpeed = 50.0f; // Adjust this to control the rotation speed
    public float moveSpeed = 10.0f;
    public float timeToMaxSpeed = 0.3f;
    public float timeToLoseMaxSpeed = 0.2f;
    public float reverseMomentumMultiplier = 0.6f;
    public float midairMovementMultiplier = 0.4f;
    public float bounciness = 0.2f;
    //movement direction local to the holders direction
    private Vector3 localMovementDirection = Vector3.zero;
    private Vector3 worldVelocity = Vector3.zero;
    public bool grounded = false;
    float tiltPower = -10.0f;
    float counterTilt = 6.0f;

    /// <summary>
    /// GameObjects
    /// </summary>

    public GameObject springArm;

    //Velocity gained per second. Applies midairMovementMultiplier when we are not grounded:
    public float VelocityGainPerSecond
    {
        get
        {
            if (grounded)
            {
                return moveSpeed / timeToMaxSpeed;
            }
            else
            {
                return (moveSpeed / timeToMaxSpeed) * midairMovementMultiplier;
            }
        }
    }

    public float VelocityLossPerSecond
    {
        get
        {
            return moveSpeed / timeToLoseMaxSpeed;
        }
    }

    /// <summary>
    /// Jumping
    /// </summary>
    public float jumpPower = 76.0f;

    void Movement()
    {
        float downforce = -.5f;
        Vector3 movement = transform.forward * Input.GetAxis("Vertical");
        movement.y = downforce;
        characterController.Move(movement * Time.deltaTime * moveSpeed);
        

        //localMovementDirection = Vector3.zero;

        //if (Input.GetKey(KeyCode.D))
        //{
        //    localMovementDirection.x = 1;
        //    Debug.Log("pressed D");
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    localMovementDirection.x = -1;
        //    Debug.Log("pressed A");
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    localMovementDirection.z = 1;
        //    Debug.Log("pressed W");
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    localMovementDirection.z = -1;
        //    Debug.Log("pressed S");
        //}

        //if (localMovementDirection != Vector3.zero ) 
        //{
        //    Debug.Log("im popping_1");
        //    Vector3 worldMovementDirection = modelHolder.TransformDirection(localMovementDirection.normalized);

        //    float multiplier = 1;

        //    float dot = Vector3.Dot(worldMovementDirection.normalized, worldVelocity.normalized);
        //    Debug.Log(worldVelocity);
        //    if (dot < 0)
        //    {
        //        Debug.Log("im popping_2");
        //        multiplier += -dot * reverseMomentumMultiplier;
        //        Vector3 newVelocity = worldVelocity + worldMovementDirection * VelocityGainPerSecond * multiplier * Time.deltaTime;

        //        if (worldVelocity.magnitude > moveSpeed)
        //        {
        //            Debug.Log("im popping_3");
        //            worldVelocity = Vector3.ClampMagnitude(newVelocity, worldVelocity.magnitude);
        //        }
        //        else
        //        {
        //            Debug.Log("im popping_4");
        //            worldVelocity = Vector3.ClampMagnitude(newVelocity, moveSpeed);
        //        }
        //    }
        //}
    }

    public void Tilt()
    {
        modelHolder.Rotate(0.0f, 0.0f, Input.GetAxis("Horizontal") * tiltPower * Time.deltaTime * counterTilt, Space.Self);
        //Debug.Log(modelHolder.localRotation.z);
        if (modelHolder.localRotation.z >= 0)
        {
            modelHolder.Rotate(0.0f, 0.0f, -tiltPower * Time.deltaTime, Space.Self);
        }
        else
        {
            modelHolder.Rotate(0.0f, 0.0f, tiltPower * Time.deltaTime, Space.Self);
        }


        //if (transform.eulerAngles.z >= 45 && transform.eulerAngles.z <= 315)
        //{
        //    levelController.ActivateLosePanel();
        //    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 90);
        //    hasControl = false;
        //    Debug.Log("player loses");
        //}
    }

    public void RotateUnicycle()
    {
        float rotationChange = rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationChange, Space.Self);
        //springArm.transform.Rotate(Vector3.up, rotationChange, Space.Self);

        
    }

    //void VelocityLoss()
    //{
    //    if(grounded && (localMovementDirection == Vector3.zero || worldVelocity.magnitude > moveSpeed))
    //    {
    //        float velocityLoss = VelocityLossPerSecond * Time.deltaTime;

    //        if(velocityLoss > worldVelocity.magnitude)
    //        {
    //            worldVelocity = Vector3.zero;
    //        }
    //        else
    //        {
    //            worldVelocity -= worldVelocity.normalized * velocityLoss;
    //        }
    //    }
    //}

    void Gravity()
    {

        //yVelocity = Mathf.Max(yVelocity - GravityPerSecond * Time.deltaTime, -maxGravity);

        
        
    }

    void Jumping()
    {
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = jumpPower;
            grounded = false;
        }
    }

    void ApplyVelocity()
    {
        if(grounded)
        {
            yVelocity = -1;
            Vector3 MovementThisFrame = (worldVelocity + (Vector3.up * yVelocity)) * Time.deltaTime;
            Vector3 predictPosition = trans.position + MovementThisFrame;

            if(MovementThisFrame.magnitude > .03f)
            {
                characterController.Move(MovementThisFrame);
            }
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
     

    }

    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("You Are On The Ground");
            grounded = true;
        }
    }
}

