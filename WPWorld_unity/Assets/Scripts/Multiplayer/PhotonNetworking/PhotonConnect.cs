using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    //Scene Objects & Variables (Gameobjects, Canvas,etc)
    [Header("Scene Objects")]
    [SerializeField]
    Text LoadingText;
    [SerializeField]
    GameObject OfflineScreen;

    //Photon Variable Values
    [Header("Variables")]
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players")]
    [SerializeField]
    byte MaximumPlayersInRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        //Set OfflineScreen to be in active frist 
        OfflineScreen.SetActive(false);
    }

    private void Update()
    {
    }

    //Attempt to connect to photon servers
    public void ConnectToPhoton()
    {
        LoadingText.text = "Connecting to Photon servers...";
        //Debug.Log(LoadingText.text);
        PhotonNetwork.ConnectUsingSettings();
        LoadingText.text = "Finding Best Region...";
        PhotonNetwork.ConnectToBestCloudServer();
    }
    
    public void CreateGameRoom()
    {
        Debug.Log("Creating Room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaximumPlayersInRoom });
    }

    public void JoinGameRoom(string RoomName)
    {
        Debug.Log("Joining Room...");
        PhotonNetwork.JoinRoom(RoomName);
    }

    public void JoinRandomGameRoom()
    {
        Debug.Log("Joining Random Room...");
        PhotonNetwork.JoinRandomRoom();
    }

    #region MonoBehaviourPunCallbacks Callbacks
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection to servers established!");
    }

    public override void OnConnected()
    {
        LoadingText.gameObject.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        LoadingText.text = cause.ToString();
        OfflineScreen.SetActive(true);

        switch (cause)
        {
            case DisconnectCause.ExceptionOnConnect:
                Debug.Log("Photon: Connection Exception\nPlease Check Your Internet Connection");
                break;
            case DisconnectCause.ServerTimeout:
                Debug.Log("Photon: Server Timeout");
                break;
            case DisconnectCause.ClientTimeout:
                Debug.Log("Photon: Client Timeout");
                break;
            case DisconnectCause.InvalidAuthentication:
                Debug.Log("Photon: Invalid AppID");
                break;
            case DisconnectCause.MaxCcuReached:
                Debug.Log("Photon: Server Limit Reached");
                break;
            case DisconnectCause.InvalidRegion:
                Debug.Log("Photon: Invalid Region");
                break;
            case DisconnectCause.DisconnectByClientLogic:
                Debug.Log("Photon: Client Disconnected");
                break;
            default:
                Debug.Log("Photon: " + cause);
                break;
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room!\nError Code:" + returnCode.ToString() + "\n" + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room!\nError Code:" + returnCode.ToString() + "\n" + message);
    }
    #endregion
}
