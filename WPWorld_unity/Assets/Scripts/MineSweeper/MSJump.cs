using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the jump and the physics of the player
/// </summary>

public class MSJump : MonoBehaviour
{
    public float JumpSpeed;
    public float MAX_UPSPEED;
    public GameObject BlockCounter;

    private bool isInAir = false;
    private bool isGrounded = true;
    private bool isDoubleJUmp = false;
    private Rigidbody Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Physic Update
    /// </summary>
    private void FixedUpdate()
    {
        // Keyboard Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        // Raycast the block below the player 
        if (isDoubleJUmp)   
        {
            // Check the Object below
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out _hit))
            {             
                // Block checking
                if(_hit.transform.gameObject.tag != "Blocks")
                {
                    return;
                }
                // If the distance is small enough, trigger it
                if (_hit.distance <= 1.0f)
                {               
                    BlockCounter.SendMessage("WhenTriggered", _hit.transform.gameObject);                 

                    isDoubleJUmp = false;
                }             
            }
        }
       
        // Speed bumper
        if (Rb.velocity.y > MAX_UPSPEED || Rb.velocity.y < -MAX_UPSPEED)
        {
            Rb.velocity = Vector3.zero;
            Rb.velocity.Set(0, MAX_UPSPEED, 0);
        }      
    }

    /// <summary>
    /// Jumping
    /// </summary>
    private void Jump()
    {
        // 1st Jump 
        if (isGrounded)
        {
            isGrounded = false;
            isInAir = true;
            Rb.velocity = Vector3.zero;
            Rb.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        }
        // 2nd Jump (ground pound)
        else
        {
            if (isInAir)
            {
                Rb.velocity = Vector3.zero;
                Rb.AddForce(-Vector3.up * JumpSpeed, ForceMode.Impulse);
                isDoubleJUmp = true;
            }
        }
    }

    /// <summary>
    /// For button jumping (phone)
    /// </summary>
    private void GetJumpButtonInput()
    {
        Jump();
    }

    /// <summary>
    /// Resets variables when hit the ground 
    /// </summary>
    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Blocks")
        {
            return;
        }      

        isInAir = false;
        isGrounded = true;   
    }
}
