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
    private AudioClip DeathSFX;

    [SerializeField]
    private int Points = 0;

    [SerializeField]
    private int DeathCounter = 0;

    private Rigidbody RigidRef;
    private PlayerMovement MovementRef;

    [SerializeField]
    private float AirborneMovementSpeed;
    private float OrgSpeed;

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
            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            Debug.DrawRay(transform.position, (-transform.up - transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, (-transform.up + transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            MovementRef.SetMovementSpeed(OrgSpeed);

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                if (Input.GetKeyDown(KeyCode.Space) && !RestrictJump)
                {
                    if (JumpSFX != null)
                        GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
                    Jump();
                }
            }
            else
            {
                IsGrounded = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            Debug.DrawRay(transform.position, (-transform.up - transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, (-transform.up + transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            Debug.DrawRay(transform.position, -transform.right.normalized * transform.lossyScale.x * 1.25f, Color.cyan);
            Debug.DrawRay(transform.position, transform.right.normalized * transform.lossyScale.x * 1.25f, Color.cyan);
            Debug.DrawRay(transform.position, -transform.forward.normalized * transform.lossyScale.x * 1.25f, Color.cyan);
            Debug.DrawRay(transform.position, transform.forward.normalized * transform.lossyScale.x * 1.25f, Color.cyan);

            MovementRef.SetMovementSpeed(AirborneMovementSpeed);

            // Check if any raycast has collided with the floor, may collide with left and right blocks unwillingly
            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                // Checks if the object that the bottom raycasts that's hitting is visible
                if(hit.transform.GetComponent<Renderer>().isVisible)
                {
                    if (Physics.Raycast(transform.position, -transform.right.normalized, out hit, transform.lossyScale.x * 1.25f)
                        || Physics.Raycast(transform.position, transform.right.normalized, out hit, transform.lossyScale.x * 1.25f)
                        || Physics.Raycast(transform.position, -transform.forward.normalized, out hit, transform.lossyScale.x * 1.25f)
                        || Physics.Raycast(transform.position, transform.forward.normalized, out hit, transform.lossyScale.x * 1.25f))
                    {
                        // Checks if the object that left and right raycast that's hitting is visible
                        if(hit.transform.gameObject.GetComponent<Renderer>().isVisible)
                        {
                            RaycastHit hit2, hit3, hit4, hit5;

                            // Checks if all 3 raycasts on the bottom is hitting something, otherwise we can determine only one raycast is hitting
                            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                                && Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit2, transform.lossyScale.y * 1.5f)
                                && Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit3, transform.lossyScale.y * 1.5f)
                                && Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit4, transform.lossyScale.y * 1.5f)
                                && Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit5, transform.lossyScale.y * 1.5f))
                            {
                                if (hit.transform.gameObject.GetComponent<Renderer>().isVisible
                                    && hit2.transform.gameObject.GetComponent<Renderer>().isVisible
                                    && hit3.transform.gameObject.GetComponent<Renderer>().isVisible
                                    && hit4.transform.gameObject.GetComponent<Renderer>().isVisible
                                    && hit5.transform.gameObject.GetComponent<Renderer>().isVisible)
                                {
                                    Debug.Log("All five is hitting, so ignore!");
                                    IsGrounded = true;
                                }
                            }
                            else
                            {
                                Debug.Log("All five isn't hitting, don't ignore!");
                                MovementRef.GetDPadInput(Vector3.zero);
                            }
                        }
                        else
                        {
                            Debug.Log("Touching left and right, but invisible!");
                            IsGrounded = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Nothing is touching");
                        IsGrounded = true;
                    }
                }
            }
        }
    }

    public void Jump()
    {
        RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
        IsGrounded = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Killbox")
        {
            Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    public void Death()
    {
        DeathCounter++;
        if (DeathSFX != null && GameObject.Find("Sound System") != null)
            GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(DeathSFX);
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

    private void GetJumpButtonInput()
    {
        Jump();
    }

    public bool GetGrounded()
    {
        return IsGrounded;
    }
}
