using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BombermanBreakable : MonoBehaviour
{
    public bool isDestroyed { get; set; }
    public int NumHits { get; set; }

    protected float FallSpeed = 2.0f;
    protected bool is_Fall = true;

    public BombermanBreakable()
    {
        isDestroyed = false;
        NumHits = 1;
    }

    private void Update()
    {
        if(isDestroyed)
        {
            BreakableHitted();
        }
        if(is_Fall)
        {
            // Falling 
            this.transform.Translate(-this.transform.up * FallSpeed * Time.deltaTime);
        } 
    }

    private void BreakableHitted()
    {
        if(NumHits <= 1)
        {
            SendPowerupinfo();
        }
        else
        {
            NumHits -= 1;
            isDestroyed = false;
        }
    }

    private void SendPowerupinfo()
    {
        var randNum = Random.Range(0, GameObject.FindGameObjectWithTag("BombermanManager").GetComponent<BombermanManager>().List_PowerUpBlocks.Count);

        if (PhotonNetwork.IsConnected)
        {
            object[] content = new object[] { this.transform.position, randNum };

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

    private void OnCollisionEnter(Collision collision)
    {
        is_Fall = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        is_Fall = true;
    }
}
