using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the jump and the physics of the player
/// </summary>

public class MSJump : MonoBehaviour
{
    public float JumpSpeed;
    public float ExplosionForce;
    public float MAX_UPSPEED;
    public float MAX_HEIGHT;

    private bool isInAir = false;
    private bool isGrounded = true;
    private bool isDoubleJUmp = false;
    private Rigidbody Rb;
    private float rtAngle;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isGrounded = false;
                isInAir = true;
                Rb.velocity = Vector3.zero;
                Rb.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
            }
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

        
        if (isDoubleJUmp)
        {
            // Check the Object below
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out _hit))
            {
                // Second Jump ground pound animation
                rtAngle += 360.0f / _hit.distance * Time.deltaTime;

                if (_hit.distance <= 0.5)
                {
                    var _OBJScript = _hit.transform.gameObject.GetComponent<BlockPara>();
                    _OBJScript.Set_isTriggered(true);

                    if(_OBJScript.Get_BlockType() == BlockCounter.BlockType.Bomb)
                    {
                        //Rb.velocity = Vector3.zero;
                        //Rb.AddExplosionForce(ExplosionForce, transform.position, 1.0f, 1.0f, ForceMode.Impulse);
                    }

                    isDoubleJUmp = false;
                }             
            }
        }
        // Ground pound animation updater
        gameObject.transform.eulerAngles = new Vector3(0, rtAngle, 0);


        // Speed bumper
        if (Rb.velocity.y > MAX_UPSPEED || Rb.velocity.y < -MAX_UPSPEED)
        {
            Rb.velocity = Vector3.zero;
            Rb.velocity.Set(0, MAX_UPSPEED, 0);
        }

        //Debug.Log(Rb.velocity);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Blocks")
        {
            return;
        }

        /*
        if(isDoubleJUmp)
        {
            var _OBJScript = col.gameObject.GetComponent<BlockPara>();
            _OBJScript.Set_isTriggered(true);

            if (_OBJScript.Get_BlockType() == BlockCounter.BlockType.Bomb)
            {
                Rb.velocity.Set(Rb.velocity.x, 0, Rb.velocity.z);
                Rb.AddExplosionForce(ExplosionForce, transform.position, 1.0f, 1.0f, ForceMode.Impulse);
            }
            else
            {
                isInAir = false;
                isGrounded = true;
                isDoubleJUmp = false;
                rtAngle = 0.0f;
            }
        }
        */

        isInAir = false;
        isGrounded = true;
        
        rtAngle = 0.0f;
    }
}
