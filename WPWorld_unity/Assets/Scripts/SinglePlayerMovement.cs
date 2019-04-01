using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 movementDir;
    [SerializeField]
    private float movementSpeed = 0.5f;

    private Rigidbody rb;
    Joystick joystickControl;
    private Vector3 respawnPos;

    private bool isDisable = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystickControl = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        gameObject.transform.forward = Vector3.forward;
        respawnPos = this.transform.position;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementDir * movementSpeed * Time.fixedDeltaTime);
    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        // Joystick Input
        if (DragInfo.Equals(Vector4.zero) || isDisable)
        {      
            movementDir = Vector4.zero;
            return;
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
        //Move towards the new direction the player is facing
        movementDir = gameObject.transform.forward;
    }


    // Respawner
    public void Respawn()
    {
        this.transform.position = respawnPos;
    }
    // Lose Locker
    public void SetIsDisable(bool _Boolvalue)
    {
        isDisable = _Boolvalue;
    }
}
