using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
        else
        {
        }
    }
}
