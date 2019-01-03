using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController :  MonoBehaviourPun, IPunObservable{
    
    public static GameObject LocalPlayerInstance;
    private int Score = 0;

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
    }

    // Use this for initialization
    void Start () {
        LocalPlayerInstance.transform.GetChild(0).GetComponent<TextMesh>().text = LocalPlayerInstance.GetComponent<PlayerController>().photonView.Owner.NickName;
    }
	
	// Update is called once per frame
	void Update () {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            photonView.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = PhotonNetwork.PlayerListOthers[0].NickName;
            return;
        }

        if(photonView.IsMine)
        {

        }
	}

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