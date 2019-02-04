using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerController :  MonoBehaviourPun, IPunObservable{
    
    public static GameObject LocalPlayerInstance;
    private int Score = 0;
    
    float Sendtimer = 0.5f;
    bool hasSent = false;
    public int PlayerScore
    {
        get { return Score; }
        set { Score = value; }
    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }

        gameObject.transform.SetParent(PhotonGameTest._GroundObject.transform, true);
    }

    // Use this for initialization
    void Start () {
        gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;

        if (photonView.IsMine)
        {
            gameObject.transform.localPosition = PhotonGameTest.SpawnPoint;
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE, gameObject.transform.localRotation, RaiseEventOptions.Default, sendOptions);

            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE, gameObject.transform.localPosition, RaiseEventOptions.Default, sendOptions);
        }
    }

	// Update is called once per frame
	void Update () {
        if (!photonView.IsMine)
        {
            return;
        }


        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE, gameObject.transform.localRotation, RaiseEventOptions.Default, sendOptions);

        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE, gameObject.transform.localPosition, RaiseEventOptions.Default, sendOptions);
    }

    SendOptions sendOptions = new SendOptions { Reliability = true };
    //private void LateUpdate()
    //{
    //    if(!photonView.IsMine)
    //    {
    //        return;
    //    }

    //    if (!hasSent)
    //    {
    //        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE, gameObject.transform.localPosition, RaiseEventOptions.Default, sendOptions);
    //        hasSent = true;
    //    }
    //    else
    //    {
    //        Sendtimer -= Time.deltaTime;

    //        if (Sendtimer <= 0)
    //        {
    //            hasSent = false;
    //            Sendtimer = 0.5f;
    //        }
    //    }
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Send other players our data
        if(stream.IsWriting)
        {

        }
        else //Receive data from other players
        {

        }
    }
}