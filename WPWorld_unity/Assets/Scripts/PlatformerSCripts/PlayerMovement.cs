using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RESTRICTMOVE
{
    X_Axis,
    Y_Axis,
    Z_Axis
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;
    private Vector3 MovementDir;

    [SerializeField]
    private float JumpSpeed;
    private bool IsGrounded = false;

    [SerializeField]
    private bool RestrictMovement;
    [SerializeField]
    private RESTRICTMOVE CurrRestriction;

    private Vector3 RespawnPoint;

    private Rigidbody RigidRef;

    // Use this for initialization
    void Start () {
		RigidRef = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -5.0f, 0);

        RespawnPoint = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        IsGrounded = Physics.Raycast(transform.position, -transform.up, transform.lossyScale.y * 1.5f);
        Debug.DrawRay(transform.position, -transform.up * (transform.lossyScale.y * 1.5f), Color.white);

        if(IsGrounded)
        {
            MovementDir = Vector3.zero;

            if(RestrictMovement)
            {
                if (CurrRestriction == RESTRICTMOVE.X_Axis)
                    MovementDir.z = Input.GetAxis("Vertical");
                else if (CurrRestriction == RESTRICTMOVE.Z_Axis)
                    MovementDir.x = Input.GetAxis("Horizontal");
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
                if (CurrRestriction == RESTRICTMOVE.X_Axis)
                    MovementDir.z = Input.GetAxis("Vertical") * 0.75f;
                else if (CurrRestriction == RESTRICTMOVE.Z_Axis)
                    MovementDir.x = Input.GetAxis("Horizontal") * 0.75f;
            }
            else
            {
                MovementDir = Input.GetAxis("Vertical") * Camera.main.transform.forward * 0.75f;
                MovementDir += Input.GetAxis("Horizontal") * Camera.main.transform.right * 0.75f;
            }
        }
	}

    void FixedUpdate()
    {
        RigidRef.MovePosition(RigidRef.position + MovementDir * MovementSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Killbox")
        {
            Debug.Log("Reset!");
            Respawn();
        }
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

    public void SetRespawn(Vector3 n_Respawn)
    {
        RespawnPoint = n_Respawn;
    }

    public bool GetGrounded()
    {
        return IsGrounded;
    }

    public void Respawn()
    {
        this.transform.position = RespawnPoint;
    }
}
