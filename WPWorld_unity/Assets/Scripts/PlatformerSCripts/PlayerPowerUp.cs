using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POWERUPS
{
    NONE,
    SPEED,
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (CurrPowerUp)
        {
            case (POWERUPS.SPEED):

                break;
            case (POWERUPS.FIREBALL):
                if (Input.GetKeyDown(KeyCode.F))
                {

                }
                break;
            case (POWERUPS.SUPERJUMP):
                GetComponent<PlayerMovement>().SetJumpSpeed(SuperJumpSpeed);
                break;
            case (POWERUPS.INVISIBILITY):
                Color ColorRef = GetComponent<MeshRenderer>().material.color;
                ColorRef.a = Mathf.Lerp(ColorRef.a, 0, Time.deltaTime);

                GetComponent<MeshRenderer>().material.color = ColorRef;
                break;
            default:
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
}
