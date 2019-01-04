using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanInputs : MonoBehaviour
{
    // private GameObject PlayerObj;

    private void Start()
    {
        // PlayerObj = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnBombButtonDown()
    {
        //if(PlayerObj.GetComponent<BomberManPlayer>() == null)
        //{
        //    Debug.Log("Error Spawn Bomb: Player not found");
        //    return;
        //}
        // PlayerObj.SendMessage("SpawnBomb");

        PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().SpawnBomb();
    }
}
