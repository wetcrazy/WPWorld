using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour {

    [SerializeField]
    private AudioClip JumpSFX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GetComponent<PlayerMovement>().GetGrounded())
            {
                if(JumpSFX != null)
                    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
            }
        }
	}
}
