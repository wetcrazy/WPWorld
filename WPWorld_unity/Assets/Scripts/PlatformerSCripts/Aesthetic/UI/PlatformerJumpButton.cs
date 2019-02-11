using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerJumpButton : MonoBehaviour {

    private TPSLogic PlayerObject;

	// Use this for initialization
	void Start () {
        PlayerObject = PlayerMovement.LocalPlayerInstance.GetComponent<TPSLogic>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnJumpButtonDown()
    {
        if (PlayerObject != null && PlayerObject.isMine())
            PlayerObject.Jump();
    }
}
