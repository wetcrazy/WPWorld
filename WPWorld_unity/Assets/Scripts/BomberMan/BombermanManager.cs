using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

using UnityEngine.UI;

public class BombermanManager : MonoBehaviourPun, IOnEventCallback
{
    [Header("Bomb Prefab")]
    // For bomb Spawning
    [SerializeField]
    GameObject BombPrefab;

    [Header("PowerUp Prefab")]
    // Power Ups
    public List<GameObject> List_PowerUpBlocks;

    [Header("Breakables")]
    // For breakable spawning
    public List<GameObject> List_BreakablesBlocks;
    private BombermanPlayingField CurrPlayerPlayingField;

    [Header("HighScore")]
    // For Highscore
    // public static int PointsForKilling = 100;
    public static int BreakableScore = 100;
    public static int Breakable2Score = 200;

    [Header("Player stats UI")]
    public Text PlayerHighScoreText;
    public Text PlayerTotalBombCount;
    public Text PlayerTotalFirePower;

    // All object rotation
    private Quaternion NewRotation;

    // Debugger
    private Text debug;

    // Gameover things
    private bool is_GameOver;

    // Spawner Cool Down
    private const float MAX_COOLDOWN = 3.0f;
    private float curr_Cooldown = 0.0f;

    public enum BREAKABLE_TYPE
    {
        BREAKABLE_ONE,
        BREAKABLE_TWO,
        BREAKABLE_COUNT,
    };


    SendOptions sendOptions = new SendOptions { Reliability = true };
    // START
    private void Start()
    {
        debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>();
        is_GameOver = false;       
    }

    // UPDATE
    private void Update()
    {
        UpdatePlayerStats();
        NewRotation = ARMultiplayerController._GroundObject.transform.rotation;
        if (CurrPlayerPlayingField == null)
        {
            FindMyPlayer();        
        }
        else if (CurrPlayerPlayingField.List_Breakables.Count < 100)
        {
            if(curr_Cooldown > MAX_COOLDOWN)
            {
                BreakableSpawn();
                curr_Cooldown = 0.0f;
            }
            else
            {
                curr_Cooldown += 1.0f * Time.deltaTime;
            }
        }
    }

    // When player Dies
    public void PlayerDead(GameObject _selectedOBJ, bool _boolValue)
    {
        _selectedOBJ.GetComponent<BomberManPlayer>().SetisDead(_boolValue);
    }

    // When button is press (local player)
    public void LocalPlayerCall_SpawnBomb()
    {  
        if(PhotonNetwork.IsConnected)
        {
            PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().onBombButtonDown();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().onBombButtonDown();
        }
    }

    // Breakable Spawner
    // Constant Spawner
    public void BreakableSpawn()
    {
        // For Curr field 
        if(CurrPlayerPlayingField.List_Floors.Count <=0)
        {
            var Arr_Floor = CurrPlayerPlayingField.FloorParent.GetComponentInChildren<Transform>();

            foreach (Transform floor in Arr_Floor)
            {
                if (floor.transform.gameObject.tag != "BombermanFloor") // Check is it pointing to the correct floor
                {
                    continue;
                }

                CurrPlayerPlayingField.List_Floors.Add(floor.gameObject);
            }

        }

        var RAND = Random.Range(0, CurrPlayerPlayingField.List_Floors.Count);
<<<<<<< HEAD
        var newPos = CurrPlayerPlayingField.List_Floors[RAND].transform.localPosition;
=======

        var newPos = CurrPlayerPlayingField.List_Floors[RAND].transform.localPosition;

>>>>>>> 3b932a29a1142f6fd45f13f88aebe4114a342d3c
        BREAKABLE_TYPE newtype;

        var RANDType = Random.Range(0, 1.0f);
        if (RANDType < 0.75f)
        {
            newtype = BREAKABLE_TYPE.BREAKABLE_ONE;
        }
        else
        {
            newtype = BREAKABLE_TYPE.BREAKABLE_TWO;
        }

        if (!ARMultiplayerController.isSinglePlayer)
        {
            object[] content = new object[]
            {
                newPos,
                newtype
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well

            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.BOMBER_EVENT_SPAWN_BREAKABLE, content, raiseEventOptions, sendOptions);
        }
        else
        {
            SpawnBreakable(newPos, newtype);
        }

    }
    
   
    // Reset Funtion
    public void ResetGame()
    {   
       
    }

