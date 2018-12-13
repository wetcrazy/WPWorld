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
    [SerializeField]
    GameObject RoomScreen;
    [SerializeField]
    GameObject LobbyScreen;

    [Header("Script Objects")]
    [SerializeField]
    PhotonRoomController RoomController;
    [SerializeField]
    PhotonSceneController SceneController;

    //Photon Variable Values
    [Header("Variables")]
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players")]
    [SerializeField]
    byte MaximumPlayersInRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Attempt to connect to photon servers
    public void ConnectToPhoton()
    {
        LoadingText.text = "Connecting to Photon servers...";
        //Debug.Log(LoadingText.text);
        PhotonNetwork.ConnectUsingSettings();
        //LoadingText.text = "Finding Best Region...";
        //PhotonNetwork.ConnectToBestCloudServer();
    }
    
    public void CreateGameRoom(string RoomID)
    {
        Debug.Log("Creating Room...");
        PhotonNetwork.CreateRoom(RoomID, new RoomOptions { MaxPlayers = MaximumPlayersInRoom });
    }

    public void JoinGameRoom(string RoomID)
    {
        Debug.Log("Joining Room...");
        PhotonNetwork.JoinRoom(RoomID);
    }

    public void JoinRandomGameRoom()
    {
        Debug.Log("Joining Random Room...");
        PhotonNetwork.JoinRandomRoom();
    }

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        LoadingText.text = "Joining Lobby...";

        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        LoadingText.text = "Connecting To Master Server...";
        Debug.Log("Connected");
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

    public override void OnJoinedLobby()
    {
        LoadingText.text = "";
        LobbyScreen.SetActive(true);

        //Assign the nickname after successfully joining the lobby
        if(PhotonNetwork.NickName == "" && PlayerPrefs.HasKey("PlayerUsername"))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerUsername");
        }

        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        RoomScreen.SetActive(true);
        LobbyScreen.SetActive(false);
        LoadingText.text = "";

        RoomController.InitRoom();


        Debug.Log("Joined Room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room!\nError Code:" + returnCode.ToString() + "\n" + message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room!\nError Code:" + returnCode.ToString() + "\n" + message);
    }

    public override void OnCreatedRoom()
    {
        //JoinGameRoom(SceneController.GetRoomID);

        Debug.Log("Created Room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomController.UpdatePlayerList();


        Debug.Log("Someone joined");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomController.UpdatePlayerList();

        Debug.Log("Someone left");
    }

    //public override void OnMasterClientSwitched(Player newMasterClient)
    //{
    //    RoomController.UpdatePlayerList();
    //}

    public override void OnLeftRoom()
    {
        RoomScreen.SetActive(false);
        LobbyScreen.SetActive(true);

        Debug.Log("Left Room");
    }
    #endregion
}