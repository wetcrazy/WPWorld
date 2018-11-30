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

    //Script Object Variables
    [Header("Script Objects")]
    [SerializeField]
    PhotonConnect photonConnect;

    // Use this for initialization
    void Start () {
        CheckForExistingUsername();

        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerUsername");
        }
	}

     private void CheckForExistingUsername()
    {
        if (PlayerPrefs.HasKey("PlayerUsername"))
        {
            InputPlayerNamePanel.SetActive(false);
        }
    }

    public void ConfirmUsername()
    {
        InputField inputField = InputFieldUsername.GetComponent<InputField>();
        string NewName = inputField.text;

        if (string.IsNullOrEmpty(NewName))
        {
            return;
        }
        else
        {
            PlayerPrefs.SetString("PlayerUsername", NewName);
        }

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerUsername");
        }
        InputPlayerNamePanel.SetActive(false);
    }

    public void ClearUsernamePref()
    {
        PlayerPrefs.DeleteKey("PlayerUsername");
        PhotonNetwork.NickName = "";
    }

    public void TryGoOnline()
    {
        photonConnect.ConnectToPhoton();
    }
}
