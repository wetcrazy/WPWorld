using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour
{

    [SerializeField]
    private float JumpSpeed;
    [SerializeField]
    private bool IsGrounded = false;
    private bool RestrictJump = false;
    private bool Colliding = false;
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
    void Start()
    {
        RigidRef = GetComponent<Rigidbody>();
        MovementRef = GetComponent<PlayerMovement>();

        OrgSpeed = MovementRef.GetMovementSpeed();

        Physics.gravity = new Vector3(0, -5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (IsGrounded)
        {
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);

            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            Debug.DrawRay(transform.position, (-transform.up - transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, (-transform.up + transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            MovementRef.SetMovementSpeed(OrgSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (!Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                IsGrounded = false;
            }
            else
            {
                if (!hit.transform.GetComponent<Renderer>() || !hit.transform.GetComponent<Renderer>().isVisible)
                {
                    IsGrounded = false;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);

            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            Debug.DrawRay(transform.position, (-transform.up - transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, (-transform.up + transform.forward * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            Debug.DrawRay(transform.position, -transform.right.normalized * transform.lossyScale.x * 1.1f, Color.cyan);
            Debug.DrawRay(transform.position, transform.right.normalized * transform.lossyScale.x * 1.1f, Color.cyan);
            Debug.DrawRay(transform.position, -transform.forward.normalized * transform.lossyScale.z * 1.1f, Color.cyan);
            Debug.DrawRay(transform.position, transform.forward.normalized * transform.lossyScale.z * 1.1f, Color.cyan);

            MovementRef.SetMovementSpeed(AirborneMovementSpeed);

            RaycastHit hit2, hit3;

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                if (hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible)
                {
                    if (Physics.Raycast(transform.position, -transform.right.normalized, out hit, transform.lossyScale.x * 1.1f)
                        || Physics.Raycast(transform.position, transform.right.normalized, out hit, transform.lossyScale.x * 1.1f)
                        || Physics.Raycast(transform.position, -transform.forward.normalized, out hit, transform.lossyScale.z * 1.1f)
                        || Physics.Raycast(transform.position, transform.forward.normalized, out hit, transform.lossyScale.z * 1.1f))
                    {
                        if ((Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit2, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit3, transform.lossyScale.y * 1.5f))
                            || (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit2, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit3, transform.lossyScale.y * 1.5f)))
                        {
                            if ((hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible) &&
                                (hit2.transform.GetComponent<Renderer>() && hit2.transform.GetComponent<Renderer>().isVisible) &&
                                (hit3.transform.GetComponent<Renderer>() && hit3.transform.GetComponent<Renderer>().isVisible))
                                IsGrounded = true;
                        }
                    }
                    else
                    {
                        if(Colliding)
                            IsGrounded = true;
                    }
                }
            }
        }
    }

    public void Jump()
    {
        if (!IsGrounded || RestrictJump)
            return;

        //if (JumpSFX != null && GameObject.Find("Sound System") != null)
        //    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
        PushUp();
    }

    public void PushUp()
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
        if (other.tag == "Killbox")
        {
            Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Colliding = false;
    }

    public void Death()
    {
        DeathCounter++;
        //if (DeathSFX != null && GameObject.Find("Sound System") != null)
 
          //  GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(DeathSFX);
 
        
 
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
