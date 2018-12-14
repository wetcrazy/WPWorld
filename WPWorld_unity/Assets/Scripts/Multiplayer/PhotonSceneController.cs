using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonSceneController : MonoBehaviour {

    //Scene Objects & Variables (Gameobjects, Canvas,etc)
    [Header("Scene Objects")]
    [SerializeField]
    GameObject InputPlayerNamePanel;
    [SerializeField]
    GameObject InputFieldUsername;
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

    //Script Object Variables
    [Header("Script Objects")]
    [SerializeField]
    PhotonConnect photonConnect;
    
    private string RoomID;

    public string GetRoomID
    {
        get { return RoomID; }
    }


    // Use this for initialization
    void Start ()
    {
        //Check if player already has a username
        if (CheckForExistingUsername())
        {
            //Try to connect to Photon servers
            TryGoOnline();
        }
        
        InputRoomIDPanel.SetActive(false);
        OfflineScreen.SetActive(false);
        LobbyScreen.SetActive(false);
        RoomScreen.SetActive(false);
    }

    //Check if a username exists in PlayerPrefs
    private bool CheckForExistingUsername()
    {
        if (PlayerPrefs.HasKey("PlayerUsername"))
        {
            //If there's already a username, no need for username input
            InputPlayerNamePanel.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Player's confirmation after inputting a username
    public void ConfirmUsername()
    {
        //Get the username input by player
        InputField inputField = InputFieldUsername.GetComponent<InputField>();
        string NewName = inputField.text;
        
        //If input field is empty
        if (string.IsNullOrEmpty(NewName))
        {
            return;
        }
        else
        {
            //Set the input username into PlayerPrefs
            PlayerPrefs.SetString("PlayerUsername", NewName);
        }

        //Set the username input panel to inactive
        InputPlayerNamePanel.SetActive(false);
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

    public void OpenJoinRoom()
    {
        InputFieldRoomID.SetActive(true);
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
        PhotonNetwork.NickName = "";
    }
}