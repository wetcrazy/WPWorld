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
            gameObject.transform.parent = GameObject.FindGameObjectWithTag("GameLevel").transform;
        }
    }

    // Use this for initialization
    void Start () {
        gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;

        gameObject.transform.localPosition = ARMultiplayerController.SpawnPoint;
    }
	
	// Update is called once per frame
	void Update () {
        
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