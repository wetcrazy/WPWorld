using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour {

    [SerializeField]
    private float JumpSpeed;
    [SerializeField]
    private bool IsGrounded = false;
    private bool RestrictJump = false;
    [SerializeField]
    private AudioClip JumpSFX;

    [SerializeField]
    private int Points = 0;

    [SerializeField]
    private int DeathCounter = 0;

    private Rigidbody RigidRef;
    private PlayerMovement MovementRef;

    [SerializeField]
    private float AirborneMovementSpeed;
    private float OrgSpeed;

    private bool HasBounced = false;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        MovementRef = GetComponent<PlayerMovement>();

        OrgSpeed = MovementRef.GetMovementSpeed();

        Physics.gravity = new Vector3(0, -5, 0);
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (IsGrounded)
        {
            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.875f, Color.red);
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.875f, Color.blue);

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.875f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.875f))
            {
                if (Input.GetKeyDown(KeyCode.Space) && !RestrictJump)
                {
                    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                    if (JumpSFX != null)
                        GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
                    IsGrounded = false;
                }
            }
            else
            {
                IsGrounded = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.875f, Color.red);
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.875f, Color.blue);

            Debug.DrawRay(transform.position, -transform.right.normalized * transform.lossyScale.x * 1.25f, Color.cyan);
            Debug.DrawRay(transform.position, transform.right.normalized * transform.lossyScale.x * 1.25f, Color.cyan);

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.875f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.875f))
            {
                if(Physics.Raycast(transform.position, -transform.right.normalized, out hit, transform.lossyScale.x * 1.25f) || Physics.Raycast(transform.position, transform.right.normalized, out hit, transform.lossyScale.x * 1.25f))
                {
                    // TO DO, CHECK FOR SPECIFIC ITEMS AND MAKE IT SO THAT THEY DON"T INTERFERE WITH JUMPING
                    if(hit.transform.GetComponent<Renderer>().isVisible)
                    {
                        Debug.Log("Left and/or right is colliding with " + hit.transform.name);
                        if (Vector3.Distance(RigidRef.velocity, Vector3.zero) > 0.01f)
                            MovementRef.GetDPadInput(Vector3.zero);
                    }
                    else
                    {
                        if (Vector3.Distance(RigidRef.velocity, Vector3.zero) < 0.01f)
                            IsGrounded = true;
                    }
                }
                else
                {
                    if (Vector3.Distance(RigidRef.velocity, Vector3.zero) < 0.01f)
                        IsGrounded = true;
                }
            }
        }
    }

    public void SetPoints(int n_Points)
    {
        Points = n_Points;
    }

    public int GetPoints()
    {
        return Points;
    }

    public int GetDeaths()
    {
        return DeathCounter;
    }

    public void Death()
    {
        DeathCounter++;
        GetComponent<PlayerMovement>().Respawn();
    }

    public bool GetJumpRestrict()
    {
        return RestrictJump;
    }

    public void SetJumpRestrict(bool n_Restrict)
    {
        RestrictJump = n_Restrict;
    }

    public bool GetGrounded()
    {
        return IsGrounded;
    }
}
