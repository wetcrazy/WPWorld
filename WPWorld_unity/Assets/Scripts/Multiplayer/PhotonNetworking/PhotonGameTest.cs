using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PhotonGameTest : MonoBehaviour {

    [SerializeField]
    GameObject PlayerObjectPrefab;


    private GameObject[] SpawnPoints;
    PhotonView photonView;
    public static Vector3 SpawnPoint;

    // Use this for initialization
    void Start () {
        photonView = PhotonView.Get(this);

        if (PhotonNetwork.IsMasterClient)
        {
            //Get spawning positions of level
            SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
            {
                photonView.RPC("ReceiveSpawnPoint", PhotonNetwork.PlayerList[i], SpawnPoints[i].transform.localPosition);
            }
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [PunRPC]
    void ReceiveSpawnPoint(Vector3 SpawnPos)
    {
        //After receiving the spawnpoint pos from host, instantiate the player
        //PhotonNetwork.Instantiate(PlayerObjectPrefab.name, SpawnPos, Quaternion.identity, 0);
        SpawnPoint = SpawnPos;
        PhotonNetwork.Instantiate(PlayerObjectPrefab.name, Vector3.zero, Quaternion.identity, 0);
    }
}
