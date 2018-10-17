using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementAvaliability // For use with scripted events like disabling player movement and forcing the character to move
{
    NONE, // ALL RESTRICTIONS, ABOSLUTELY NO MOVEMENT
    X_ONLY, // MOVES ONLY ON X PLANE
    Z_ONLY, // MOVES ONLY ON Z PLANE
    BOTH // BOTH ARE ALLOWED, DOESN"T RESTRICT PLAYER
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
    private MovementAvaliability CurrAvaliability;
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
    void Update()
    {
        // Resets the acceleration of the gameobject to 0

        //MovementDir = Vector3.zero;

        //// Moves the player according to Key Input
        //if(CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.X_ONLY)
        //    MovementDir = Input.GetAxis("Vertical") * this.transform.forward; // Vertical = W, S, Up Arrow, Down Arrow
        //if (CurrRestriction != MovementRestrict.BOTH && CurrRestriction != MovementRestrict.Z_ONLY)
        //    MovementDir += Input.GetAxis("Horizontal") * this.transform.right; // Horizontal = A, D, Left Arrow, Right Arrow

        //MovementDir = Vector3.zero;

        //if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        //{
        //    MovementDir = Vector3.zero;
        //    // Moves the player according to Key Input
        //    if (CurrRestriction != MovementRestrict.NONE && CurrRestriction != MovementRestrict.X_ONLY)
        //        MovementDir = Input.GetAxis("Vertical") * this.transform.forward; // Vertical = W, S, Up Arrow, Down Arrow
        //    if (CurrRestriction != MovementRestrict.NONE && CurrRestriction != MovementRestrict.Z_ONLY)
        //        MovementDir += Input.GetAxis("Horizontal") * this.transform.right; // Horizontal = A, D, Left Arrow, Right Arrow
        //}

        switch(CurrAvaliability)
        {
            case (MovementAvaliability.NONE):
                RigidRef.constraints = RigidbodyConstraints.None;
                break;
            case (MovementAvaliability.X_ONLY):
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
                break;
            case (MovementAvaliability.Z_ONLY):
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
                break;
            case (MovementAvaliability.BOTH):
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation;
                break;
        }
    }

    public void GetDPadInput(Vector3 MoveDirection)
    {
        MovementDir = MoveDirection;
    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        //Joystick input has stopped
        if (DragInfo.Equals(Vector4.zero) || CurrAvaliability == MovementAvaliability.NONE)
        {
            MovementDir = Vector3.zero;
            return;
        }

        //float DragAngle = DragInfo.w;

        Vector3 n_Forward;

        // Rotates the player to the designated forward looking area
        switch ((Joystick.JoystickDirection)DragInfo.w)
        {
            case Joystick.JoystickDirection.DIR_FORWARD:
                {
                    n_Forward = Camera.main.transform.forward;
                    n_Forward.y = 0;
                    if(Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = n_Forward;
                    break;
                }
            case Joystick.JoystickDirection.DIR_RIGHT:
                {
                    n_Forward = Camera.main.transform.right;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = n_Forward;
                    break;
                }
            case Joystick.JoystickDirection.DIR_LEFT:
                {
                    n_Forward = Camera.main.transform.right;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = -n_Forward;
                    break;
                }
            case Joystick.JoystickDirection.DIR_BACK:
                {
                    n_Forward = Camera.main.transform.forward;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = -n_Forward;
                    break;
                }
            default:
                break;
        }

        //if(DragAngle < 90)
        //{
        //    gameObject.transform.forward = Vector3.forward;
        //}
        //else if(DragAngle < 180)
        //{
        //    gameObject.transform.forward = Vector3.right;
        //}
        //else if (DragAngle < 270)
        //{
        //    gameObject.transform.forward = -Vector3.forward;
        //}
        //else
        //{
        //    gameObject.transform.forward = -Vector3.right;
        //}

        //Rotate the player object based on the dragged angle and using world's forward vector as reference axis
        //gameObject.transform.forward = Quaternion.AngleAxis(DragAngle, gameObject.transform.up) * Camera.main.transform.forward;
        //Vector3 n_Dir = gameObject.transform.forward;
        //n_Dir.y = 0;

        //switch (CurrRestriction)
        //{
        //    case (MovementRestrict.X_ONLY):
        //        n_Dir.z = 0;
        //        break;
        //    case (MovementRestrict.Z_ONLY):
        //        n_Dir.x = 0;
        //        break;
        //}

        //gameObject.transform.forward = n_Dir;

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
        if(n_Multiplier > 1)
        {
            n_Multiplier = 1;
        }
        MovementMultiplier = n_Multiplier;
    }

    public MovementAvaliability GetRestriction()
    {
        return CurrAvaliability;
    }

    public void SetRestriction(MovementAvaliability n_Restrict)
    {
        CurrAvaliability = n_Restrict;
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
