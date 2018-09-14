using UnityEngine;
using UnityEngine.UI;

public class PlayerFinal : MonoBehaviour {
    
    [SerializeField]
    float PlayerSpeed = 70.0f;
    [SerializeField]
    Image PlayerHealthBar;
    [SerializeField]
    float CurrentHealth = 100;
    [SerializeField]
    float MaximumHealth = 100;
    [SerializeField]
    float PlayerLoseHealthSpeed = 5;
    [SerializeField]
    float DebuffDuration = 5;

    float DebuffTimer = 0;
    SceneControlFinal SceneControllerScript = null;
    //bool isFalling = true;

    public enum DEBUFF_EFFECT
    {
        DEBUFF_NONE,
        DEBUFF_SLOW,  //Halves player speed
        DEBUFF_INVERT, //Invert player controls

        TOTAL_DEBUFF_EFFECT
    }

    public DEBUFF_EFFECT DebuffEffect;
    private int PlayerSpeedMultiplier = 1;

    // Use this for initialization
    void Start () {
        SceneControllerScript = GameObject.Find("Scripts").GetComponent<SceneControlFinal>();
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.01f, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {

        //Make player health gradually fall
        if (CurrentHealth > 0)
        {
            CurrentHealth -= PlayerLoseHealthSpeed * Time.deltaTime;
            PlayerHealthBar.rectTransform.localScale = new Vector3(CurrentHealth / MaximumHealth, 1, 1);
        }
        else if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            PlayerHealthBar.rectTransform.localScale = new Vector3(0, 1, 1);

            //Player dies
        }

        if(DebuffEffect != DEBUFF_EFFECT.DEBUFF_NONE)
        {
            //Countdown the debuff timer
            DebuffTimer -= Time.deltaTime;

            if(DebuffTimer <= 0)
            {
                //Revert to original settings
                switch (DebuffEffect)
                {
                    case DEBUFF_EFFECT.DEBUFF_SLOW:
                        {
                            PlayerSpeed *= 2;
                            break;
                        }
                    case DEBUFF_EFFECT.DEBUFF_INVERT:
                        {
                            PlayerSpeedMultiplier = 1;
                            break;
                        }
                    default:
                        break;
                }

                //Remove the debuff after time is up
                DebuffEffect = DEBUFF_EFFECT.DEBUFF_NONE;
            }
        }

        PlayerFall();
        KeyInput();
    }

    void KeyInput()
    {
        //NOTE: These are temporary controls for debugging purposes
        if(Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += (gameObject.transform.forward * PlayerSpeed * Time.deltaTime) * PlayerSpeedMultiplier;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position -= (gameObject.transform.forward * PlayerSpeed * Time.deltaTime) * PlayerSpeedMultiplier;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position -= (gameObject.transform.right * PlayerSpeed * Time.deltaTime) * PlayerSpeedMultiplier;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += (gameObject.transform.right * PlayerSpeed * Time.deltaTime) * PlayerSpeedMultiplier;
        }
    }

    void PlayerFall()
    {
        //Adds a constant force that pushes the player towards the center of the planet
        GetComponent<Rigidbody>().AddForce((SceneControllerScript.PlanetObject.transform.position - gameObject.transform.position).normalized * SceneControllerScript.GRAVITY);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision Enter");
    //    if (collision.gameObject == PlanetObject)
    //    {
    //        isFalling = false;
    //        Debug.Log("Player Has Stopped Falling");
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("Collision Exit");
    //    if (collision.gameObject == PlanetObject)
    //    {
    //        isFalling = true;
    //        Debug.Log("Player Has Started Falling");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Asteroid")
        {
            //Removes the asteroid
            Destroy(other.gameObject);
        }
        else if (other.name == "HealthPowerup")
        {
            CurrentHealth += other.gameObject.GetComponent<HealthPowerup>().HealthRegenAmount;
            other.gameObject.SetActive(false);

            if(CurrentHealth > MaximumHealth)
            {
                CurrentHealth = MaximumHealth;
            }

            //Assign a random debuff
            DebuffEffect = (DEBUFF_EFFECT)Random.Range((int)(DEBUFF_EFFECT.DEBUFF_NONE + 1), (int)(DEBUFF_EFFECT.TOTAL_DEBUFF_EFFECT));
            DebuffTimer = DebuffDuration;

            switch (DebuffEffect)
            {
                case DEBUFF_EFFECT.DEBUFF_SLOW:
                    {
                        PlayerSpeed *= 0.5f;
                        break;
                    }
                case DEBUFF_EFFECT.DEBUFF_INVERT:
                    {
                        PlayerSpeedMultiplier = -1;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
