using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController :  MonoBehaviourPun, IPunObservable{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
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
