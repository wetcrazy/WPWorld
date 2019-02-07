﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour {

    GameObject PlayerObject = null;

	// Use this for initialization
	void Start () {
        PlayerObject = PlayerMovement.LocalPlayerInstance;
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
