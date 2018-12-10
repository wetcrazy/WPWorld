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

	// Death Variables
	public bool IsImmortal;
	public int ScoreAmount;
	public GameObject ScoreUI;

	// Hidden Variables
	private bool IsConcealing;

	// Reset Variables
	private Vector3 OrgPos;
	private Vector3 OrgSize;
	private ENEMYTYPES OrgType;

	// Variables to grab
	private Rigidbody RigidRef;
	private Renderer RenderRef;

	void Start() {
		PatrolMarkerA = transform.localPosition + PatrolPointA;
		PatrolMarkerB = transform.localPosition + PatrolPointB;

		OrgPos = transform.localPosition;
		OrgSize = transform.localScale;
		OrgType = CurrType;

		RigidRef = GetComponent<Rigidbody>();
		RenderRef = GetComponent<Renderer>();
	}

	void Update() {
		if (CurrType == ENEMYTYPES.WALK
			|| CurrType == ENEMYTYPES.WALKJUMP
			|| CurrType == ENEMYTYPES.HIDDENWALKJUMP)
		{
			MoveDir = transform.forward;
		}
		else
		{
			if (PatrolFirstIteration)
			{
				if (Vector3.Distance(transform.localPosition, PatrolMarkerA) < transform.localScale.x)
					PatrolFirstIteration = false;
				else
					MoveDir = Vector3.Lerp(transform.localPosition, PatrolMarkerA, Time.deltaTime).normalized;
			}
			else
			{
				if (Vector3.Distance(transform.localPosition, PatrolMarkerB) < transform.localScale.x)
					PatrolFirstIteration = true;
				else
					MoveDir = Vector3.Lerp(transform.localPosition, PatrolMarkerB, Time.deltaTime).normalized;
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
				IsConcealing = true;
		}

		if (other.tag == "Killbox")
		{
			RenderRef.enabled = false;
			RigidRef.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject CollidedRef = collision.gameObject;

		if (CollidedRef.tag != "Player")
		{
			if (CurrType == ENEMYTYPES.WALK
				&& transform.localPosition.y + transform.localScale.y * 0.5f >= CollidedRef.transform.position.y
				&& transform.localPosition.y - transform.localScale.y * 0.5f <= CollidedRef.transform.position.y)
			{
				transform.forward *= -1.0f;
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		GameObject CollidedRef = collision.gameObject;

		if (CollidedRef.tag == "Player")
		{

		}
	}

	public void Reset()
	{
		transform.localPosition = OrgPos;
		transform.localScale = OrgSize;
		CurrType = OrgType;

		RenderRef.enabled = true;

	}
}
