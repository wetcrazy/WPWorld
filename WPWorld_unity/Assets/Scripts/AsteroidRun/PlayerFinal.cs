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
    [SerializeField]
    float TurnSpeed = 0.5f;
    [SerializeField]
    GameObject JoystickObject;

    float DebuffTimer = 0;
    SceneControlFinal SceneControllerScript = null;
    Joystick JoysticControls;
    Vector3 PivotAxis;

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
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -0.02f, 0);
        JoysticControls = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
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
    }

    private void FixedUpdate()
    {
        PlayerFall();
        KeyInput();
    }

    void KeyInput()
    {
        //NOTE: These are temporary controls for debugging purposes
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.RotateAround(SceneControllerScript.PlanetObject.transform.position, gameObject.transform.right, PlayerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.RotateAround(SceneControllerScript.PlanetObject.transform.position, -gameObject.transform.right, PlayerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.RotateAround(SceneControllerScript.PlanetObject.transform.position, gameObject.transform.forward, PlayerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.RotateAround(SceneControllerScript.PlanetObject.transform.position, -gameObject.transform.forward, PlayerSpeed * Time.deltaTime);
        }
    }

    public void GetDPadInput(Vector3 newPivotAxis)
    {
        if(newPivotAxis.Equals(Vector3.zero))
        {
            //Player has stopped holding down a DPad key, so stop moving the player
            //isMoving = false;
        }
        else
        {
        //    //Player is moving
        //    isMoving = true;

            //Check if the pivot is the same as before. If so, dont need to change it again
            if(!PivotAxis.Equals(newPivotAxis))
            {
                PivotAxis = newPivotAxis;
            }
        }
    }

    void GetJoystickInput(Vector4 DragInfo)
    {
        //Joystick input has stopped
        if(DragInfo.Equals(Vector4.zero))
        {
            return;
        }

        float DragLength = new Vector3(DragInfo.x, DragInfo.y, DragInfo.z).magnitude;
        float DragAngle = DragInfo.w, RefAxis;

        //Cap the drag length to that maximum length to player can drag
        if (DragLength > JoysticControls.JoystickBallDragLengthLimit)
        {
            DragLength = JoysticControls.JoystickBallDragLengthLimit;
        }

        //Determine the reference axis based on the angle of joytsick is dragged at
        if(DragAngle <= 90)
        {
            RefAxis = 0;
        }
        else if(DragAngle <= 180)
        {
            RefAxis = 90;
        }
        else if(DragAngle <= 270)
        {
            RefAxis = 180;
        }
        else
        {
            RefAxis = 270;
        }
        
        //The fractional modifier that determines how much moving and turning
        //based on the direction & angle of joystick ball
        float Modifier = (DragAngle - RefAxis) / 90;
        float TempTurn, TempMove;

        if (RefAxis == 0 || RefAxis == 180)
        {
            //Assign the fractional modifier based on reference axis
            TempTurn = TurnSpeed * Modifier;
            TempMove = PlayerSpeed * (1 - Modifier);

            if(RefAxis == 180)
            {
                //Moving backwards, so just flip the movement speed
                TempMove = -TempMove;
            }
        }
        else
        {
            //Assign the fractional modifier based on reference axis
            TempTurn = TurnSpeed * (1 - Modifier);
            TempMove = PlayerSpeed * Modifier;

            if(RefAxis == 90)
            {
                //Moving backwards, so just flip the movement speed
                TempMove = -TempMove;
            }
        }

        if (DragAngle > 180) //Dragged to the left
        {
            //Player turns leftwards
            gameObject.transform.Rotate(new Vector3(0,1,0), -DragLength * TempTurn * Time.deltaTime, Space.Self);
        }
        else //Dragged to the right
        {
            //Player turns rightwards
            gameObject.transform.Rotate(new Vector3(0, 1, 0), DragLength * TempTurn * Time.deltaTime, Space.Self);
        }

        //Rotate the player around the pivot axis to simulate movement on a sphere
        gameObject.transform.RotateAround(SceneControllerScript.PlanetObject.transform.position, gameObject.transform.right, TempMove * Time.deltaTime);
    }

    void PlayerFall()
    {
        //Adds a constant force that pushes the player towards the center of the planet
        GetComponent<Rigidbody>().AddForce((SceneControllerScript.PlanetObject.transform.position - gameObject.transform.position).normalized * SceneControllerScript.GRAVITY);

    }
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Asteroid")
        {
            //Removes the asteroid
            Destroy(other.gameObject);
        }
        else if (other.name == "HealthPowerup")
        {
            //Add health to the health bar
            CurrentHealth += other.gameObject.GetComponent<HealthPowerup>().HealthRegenAmount;
            //Set health powerup gameobject to false
            other.gameObject.SetActive(false);

            //Prevent health overflow
            if(CurrentHealth > MaximumHealth)
            {
                CurrentHealth = MaximumHealth;
            }

            //Assign a random debuff
            DebuffEffect = (DEBUFF_EFFECT)Random.Range((int)(DEBUFF_EFFECT.DEBUFF_NONE + 1), (int)(DEBUFF_EFFECT.TOTAL_DEBUFF_EFFECT));
            DebuffTimer = DebuffDuration;

            //Change the values related to the debuff
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
