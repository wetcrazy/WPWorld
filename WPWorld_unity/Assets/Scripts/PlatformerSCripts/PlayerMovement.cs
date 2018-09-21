using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementRestrict
{
    NONE,
    X_ONLY,
    Z_ONLY,
    BOTH
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;
    private Vector3 MovementDir = Vector3.zero;

    [SerializeField]
    private MovementRestrict CurrRestriction;

    private Vector3 RespawnPoint;

    private Rigidbody RigidRef;

    // Use this for initialization
    void Start () {
		RigidRef = GetComponent<Rigidbody>();

        RespawnPoint = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        // Resets the Movement Dir after each frame to prevent added force acceleration
        MovementDir = Vector3.zero;

        // Moves the player according to Key Input
        if(CurrRestriction != MovementRestrict.NONE && CurrRestriction != MovementRestrict.X_ONLY)
            MovementDir = Input.GetAxis("Vertical") * this.transform.forward; // Vertical = W, S, Up Arrow, Down Arrow
        if (CurrRestriction != MovementRestrict.NONE && CurrRestriction != MovementRestrict.Z_ONLY)
            MovementDir += Input.GetAxis("Horizontal") * this.transform.right; // Horizontal = A, D, Left Arrow, Right Arrow
    }

    public void GetDPadInput(Vector3 MoveDirection)
    {
        MovementDir = MoveDirection;
    }

    public Vector3 GetMovementDir()
    {
        return MovementDir;
    }

    void FixedUpdate()
    {
        // Actually moves the player according to the Movement Direction, Movement speed is attached here to prevent multiple movement speed from being multiplied in Update
        RigidRef.MovePosition(RigidRef.position + MovementDir * MovementSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Killbox")
        {
            Debug.Log("Reset!");
            Respawn();
        }
    }

    public void SetMovementSpeed(float n_MovementSpeed)
    {
        MovementSpeed = n_MovementSpeed;
    }

    public float GetMovementSpeed()
    {
        return MovementSpeed;
    }

    public void SetRespawn(Vector3 n_Respawn)
    {
        RespawnPoint = n_Respawn;
    }

    public void Respawn()
    {
        this.transform.position = RespawnPoint;
    }
}
