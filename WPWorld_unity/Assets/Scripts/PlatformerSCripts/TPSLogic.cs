using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour {

    [SerializeField]
    private float JumpSpeed;
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
        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !RestrictJump && Vector3.Distance(RigidRef.velocity, Vector3.zero) < 0.1f)
            {
                RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                if (JumpSFX != null)
                    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
                IsGrounded = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (transform.position.y - transform.lossyScale.y * 0.5f >= CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y * 0.5f)
        {
            if (!IsGrounded && CollidedObject.GetComponent<Renderer>().isVisible)
            {
                Debug.Log(Mathf.Abs(transform.position.x - CollidedObject.transform.position.x) + " , " + transform.lossyScale.x);

                IsGrounded = true;
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
