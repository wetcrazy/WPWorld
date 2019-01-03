using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonRoomController : MonoBehaviour
{

    [Header("Scene Objects")]
    [SerializeField]
    Text RoomIDText;
    [SerializeField]
    Text RoomVisibilityText;
    [SerializeField]
    GameObject PlayerListPanel;
    [SerializeField]
    GameObject HostControls;

    //List<Text> PlayerTextList = new List<Text>();
    Text[] PlayerTextList;
    private Dictionary<int, Player> players = new Dictionary<int, Player>();
   
    public void InitRoom()
    {
        PlayerTextList = new Text[PlayerListPanel.transform.childCount];
        PlayerTextList = PlayerListPanel.GetComponentsInChildren<Text>();

        RoomIDText.text += PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();

        //Is not host of room
        if (!PhotonNetwork.IsMasterClient)
        {
            HostControls.SetActive(false);
        }
        else
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
        }
    }

    public void UpdatePlayerList()
    {
        if(!PhotonNetwork.InRoom)
        {
            Debug.Log("Updating player list while not in room");
        }

        for (int i = 0; i < PlayerTextList.Length; ++i)
        {
            if (i < PhotonNetwork.CurrentRoom.PlayerCount)
            {
                PlayerTextList[i].text = PhotonNetwork.PlayerList[i].NickName;
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

        if (PhotonNetwork.CurrentRoom.IsVisible)
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
        if(PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            return;
        }

        for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; ++i)
        {
            var thePlayer = PhotonNetwork.PlayerListOthers[i];

            if (!thePlayer.IsInactive)
            {
                PhotonNetwork.CurrentRoom.SetMasterClient(thePlayer);

                HostControls.SetActive(false);
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("BecomeHost", thePlayer);
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
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < PhotonNetwork.PlayerListOthers.Length; ++i)
            {
                var thePlayer = PhotonNetwork.PlayerListOthers[i];

                if (!thePlayer.IsInactive)
                {
                    PhotonView photonView = PhotonView.Get(this);
                    photonView.RPC("BecomeHost", thePlayer);
                }
            }
        }

        PhotonNetwork.LeaveRoom();
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(2.0f);
    }

    public void StartNetworkGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("PhotonGameTest");
        }

    }

    //Remote Procedure Calls methods
    [PunRPC]
    private void BecomeHost()
    {
        HostControls.SetActive(true);
    }
}