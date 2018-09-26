using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSJump : MonoBehaviour
{
    public float JumpSpeed;

    private bool isInAir = false;
    private bool isGrounded = true;
    private Rigidbody Rb;
    private float rtAngle;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isGrounded = false;
                isInAir = true;
                Rb.velocity.Set(Rb.velocity.x, 0, Rb.velocity.z);
                Rb.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
            }
            else
            {
                if (isInAir)
                {
                    Rb.velocity.Set(Rb.velocity.x, 0, Rb.velocity.z);
                    Rb.AddForce(-Vector3.up * JumpSpeed, ForceMode.Impulse);
                }
            }
        }

        if (isInAir)
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out _hit))
            {
                rtAngle += 360.0f / _hit.distance * Time.deltaTime;
            }
        }
        gameObject.transform.eulerAngles = new Vector3(0, rtAngle, 0);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Blocks")
        {
            return;
        }
        isInAir = false;
        isGrounded = true;
        rtAngle = 0.0f;       
    }
}
