﻿using System.Collections;
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
    Text ChatInputText;
    [SerializeField]
    Text ChatPanelText;
    [SerializeField]
    GameObject PlayerListPanel;
    [SerializeField]
    GameObject HostControls;
    [SerializeField]
    Image CurrentGameModeImage;
    [SerializeField]
    GameObject LoadingScreen;

    [Header("GameMode Sprites")]
    [SerializeField]
    Sprite SnakeSprite;
    [SerializeField]
    Sprite TronSprite;
    [SerializeField]
    Sprite PlatformerSprite;
    [SerializeField]
    Sprite BombermanSprite;

    //List<Text> PlayerTextList = new List<Text>();
    Text[] PlayerTextList;
    private Dictionary<int, Player> players = new Dictionary<int, Player>();
    PhotonView photonView;

    public enum GAMEMODE
    {
        GAMEMODE_SNAKE,
        GAMEMODE_TRON,
        GAMEMODE_PLATFORMER,
        GAMEMODE_BOMBERMAN,

        GAMEMODE_TOTAL
    }

    public static GAMEMODE CurrentGamemode = 0;

    /// <summary>
    /// Initialise the game room
    /// </summary>
    public void InitRoom(bool isFromGame = false)
    {
        photonView = PhotonView.Get(this);

        //Check if the player is returning to the room from a game scene
        if(!isFromGame)
        {
            photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.NickName + " has joined the room");
        }

        PlayerTextList = new Text[PlayerListPanel.transform.childCount];
        PlayerTextList = PlayerListPanel.GetComponentsInChildren<Text>();

        RoomIDText.text += PhotonNetwork.CurrentRoom.Name;
        ChatPanelText.text = "";
        UpdatePlayerList();

        LoadingScreen.SetActive(false);

        //If you are host of room
        if (PhotonNetwork.IsMasterClient)
        {
            if (!isFromGame)
            {
                PhotonNetwork.CurrentRoom.IsVisible = true;
                UpdateCurrentGameMode(CurrentGamemode);
            }
            else
            {
                //Init the gamemode that was selected
                photonView.RPC("UpdateCurrentGameMode", RpcTarget.All, (GAMEMODE)PlayerPrefs.GetInt("SavedGamemode"));
                PlayerPrefs.DeleteKey("SavedGamemode");
                PlayerPrefs.Save();

                //Init the visibility text
                if (PhotonNetwork.CurrentRoom.IsVisible)
                {
                    RoomVisibilityText.text = "Visibility: Public";
                }
                else
                {
                    RoomVisibilityText.text = "Visibility: Private";
                }
            }

            HostControls.SetActive(true);
        }
        else
        {
            HostControls.SetActive(false);
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
            photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.NickName + " has set the room visibility to public");
        }
        else
        {
            RoomVisibilityText.text = "Visibility: Private";
            photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.NickName + " has set the room visibility to private");
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
            foreach (Player player in PhotonNetwork.PlayerListOthers)
            {
                if (!player.IsInactive)
                {
                    photonView.RPC("BecomeHost", player);
                    break;
                }
            }
        }

        RoomIDText.text = "Room ID: ";

        photonView.RPC("ReceiveChatMessage", RpcTarget.Others, PhotonNetwork.NickName + " has left the room");
        PhotonNetwork.LeaveRoom();
    }

    public void StartNetworkGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Activate the loading screen while the level starts
            photonView.RPC("ActivateLoadingScreen", RpcTarget.All);

            //Save the gamemode that was selected
            PlayerPrefs.SetInt("SavedGamemode", (int)CurrentGamemode);
            PlayerPrefs.Save();

            switch (CurrentGamemode)
            {
                case GAMEMODE.GAMEMODE_SNAKE:
                    PhotonNetwork.LoadLevel("SNAKE2.0");
                    break;
                case GAMEMODE.GAMEMODE_PLATFORMER:
                    PhotonNetwork.LoadLevel("Platformer");
                    break;
                case GAMEMODE.GAMEMODE_BOMBERMAN:
                    PhotonNetwork.LoadLevel("BomberMan");
                    break;
                default:
                    break;
            }
        }
    }

    public void NextGamemode(int LeftRightValue)
    {
        CurrentGamemode += LeftRightValue;

        if(CurrentGamemode < 0)
        {
            CurrentGamemode = GAMEMODE.GAMEMODE_TOTAL - 1;
        }
        else if (CurrentGamemode >= GAMEMODE.GAMEMODE_TOTAL)
        {
            CurrentGamemode = 0;
        }


        photonView.RPC("UpdateCurrentGameMode", RpcTarget.All, CurrentGamemode);
    }

    //Send the chat message that was typed in input field
    public void SendChatMessage()
    {
        if (ChatInputText.text == "")
        {
            return;
        }

        photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.NickName + ": " + ChatInputText.text);
        ChatInputText.text = "";
    }

    int ChatPanelChars = 0;
    void AddChatPanelText(string Text)
    {
        Text += "\n";
        if (ChatPanelText.text.Length > 0)
        {
            ChatPanelChars += 10;

            if (ChatPanelChars >= 250)
            {
                string TextToRemove = ChatPanelText.text.Substring(0, ChatPanelText.text.IndexOf("\n"));
                ChatPanelText.text = ChatPanelText.text.Substring(ChatPanelText.text.IndexOf("\n") + 1, ChatPanelText.text.Length - TextToRemove.Length - 1);
                //ChatPanelText.text.Remove(0);
                ChatPanelChars -= TextToRemove.Length - 10;
            }
        }

        ChatPanelText.text += Text;
        ChatPanelChars += ChatPanelText.text.Length;
    }

    //Remote Procedure Calls methods
    [PunRPC]
    private void BecomeHost()
    {
        photonView.RPC("ReceiveChatMessage", RpcTarget.All, PhotonNetwork.NickName + " is now the host");
        HostControls.SetActive(true);
    }
    
    [PunRPC]
    private void UpdateCurrentGameMode(GAMEMODE CurrentGamemode)
    {
        switch (CurrentGamemode)
        {
            case GAMEMODE.GAMEMODE_SNAKE:
                {
                    CurrentGameModeImage.sprite = SnakeSprite;
                    PhotonRoomController.CurrentGamemode = CurrentGamemode;
                    break;
                }
            case GAMEMODE.GAMEMODE_TRON:
                {
                    CurrentGameModeImage.sprite = TronSprite;
                    PhotonRoomController.CurrentGamemode = CurrentGamemode;
                    break;
                }
            case GAMEMODE.GAMEMODE_PLATFORMER:
                {
                    CurrentGameModeImage.sprite = PlatformerSprite;
                    PhotonRoomController.CurrentGamemode = CurrentGamemode;
                    break;
                }
            case GAMEMODE.GAMEMODE_BOMBERMAN:
                {
                    CurrentGameModeImage.sprite = BombermanSprite;
                    PhotonRoomController.CurrentGamemode = CurrentGamemode;
                    break;
                }
            default:
                break;
        }
    }

    [PunRPC]
    private void ActivateLoadingScreen()
    {
        LoadingScreen.SetActive(true);
    }

    [PunRPC]
    public void ReceiveChatMessage(string ChatMessage)
    {
        AddChatPanelText(ChatMessage);
    }
}