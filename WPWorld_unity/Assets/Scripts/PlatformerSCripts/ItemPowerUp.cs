using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemPowerUp : MonoBehaviour {

    [SerializeField]
    private POWERUPS PowerUp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(PowerUp)
        {
            case (POWERUPS.SPEED):
                break;
            case (POWERUPS.FIREBALL):
                break;
            case (POWERUPS.SUPERJUMP):
                break;
            case (POWERUPS.INVISIBILITY):
                break;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        switch (PowerUp)
        {
            case (POWERUPS.SPEED):
                break;
            case (POWERUPS.FIREBALL):
                break;
            case (POWERUPS.SUPERJUMP):
                break;
            case (POWERUPS.INVISIBILITY):
                break;
        }
    }
}
