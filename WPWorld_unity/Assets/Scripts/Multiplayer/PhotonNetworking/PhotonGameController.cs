using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonGameController : MonoBehaviour {

    [SerializeField]
    GameObject PlayerObjectPrefab;
    
    // Use this for initialization
    void Start () {

        if(PlayerController.LocalPlayerInstance == null)
        {
           var thePlayer = PhotonNetwork.Instantiate(this.PlayerObjectPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            thePlayer.transform.GetChild(0).GetComponent<TextMesh>().text = thePlayer.GetComponent<PlayerController>().photonView.Owner.NickName;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
