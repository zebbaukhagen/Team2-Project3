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
    public float maxGravity = 92.0f;
    public float timeToMaxGravity = 0.6f;

    //Property that gets the downward momentum per second to apply as gravity:
    public float GravityPerSecond
    {
        get
        {
            return maxGravity / timeToMaxGravity;
        }
    }
    
    private float yVelocity = 0.0f;

    /// <summary>
    /// Movement
    /// </summary>
    public float moveSpeed = 42.0f;
    public float timeToMaxSpeed = 0.3f;
    public float timeToLoseMaxSpeed = 0.2f;
    public float reverseMomentumMultiplier = 0.6f;
    public float midairMovementMultiplier = 0.4f;
    public float bounciness = 0.2f;
    //movement direction local to the holders direction
    private Vector3 localMovementDirection = Vector3.zero;
    private Vector3 worldVelocity = Vector3.zero;
    public bool grounded = false;
    
    //Velocity gained per second. Applies midairMovementMultiplier when we are not grounded:
    public float VelocityGainPerSecond
    {
        get
        {
            if(grounded)
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
        localMovementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            localMovementDirection.x = 1;
            Debug.Log("pressed D");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            localMovementDirection.x = -1;
            Debug.Log("pressed A");
        }
        if (Input.GetKey(KeyCode.W))
        {
            localMovementDirection.z = 1;
            Debug.Log("pressed W");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            localMovementDirection.z = -1;
            Debug.Log("pressed S");
        }

        if (localMovementDirection != Vector3.zero)
        {
            Vector3 worldMovementDirection = modelHolder.TransformDirection(localMovementDirection.normalized);

            float multiplier = 1;

            float dot = Vector3.Dot(worldMovementDirection.normalized, worldVelocity.normalized);

            if (dot < 0)
            {
                multiplier += -dot * reverseMomentumMultiplier;
                Vector3 newVelocity = worldVelocity + worldMovementDirection * VelocityGainPerSecond * multiplier * Time.deltaTime;

                if (worldVelocity.magnitude > moveSpeed)
                {
                    worldVelocity = Vector3.ClampMagnitude(newVelocity, worldVelocity.magnitude);
                }
                else
                {
                    worldVelocity = Vector3.ClampMagnitude(newVelocity, moveSpeed);
                }
            }
        }
    }

    void VelocityLoss()
    {
        if(grounded && (localMovementDirection == Vector3.zero || worldVelocity.magnitude > moveSpeed))
        {
            float velocityLoss = VelocityLossPerSecond * Time.deltaTime;

            if(velocityLoss > worldVelocity.magnitude)
            {
                worldVelocity = Vector3.zero;
            }
            else
            {
                worldVelocity -= worldVelocity.normalized * velocityLoss;
            }
        }
    }

    void Gravity()
    {
        if(!grounded && yVelocity > -maxGravity)
        {
            yVelocity = Mathf.Max(yVelocity - GravityPerSecond * Time.deltaTime, -maxGravity);
        }
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

            if (!grounded && characterController.collisionFlags.HasFlag(CollisionFlags.Below))
            {
                grounded = true;
            }
            else if(grounded && !characterController.collisionFlags.HasFlag(CollisionFlags.Below))
            {
                grounded = false;
            }
            if(!grounded && characterController.collisionFlags.HasFlag(CollisionFlags.Sides))
            {
                worldVelocity = (trans.position - predictPosition).normalized * (worldVelocity.magnitude * bounciness);
            }
            if(yVelocity > 0 && characterController.collisionFlags.HasFlag(CollisionFlags.Above))
            {
                yVelocity = 0;
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
        VelocityLoss();
        Gravity();
        Jumping();
        ApplyVelocity();
    }
}
