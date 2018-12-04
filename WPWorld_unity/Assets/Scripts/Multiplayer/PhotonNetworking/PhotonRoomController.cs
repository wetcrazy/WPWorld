using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonRoomController : MonoBehaviour {

    [Header("Scene Objects")]
    [SerializeField]
    Text RoomIDText;
    [SerializeField]
    GameObject TransferHostButton;
    [SerializeField]
    Text RoomVisibilityText;
    [SerializeField]
    GameObject StartGameButton;
    [SerializeField]
    GameObject PlayerListPanel;

    //List<Text> PlayerTextList = new List<Text>();
    Text[] PlayerTextList;

    public void InitRoom()
    {
        PlayerTextList = new Text[PlayerListPanel.transform.childCount];
        PlayerTextList = PlayerListPanel.GetComponentsInChildren<Text>();
        
        RoomIDText.text += PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();

        //for (int i = 0; i < PlayerListPanel.transform.childCount; ++i)
        //{
        //    PlayerTextList.Add(PlayerListPanel.transform.GetChild(0).gameObject);
        //}

        //Is not host of room
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            TransferHostButton.SetActive(false);
            RoomVisibilityText.transform.parent.gameObject.SetActive(false);
            StartGameButton.SetActive(false);
        }
        else
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;

            TransferHostButton.SetActive(true);
            RoomVisibilityText.transform.parent.gameObject.SetActive(true);
            StartGameButton.SetActive(true);
        }
    }
    
    public void UpdatePlayerList()
    {
        for (int i = 0; i < PlayerTextList.Length; ++i)
        {
            if(i < PhotonNetwork.CurrentRoom.PlayerCount)
            {
                PlayerTextList[i].text = PhotonNetwork.CurrentRoom.Players[i].NickName;
            }
            else
            {
                PlayerTextList[i].text = "";
            }
        }
    }

    public void SetRoomVisibility()
    {
        PhotonNetwork.CurrentRoom.IsVisible = !PhotonNetwork.CurrentRoom.IsVisible;
        
        if(PhotonNetwork.CurrentRoom.IsVisible)
        {
            RoomVisibilityText.text = "Visibility: Public";
        }
        else
        {
            RoomVisibilityText.text = "Visibility: Private";
        }
    }

    public void TransferHost()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)
        {
            var thePlayer = PhotonNetwork.CurrentRoom.Players[i];

            if (thePlayer != PhotonNetwork.LocalPlayer)
            {
                PhotonNetwork.CurrentRoom.SetMasterClient(thePlayer);
            }
        }

        UpdatePlayerList();
    }

    public void TransferHost(int ActorNumber)
    {
        PhotonNetwork.CurrentRoom.SetMasterClient(PhotonNetwork.CurrentRoom.GetPlayer(ActorNumber));
        UpdatePlayerList();
    }

    public void LeaveRoom()
    //{
    //    TransferHost();
    //    Wait(2.0f);
    { 
        PhotonNetwork.LeaveRoom();
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(2.0f);
    }
}