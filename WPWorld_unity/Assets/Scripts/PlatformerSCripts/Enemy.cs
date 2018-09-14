using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYTYPES
{
    WALK,               // Walk in one direction only
    PATROL,             // Move from one point to another point
    WALKJUMP,           // Jumps in one direction only
    PATROLJUMP,         // Jumps from one point to another point
    HIDDENWALKJUMP,     // Walks in one direction and jumps only when player is in it's detection radius
    HIDDENPATROLJUMP    // Moves from one point to another point and jumps only when player is in it's detection radius
}

public class Enemy : MonoBehaviour {

    [SerializeField]
    private ENEMYTYPES CurrType;

    [SerializeField]
    private float WalkDirection;

    [SerializeField]
    private float JumpSpeed;

    [SerializeField]
    private GameObject PatrolPointA;
    [SerializeField]
    private GameObject PatrolPointB;
    [SerializeField]
    private bool PatrolToA = false;

    private bool Hidden = true;

    Rigidbody RigidRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        bool IsGrounded = Physics.Raycast(transform.position, -transform.up, transform.lossyScale.y / 2* 1.001f);
        Debug.DrawRay(transform.position, -transform.up * (transform.lossyScale.y / 2 * 1.001f), Color.white);

        switch (CurrType)
        {
            case (ENEMYTYPES.WALK):
                RigidRef.MovePosition(RigidRef.position + new Vector3(WalkDirection, 0) * Time.fixedDeltaTime);
                break;
            case (ENEMYTYPES.PATROL):
                if(PatrolToA)
                {
                    if(Vector3.Distance(RigidRef.position, PatrolPointA.transform.position) > 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointA.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkDirection) * Time.fixedDeltaTime);
                    }
                    else
                    {
                        PatrolToA = false;
                    }
                }
                else
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointB.transform.position) > 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointB.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkDirection) * Time.fixedDeltaTime);
                    }
                    else
                    {
                        PatrolToA = true;
                    }
                }
                break;
            case (ENEMYTYPES.WALKJUMP):
                RigidRef.MovePosition(RigidRef.position + new Vector3(WalkDirection, 0) * Time.fixedDeltaTime);
                if(IsGrounded)
                {
                    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                }
                break;
            case (ENEMYTYPES.PATROLJUMP):
                if (PatrolToA)
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointA.transform.position) > 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointA.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkDirection) * Time.fixedDeltaTime);
                        if (IsGrounded)
                        {
                            RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                        }
                    }
                    else
                    {
                        PatrolToA = false;
                    }
                }
                else
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointB.transform.position) > 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointB.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkDirection) * Time.fixedDeltaTime);
                        if (IsGrounded)
                        {
                            RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                        }
                    }
                    else
                    {
                        PatrolToA = true;
                    }
                }
                break;
            case (ENEMYTYPES.HIDDENWALKJUMP):
                if (Hidden)
                    RigidRef.MovePosition(RigidRef.position + new Vector3(WalkDirection, 0) * Time.fixedDeltaTime);
                else
                {
                    RigidRef.MovePosition(RigidRef.position + new Vector3(WalkDirection, 0) * Time.fixedDeltaTime);
                    if (IsGrounded)
                    {
                        RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                    }
                }
                break;
            case (ENEMYTYPES.HIDDENPATROLJUMP):
                if (PatrolToA)
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointA.transform.position) > 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointA.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkDirection) * Time.fixedDeltaTime);
                        if (IsGrounded && !Hidden)
                        {
                            RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                        }
                    }
                    else
                    {
                        PatrolToA = false;
                    }
                }
                else
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointB.transform.position) > 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointB.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkDirection) * Time.fixedDeltaTime);
                        if (IsGrounded && !Hidden)
                        {
                            RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                        }
                    }
                    else
                    {
                        PatrolToA = true;
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (CurrType.ToString().Contains("HIDDEN"))
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, other.gameObject.transform.position - transform.position, out hit))
                {
                    if(hit.transform.tag == "Player")
                    {
                        Hidden = false;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Killbox")
        {

        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().Respawn();
        }

        if (collision.gameObject.transform.position.y - collision.gameObject.transform.lossyScale.y / 2
            >= transform.position.y + transform.lossyScale.y / 2)
        {
            Vector3 Knockback_Y = RigidRef.velocity;
            Knockback_Y *= -2;

            RigidRef.AddForce(Knockback_Y, ForceMode.VelocityChange);
        }
    }
}
