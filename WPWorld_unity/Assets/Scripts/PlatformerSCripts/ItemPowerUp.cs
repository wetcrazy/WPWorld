using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowerUp : MonoBehaviour {

    [SerializeField]
    private POWERUPS PowerUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
        {
            other.GetComponent<PlayerPowerUp>().SetPowerUp(PowerUp);
        }
    }
}
