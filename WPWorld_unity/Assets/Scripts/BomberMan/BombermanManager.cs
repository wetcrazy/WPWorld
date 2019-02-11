﻿using System.Collections;
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
    private List<BombermanPlayingField> List_CurrPlayerPlayingField;
    private GameObject[] Array_PlayerPlayingField;

    [Header("PlayerPlaySpace")]
    // For player play space
    public List<GameObject> List_PlayerPlaySpace;

    [Header("HighScore")]
    // For Highscore
    public static int PointsForKilling = 100;

    [Header("Player stats UI")]
    // public Text PlayerHighScoreText;
    public Text PlayerTotalBombCount;
    public Text PlayerTotalFirePower;

    [Header("Debugging Text")]
    // For Debugging
    public Text Debug01;
    public Text Debug02;

    // All object rotation
    private Quaternion NewRotation;
    // If game needs reset
    private bool is_Reset;

    public enum BREAKABLE_TYPE
    {
        BREAKABLE_ONE,
        BREAKABLE_TWO,
    };

    // START
    private void Start()
    {
        Array_PlayerPlayingField = GameObject.FindGameObjectsWithTag("BombermanPlayingField");
        is_Reset = true;
    }

    // UPDATE
    private void Update()
    {
        UpdatePlayerStats();
        NewRotation = ARMultiplayerController._GroundObject.transform.rotation;
        if (is_Reset)
        {

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
    public void ConstantBreakableSpawner()
    {
        foreach (BombermanPlayingField currField in List_CurrPlayerPlayingField)
        {
            var Arr_Floor = currField.FloorParent.GetComponentInChildren<Transform>();
         
            foreach (Transform floor in Arr_Floor)
            {
                if (floor.transform.gameObject.tag != "BombermanFloor") // Check is it pointing to the correct floor
                {
                    continue;
                }

                currField.List_Floors.Add(floor.gameObject);
            }

            var RAND = Random.Range(0, currField.List_Floors.Count);


        }
    }
   
    // Reset Funtion
    public void ResetGame()
    {   
        if (List_CurrPlayerPlayingField.Count <= 0)
        {
            FindPlayers();
        }
    }

    // Find players playing field
    public void FindPlayers()
    {
        // Temp player counting
        int playerCount = 0;
        // Check each field
        foreach (GameObject field in Array_PlayerPlayingField)
        {
            // Dont check anymore if we found all the players
            if(playerCount == GameObject.FindGameObjectsWithTag("Player").Length)
            {
                break;
            }

            var Arr_Floor = field.GetComponent<BombermanPlayingField>().FloorParent.GetComponentInChildren<Transform>();
            RaycastHit hit;

            // Each floor block
            foreach (Transform floor in Arr_Floor)
            {
                if(floor.transform.gameObject.tag != "BombermanFloor") // Check is it pointing to the correct floor
                {
                    continue;
                }

                if(Physics.Raycast(floor.localPosition, Vector3.up, out hit, floor.localScale.x))
                {
                    // Player is found
                    if(hit.transform.tag == "Player")
                    {
                        field.GetComponent<BombermanPlayingField>().Player = hit.transform.gameObject;
                        List_CurrPlayerPlayingField.Add(field.GetComponent<BombermanPlayingField>()); // Push into current field that is being played by the players
                        playerCount += 1;
                        break;
                    }
                }
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
        }
        else
        {
            PlayerTotalFirePower.text = ": " + GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().GetBombPower().ToString();
            PlayerTotalBombCount.text = ": " + GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().GetMaxBombCount().ToString();
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
        newBomb.transform.Translate(BombPos, Space.Self);
        newBomb.transform.localPosition = Vector3.zero;
        newBomb.transform.LookAt(ARMultiplayerController.LevelForwardAnchor.transform);
        newBomb.transform.localPosition = BombPos;

        newBomb.transform.localPosition = Vector3.zero;
        newBomb.transform.LookAt(ARMultiplayerController.LevelForwardAnchor.transform);
        newBomb.transform.localPosition = BombPos;
       

        newBomb.GetComponent<Bomb>().SetBombPower(firepower);
        newBomb.GetComponent<Bomb>().SetBombOwnerPUN(PhotonNetwork.CurrentRoom.GetPlayer(OwnerActorID));

        Debug01.text = newBomb.transform.forward.ToString();
        Debug02.text = ARMultiplayerController._GroundObject.transform.forward.ToString();
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

        Debug01.text = newBomb.transform.forward.ToString();
        Debug02.text = ARMultiplayerController._GroundObject.transform.forward.ToString();
    }

    // Spawn Power Up 
    public void SpawnPowerUp(Vector3 PowerPos, int randNum)
    {
        //Debug01.text = "Spawning Power";
        var newPower = Instantiate(List_PowerUpBlocks[randNum], PowerPos, NewRotation, ARMultiplayerController._GroundObject.transform);

        newPower.transform.forward = ARMultiplayerController._GroundObject.transform.forward;
        newPower.transform.Translate(PowerPos, Space.Self);
        newPower.transform.localPosition = Vector3.zero;
        newPower.transform.LookAt(ARMultiplayerController.LevelForwardAnchor.transform);
        newPower.transform.localPosition = PowerPos;
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

    // Spawn Breakable 
    public void SpawnBreakable(Vector3 BreakablePos, BREAKABLE_TYPE typeValue)
    {

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
            case EventCodes.EVENT_CODES.BOMBER_EVENT_SPAWN_BREAKABLE:
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
