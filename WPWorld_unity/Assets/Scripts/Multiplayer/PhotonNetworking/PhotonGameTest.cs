using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonGameTest : MonoBehaviour, IOnEventCallback
{
    [SerializeField]
    GameObject PlayerObjectPrefab;
    [SerializeField]
    GameObject LevelObj;

    PhotonView photonView;
    public static Vector3 SpawnPoint;
    GameObject[] LevelSpawnPoints;

    Dictionary<int, GameObject> PlayerGoDict = new Dictionary<int, GameObject>();

    // Use this for initialization
    void Start () {
        photonView = PhotonView.Get(this);

        LevelSpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        if (PhotonNetwork.IsMasterClient)
        {
            //Send each player a spawnpoint
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
            {
                photonView.RPC("ReceiveSpawnPoint", PhotonNetwork.PlayerList[i], LevelSpawnPoints[i].name);
            }
        }
       
    }

    [PunRPC]
    void ReceiveSpawnPoint(string SpawnPosName)
    {
        foreach (GameObject spawnpoint in LevelSpawnPoints)
        {
            if(spawnpoint.name == SpawnPosName)
            {
                SpawnPoint = spawnpoint.transform.localPosition;
                break;
            }
        }

        PhotonNetwork.Instantiate(PlayerObjectPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE:
                {
                    Vector3 PlayerLocalPos = (Vector3)photonEvent.CustomData;

                    if(!PlayerGoDict.ContainsKey(photonEvent.Sender))
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
                    
                    PlayerGoDict[photonEvent.Sender].transform.localPosition = PlayerLocalPos;

                    break;
                }
            default:
                break;
        }
    }
}
