using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POWERUPS
{
    NONE,
    SUPERSPEED,
    SUPERJUMP,
    FIREBALL,
    INVISIBILITY,
    INSTANTDEATH,
}

public class PlayerPowerUp : MonoBehaviour {

    [Header("Debug Settings")]
    [SerializeField]
    private POWERUPS CurrPowerUp;

    [Header("Super Jump Settings")]
    [SerializeField]
    private float SuperMovementSpeed;
    private float OrgMovementSpeed;

    [Header("Super Jump Settings")]
    [SerializeField]
    private float SuperJumpForce;
    private float OrgJumpForce;

    [Header("Fireball Settings")]
    [SerializeField]
    private GameObject FireBallPrefab;
    [SerializeField]
    private float FireballDelay;
    [SerializeField]
    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        OrgMovementSpeed = GetComponent<PlayerMovement>().GetMovementSpeed();
        OrgJumpForce = GetComponent<TPSLogic>().GetJumpForce();
	}
	
	// Update is called once per frame
	void Update () {
        switch(CurrPowerUp)
        {
            case (POWERUPS.SUPERSPEED):
                break;
            case (POWERUPS.SUPERJUMP):
                break;
            case (POWERUPS.FIREBALL):
                if (TimeElapsed > 0)
                    TimeElapsed -= Time.deltaTime;
                else
                    TimeElapsed = 0;
                break;
            case (POWERUPS.INVISIBILITY):
                break;
            case (POWERUPS.INSTANTDEATH):
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

            TimeElapsed = FireballDelay;
        }
    }

    public void Reset()
    {
        GetComponent<PlayerMovement>().SetMovementSpeed(OrgMovementSpeed);
        GetComponent<TPSLogic>().SetJumpForce(OrgJumpForce);
        CurrPowerUp = POWERUPS.NONE;
    }
}
