using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviourPun, IPunObservable, IOnEventCallback
{
    [SerializeField]
    GameObject FireBallPrefab;
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

    //private List<CollectOnCollide> ListOfCoins = new List<CollectOnCollide>();
    //private List<DestroyOnHit> ListOfBreakables = new List<DestroyOnHit>();
    //private List<ShowOnHit> ListOfHidden = new List<ShowOnHit>();
    //private List<SpawnOnHit> ListOfSpawners = new List<SpawnOnHit>();
    //private List<Enemy> ListOfEnemies = new List<Enemy>();
    //private List<FallOnTop> ListOfFalling = new List<FallOnTop>();
    //private List<MoveOnCollide> ListOfMoving = new List<MoveOnCollide>();
    //private List<BounceOnHit> ListOfBouncingBlocks = new List<BounceOnHit>();
    //private List<ButtonScript> ListofButtons = new List<ButtonScript>();
    //private List<LeverScript> ListofLevers = new List<LeverScript>();
    
    Dictionary<int, CollectOnCollide> ListOfCoins = new Dictionary<int, CollectOnCollide>();
    Dictionary<int, DestroyOnHit> ListOfBreakables = new Dictionary<int, DestroyOnHit>();
    Dictionary<int, ShowOnHit> ListOfHidden = new Dictionary<int, ShowOnHit>();
    Dictionary<int, SpawnOnHit> ListOfSpawners = new Dictionary<int, SpawnOnHit>();
    Dictionary<int, Enemy> ListOfEnemies = new Dictionary<int, Enemy>();
    Dictionary<int, FallOnTop> ListOfFalling = new Dictionary<int, FallOnTop>();
    Dictionary<int, MoveOnCollide> ListOfMoving = new Dictionary<int, MoveOnCollide>();
    Dictionary<int, BounceOnHit> ListOfBouncingBlocks = new Dictionary<int, BounceOnHit>();
    Dictionary<int, ButtonScript> ListofButtons = new Dictionary<int, ButtonScript>();
    Dictionary<int, LeverScript> ListofLevers = new Dictionary<int, LeverScript>();
    Dictionary<int, GivePowerUpOnCollide> ListOfPowerups = new Dictionary<int, GivePowerUpOnCollide>();

    Dictionary<int, GameObject> PlayerGoDict = new Dictionary<int, GameObject>();

    // Use this for initialization
    void Start()
    {
        if(!photonView.IsMine)
        {
            return;
        }

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

        InitObjectLists();
    }

    void InitObjectLists()
    {
        //Coins List
        var CoinsArray = FindObjectsOfType(typeof(CollectOnCollide)) as CollectOnCollide[];
        foreach (var item in CoinsArray)
        {
            ListOfCoins.Add(item.ID, item);
        }

        //Breakable Blocks List
        var BreakablesArray = FindObjectsOfType(typeof(DestroyOnHit)) as DestroyOnHit[];
        foreach (var item in BreakablesArray)
        {
            ListOfBreakables.Add(item.ID, item);
        }

        //Hidden Blocks List
        var HiddenArray = FindObjectsOfType(typeof(ShowOnHit)) as ShowOnHit[];
        foreach (var item in HiddenArray)
        {
            ListOfHidden.Add(item.ID, item);
        }

        //Spawner Blocks List
        var SpawnerArray = FindObjectsOfType(typeof(SpawnOnHit)) as SpawnOnHit[];
        foreach (var item in SpawnerArray)
        {
            ListOfSpawners.Add(item.ID, item);
        }

        //Enemy List
        var EnemyArray = FindObjectsOfType(typeof(Enemy)) as Enemy[];
        foreach (var item in EnemyArray)
        {
            ListOfEnemies.Add(item.ID, item);
        }

        //Falling Blocks List
        var FallingArray = FindObjectsOfType(typeof(FallOnTop)) as FallOnTop[];
        foreach (var item in FallingArray)
        {
            ListOfFalling.Add(item.ID, item);
        }

        //Moving Blocks List
        var MovingArray = FindObjectsOfType(typeof(MoveOnCollide)) as MoveOnCollide[];
        foreach (var item in MovingArray)
        {
            ListOfMoving.Add(item.ID, item);
        }

        //Bouncing Blocks List
        var BouncingArray = FindObjectsOfType(typeof(BounceOnHit)) as BounceOnHit[];
        foreach (var item in BouncingArray)
        {
            ListOfBouncingBlocks.Add(item.ID, item);
        }

        //Levers List
        var LeverArray = FindObjectsOfType(typeof(LeverScript)) as LeverScript[];
        foreach (var item in LeverArray)
        {
            ListofLevers.Add(item.ID, item);
        }

        //Buttons List
        var ButtonArray = FindObjectsOfType(typeof(ButtonScript)) as ButtonScript[];
        foreach (var item in ButtonArray)
        {
            ListofButtons.Add(item.ID, item);
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
    SendOptions sendOptions = new SendOptions { Reliability = true };
    public void Death()
    {
        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORMER_EVENT_PLAYER_DEATH, null, RaiseEventOptions.Default, sendOptions);

        DeathCounter++;
        if (DeathSFX != "")
            SoundSystemRef.PlaySFX(DeathSFX);

        GetComponent<PlayerMovement>().Respawn();
        RigidRef.velocity = Vector3.zero;
        IsGrounded = true;

        HealthPopup DeathUI = FindObjectOfType<HealthPopup>() as HealthPopup;
        if (DeathUI != null)
            DeathUI.ShowDisplay();

        foreach(CollectOnCollide CoinRef in ListOfCoins.Values)
        {
            if(!CoinRef.HasCollected)
                CoinRef.Reset();
        }

        foreach(DestroyOnHit BrickRef in ListOfBreakables.Values)
            BrickRef.Reset();

        foreach(ShowOnHit TrollRef in ListOfHidden.Values)
            TrollRef.Reset();

        foreach (SpawnOnHit SpawnRef in ListOfSpawners.Values)
            SpawnRef.Reset();

        foreach (Enemy EnemyRef in ListOfEnemies.Values)
            EnemyRef.Reset();

        foreach (FallOnTop FallBlock in ListOfFalling.Values)
            FallBlock.Reset();

        //foreach (MoveOnCollide MoveBlock in ListOfMoving.Values)
        //{
        //    if (!MoveBlock.CannotReset)
        //        MoveBlock.Reset();
        //    else
        //        MoveBlock.Reset();
        //}

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

    public void AddPowerUp(GameObject n_Powerup)
    {
        ListOfPowerups.Add(ListOfPowerups.Count + 1, n_Powerup.GetComponent<GivePowerUpOnCollide>());
    }

    public void AddEnemy(GameObject n_Enemy)
    {
        ListOfEnemies.Add(ListOfEnemies.Count + 1, n_Enemy.GetComponent<Enemy>());
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
            case EventCodes.EVENT_CODES.PLATFORMER_EVENT_PLAYER_DEATH:
                {
                    if (!PlayerGoDict.ContainsKey(photonEvent.Sender))
                    {
                        GameObject[] PlayerGoList = GameObject.FindGameObjectsWithTag("Player");

                        foreach (GameObject player in PlayerGoList)
                        {
                            if (player.GetPhotonView().OwnerActorNr == photonEvent.Sender)
                            {
                                PlayerGoDict.Add(photonEvent.Sender, player);
                                break;
                            }
                        }
                    }

                    PlayerGoDict[photonEvent.Sender].GetComponent<TPSLogic>().Death();
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_BOUNCE:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    ListOfBouncingBlocks[BlockID].Bounce();

                    //foreach (var block in ListOfBouncingBlocks)
                    //{
                    //    if (block.ID == BlockID)
                    //    {
                    //        block.Bounce();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_BREAK:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    ListOfBreakables[BlockID].Destroy();
                    //foreach (var block in ListOfBreakables)
                    //{
                    //    if (block.ID == BlockID)
                    //    {
                    //        block.Destroy();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_FALL:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    ListOfFalling[BlockID].Fall();
                    //foreach (var block in ListOfFalling)
                    //{
                    //    if(block.ID == BlockID)
                    //    {
                    //        block.Fall();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_SPAWNER:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    ListOfSpawners[BlockID].Spawn();
                    //foreach (var block in ListOfSpawners)
                    //{
                    //    if (block.ID == BlockID)
                    //    {
                    //        block.Spawn();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_HIDDEN:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int BlockID = (int)data[0];

                    ListOfHidden[BlockID].Show();
                    //foreach (var block in ListOfHidden)
                    //{
                    //    if (block.ID == BlockID)
                    //    {
                    //        block.Show();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_AIR:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int EnemyID = (int)data[0];

                    ListOfEnemies[EnemyID].AirDeath();
                    //foreach (var enemy in ListOfEnemies)
                    //{
                    //    if (enemy.ID == EnemyID)
                    //    {
                    //        enemy.AirDeath();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_GROUND:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int EnemyID = (int)data[0];

                    ListOfEnemies[EnemyID].GroundDeath();
                    //foreach (var enemy in ListOfEnemies)
                    //{
                    //    if (enemy.ID == EnemyID)
                    //    {
                    //        enemy.GroundDeath();
                    //        break;
                    //    }
                    //}
                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_BUTTON_TRIGGERED:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int ButtonID = (int)data[0];
                    bool isButtonOn = (bool)data[1];

                    if (isButtonOn)
                    {
                        ListofButtons[ButtonID].OnButton();
                    }
                    else
                    {
                        ListofButtons[ButtonID].OffButton();
                    }
                    //foreach (var button in ListofButtons)
                    //{
                    //    if (button.ID == ButtonID)
                    //    {
                    //        if (isButtonOn)
                    //        {
                    //            button.OnButton();
                    //        }
                    //        else
                    //        {
                    //            button.OffButton();
                    //        }
                    //        break;
                    //    }
                    //}

                    break;
                }
            case EventCodes.EVENT_CODES.PLATOFRM_EVENT_LEVER_TRIGGERED:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int LeverID = (int)data[0];
                    bool isLeverOn = (bool)data[1];

                    if (isLeverOn)
                    {
                        ListofLevers[LeverID].OnLever();
                    }
                    else
                    {
                        ListofLevers[LeverID].OnLever();
                    }

                    //foreach (var lever in ListofLevers)
                    //{
                    //    if (lever.ID == LeverID)
                    //    {
                    //        if (isLeverOn)
                    //        {
                    //            lever.OnLever();
                    //        }
                    //        else
                    //        {
                    //            lever.OffLever();
                    //        }
                    //        break;
                    //    }
                    //}

                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORMER_EVENT_PLAYER_FIREBALL:
                {
                    object[] data = (object[])photonEvent.CustomData;

                    GameObject Fireball = Instantiate(FireBallPrefab, Vector3.zero, Quaternion.identity, transform.parent);
                    Fireball.transform.localPosition = (Vector3)data[0];
                    Fireball.transform.localRotation = (Quaternion)data[1];

                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_COIN_PICKUP:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    int CoinID = (int)data[0];

                    ListOfCoins[CoinID].Collect();

                    break;
                }
            case EventCodes.EVENT_CODES.PLATFORM_EVENT_POWERUP_PICKUP:
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
