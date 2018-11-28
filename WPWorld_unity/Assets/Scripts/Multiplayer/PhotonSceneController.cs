using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonSceneController : MonoBehaviour {
    
    //Scene Objects & Variables (Gameobjects, Canvas,etc)
    [SerializeField]
    GameObject InputPlayerNamePanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     private void CheckForExistingUsername()
    {
        if (PlayerPrefs.HasKey("PlayerUsername"))
        {
            InputPlayerNamePanel.SetActive(false);
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerUsername");
        }
    }

    private void ConfirmUsername()
    {
        InputField inputField = InputPlayerNamePanel.GetComponent<InputField>();
        string NewName = inputField.text;

        if (string.IsNullOrEmpty(NewName))
        {
            return;
        }
        else
        {
            PlayerPrefs.SetString("PlayerUsername", NewName);
        }

        CheckForExistingUsername();
    }
}
