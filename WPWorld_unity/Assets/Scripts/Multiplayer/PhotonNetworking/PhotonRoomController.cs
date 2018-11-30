using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoomController : MonoBehaviour {

    public void InitRoom()
    {
        //Is host of room
       if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {

        }
        else
        {

        }
    }
    
    public void SetRoomVisibility(bool IsVisible)
    {
        PhotonNetwork.CurrentRoom.IsVisible = IsVisible;   
    }

    public void TransferHost(int ActorNumber)
    {
        

        Player NewHost = PhotonNetwork.CurrentRoom.GetPlayer(ActorNumber);
        PhotonNetwork.CurrentRoom.SetMasterClient(NewHost);
    }

    
}
