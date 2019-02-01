using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class PhotonGameTest : MonoBehaviour, IOnEventCallback
{
    [SerializeField]
    GameObject PlayerObjectPrefab;
    [SerializeField]
    GameObject LevelObj;
    [SerializeField]
    Text DebugText;

    public static GameObject _GroundObject;
    PhotonView photonView;
    public static Vector3 SpawnPoint;
    GameObject[] LevelSpawnPoints;

    public static GameObject LevelForwardAnchor;

    Dictionary<int, GameObject> PlayerGoDict = new Dictionary<int, GameObject>();

    private void Awake()
    {
        _GroundObject = LevelObj;
    }

    // Use this for initialization
    void Start () {
        photonView = PhotonView.Get(this);

        LevelSpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        LevelForwardAnchor = GameObject.FindGameObjectWithTag("LevelForwardAnchor");

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
        DebugText.text = GameObject.FindGameObjectsWithTag("Player").Length.ToString();

    }

    //public void OnEnable()
    //{
    //    PhotonNetwork.AddCallbackTarget(this);
    //}

    //public void OnDisable()
    //{
    //    PhotonNetwork.RemoveCallbackTarget(this);
    //}

    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("on event");
        if (!PlayerGoDict.ContainsKey(photonEvent.Sender))
        {
            Debug.Log("key not found");
            GameObject[] PlayerGoList = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in PlayerGoList)
            {
                Debug.Log("Sender: " + photonEvent.Sender.ToString() + "\nComparing with " + player.GetPhotonView().OwnerActorNr.ToString());
                if (player.GetPhotonView().OwnerActorNr == photonEvent.Sender)
                {
                    PlayerGoDict.Add(photonEvent.Sender, player);

                    Debug.Log("Added to Dict" + photonEvent.Sender.ToString());
                    break;
                }
            }
        }

        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE:
                {
                    Vector3 PlayerLocalPos = (Vector3)photonEvent.CustomData;
                    PlayerGoDict[photonEvent.Sender].transform.localPosition = PlayerLocalPos;
                    Debug.Log("Received Pos");
                    break;
                }
            case EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE:
                {
                    Quaternion PlayerLocalRot = (Quaternion)photonEvent.CustomData;
                    PlayerGoDict[photonEvent.Sender].transform.localRotation = PlayerLocalRot;

                    Debug.Log("Received Rot");

                    break;
                }
            default:
                break;
        }
    }
}
