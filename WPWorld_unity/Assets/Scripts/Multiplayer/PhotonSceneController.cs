using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonSceneController : MonoBehaviour {

    //Scene Objects & Variables (Gameobjects, Canvas,etc)
    [Header("Scene Objects")]
    [SerializeField]
    GameObject InputPlayerPanel;
    [SerializeField]
    GameObject InputFieldUsername;
    [SerializeField]
    GameObject InputFieldPassword;
    [SerializeField]
    GameObject InputRoomIDPanel;
    [SerializeField]
    GameObject InputFieldRoomID;
    [SerializeField]
    GameObject OfflineScreen;
    [SerializeField]
    GameObject LobbyScreen;
    [SerializeField]
    GameObject RoomScreen;
    [SerializeField]
    GameObject LoadingScreen;
    [SerializeField]
    GameObject RegionSelectScreen;

    [SerializeField]
    Text LoadingText;
    [SerializeField]
    Text RegionText;

    //Script Object Variables
    [Header("Script Objects")]
    [SerializeField]
    PhotonConnect photonConnect;
    [SerializeField]
    PhotonRoomController roomController;

    private string RoomID;

    public string GetRoomID
    {
        get { return RoomID; }
    }

    // Use this for initialization
    void Start ()
    {
        InputPlayerPanel.SetActive(false);
        InputRoomIDPanel.SetActive(false);
        OfflineScreen.SetActive(false);
        LobbyScreen.SetActive(false);
        LoadingScreen.SetActive(false);
        RoomScreen.SetActive(PhotonNetwork.InRoom);
        RegionSelectScreen.SetActive(false);
        RegionText.text = "";

        if(PhotonNetwork.InRoom)
        {
            roomController.InitRoom(true);
            LoadingText.text = "";
            return;
        }

        //Check if player already exists
        CheckForExistingPlayer();
    }

    //Check if a local player exists 
    private void CheckForExistingPlayer()
    {
        if (PlayerPrefs.HasKey("PlayerUsername") && PlayerPrefs.HasKey("PlayerPassword"))
        {
            GamesparksManager.LocalGamesparkInstance.GetComponent<GamesparksManager>().AuthenticatePlayer();
            LoadingText.text = "Authenticating Player...";
        }
        else
        {
            InputPlayerPanel.SetActive(true);
        }

    }

    public void ReturnAuthentication(bool isAuthenticated)
    {
        if(isAuthenticated)
        {
            //If there's already a local player, no need to create again
            InputPlayerPanel.SetActive(false);

            TryGoOnline();
        }
        else
        {
            InputPlayerPanel.SetActive(true);
            LoadingText.text = "";
        }
    }

    //Player's confirmation after inputting a username & password
    public void ConfirmPlayer()
    {
        //Get the username input by player
        InputField inputFieldUsername = InputFieldUsername.GetComponent<InputField>();
        string NewName = inputFieldUsername.text;

        //Get the password input by player
        InputField inputFieldPassword = InputFieldPassword.GetComponent<InputField>();
        string NewPassword = inputFieldUsername.text;

        //If input field is empty
        if (string.IsNullOrEmpty(NewName) || string.IsNullOrEmpty(NewPassword))
        {
            return;
        }
        else
        {
            //Set the input username into PlayerPrefs
            PlayerPrefs.SetString("PlayerUsername", NewName);
            //Set the username in Photon
            PhotonNetwork.NickName = NewName;

            //Set the input password into PlayerPrefs
            PlayerPrefs.SetString("PlayerPassword", NewPassword);
        }

        //Set the username input panel to inactive
        InputPlayerPanel.SetActive(false);
        
        //Register with Gamesparks
        GamesparksManager.LocalGamesparkInstance.RegisterPlayer();

        //Try to connect to photon servers
        TryGoOnline();
    }

    //Attempt to connect to Photon servers
    public void TryGoOnline()
    {
        //Set OfflineScreen to be inactive frist 
        OfflineScreen.SetActive(false);

        //Connect to Photon Servers
        photonConnect.ConnectToPhoton();
    }

    public string RandomAlphanumericString(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string AlphaNumString = "";
        
        System.Random random = new System.Random();

        for (int i = 0; i < length; ++i)
        {
            AlphaNumString += chars[random.Next(chars.Length)];
        }

        return AlphaNumString;
    }

    public void CreateGameRoom()
    {
        RoomID = RandomAlphanumericString(6);
        photonConnect.CreateGameRoom(RoomID);
    }

    public void OpenJoinRoom(bool active)
    {
        InputRoomIDPanel.SetActive(active);
    }

    public void ConfirmJoinRoom()
    {
        photonConnect.JoinGameRoom(InputFieldRoomID.GetComponent<InputField>().text);
        InputRoomIDPanel.SetActive(false);
    }

    public void CancelJoinRoom()
    {
        InputRoomIDPanel.SetActive(false);
    }

    public void ClearUsernamePref()
    {
        PlayerPrefs.DeleteKey("PlayerUsername");
        PlayerPrefs.DeleteKey("PlayerPassword");
        PhotonNetwork.NickName = "";
    }

    public void OpenRegionSelect()
    {
        RegionSelectScreen.SetActive(true);
    }

    public void SelectRegion(string RegionCode)
    {
        photonConnect.ConnectToRegion(RegionCode);
        RegionSelectScreen.SetActive(false);
        LobbyScreen.SetActive(false);
    }

    public void CloseRegionSlect()
    {
        RegionSelectScreen.SetActive(false);
    }
}