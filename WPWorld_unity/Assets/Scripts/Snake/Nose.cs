using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose : MonoBehaviour {
    public bool deathcollided = false;
    public void Restart()
    {
        deathcollided = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("okay");
            deathcollided = true;
        }
        else if(other.CompareTag("Player") && other.name != GetComponentInParent<GameObject>().name)
        {
            deathcollided = true;
        }
        else if((other.name == "Body(Clone)") || (other.CompareTag("Body")))
        {
            deathcollided = true;
        }
        else if (other.CompareTag("Food"))
        {
            gameObject.GetComponentInParent<Head>().AddAppleAte();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Speedy"))
        {
            ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
            Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_STUN, null, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

            //gameObject.GetComponentInParent<Head>().Stun();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Food_Block"))
        {
            //ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
            //Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BLOCKS_POP_UP, null, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
            gameObject.GetComponentInParent<Head>().Block_Pop_up();
            Destroy(other.gameObject);
            
        }

    }
}
