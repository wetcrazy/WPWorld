using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBJump : MonoBehaviour
{
    public float jumpSpeed;

    private Vector3 Dir;
    private Rigidbody Rb;
    private bool isGrounded = true;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("JUMPING");
            Rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            isGrounded = false;
        }
#endif
        //Debug.Log(isGrounded);
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }
}
