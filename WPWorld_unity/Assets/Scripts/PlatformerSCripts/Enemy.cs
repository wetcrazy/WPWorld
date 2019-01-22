using System;
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

    public int ID;

	public ENEMYTYPES CurrType;

	// Walk Variables
	public float MovementSpeed;
	private Vector3 MoveDir;

	// Patrol Variables
	public Vector3 PatrolPointA;
	private Vector3 PatrolMarkerA;
	public Vector3 PatrolPointB;
	private Vector3 PatrolMarkerB;
	private bool PatrolFirstIteration = true; // True = Move towards A, False = Move towards B

	// Jump Variables
	public float JumpSpeed;
    private bool isGrounded;

    // Death Variables
    public float TimeToTransition;
    private float TimeElapsed;
    public bool IsImmortal;
	public int ScoreAmount;
	public GameObject ScoreUI;
    public string DeathSFX;

	// Hidden Variables
	private bool NotHidden = false;

	// Reset Variables
	private Vector3 OrgPos;
    private Vector3 OrgRotation;
	private Vector3 OrgSize;
	private ENEMYTYPES OrgType;
    private RigidbodyConstraints OrgConstraints;

	// Variables to grab
	private Rigidbody RigidRef;
	private Renderer RenderRef;
    private Collider ColliderRef;
    private SoundSystem SoundSystemRef;

	void Start() {
		PatrolMarkerA = transform.localPosition + PatrolPointA;
		PatrolMarkerB = transform.localPosition + PatrolPointB;

		OrgPos = transform.localPosition;
        OrgRotation = transform.localEulerAngles;
		OrgSize = transform.localScale;
		OrgType = CurrType;

		RigidRef = GetComponent<Rigidbody>();
		RenderRef = GetComponent<Renderer>();
        ColliderRef = GetComponent<Collider>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        OrgConstraints = RigidRef.constraints;
    }

	void Update() {
        MoveDir = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, transform.lossyScale.y * 0.6f))
        {
            if (hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible)
                isGrounded = true;
            else
                isGrounded = false;
        }
        else
            isGrounded = false;

        // Horizontal Movement
        if (CurrType.ToString().Contains("WALK"))
		{
			MoveDir = transform.forward;
		}
		else
		{
            // Moving to Point A
			if (PatrolFirstIteration)
			{
                if (Vector3.Distance(transform.localPosition, PatrolMarkerA) < transform.localScale.x * 0.5f)
                    PatrolFirstIteration = false;
                else
                    MoveDir = (PatrolMarkerA - transform.localPosition).normalized;
			}
            // Moving to Point B
			else
			{
                if (Vector3.Distance(transform.localPosition, PatrolMarkerB) < transform.localScale.x * 0.5f)
                    PatrolFirstIteration = true;
                else
                    MoveDir = (PatrolMarkerB - transform.localPosition).normalized;
            }
		}

        // Vertical Movement
        if(CurrType.ToString().Contains("JUMP"))
        {
            if(CurrType.ToString().Contains("HIDDEN"))
            {
                if (isGrounded && !NotHidden)
                {
                    RigidRef.velocity = Vector3.zero;
                    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                }
            }
            else
            {
                if (isGrounded)
                {
                    RigidRef.velocity = Vector3.zero;
                    RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                }
            }
        }

        // Death
        if(CurrType.ToString().Contains("DEAD"))
        {
            if(TimeToTransition > TimeElapsed)
                TimeElapsed += Time.deltaTime;
            else
            {
                if(IsImmortal)
                {
                    CurrType = OrgType;
                    Vector3 NewPos = transform.localPosition;
                    NewPos.y += transform.localScale.y * 0.5f;
                    transform.localPosition = NewPos;
                    transform.localScale = OrgSize;
                    RigidRef.constraints = OrgConstraints;
                }
                else
                {
                    RenderRef.enabled = false;
                    transform.localScale = OrgSize;
                }
            }
        }
	}

    private void FixedUpdate()
    {
		RigidRef.MovePosition(RigidRef.position + MoveDir * MovementSpeed * Time.deltaTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if (CurrType == ENEMYTYPES.HIDDENWALKJUMP
				|| CurrType == ENEMYTYPES.HIDDENPATROLJUMP)
				NotHidden = true;
		}

		if (other.tag == "Killbox")
		{
			RenderRef.enabled = false;
			RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (CurrType == ENEMYTYPES.DEAD)
            return;

		GameObject CollidedRef = collision.gameObject;

		if (CollidedRef.tag == "Player" && CollidedRef.GetComponent<TPSLogic>().isMine())
		{
            if(!CollidedRef.GetComponent<TPSLogic>().GetGrounded()
                && CollidedRef.transform.localPosition.y - CollidedRef.transform.localScale.y * 0.5f > transform.localPosition.y + transform.localScale.y * 0.5f
                )
            {
                ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };

                if (isGrounded)
                {
                    object[] content02 = new object[]
                               {
                                   ID
                               };

                    Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_GROUND, content02, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                    GroundDeath();
                }
                else
                {
                    object[] content02 = new object[]
                               {
                                   ID
                               };

                    Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_AIR, content02, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                    AirDeath();
                }

                CollidedRef.GetComponent<TPSLogic>().PushUp();
            }
            else
            {
                CollidedRef.GetComponent<TPSLogic>().Death();
            }
		}
        else
        {
            // Change direction when hitting a block
            if (CurrType.ToString().Contains("WALK")
                && transform.localPosition.y + transform.localScale.y * 0.5f >= CollidedRef.transform.position.y
                && transform.localPosition.y - transform.localScale.y * 0.5f <= CollidedRef.transform.position.y)
            {
                transform.forward *= -1.0f;
            }
        }
	}

    public void GroundDeath()
    {
        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
        Vector3 VectorProperty = transform.localPosition;
        VectorProperty.y -= transform.localScale.y * 0.5f;
        transform.localPosition = VectorProperty;

        VectorProperty = transform.localScale;
        VectorProperty.y *= 0.5f;
        transform.localScale = VectorProperty;

        CurrType = ENEMYTYPES.DEAD;
        ColliderRef.isTrigger = true;

        // Player Feedback, UI & Sound & Character Reaction
        GameObject UIScore = Instantiate(ScoreUI, transform.position, transform.rotation, transform);
        UIScore.GetComponent<TextMesh>().text = ScoreAmount.ToString();
        if (DeathSFX != "")
            SoundSystemRef.PlaySFX(DeathSFX);
    }

    public void AirDeath()
    {
        RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        RigidRef.velocity = Vector3.zero;
        RigidRef.AddForce(transform.up * JumpSpeed * 0.5f, ForceMode.VelocityChange);

        Vector3 NewProperty = transform.localEulerAngles;
        NewProperty.z += 180;
        transform.localEulerAngles = NewProperty;

        CurrType = ENEMYTYPES.DEAD;
        ColliderRef.isTrigger = true;

        // Player Feedback, UI & Sound & Character Reaction
        GameObject UIScore = Instantiate(ScoreUI, transform.position, transform.rotation, transform);
        UIScore.GetComponent<TextMesh>().text = ScoreAmount.ToString();
        if (DeathSFX != "")
            SoundSystemRef.PlaySFX(DeathSFX);
    }

	public void Reset()
	{
        MoveDir = Vector3.zero;

		transform.localPosition = OrgPos;
        transform.localEulerAngles = OrgRotation;
		transform.localScale = OrgSize;
		CurrType = OrgType;
        RigidRef.constraints = OrgConstraints;

        TimeElapsed = 0;

        RigidRef.velocity = Vector3.zero;
        RenderRef.enabled = true;
        ColliderRef.isTrigger = false;
	}
}
