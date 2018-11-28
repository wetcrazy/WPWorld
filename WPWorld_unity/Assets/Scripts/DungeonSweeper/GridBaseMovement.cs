using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBaseMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject blockObj1;
    [SerializeField]
    private GameObject blockObj2;  
    [SerializeField]
    private Vector3 movementDir;
    [SerializeField]
    private float movementSpeed;

    private Rigidbody rb;
    Joystick joystickControl;
    private Vector3 respawnPos;

    // Distance Check
    public float distance = 0.0f;
    public float distanceCovered = 0.0f;
    public Vector3 lastBlockpos = Vector3.zero;
    // Direction
    public Joystick.JoystickDirection currDirection = Joystick.JoystickDirection.DIR_NONE;
    public Joystick.JoystickDirection nextDirection = Joystick.JoystickDirection.DIR_NONE;
    private bool isLose = false;
    private bool isWalking = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        joystickControl = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        gameObject.transform.forward = Vector3.forward;
        respawnPos = this.transform.position;
        distance = Vector3.Distance(blockObj1.transform.localPosition, blockObj2.transform.localPosition);

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}
	
    void FixedUpdate()
    {
        if(!isLose)
        {
            if(isWalking)
            {
                rb.MovePosition(rb.position + movementDir * movementSpeed * Time.fixedDeltaTime);
            }

            if (distanceCovered >= distance)
            {
                currDirection = Joystick.JoystickDirection.DIR_NONE;
                distanceCovered = 0.0f;
                lastBlockpos = this.transform.localPosition;
            }
        }    
    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        // Joystick Input
        if (DragInfo.Equals(Vector4.zero))
        {
            movementDir = Vector4.zero;
            return;
        }

        if(currDirection == Joystick.JoystickDirection.DIR_NONE)
        {
            currDirection = (Joystick.JoystickDirection)DragInfo.w;
            lastBlockpos = this.transform.localPosition;
        }

        Vector3 n_Forward;

        // Rotates the player to the designated forward looking area
        switch ((Joystick.JoystickDirection)DragInfo.w)
        {
            case Joystick.JoystickDirection.DIR_FORWARD:
                {
                    if ((-45 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 45)
                        || (135 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 225))
                        n_Forward = Camera.main.transform.forward;
                    else
                        n_Forward = Camera.main.transform.up;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = n_Forward;
                    break;
                }
            case Joystick.JoystickDirection.DIR_RIGHT:
                {
                    n_Forward = Camera.main.transform.right;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = n_Forward;
                    break;
                }
            case Joystick.JoystickDirection.DIR_LEFT:
                {
                    n_Forward = Camera.main.transform.right;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = -n_Forward;
                    break;
                }
            case Joystick.JoystickDirection.DIR_BACK:
                {
                    if ((-45 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 45)
                        || (135 < Camera.main.transform.eulerAngles.x && Camera.main.transform.eulerAngles.x < 225))
                        n_Forward = Camera.main.transform.forward;
                    else
                        n_Forward = Camera.main.transform.up;
                    n_Forward.y = 0;
                    if (Mathf.Abs(n_Forward.x) > Mathf.Abs(n_Forward.z))
                    {
                        //Prioritize X Dir over Z dir
                        if (n_Forward.x > 0)
                            n_Forward.x = Mathf.Round(n_Forward.x);
                        else
                            n_Forward.x = Mathf.Floor(n_Forward.x);

                        n_Forward.z = 0;
                    }
                    else
                    {
                        //Prioritize Z Dir over X dir
                        if (n_Forward.z > 0)
                            n_Forward.z = Mathf.Round(n_Forward.z);
                        else
                            n_Forward.z = Mathf.Floor(n_Forward.z);

                        n_Forward.x = 0;
                    }
                    gameObject.transform.forward = -n_Forward;
                    break;
                }
            default:
                break;
        }

        distanceCovered = Vector3.Distance(lastBlockpos, this.transform.position);
        nextDirection = (Joystick.JoystickDirection)DragInfo.w;

        if (distanceCovered < distance)
        {
            if (currDirection != nextDirection)
            {
                isWalking = false;
            }
            else
            {
                isWalking = true;    
            }
        }

        //Move towards the new direction the player is facing
        movementDir = gameObject.transform.forward;
    }


    // Respawner
    public void Respawn()
    {     
        this.transform.position = respawnPos;
    }
    public void SetIsLose(bool _Boolvalue)
    {
        isLose = _Boolvalue;
    }
}
