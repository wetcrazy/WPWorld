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
    Text RegionText;
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

    bool isSwitchingRegion = false;
    string RegionCode;
   
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Attempt to connect to photon servers
    public void ConnectToPhoton()
    {
        LoadingText.text = "Connecting to Photon servers...";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public void ConnectToRegion(string RegionCode)
    {
        LoadingText.text = "Leaving Current Lobby...";

        isSwitchingRegion = true;
        this.RegionCode = RegionCode;

        PhotonNetwork.Disconnect();
    }

    public void CreateGameRoom(string RoomID)
    {
        PhotonNetwork.CreateRoom(RoomID, new RoomOptions { MaxPlayers = MaximumPlayersInRoom });
    }

    public void JoinGameRoom(string RoomID)
    {
        PhotonNetwork.JoinRoom(RoomID);
    }

    public void JoinRandomGameRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        LoadingText.text = "Joining Lobby...";
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        LoadingText.text = "Connecting To Master Server...";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if(isSwitchingRegion)
        {
            LoadingText.text = "Connecting To New Region...";
            PhotonNetwork.ConnectToRegion(RegionCode);
            isSwitchingRegion = false;
            return;
        }

        LoadingText.text = cause.ToString();
        LobbyScreen.SetActive(false);
        OfflineScreen.SetActive(true);
        
        switch (cause)
        {
            case DisconnectCause.ExceptionOnConnect:
                LoadingText.text = "Photon: Connection Exception\nPlease Check Your Internet Connection";
                break;
            case DisconnectCause.ServerTimeout:
                LoadingText.text = "Photon: Server Timeout";
                break;
            case DisconnectCause.ClientTimeout:
                LoadingText.text = "Photon: Client Timeout";
                break;
            case DisconnectCause.InvalidAuthentication:
                LoadingText.text = "Photon: Invalid AppID";
                break;
            case DisconnectCause.MaxCcuReached:
                LoadingText.text = "Photon: Server Limit Reached";
                break;
            case DisconnectCause.InvalidRegion:
                LoadingText.text = "Photon: Invalid Region";
                break;
            case DisconnectCause.DisconnectByClientLogic:
                LoadingText.text = "Photon: Client Disconnected";
                break;
            default:
                LoadingText.text = "Photon: " + cause;
                break;
        }
    }

    public override void OnJoinedLobby()
    {
        RegionText.text = "Region: " + PhotonNetwork.CloudRegion;
        LoadingText.text = "";
        LobbyScreen.SetActive(true);

        //Assign the nickname after successfully joining the lobby
        if(PhotonNetwork.NickName == "" && PlayerPrefs.HasKey("PlayerUsername"))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerUsername");
        }
    }

    public override void OnJoinedRoom()
    {
        RoomScreen.SetActive(true);
        LobbyScreen.SetActive(false);
        LoadingText.text = "";

        RoomController.InitRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        LoadingText.text = "Failed to join room!\nCode " + returnCode.ToString() + "\n" + message;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LoadingText.text = "Failed to join random room!\nCode " + returnCode.ToString() + "\n" + message;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomController.UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomController.UpdatePlayerList();
        RoomController.GetComponent<PhotonView>().RPC("UpdateCurrentGameMode", newPlayer, PhotonRoomController.CurrentGamemode);
    }

    //public override void OnMasterClientSwitched(Player newMasterClient)
    //{
    //    RoomController.UpdatePlayerList();
    //}

    public override void OnLeftRoom()
    {
        RoomScreen.SetActive(false);
        LobbyScreen.SetActive(true);
    }
    #endregion
}