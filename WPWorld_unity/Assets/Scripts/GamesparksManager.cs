using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using GameSparks.Api;
using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class GamesparksManager : MonoBehaviour {

    /// <summary>The GameSparks Manager singleton</summary>
    public static GamesparksManager LocalGamesparkInstance = null;

    [SerializeField]
    PhotonSceneController SceneController;

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
        new RegistrationRequest()
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

        AuthenticatePlayer();
        AuthenticateDeviceAndPlayer();
    }

    public void AuthenticatePlayer()
    {
        new AuthenticationRequest().SetUserName(PlayerPrefs.GetString("PlayerUsername"))
            .SetPassword(PlayerPrefs.GetString("PlayerPassword"))
            .Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("Player Authenticated...");
                    SceneController.ReturnAuthentication(true);
            }
            else
            {
                Debug.Log("Error Authenticating Player...");
                    SceneController.ReturnAuthentication(false);
            }
        });
        
    }

    public void AuthenticateDeviceAndPlayer()
    {
        new DeviceAuthenticationRequest().Send((response) => {
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
}
