﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

using UnityEngine.UI;

public class BombermanManager : MonoBehaviourPun, IOnEventCallback
{
    // For bomb Spawning
    [SerializeField]
    GameObject BombPrefab;

    // For Bomb UI
    public GameObject AnchorUIObj;

    public GameObject SpawnBombButton;
    
    // For Highscore
    public static int PointsForKilling = 100;

    // For Debugging
    public Text Debug01;
    public Text Debug02;

    // UPDATE
    private void Update()
    {
        EnableBombUi();
    }



    public void PlayerDead(GameObject _selectedOBJ, bool _boolValue)
    {
        _selectedOBJ.GetComponent<BomberManPlayer>().SetisDead(_boolValue);
    }

    public void DestoryMyBombCount(GameObject _selectedOBJ)
    {
        _selectedOBJ.GetComponent<BomberManPlayer>().OneBombDestory();
    }

    public void LocalPlayerCall_SpawnBomb()
    {
        Debug02.text = "Pressing";

        if(PhotonNetwork.IsConnected)
        {
            BomberManPlayer.LocalPlayerInstance.GetComponent<BomberManPlayer>().onBombButtonDown();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<BomberManPlayer>().onBombButtonDown();
        }
       
        Debug02.text = "Press Already";
    }

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


    // Event Functions

    // Spawn Bomb
    public void SpawnBomb(Vector3 BombPos, int firepower, int OwnerActorID)
    {
        Debug01.text = "I Was here 01";
        GameObject newBomb = Instantiate(BombPrefab, BombPos, Quaternion.identity, ARMultiplayerController._GroundObject.transform);
        newBomb.GetComponent<Bomb>().SetBombPower(firepower);

        Debug01.text = "I Was here 02";
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            if(player.ActorNumber == OwnerActorID)
            {
                Debug01.text = "I Was here 03";
                newBomb.GetComponent<Bomb>().SetBombOwnerPUN(player);
                break;
            }
        }

        Debug01.text = "I Was here 04";
    }

    public void SpawnBomb(Vector3 BombPos, int firepower, GameObject player)
    {
        Debug01.text = "I Was here 01";
        GameObject newBomb = Instantiate(BombPrefab, BombPos, Quaternion.identity, ARMultiplayerController._GroundObject.transform);
        newBomb.GetComponent<Bomb>().SetBombPower(firepower);

        Debug01.text = "I Was here 02";


        newBomb.GetComponent<Bomb>().SetBombOwner(player);


        Debug01.text = "I Was here 03";

    }

    // Player death
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

    // EVENTS
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
