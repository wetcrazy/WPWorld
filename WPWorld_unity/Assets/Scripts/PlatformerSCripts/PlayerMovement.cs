using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RESTRICTMOVE
{
    X_Axis,
    Z_Axis
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;
    private Vector3 MovementDir;

    [SerializeField]
    private float JumpSpeed;

    [SerializeField]
    private bool RestrictMovement;
    [SerializeField]
    RESTRICTMOVE CurrRestrictions;

    Rigidbody RigidRef;

    // Use this for initialization
    void Start () {
		RigidRef = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -20.0f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        bool IsGrounded = Physics.Raycast(transform.position, -transform.up, transform.lossyScale.y + 0.5f);
        Debug.DrawRay(transform.position, -transform.up * (transform.lossyScale.y + 0.5f), Color.white);

        if(IsGrounded)
        {
            MovementDir = Vector3.zero;

            if(RestrictMovement)
            {
                if (CurrRestrictions == RESTRICTMOVE.X_Axis)
                    MovementDir.x = Input.GetAxis("Horizontal");
                else
                    MovementDir.z = Input.GetAxis("Vertical");
            }
            else
            {
                MovementDir = Input.GetAxis("Vertical") * Camera.main.transform.forward * 1.5f;
                MovementDir += Input.GetAxis("Horizontal") * Camera.main.transform.right * 1.5f;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
            }
        }
        else
        {
            MovementDir = Vector3.zero;

            if (RestrictMovement)
            {
                if (CurrRestrictions == RESTRICTMOVE.X_Axis)
                    MovementDir.x = Input.GetAxis("Horizontal");
                else
                    MovementDir.z = Input.GetAxis("Vertical");
            }
            else
            {
                MovementDir = Input.GetAxis("Vertical") * Camera.main.transform.forward * 1.5f;
                MovementDir += Input.GetAxis("Horizontal") * Camera.main.transform.right * 1.5f;
            }
        }
	}

    void FixedUpdate()
    {
        RigidRef.MovePosition(RigidRef.position + MovementDir * MovementSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.position.y - collision.gameObject.transform.lossyScale.y / 2
            >= transform.position.y + transform.lossyScale.y / 2)
        {
            Vector3 Knockback_Y = RigidRef.velocity;
            Knockback_Y *= -2;

            RigidRef.AddForce(Knockback_Y, ForceMode.VelocityChange);
        }
    }

    public void SetJumpSpeed(float n_JumpSpeed)
    {
        JumpSpeed = n_JumpSpeed;
    }

    public float GetJumpSpeed()
    {
        return JumpSpeed;
    }
}
