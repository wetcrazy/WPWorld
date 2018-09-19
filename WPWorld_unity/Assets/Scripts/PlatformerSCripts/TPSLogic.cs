using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour {

    [SerializeField]
    private float JumpSpeed;
    private bool IsGrounded = false;
    [SerializeField]
    private AudioClip JumpSFX;

    [SerializeField]
    private int Points = 0;

    [SerializeField]
    private int DeathCounter = 0;

    private Rigidbody RigidRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // Draws a raycast in three different directions to ensure that the player is grounded
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up.normalized * transform.lossyScale.y * 1.1f, Color.white);
        Debug.DrawRay(transform.position, (-transform.up + transform.right).normalized * transform.lossyScale.x * 0.75f, Color.white);
        Debug.DrawRay(transform.position, (-transform.up - transform.right).normalized * transform.lossyScale.x * 0.75f, Color.white);

        if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y) ||
            Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f) ||
            Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f))
        {
            // Checks if the raycasted hit only hits the ground and nothing else
            if (hit.transform.GetComponent<Renderer>().isVisible && hit.transform.position.y < transform.position.y)
            {
                IsGrounded = true;
                if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y))
                    Debug.Log("Straight down");
                if (Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f))
                    Debug.Log("Bottom Right");
                if (Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f))
                    Debug.Log("Bottom Left");
            }
        }
        else
        {
            IsGrounded = false;
        }

        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RigidRef.AddForce(transform.up * JumpSpeed, ForceMode.VelocityChange);
                if (JumpSFX != null)
                    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
                IsGrounded = false;
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

    public bool GetGrounded()
    {
        return IsGrounded;
    }
}
