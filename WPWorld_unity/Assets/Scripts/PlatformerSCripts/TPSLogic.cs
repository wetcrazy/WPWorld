using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviourPun, IPunObservable, IOnEventCallback
{
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private bool IsGrounded = false;
    private bool AbleToJump = true;
    public bool AbleToJumpPub
    {
        get
        {
            return AbleToJump;
        }
        set
        {
            AbleToJump = value;
        }
    }
    private bool Colliding = false;
    [SerializeField]
    private string JumpSFX;

    [SerializeField]
    private string DeathSFX;

    [SerializeField]
    private int CurrPoints = 0;
    public int CurrPointsPub
    {
        get
        {
            return CurrPoints;
        }

        set
        {
            CurrPoints = value;
        }
    }

    [SerializeField]
    private int DeathCounter = 0;
    public int DeathCounterPub
    {
        get
        {
            return DeathCounter;
        }

        set
        {
            DeathCounter = value;
        }
    }

    private Rigidbody RigidRef;
    private PlayerMovement MovementRef;
    private SoundSystem SoundSystemRef;
    public LeverScript LeverRef;

    private List<CollectOnCollide> ListOfCoins = new List<CollectOnCollide>();
    private List<DestroyOnHit> ListOfBreakables = new List<DestroyOnHit>();
    private List<ShowOnHit> ListOfHidden = new List<ShowOnHit>();
    private List<SpawnOnHit> ListOfSpawners = new List<SpawnOnHit>();
    private List<Enemy> ListOfEnemies = new List<Enemy>();
    private List<FallOnTop> ListOfFalling = new List<FallOnTop>();
    private List<MoveOnCollide> ListOfMoving = new List<MoveOnCollide>();
    private List<BounceOnHit> ListOfBouncingBlocks = new List<BounceOnHit>();

    //The local player instance
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            LocalPlayerInstance.transform.parent = ARMultiplayerController._GroundObject.transform;
            LocalPlayerInstance.transform.localPosition = ARMultiplayerController.SpawnPoint;
        }
    }

    // Use this for initialization
    void Start()
    {
        RigidRef = GetComponent<Rigidbody>();
        MovementRef = GetComponent<PlayerMovement>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        Physics.gravity = new Vector3(0, -5f * transform.parent.parent.lossyScale.y, 0);

        if(transform.parent.parent.eulerAngles.y == 90 || transform.parent.parent.eulerAngles.y == 270)
        {
            MovementRef.SetRestriction(MovementAvaliability.Z_ONLY);
        }
        else
        {
            MovementRef.SetRestriction(MovementAvaliability.X_ONLY);
        }

        ListOfCoins.AddRange(FindObjectsOfType(typeof(CollectOnCollide)) as CollectOnCollide[]);
        ListOfBreakables.AddRange(FindObjectsOfType(typeof(DestroyOnHit)) as DestroyOnHit[]);
        ListOfHidden.AddRange(FindObjectsOfType(typeof(ShowOnHit)) as ShowOnHit[]);
        ListOfSpawners.AddRange(FindObjectsOfType(typeof(SpawnOnHit)) as SpawnOnHit[]);
        ListOfEnemies.AddRange(FindObjectsOfType(typeof(Enemy)) as Enemy[]);
        ListOfFalling.AddRange(FindObjectsOfType(typeof(FallOnTop)) as FallOnTop[]);
        ListOfMoving.AddRange(FindObjectsOfType(typeof(MoveOnCollide)) as MoveOnCollide[]);
        ListOfBouncingBlocks.AddRange(FindObjectsOfType(typeof(BounceOnHit)) as BounceOnHit[]);

        //Setting the username text that is above the player objects
        if (photonView.IsMine)
        {
            LocalPlayerInstance.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Don't update if the player obj doesn't belong to you or is disconnected
        if (!photonView.IsMine)
        {
            return;
        }

        RaycastHit hit;

        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // If none of the raycast is hitting the ground, automatically convert grounded to false
            if (!Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                && !Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                MovementRef.SetMovementSpeed(MovementRef.GetMovementSpeed() * 1.5f);
                IsGrounded = false;
            }
            else
            {
                // If one of the hit is currently hitting something, check if it is invisible or even has a renderer component
                if (!hit.transform.GetComponent<Renderer>() || !hit.transform.GetComponent<Renderer>().isVisible)
                {
                    if(!hit.transform.name.Contains("Invisible") && !hit.transform.name.Contains("Boundary"))
                    {
                        MovementRef.SetMovementSpeed(MovementRef.GetMovementSpeed() * 1.5f);
                        IsGrounded = false;
                    }
                }
            }
        }
        else
        {
            RaycastHit hit2, hit3;

            if (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f)
                || Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit, transform.lossyScale.y * 1.5f))
            {
                if (hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible)
                {
                    if (Physics.Raycast(transform.position, -transform.right.normalized, out hit, transform.lossyScale.x * 1.1f)
                        || Physics.Raycast(transform.position, transform.right.normalized, out hit, transform.lossyScale.x * 1.1f)
                        || Physics.Raycast(transform.position, -transform.forward.normalized, out hit, transform.lossyScale.z * 1.1f)
                        || Physics.Raycast(transform.position, transform.forward.normalized, out hit, transform.lossyScale.z * 1.1f))
                    {
                        if ((Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up - transform.right).normalized, out hit2, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up + transform.right).normalized, out hit3, transform.lossyScale.y * 1.5f))
                            || (Physics.Raycast(transform.position, -transform.up.normalized, out hit, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up - transform.forward).normalized, out hit2, transform.lossyScale.y * 1.5f) && Physics.Raycast(transform.position, (-transform.up + transform.forward).normalized, out hit3, transform.lossyScale.y * 1.5f)))
                        {
                            if ((hit.transform.GetComponent<Renderer>() && hit.transform.GetComponent<Renderer>().isVisible) &&
                                (hit2.transform.GetComponent<Renderer>() && hit2.transform.GetComponent<Renderer>().isVisible) &&
                                (hit3.transform.GetComponent<Renderer>() && hit3.transform.GetComponent<Renderer>().isVisible) &&
                                (hit.transform.name == hit2.transform.name && hit.transform.name == hit3.transform.name))
                            {
                                MovementRef.SetMovementSpeed(MovementRef.GetMovementSpeed() / 1.5f);
                                IsGrounded = true;
                            }
                        }
                    }
                    else
                    {
                        if(Colliding)
                        {
                            MovementRef.SetMovementSpeed(MovementRef.GetMovementSpeed() / 1.5f);
                            IsGrounded = true;
                        }
                    }
                }
                else
                {
                    if(hit.transform.name.Contains("Invisible"))
                    {
                        MovementRef.SetMovementSpeed(MovementRef.GetMovementSpeed() / 1.5f);
                        IsGrounded = true;
                    }
                }
            }
        }
    }

    public void Jump()
    {
        if (!IsGrounded || !AbleToJump)
            return;

        if (JumpSFX != "")
            SoundSystemRef.PlaySFX("Jump");
        PushUp();
    }

    public void PushUp()
    {
        RigidRef.velocity = Vector3.zero;
        RigidRef.AddForce(transform.up * JumpForce, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
        {
            Death();
        }

        if(other.name.Contains("Lever"))
        {
            LeverRef = other.GetComponent<LeverScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LeverRef = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Colliding = false;
    }

    public void Death()
    {
        DeathCounter++;
        if (DeathSFX != "")
            SoundSystemRef.PlaySFX(DeathSFX);

        GetComponent<PlayerMovement>().Respawn();
        RigidRef.velocity = Vector3.zero;
        IsGrounded = true;

        HealthPopup DeathUI = FindObjectOfType<HealthPopup>() as HealthPopup;
        if (DeathUI != null)
            DeathUI.ShowDisplay();

        foreach(CollectOnCollide CoinRef in ListOfCoins)
        {
            if(!CoinRef.HasCollected)
                CoinRef.Reset();
        }

        foreach(DestroyOnHit BrickRef in ListOfBreakables)
            BrickRef.Reset();

        foreach(ShowOnHit TrollRef in ListOfHidden)
            TrollRef.Reset();

        foreach (SpawnOnHit SpawnRef in ListOfSpawners)
            SpawnRef.Reset();

        foreach (Enemy EnemyRef in ListOfEnemies)
            EnemyRef.Reset();

        foreach (FallOnTop FallBlock in ListOfFalling)
            FallBlock.Reset();

        foreach (MoveOnCollide MoveBlock in ListOfMoving)
        {
            if (!MoveBlock.CannotReset)
                MoveBlock.Reset();
            else
                MoveBlock.Reset();
        }

        foreach(Enemy ClonedEnemy in FindObjectsOfType(typeof(Enemy)) as Enemy[])
        {
            if (ClonedEnemy.name.Contains("Clone"))
                Destroy(ClonedEnemy.gameObject);
        }

        foreach(GivePowerUpOnCollide PowerUpRef in FindObjectsOfType(typeof(GivePowerUpOnCollide)) as GivePowerUpOnCollide[])
            Destroy(PowerUpRef.gameObject);

        // IMPORTANT TO RESET
        GetComponent<PlayerPowerUp>().Reset();
    }

    private void GetJumpButtonInput()
    {
        Jump();
    }

    public bool GetGrounded()
    {
        return IsGrounded;
    }

    public float GetJumpForce()
    {
        return JumpForce;
    }

    public void SetJumpForce(float n_JumpForce)
    {
        JumpForce = n_JumpForce;
    }


    /// <summary>
    /// The network stream where data can be constantly be sent & received
    /// </summary>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    //Receiving events sent by other players
    public void OnEvent(EventData photonEvent)
    {
        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_BOUNCE:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    foreach (var block in ListOfBouncingBlocks)
                    {
                        if (block.ID == BlockID)
                        {
                            block.Bounce();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_BREAK:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    foreach (var block in ListOfBreakables)
                    {
                        if (block.ID == BlockID)
                        {
                            block.Destroy();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_FALL:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    foreach (var block in ListOfFalling)
                    {
                        if(block.ID == BlockID)
                        {
                            block.Fall();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_SPAWNER:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    foreach (var block in ListOfSpawners)
                    {
                        if (block.ID == BlockID)
                        {
                            block.Spawn();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_HIDDEN:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    foreach (var block in ListOfHidden)
                    {
                        if (block.ID == BlockID)
                        {
                            block.Show();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_AIR:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int EnemyID = (int)data[0];

                    foreach (var enemy in ListOfEnemies)
                    {
                        if (enemy.ID == EnemyID)
                        {
                            enemy.AirDeath();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_GROUND:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int EnemyID = (int)data[0];

                    foreach (var enemy in ListOfEnemies)
                    {
                        if (enemy.ID == EnemyID)
                        {
                            enemy.GroundDeath();
                            break;
                        }
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_COIN_PICKUP:
                break;
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_POWERUP_PICKUP:
                break;
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BUTTON_TRIGGERED:
                break;
            case EventCodes.EVENT_CODES.PLATOFRM_EVENT_LEVER_TRIGGERED:
                break;
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_CHECKPOINT_TRIGGERED:
                break;
            default:
                break;
        }
    }

    //Adds this script as one of the callback targets
    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    /// <summary>
    /// Returns true if the player object is belongs to the client
    /// </summary>
    public bool isMine()
    {
        return photonView.IsMine;
    }
}
