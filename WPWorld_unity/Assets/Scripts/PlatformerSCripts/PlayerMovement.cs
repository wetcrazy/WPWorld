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
    [SerializeField]
    private Vector3 MovementDir = Vector3.zero;

    [SerializeField]
    private float MovementMultiplier;

    [SerializeField]
    private MovementRestrict CurrRestriction;
    private Vector3 RespawnPoint;
    private Vector3 PermenantNorthDirection;
    private Rigidbody RigidRef;

    // Use this for initialization
    void Start () {
		RigidRef = GetComponent<Rigidbody>();

        RespawnPoint = transform.position;

        //JoysticControls = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        gameObject.transform.forward = Vector3.forward;
    }
	
	// Update is called once per frame
	void Update () {
        // Resets the acceleration of the gameobject to 0

        //MovementDir = Vector3.zero;
    
        //// Moves the player according to Key Input
        //if(CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.X_ONLY)
        //    MovementDir = Input.GetAxis("Vertical") * this.transform.forward; // Vertical = W, S, Up Arrow, Down Arrow
        //if (CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.Z_ONLY)
        //    MovementDir += Input.GetAxis("Horizontal") * this.transform.right; // Horizontal = A, D, Left Arrow, Right Arrow

        //MovementDir = Vector3.zero;

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            // Moves the player according to Key Input
            if (CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.X_ONLY)
                MovementDir = Input.GetAxis("Vertical") * this.transform.forward; // Vertical = W, S, Up Arrow, Down Arrow
            if (CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.Z_ONLY)
                MovementDir += Input.GetAxis("Horizontal") * this.transform.right; // Horizontal = A, D, Left Arrow, Right Arrow
        }

    }

    public void GetDPadInput(Vector3 MoveDirection)
    {
        MovementDir = MoveDirection;
    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        //Joystick input has stopped
        if (DragInfo.Equals(Vector4.zero))
        {
            MovementDir = Vector3.zero;
            return;
        }
        
        float DragAngle = DragInfo.w;
        
        //Rotate the player object based on the dragged angle and using world's forward vector as reference axis
        gameObject.transform.forward = Quaternion.AngleAxis(DragAngle, gameObject.transform.up) * Camera.main.transform.forward;
        Vector3 n_Dir = gameObject.transform.forward;

        switch (CurrRestriction)
        {
            case (MovementRestrict.NONE):
                gameObject.transform.forward = Vector3.zero;
                break;
            case (MovementRestrict.X_ONLY):
                n_Dir.z = 0;
                gameObject.transform.forward = n_Dir;
                break;
            case (MovementRestrict.Z_ONLY):
                n_Dir.x = 0;
                gameObject.transform.forward = n_Dir;
                break;
        }

        //Move towards the new direction the player is facing
        MovementDir = gameObject.transform.forward;
    }

    public Vector3 GetMovementDir()
    {
        return MovementDir;
    }

    void FixedUpdate()
    {
        // Actually moves the player according to the Movement Direction, Movement speed is attached here to prevent multiple movement speed from being multiplied in Update
        RigidRef.MovePosition(RigidRef.position + MovementDir * MovementSpeed * MovementMultiplier * Time.fixedDeltaTime);
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

    public void SetMovementMultiplier(float n_Multiplier)
    {
        MovementMultiplier = n_Multiplier;
    }

    public MovementRestrict GetRestriction()
    {
        return CurrRestriction;
    }

    public void SetRestriction(MovementRestrict n_Restrict)
    {
        CurrRestriction = n_Restrict;
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
