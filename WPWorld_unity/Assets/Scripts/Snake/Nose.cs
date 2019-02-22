using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Nose : MonoBehaviour {
    public bool deathcollided = false;
    
    public void Restart()
    {
        deathcollided = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!GetComponentInParent<PhotonView>().IsMine)
        {
            return;
        }

        if (other.CompareTag("Block"))
        {
            deathcollided = true;
        }
        else if(other.CompareTag("Player") && !other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            deathcollided = true;
        }
        else if((other.name == "Body(Clone)") || (other.CompareTag("Body")))
        {
            deathcollided = true;
        }
        else if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_EATFOOD, null, GameController.raiseEventAll, GameController.sendOptions);
        }
        else if (other.CompareTag("Speedy"))
        {
            Destroy(other.gameObject);
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_STUN, null, GameController.raiseEventAll, GameController.sendOptions);
        }
        else if (other.CompareTag("Food_Block"))
        {
            gameObject.GetComponentInParent<Head>().Block_Pop_up();
            Destroy(other.gameObject);
        }

    }
}
