using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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

public class PlayerMovement : MonoBehaviourPun, IPunObservable {

    [SerializeField]
    private float MovementSpeed;
    
    // UN-NEEDED FOR NOW
    // Joystick JoysticControls;

    [SerializeField]
    private Vector3 MovementDir = Vector3.zero;
    private float MovementMultiplier;

    [SerializeField]
    private MovementAvaliability CurrAvaliability;
    private Vector3 RespawnPoint;
    private Vector3 PermenantNorthDirection;
    private Rigidbody RigidRef;

    SendOptions sendOptions = new SendOptions { Reliability = true };
    
    // Player Local Instance
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected || ARMultiplayerController.isSinglePlayer)
        {
            LocalPlayerInstance = gameObject;
        }

        gameObject.transform.SetParent(ARMultiplayerController._GroundObject.transform, true);
    }

    // Use this for initialization
    void Start () {

        //Setting the username text that is above the player objects
        gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;

        if (!photonView.IsMine && PhotonNetwork.IsConnected && !ARMultiplayerController.isSinglePlayer)
        {
            return;
        }

        //Init the player pos to spawnpoint pos
        gameObject.transform.localPosition = ARMultiplayerController.SpawnPoint;
        //Init the player rot
        gameObject.transform.localRotation = Quaternion.identity;
        
        //Get the rigidbody component
        RigidRef = GetComponent<Rigidbody>();

        //Init the respawn point
        RespawnPoint = transform.position;

        if (PhotonNetwork.IsConnected || ARMultiplayerController.isSinglePlayer)
        {
            //Init your player transform on other clients
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE, gameObject.transform.localRotation, RaiseEventOptions.Default, sendOptions);
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE, gameObject.transform.localPosition, RaiseEventOptions.Default, sendOptions);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected && !ARMultiplayerController.isSinglePlayer)
        {
            return;
        }

        switch (CurrAvaliability)
        {
            case (MovementAvaliability.NONE):
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
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

        if (PhotonNetwork.IsConnected || ARMultiplayerController.isSinglePlayer)
        {
            //Update position on other client
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE, gameObject.transform.localPosition, RaiseEventOptions.Default, sendOptions);

            //Update your rotation on other clients
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE, gameObject.transform.localRotation, RaiseEventOptions.Default, sendOptions);
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
                    if ((-45 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 45)
                        || (135 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 225))
                        n_Forward = Camera.main.transform.forward;
                    else
                        n_Forward = Camera.main.transform.up;
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
                    if ((-45 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 45)
                        || (135 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 225))
                        n_Forward = Camera.main.transform.forward;
                    else
                        n_Forward = Camera.main.transform.up;
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

        //Move towards the new direction the player is facing
        MovementDir = gameObject.transform.forward;
    }

    public Vector3 GetMovementDir()
    {
        return MovementDir;
    }

    void FixedUpdate()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected && !ARMultiplayerController.isSinglePlayer)
        {
            return;
        }

        // Actually moves the player according to the Movement Direction, Movement speed is attached here to prevent multiple movement speed from being multiplied in Update
        RigidRef.MovePosition(RigidRef.position + MovementDir * MovementSpeed * MovementMultiplier * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Killbox")
        {
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

    public Vector3 GetRespawn()
    {
        return RespawnPoint;
    }

    public void Respawn()
    {
        this.transform.localPosition = RespawnPoint;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}