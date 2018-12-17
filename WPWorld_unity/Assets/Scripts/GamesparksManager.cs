﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GamesparksManager : MonoBehaviour {

    /// <summary>The GameSparks Manager singleton</summary>
    private static GamesparksManager instance = null;
    void Awake()
    {
        if (instance == null) // check to see if the instance has a reference
        {
            instance = this; // if not, give it a reference to this class...
            DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes
        }
        else // if we already have a reference then remove the extra manager from the scene
        {
            Destroy(this.gameObject);
        }
    }

    public void RegisterPlayer()
    {
        new GameSparks.Api.Requests.RegistrationRequest()
          .SetDisplayName(PhotonNetwork.NickName)
          .SetPassword(PlayerPrefs.GetString("PlayerPassword"))
          .SetUserName(PhotonNetwork.NickName)
          .Send((response) => {
              if (!response.HasErrors)
              {
                  Debug.Log("Player Registered");
              }
              else
              {
                  Debug.Log("Error Registering Player");
              }
          }
        );
    }

    public void AuthenticateDeviceAndPlayer()
    {
        new GameSparks.Api.Requests.DeviceAuthenticationRequest().SetDisplayName(PhotonNetwork.NickName).Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("Device Authenticated...");
            }
            else
            {
                Debug.Log("Error Authenticating Device...");
            }
        });
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}