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

    [Header("Bomberman UI")]
    // For Bomb UI
    public GameObject AnchorUIObj;
    public GameObject SpawnBombButton;

    [Header("Breakables")]
    // For breakable spawning
    public List<GameObject> List_BreakablesBlocks;

    [Header("PlayerPlaySpace")]
    // For player play space
    public List<GameObject> List_PlayerPlaySpace;

    [Header("HighScore")]
    // For Highscore
    public static int PointsForKilling = 100;

    [Header("Debugging Text")]
    // For Debugging
    public Text Debug01;
    public Text Debug02;


    private Quaternion NewRotation;

    // UPDATE
    private void Update()
    {
        EnableBombUi();
        NewRotation = ARMultiplayerController._GroundObject.transform.rotation;
        // NewRotation = Quaternion.identity;
        // Debug02.text = GameObject.FindGameObjectsWithTag("Player").Length.ToString();
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
    public void ConstantBreakableSpawner()
    {

    }

    // Bomb UI
    public void EnableBombUi()
    {
        if(AnchorUIObj.activeSelf)
        {      
            SpawnBombButton.SetActive(false);         
        }
        else
        {         
            SpawnBombButton.SetActive(true);        
        }
    }

    //

    // =============
    //    EVENTS
    // =============

    // Spawn Bomb (Multiplayer)
    public void SpawnBomb(Vector3 BombPos, int firepower, int OwnerActorID)
    {
        GameObject newBomb = Instantiate(BombPrefab, Vector3.zero, Quaternion.identity, ARMultiplayerController._GroundObject.transform);

        newBomb.transform.forward = ARMultiplayerController._GroundObject.transform.forward;   
        newBomb.transform.Translate(BombPos, Space.Self);
        newBomb.GetComponent<Bomb>().SetBombPower(firepower);
        newBomb.GetComponent<Bomb>().SetBombOwnerPUN(PhotonNetwork.CurrentRoom.GetPlayer(OwnerActorID));

        Debug01.text = newBomb.transform.forward.ToString();
        Debug02.text = ARMultiplayerController._GroundObject.transform.forward.ToString();
    }

    // Spawn Bomb (Singleplayer)
    public void SpawnBomb(Vector3 BombPos, int firepower, GameObject player)
    {
        GameObject newBomb = Instantiate(BombPrefab, BombPos, NewRotation, ARMultiplayerController._GroundObject.transform);
        newBomb.transform.localPosition = Vector3.zero;
        newBomb.transform.forward = ARMultiplayerController._GroundObject.transform.forward;
        newBomb.transform.localPosition = player.transform.localPosition;

        newBomb.GetComponent<Bomb>().SetBombPower(firepower);
        newBomb.GetComponent<Bomb>().SetBombOwner(player);
        Debug01.text = newBomb.transform.forward.ToString();
        Debug02.text = ARMultiplayerController._GroundObject.transform.forward.ToString();
    }

    // Spawn Power Up 
    public void SpawnPowerUp(Vector3 PowerPos, int randNum)
    {
        //Debug01.text = "Spawning Power";
        var newPower = Instantiate(List_PowerUpBlocks[randNum], PowerPos, NewRotation, ARMultiplayerController._GroundObject.transform);
        //Debug01.text = "Spawned Power";
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
