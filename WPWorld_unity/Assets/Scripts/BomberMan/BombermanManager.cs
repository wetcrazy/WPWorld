﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

using UnityEngine.UI;

public class BombermanManager : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField]
    GameObject BombPrefab;

   
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
        BomberManPlayer.LocalPlayerInstance.GetComponent<BomberManPlayer>().onBombButtonDown();
    }


    // Event Functions
    public void SpawnBomb(Vector3 BombPos, int firepower, int OwnerActorID)
    {
        GameObject newBomb = Instantiate(BombPrefab, BombPos, Quaternion.identity, ARMultiplayerController._GroundObject.transform);
        newBomb.GetComponent<Bomb>().SetBombPower(firepower);

        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            if(player.ActorNumber == OwnerActorID)
            {
                newBomb.GetComponent<Bomb>().SetBombOwnerPUN(player);
            }
        }
    }

    public void PlayerDeath(int OwnerActorID,BomberManPlayer EnemyBombScript)
    {
        foreach (Player player in PhotonNetwork.PlayerListOthers)
        {
            if (player.ActorNumber == OwnerActorID)
            {
                
            }
        }
    }

    // EVENTS
    public void OnEvent(EventData photonEvent)
    {
        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.EVENT_DROP_BOMB: // Bomb
                {
                    object[] data = (object[])photonEvent.CustomData;

                    Vector3 BombPos = (Vector3)data[0];
                    int firepower = (int)data[1];

                    SpawnBomb(BombPos, firepower, photonEvent.Sender);
                    break;
                }
            case EventCodes.EVENT_CODES.EVENT_PLAYER_DEATH:
                {
                    object[] data = (object[])photonEvent.CustomData;

                    int OwnerActorID = (int)data[0];
                    BomberManPlayer EnemyBombScript = (BomberManPlayer)data[1];

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
