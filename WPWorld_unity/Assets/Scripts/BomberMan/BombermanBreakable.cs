using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BombermanBreakable : MonoBehaviour
{
    public bool isDestroyed { get; set; }
    public int NumHits;
    public Vector3 target { get; set; }

    protected float FallSpeed = 6.0f;
    protected bool is_Fall = true;

    private void Start()
    {
        isDestroyed = false;
    }

    private void Update()
    {
        if(isDestroyed)
        {
            BreakableHitted();
        }
        if(is_Fall)
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, FallSpeed * Time.deltaTime);
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
            object[] content = new object[] { this.transform.localPosition, randNum };

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
        if(collision.transform.tag == "BombermanFloor" || collision.transform.tag == "BombermanBreakable")
        {
            is_Fall = false;
        }
        else
        {
            is_Fall = true;
        }
        if(collision.transform.gameObject == PlayerMovement.LocalPlayerInstance && !is_Fall)
        {
            PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().SetisDead(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "BombermanFloor" || collision.transform.tag == "BombermanBreakable")
        {
            is_Fall = true;
        }
    }
}
