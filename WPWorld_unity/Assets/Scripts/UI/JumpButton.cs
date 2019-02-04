using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour {

    GameObject PlayerObject = null;

	// Use this for initialization
	void Start () {
        PlayerObject = PlayerMovement.LocalPlayerInstance;

        if(PlayerObject.GetComponent<TPSLogic>() != null)
        {
            foreach (GameObject n_Player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (!n_Player.GetComponent<TPSLogic>().isMine())
                    continue;
                PlayerObject = n_Player;
                break;
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
