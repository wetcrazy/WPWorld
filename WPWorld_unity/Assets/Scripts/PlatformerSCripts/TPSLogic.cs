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
            Debug.DrawRay(transform.position, (-transform.up - transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.red);
            Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.5f, Color.green);
            Debug.DrawRay(transform.position, (-transform.up + transform.right * (transform.lossyScale.x * 10)).normalized * transform.lossyScale.y * 1.5f, Color.blue);

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                if (Input.GetKeyDown(KeyCode.Space) && !RestrictJump)
                {
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

            Debug.DrawRay(transform.position, -transform.right.normalized * transform.lossyScale.x * 1.25f, Color.cyan);
            Debug.DrawRay(transform.position, transform.right.normalized * transform.lossyScale.x * 1.25f, Color.cyan);

            // Check if any raycast has collided with the floor, may collide with left and right blocks unwillingly
            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                RaycastHit hit2, hit3;

                // Checks if all three or only one raycast is hitting
                if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                    && Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit2, transform.lossyScale.y * 1.5f)
                    && Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit3, transform.lossyScale.y * 1.5f))
                {
                    if(hit.transform.gameObject == hit2.transform.gameObject && hit.transform.gameObject == hit3.transform.gameObject
                        && (!hit.transform.name.Contains("Coin") && !hit.transform.name.Contains("Enemy")))
                    {
                        Debug.Log("All 3 raycast hits " + hit.transform.gameObject.name);
                        if (Vector3.Distance(RigidRef.velocity, Vector3.zero) < 0.01f)
                            IsGrounded = true;
                    }
                    else
                    {
                        Debug.Log(hit.transform.name + " , " + hit2.transform.name + " , " + hit3.transform.name);
                        if (Vector3.Distance(RigidRef.velocity, Vector3.zero) > 0.01f)
                            MovementRef.GetDPadInput(Vector3.zero);
                    }
                }
                else
                {
                    // Checks if left and right raycast has detected anything during three points hitting to ensure it only detects the bottom and not it's side by mistake
                    if (Physics.Raycast(transform.position, -transform.right.normalized, out hit, transform.lossyScale.x * 1.25f) || Physics.Raycast(transform.position, transform.right.normalized, out hit, transform.lossyScale.x * 1.25f))
                    {
                        // Checks if the hit object is visible as certain objects will go invisible during the platforming section of the game
                        if (hit.transform.GetComponent<Renderer>().isVisible && !hit.transform.name.Contains("Coin"))
                        {
                            Debug.Log("Left and/or right is colliding with " + hit.transform.name);
                            if (Vector3.Distance(RigidRef.velocity, Vector3.zero) > 0.01f)
                                MovementRef.GetDPadInput(Vector3.zero);
                        }
                        else
                        {
                            // Checks if the rigid body is near zero to prevent resetting during a jump
                            if (Vector3.Distance(RigidRef.velocity, Vector3.zero) < 0.01f)
                                IsGrounded = true;
                        }
                    }
                    else
                    {
                        // If unable to find any left or right collision, instantly set to grounded after checking rigidbody velocity
                        if (Vector3.Distance(RigidRef.velocity, Vector3.zero) < 0.01f)
                            IsGrounded = true;
                    }
                }
            }
        }
    }

    public void Jump()
    {
        RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
        if (JumpSFX != null)
            GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
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

    //private void Jump()
    //{
    //    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
    //    if (JumpSFX != null)
    //        GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
    //    IsGrounded = false;
    //}

    private void GetJumpButtonInput()
    {
        Jump();
    }

    public bool GetGrounded()
    {
        return IsGrounded;
    }
}
