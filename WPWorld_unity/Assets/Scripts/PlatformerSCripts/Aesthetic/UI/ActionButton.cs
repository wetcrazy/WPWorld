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
        //foreach(GameObject n_Player in GameObject.FindGameObjectsWithTag("Player"))
        //{
        //    if (!n_Player.GetComponent<TPSLogic>().isMine())
        //        continue;
        //    PlayerObject = n_Player;
        //    break;
        //}
        //// Remove this when testing with multiplayer
        //PlayerObject = GameObject.FindGameObjectWithTag("Player");

        PlayerObject = PlayerMovement.LocalPlayerInstance;
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem");
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
