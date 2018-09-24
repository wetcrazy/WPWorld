using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementRestrict // For use with scripted events like disabling player movement and forcing the character to move
{
    NONE, // NO RESTRICTIONS, ABOSLUTELY FREE MOVEMENT
    X_ONLY, // MOVES ONLY ON X PLANE
    Z_ONLY, // MOVES ONLY ON Z PLANE
    BOTH // BOTH ARE NOT ALLOWED, FREEZE PLAYER
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;
    
    Joystick JoysticControls;
    private Vector3 MovementDir = Vector3.zero;

    [SerializeField]
    private MovementRestrict CurrRestriction;
    private Vector3 RespawnPoint;
    private Vector3 PermenantNorthDirection;
    private Rigidbody RigidRef;

    // Use this for initialization
    void Start () {
		RigidRef = GetComponent<Rigidbody>();

        RespawnPoint = transform.position;

        JoysticControls = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        gameObject.transform.forward = Vector3.forward;
    }
	
	// Update is called once per frame
	void Update () {
        // Resets the acceleration of the gameobject to 0
        MovementDir = Vector3.zero;
    
        // Moves the player according to Key Input
        if(CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.X_ONLY)
            MovementDir = Input.GetAxis("Vertical") * this.transform.forward; // Vertical = W, S, Up Arrow, Down Arrow
        if (CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.Z_ONLY)
            MovementDir += Input.GetAxis("Horizontal") * this.transform.right; // Horizontal = A, D, Left Arrow, Right Arrow
    }

    public void GetDPadInput(Vector3 MoveDirection)
    {
        MovementDir = MoveDirection;      
    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        if(DragInfo.Equals(Vector4.zero))
        {
            MovementDir = Vector3.zero;
            return;
        }

        float DragLength = new Vector3(DragInfo.x, DragInfo.y, DragInfo.z).magnitude;
        float DragAngle = DragInfo.w;
        if (DragLength > JoysticControls.JoystickBallDragLengthLimit)
        {
            DragLength = JoysticControls.JoystickBallDragLengthLimit;
        }

        gameObject.transform.forward = Quaternion.AngleAxis(DragAngle, gameObject.transform.up) * Vector3.forward;
        MovementDir = gameObject.transform.forward;
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
