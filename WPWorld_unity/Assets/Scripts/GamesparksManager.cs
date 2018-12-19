using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GamesparksManager : MonoBehaviour {

    /// <summary>The GameSparks Manager singleton</summary>
    public static GamesparksManager LocalGamesparkInstance = null;

    void Awake()
    {
        if (LocalGamesparkInstance == null) // check to see if the instance has a reference
        {
            LocalGamesparkInstance = this; // if not, give it a reference to this class...
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

    public bool AuthenticateDeviceAndPlayer()
    {
        bool isAuthenticated = false;

        new GameSparks.Api.Requests.DeviceAuthenticationRequest().SetDisplayName(PhotonNetwork.NickName).Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("Device Authenticated...");
                isAuthenticated = true;
            }
            else
            {
                Debug.Log("Error Authenticating Device...");
                isAuthenticated = false;
                
            }
        });

        return isAuthenticated;
    }
}
