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
    HIDDENPATROLJUMP,    // Moves from one point to another point and jumps only when player is in it's detection radius
    DEAD
}

public class Enemy : MonoBehaviour {

    [SerializeField]
    private ENEMYTYPES CurrType;

    [SerializeField]
    private float WalkSpeed;

    [SerializeField]
    private float JumpSpeed;

    private bool IsGrounded;

    [SerializeField]
    private GameObject PatrolPointA;
    [SerializeField]
    private GameObject PatrolPointB;
    [SerializeField]
    private bool PatrolToA = false;

    private bool Hidden = true;

    private float TimeElapsed;
    [SerializeField]
    private float TimeToDecay;

    [SerializeField]
    private bool IsImmortal;
    private ENEMYTYPES PrevType;
    private RigidbodyConstraints PrevConstraints;

    [SerializeField]
    private GameObject ScorePopup;
    [SerializeField]
    private int Score;

    [SerializeField]
    private string DeathSound;

    private Rigidbody RigidRef;
    private SoundSystem SoundSystemRef;

    // Reset Variables
    private Vector3 OrgPos;
    private Vector3 OrgSize;
    private ENEMYTYPES OrgType;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        OrgPos = transform.localPosition;
        OrgSize = transform.localScale;
        OrgType = CurrType;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position, -transform.up, transform.lossyScale.y / 2* 1.001f);
        Debug.DrawRay(transform.position, -transform.up * (transform.lossyScale.y / 2 * 1.001f), Color.white);

        switch (CurrType)
        {
            case (ENEMYTYPES.WALK):
                RigidRef.MovePosition(transform.position + transform.forward * WalkSpeed * Time.fixedDeltaTime);
                break;
            case (ENEMYTYPES.PATROL):
                if(PatrolToA)
                {
                    if(Vector3.Distance(RigidRef.position, PatrolPointA.transform.position) > transform.lossyScale.x * 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointA.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkSpeed) * Time.fixedDeltaTime);
                    }
                    else
                    {
                        PatrolToA = false;
                    }
                }
                else
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointB.transform.position) > transform.lossyScale.x * 0.1f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointB.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkSpeed) * Time.fixedDeltaTime);
                    }
                    else
                    {
                        PatrolToA = true;
                    }
                }
                break;
            case (ENEMYTYPES.WALKJUMP):
                RigidRef.MovePosition(transform.position + transform.forward * WalkSpeed * Time.fixedDeltaTime);
                if (IsGrounded)
                {
                    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                }
                break;
            case (ENEMYTYPES.PATROLJUMP):
                if (PatrolToA)
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointA.transform.position) > transform.lossyScale.x * 0.5f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointA.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkSpeed) * Time.fixedDeltaTime);
                        if (IsGrounded)
                            RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                    }
                    else
                    {
                        if(IsGrounded)
                            PatrolToA = false;
                    }
                }
                else
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointB.transform.position) > transform.lossyScale.x * 0.5f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointB.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkSpeed) * Time.fixedDeltaTime);
                        if (IsGrounded)
                        {
                            RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                        }
                    }
                    else
                    {
                        if (IsGrounded)
                            PatrolToA = true;
                    }
                }
                break;
            case (ENEMYTYPES.HIDDENWALKJUMP):
                RigidRef.MovePosition(transform.position + transform.forward * WalkSpeed * Time.fixedDeltaTime);
                if (IsGrounded && !Hidden)
                {
                    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                }
                break;
            case (ENEMYTYPES.HIDDENPATROLJUMP):
                if (PatrolToA)
                {
                    if (Vector3.Distance(RigidRef.position, PatrolPointA.transform.position) > transform.lossyScale.x * 0.5f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointA.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkSpeed) * Time.fixedDeltaTime);
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
                    if (Vector3.Distance(RigidRef.position, PatrolPointB.transform.position) > transform.lossyScale.x * 0.5f)
                    {
                        RigidRef.MovePosition(RigidRef.position + (PatrolPointB.transform.position - RigidRef.position).normalized * Mathf.Abs(WalkSpeed) * Time.fixedDeltaTime);
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
            case (ENEMYTYPES.DEAD):
                if(TimeElapsed >= TimeToDecay)
                {
                    if(!IsImmortal)
                    {
                        transform.localScale = OrgSize;
                        if (transform.name.Contains("Clone"))
                            Destroy(this.gameObject);
                        else
                            GetComponent<Renderer>().enabled = false;
                    }
                    else
                    {
                        transform.localScale = OrgSize;
                        CurrType = PrevType;
                        GetComponent<Collider>().isTrigger = false;
                        transform.position += new Vector3(0, transform.lossyScale.y * 0.5f, 0);
                        RigidRef.constraints = PrevConstraints;
                        TimeElapsed = 0;
                    }
                }
                else
                {
                    TimeElapsed += Time.deltaTime;
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

        if (other.tag == "Killbox")
        {
            CurrType = ENEMYTYPES.DEAD;
            RigidRef.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Collider>().isTrigger = true;

            TimeElapsed = TimeToDecay;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if(CurrType != ENEMYTYPES.DEAD)
        {
            if (CollidedObject.tag == "Player")
            {
                if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded()
                    && CollidedObject.transform.position.y - CollidedObject.transform.lossyScale.y / 2 >= transform.position.y + transform.lossyScale.y / 2
                    && CollidedObject.GetComponent<Rigidbody>().velocity.y <= 0
                    )
                {
                    PrevType = CurrType;
                    CurrType = ENEMYTYPES.DEAD;
                    GetComponent<Collider>().isTrigger = true;
                    if(IsGrounded || IsImmortal)
                    {
                        GroundDeath();
                    }
                    else
                        AirborneDeath();

                    CollidedObject.GetComponent<TPSLogic>().PushUp();
                }
                else
                {
                    CollidedObject.GetComponent<TPSLogic>().Death();
                }
            }
            else
            {
                // Change Walk Direction if bumped into something
                if(Mathf.Abs(transform.position.y - CollidedObject.transform.position.y) < CollidedObject.transform.lossyScale.y / 2)
                    WalkSpeed = -WalkSpeed;
            }
        }
    }

    public void GroundDeath()
    {
        CurrType = ENEMYTYPES.DEAD;
        GetComponent<Collider>().isTrigger = true;

        PrevConstraints = RigidRef.constraints;

        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
        transform.localScale *= 0.5f;
        transform.localScale = new Vector3(OrgSize.x, transform.localScale.y, OrgSize.z);

        transform.position -= new Vector3(0, transform.lossyScale.y * 0.5f, 0);

        if (DeathSound != "")
            SoundSystemRef.PlaySFX(DeathSound);

        GameObject n_Score = Instantiate(ScorePopup, transform);
        n_Score.transform.parent = null;
        n_Score.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        n_Score.transform.position = this.transform.position;

        n_Score.GetComponent<TextMesh>().text = Score.ToString();
    }

    public void AirborneDeath()
    {
        CurrType = ENEMYTYPES.DEAD;
        GetComponent<Collider>().isTrigger = true;

        RigidRef.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        if (DeathSound != "")
            SoundSystemRef.PlaySFX(DeathSound);

        GameObject n_Score = Instantiate(ScorePopup, transform);
        n_Score.transform.parent = null;
        n_Score.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        n_Score.transform.position = this.transform.position;

        n_Score.GetComponent<TextMesh>().text = Score.ToString();
    }

    public void Reset()
    {
        transform.localPosition = OrgPos;
        CurrType = OrgType;
        transform.localScale = OrgSize;
        if(RigidRef.constraints == RigidbodyConstraints.FreezeAll)
            RigidRef.constraints = PrevConstraints;
        TimeElapsed = 0;
    }
}
