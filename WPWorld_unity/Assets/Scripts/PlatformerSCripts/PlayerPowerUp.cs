using System.Collections;
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

    private POWERUPS PrevPowerUp;

    [SerializeField]
    private float SuperMovementSpeed;
    private float OrgMovementSpeed;

    [SerializeField]
    private float SuperJumpForce;
    private float OrgJumpForce;

    [SerializeField]
    private float FireballDelay;

    [SerializeField]
    private GameObject FireBallPrefab;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        PrevPowerUp = CurrPowerUp;

        OrgMovementSpeed = GetComponent<PlayerMovement>().GetMovementSpeed();
        OrgJumpForce = GetComponent<TPSLogic>().GetJumpForce();
	}
	
	// Update is called once per frame
	void Update () {
        if(CurrPowerUp != PrevPowerUp)
        {
            switch(CurrPowerUp)
            {
                case (POWERUPS.SUPERSPEED):
                    GetComponent<PlayerMovement>().SetMovementSpeed(SuperMovementSpeed);
                    break;
                case (POWERUPS.SUPERJUMP):
                    GetComponent<TPSLogic>().SetJumpForce(SuperJumpForce);
                    break;
                case (POWERUPS.INSTANTDEATH):
                    GetComponent<TPSLogic>().Death();
                    break;
            }
        }
        PrevPowerUp = CurrPowerUp;
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
            if (CollisionRef.transform.position.y + CollisionRef.transform.lossyScale.y / 2 >= transform.position.y - transform.lossyScale.y / 2
                && !GetComponent<TPSLogic>().GetGrounded())
            {
                GetComponent<TPSLogic>().Death();
            }
        }
        else if (CurrPowerUp == POWERUPS.SUPERSPEED)
        {
            if (Mathf.Abs(CollisionRef.transform.position.y - transform.position.y) < CollisionRef.transform.lossyScale.y / 2)
            {
                GetComponent<TPSLogic>().Death();
            }
        }
    }

    private void ShootFireball()
    {
        if (TimeElapsed <= 0)
        {
            Vector3 SpawnPosition = transform.position + (transform.forward * 0.1f);

            GameObject n_Fireball = Instantiate(FireBallPrefab, SpawnPosition, transform.rotation);

            TimeElapsed += FireballDelay;
        }
    }

    public void Reset()
    {
        GetComponent<PlayerMovement>().SetMovementSpeed(OrgMovementSpeed);
        GetComponent<TPSLogic>().SetJumpForce(OrgJumpForce);
        CurrPowerUp = POWERUPS.NONE;
    }
}
