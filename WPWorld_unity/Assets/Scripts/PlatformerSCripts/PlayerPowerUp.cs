﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POWERUPS
{
    NONE,
    SUPERSPEED,
    FIREBALL,
    SUPERJUMP,
    INVISIBILITY,
    INSTANTDEATH,
}

public class PlayerPowerUp : MonoBehaviour {

    [SerializeField]
    private POWERUPS CurrPowerUp;

    [SerializeField]
    private float SuperMovementSpeed;

    [SerializeField]
    private float SuperJumpSpeed;

    [SerializeField]
    private float FireballDelay;

    [SerializeField]
    private GameObject FireBallPrefab;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (CurrPowerUp)
        {
            case (POWERUPS.SUPERSPEED):
                GetComponent<PlayerMovement>().SetMovementSpeed(SuperMovementSpeed);
                break;
            case (POWERUPS.FIREBALL):
                if(TimeElapsed < 0)
                {
                    TimeElapsed = 0;
                }
                else
                {
                    TimeElapsed -= Time.deltaTime;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(TimeElapsed <= 0)
                    {
                        Vector3 SpawnPosition = transform.position + (transform.forward * 0.1f);

                        GameObject n_Fireball = Instantiate(FireBallPrefab, SpawnPosition, transform.rotation);

                        TimeElapsed += FireballDelay;
                    }
                }
                break;
            case (POWERUPS.SUPERJUMP):

                break;
            case (POWERUPS.INVISIBILITY):
                Color ColorRef = transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                ColorRef.a = Mathf.Lerp(ColorRef.a, 0, Time.deltaTime);

                for(int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(0).GetComponent<MeshRenderer>().material.color = ColorRef;
                }

                break;
            case (POWERUPS.INSTANTDEATH):
                GetComponent<TPSLogic>().Death();
                CurrPowerUp = POWERUPS.NONE;
                break;
        }
    }

    public void SetPowerUp(POWERUPS n_PowerUp)
    {
        CurrPowerUp = n_PowerUp;
    }

    public POWERUPS GetPowerUp()
    {
        return CurrPowerUp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollisionRef = collision.gameObject;

        if(CurrPowerUp == POWERUPS.SUPERJUMP)
        {
            if (CollisionRef.transform.position.y + CollisionRef.transform.lossyScale.y / 2
                >= transform.position.y - transform.lossyScale.y / 2 && !GetComponent<TPSLogic>().GetGrounded())
            {
                GetComponent<TPSLogic>().Death();
                CurrPowerUp = POWERUPS.NONE;
            }
        }
        else if (CurrPowerUp == POWERUPS.SUPERSPEED)
        {
            if (Mathf.Abs(CollisionRef.transform.position.y - transform.position.y) < CollisionRef.transform.lossyScale.y / 2)
            {
                GetComponent<TPSLogic>().Death();
                CurrPowerUp = POWERUPS.NONE;
            }
        }
    }
}
