using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour {

    GameObject PlayerObject = null;

	// Use this for initialization
	void Start () {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");

        if(PlayerObject.GetComponent<TPSLogic>() != null)
        {
            GameObject[] PlayersToGet = GameObject.FindGameObjectsWithTag("Player");

            foreach(GameObject n_Player in PlayersToGet)
            {
                if(n_Player.GetComponent<TPSLogic>().isMine())
                {
                    PlayerObject = n_Player;
                }
            }
        }
	}
	
	public void onJumpButtonDown()
    {
        if (PlayerObject.GetComponent<TPSLogic>() != null || PlayerObject.GetComponent<DSPlayer>() != null)
        {
            PlayerObject.SendMessage("GetJumpButtonInput");
        }
        else
        {
            Debug.Log("A jumping script doesn't exist in your Player GameObject!");
        }
    }
}
