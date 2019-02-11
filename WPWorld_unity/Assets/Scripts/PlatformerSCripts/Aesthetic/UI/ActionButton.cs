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
        PlayerObject = PlayerMovement.LocalPlayerInstance;

        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem");
    }

    void Update()
    {
        if (PlayerObject == null)
        {
            PlayerObject = PlayerMovement.LocalPlayerInstance;
        }
    }

    public void onActionButtonDown()
    {
        if(PlayerObject.GetComponent<TPSLogic>().LeverRef != null)
        {
            PlayerObject.GetComponent<TPSLogic>().LeverRef.Activate();
        }
        else
        {
            if (PlayerObject.GetComponent<PlayerPowerUp>() && PlayerObject.GetComponent<PlayerPowerUp>().GetPowerUp() == POWERUPS.FIREBALL)
            {
                if (SoundToPlay != "")
                    SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(SoundToPlay);
                PlayerObject.SendMessage("ShootFireball");
            }
        }
    }
}
