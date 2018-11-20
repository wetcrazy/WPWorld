using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour {

    GameObject PlayerObject = null;

    [SerializeField]
    private string SoundToPlay;

    GameObject SoundSystemRef;

    // Use this for initialization
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem");
    }

    public void onActionButtonDown()
    {
        if (PlayerObject.GetComponent<PlayerPowerUp>() != null && PlayerObject.GetComponent<PlayerPowerUp>().GetPowerUp() == POWERUPS.FIREBALL)
        {
            if(SoundToPlay != "")
                SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(SoundToPlay);
            PlayerObject.SendMessage("ShootFireball");
        }
        else
        {
            Debug.Log("Either powerup doesn't exist or powerup doesn't equals to fireball");
        }
    }
}