    // Find own player playing field
    public void FindMyPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerMovement.LocalPlayerInstance.transform.position, -Vector3.up , out hit, 5))
        {          
            if (hit.transform.parent.parent.tag == "BombermanPlayingField")
            {
                CurrPlayerPlayingField = hit.transform.parent.parent.gameObject.GetComponent<BombermanPlayingField>();
            }
        }
    }

    // ============ UI =================

    // Update Player Stats Ui
    public void UpdatePlayerStats()
    {
        if (!ARMultiplayerController.isSinglePlayer)
        {
            PlayerTotalFirePower.text = ": " + PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().GetBombPower().ToString();
            PlayerTotalBombCount.text = ": " + PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().GetMaxBombCount().ToString();
            PlayerHighScoreText.text = ": " + PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().GetHighScore().ToString();
        }
        else
        {
            PlayerTotalFirePower.text = ": " + GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().GetBombPower().ToString();
            PlayerTotalBombCount.text = ": " + GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().GetMaxBombCount().ToString();
            PlayerHighScoreText.text = ": " + GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().GetHighScore().ToString();
        }
    }
    // =============
    //    EVENTS
    // =============

    // Spawn Bomb (Multiplayer)
    public void SpawnBomb(Vector3 BombPos, int firepower, int OwnerActorID)
    {
        GameObject newBomb = Instantiate(BombPrefab, Vector3.zero, Quaternion.identity, ARMultiplayerController._GroundObject.transform);

        newBomb.transform.forward = ARMultiplayerController._GroundObject.transform.forward;
        newBomb.transform.localEulerAngles = Vector3.zero;
        newBomb.transform.localPosition = BombPos;

        newBomb.transform.localPosition = Vector3.zero;
        newBomb.transform.LookAt(ARMultiplayerController.LevelForwardAnchor.transform);
        newBomb.transform.localPosition = BombPos;

        newBomb.GetComponent<Bomb>().SetBombPower(firepower);
        newBomb.GetComponent<Bomb>().SetBombOwnerPUN(PhotonNetwork.CurrentRoom.GetPlayer(OwnerActorID));
    }

    // Spawn Bomb (Singleplayer)
    public void SpawnBomb(Vector3 BombPos, int firepower, GameObject player)
    {
        GameObject newBomb = Instantiate(BombPrefab, Vector3.zero, Quaternion.identity, ARMultiplayerController._GroundObject.transform);

        //Set the rotation & position of the new bomb
        newBomb.transform.localPosition = Vector3.zero;
        newBomb.transform.LookAt(ARMultiplayerController.LevelForwardAnchor.transform);
        newBomb.transform.localPosition = player.transform.localPosition;

        // Set properties
        newBomb.GetComponent<Bomb>().SetBombPower(firepower);
        newBomb.GetComponent<Bomb>().SetBombOwner(player);
    }

    // Spawn Power Up 
    public void SpawnPowerUp(Vector3 PowerPos, int randNum)
    {
        var newPower = Instantiate(List_PowerUpBlocks[randNum], PowerPos, NewRotation, ARMultiplayerController._GroundObject.transform);
        newPower.transform.localEulerAngles = Vector3.zero;
        newPower.transform.localPosition = PowerPos;   
    }

    // Player death (Multiplayer)
    public void PlayerDeath(int OwnerActorID)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Who Die
        foreach (GameObject player in players)
        {
            if(player.GetPhotonView().OwnerActorNr == OwnerActorID)
            {
                player.GetComponent<BomberManPlayer>().SetisDead(true);
                break;  
            }
        }
    }

    // Spawn Breakable (Single and multiplayer)
    public void SpawnBreakable(Vector3 BreakablePos, BREAKABLE_TYPE typeValue)
    {
        GameObject newPreab;
        if(typeValue == BREAKABLE_TYPE.BREAKABLE_ONE)
        {
            newPreab = List_BreakablesBlocks[0].gameObject;     
        }
        else
        {
            newPreab = List_BreakablesBlocks[1].gameObject;
        }

        debug.text = BreakablePos.ToString();
        GameObject newBreakable = Instantiate(newPreab, Vector3.zero, Quaternion.identity, ARMultiplayerController._GroundObject.transform);

        BreakablePos.y += 10;
        newBreakable.transform.localEulerAngles = Vector3.zero;
        newBreakable.transform.localPosition = BreakablePos;
        BreakablePos.y -= 10;

        newBreakable.GetComponent<BombermanBreakable>().target = BreakablePos;
        CurrPlayerPlayingField.GetComponent<BombermanPlayingField>().List_Breakables.Add(newBreakable);
    }


    // ==============
    //  EVENT TRIGGER
    // ==============

    public void OnEvent(EventData photonEvent)
    {
        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.BOMBER_EVENT_DROP_BOMB: // Bomb
                {
                    object[] data = (object[])photonEvent.CustomData;

                    Vector3 BombPos = (Vector3)data[0];
                    int firepower = (int)data[1];

                    SpawnBomb(BombPos, firepower, photonEvent.Sender);
                    break;
                }
            case EventCodes.EVENT_CODES.BOMBER_EVENT_PLAYER_DEATH: // Some one dies and need some love and appreciate 
                {
                    PlayerDeath(photonEvent.Sender);

                    break;  
                }
            case EventCodes.EVENT_CODES.BOMBER_EVENT_SPAWN_POWERUP: // PowerUp
                {
                    object[] data = (object[])photonEvent.CustomData;

                    var SpawnPos = (Vector3)data[0];
                    var RandNum = (int)data[1];

                    SpawnPowerUp(SpawnPos, RandNum);

                    break;
                }
            case EventCodes.EVENT_CODES.BOMBER_EVENT_SPAWN_BREAKABLE: // Breakables
                {
                    object[] data = (object[])photonEvent.CustomData;

                    var SpawnPos = (Vector3)data[0];
                    var type = (BREAKABLE_TYPE)data[1];
                 
                    SpawnBreakable(SpawnPos, type);

                    break;
                }
            default:
                break;
        }
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    } 
}
