using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BombermanBreakable : MonoBehaviourPun, IPunObservable
{
    public bool isDestroyed { get; set; }

    private void Start()
    {
        isDestroyed = false;    
    }  

    private void Update()
    {
        if(isDestroyed)
        {
            var randNum = Random.Range(0, GameObject.FindGameObjectWithTag("BombermanManager").GetComponent<BombermanManager>().List_PowerUpBlocks.Count);

            if (Photon.Pun.PhotonNetwork.IsConnected)
            {
                object[] content = new object[] { this.transform.position , randNum };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well

                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.BOMBER_EVENT_SPAWN_POWERUP, content, raiseEventOptions, sendOptions);
            }
            else
            {
                GameObject.FindGameObjectWithTag("BombermanManager").GetComponent<BombermanManager>().SpawnPowerUp(this.transform.position, randNum);
            }

            Destroy(this.gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Send other players our data
        if (stream.IsWriting)
        {

        }
        else //Receive data from other players
        {

        }
    }
}
