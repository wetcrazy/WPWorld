using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PhotonGameTest : MonoBehaviour {

    [SerializeField]
    GameObject PlayerObjectPrefab;

    // Use this for initialization
    void Start () {
        PhotonNetwork.Instantiate(PlayerObjectPrefab.name, new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
