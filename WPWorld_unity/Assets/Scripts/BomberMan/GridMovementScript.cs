using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Block;
    [SerializeField]
    private Vector3 lookDir;
    public Vector3 currlookDir;
    [SerializeField]
    private float movementSpeed = 1.0f;
    [SerializeField]
    private float movementMultiplier = 0.0f;

    private Joystick joyStick;
    private Rigidbody rb;
    public Vector3 target;
    private Vector3 respawnPoint;
    private Vector3 firstPos;
    Vector3 Targetstore;
    Vector3 StartPos;

    private bool ismoving;
    [SerializeField]
    private float movetime; // time it takes for  character to move
    float movetimer; // count from zero to movetime

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        joyStick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        lookDir = new Vector3();
        target = new Vector3();
        currlookDir = new Vector3();
        respawnPoint = this.transform.position;

        StartPos = transform.position;
        Targetstore = StartPos;
    }


    void FixedUpdate()
    {

    }

    private void Update()
    {
        if (ismoving)
        {
            movetimer += Time.deltaTime;
        }
        if (movetimer >= movetime)
        {
            StartPos = transform.position;
            Targetstore = this.transform.position + this.transform.forward;
            movetimer = 0;
            ismoving = false;
        }
            this.transform.position = Vector3.Lerp(StartPos , Targetstore, movetimer / movetime);


    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        if (DragInfo == Vector4.zero)
        {
            //  this.transform.position = target;
            lookDir = Vector3.zero;
            return;
        }
        else
        {
            ismoving = true;
        }

        Vector3 n_Forward = new Vector3();
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
                    lookDir = n_Forward;
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
                    lookDir = n_Forward;
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
                    lookDir = -n_Forward;
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
                    lookDir = -n_Forward;
                    break;
                }
            default:
                break;
        }


        this.transform.forward = lookDir;
<<<<<<< HEAD
        target = this.transform.position + lookDir * Block.transform.localScale.x;

=======
        target = this.transform.position + lookDir * Block.transform.localScale.x;   
>>>>>>> 4f713c622a8159067741784d8036181a1cd36155
    }
    public void SetMovementMultiplier(float _multiplier)
    {
        if (_multiplier > 1)
        {
            _multiplier = 1;
        }
        movementMultiplier = _multiplier;
    }

    public void SetRespawnPoint(Vector3 _newVec)
    {
        respawnPoint = _newVec;
    }

    public void Respawn()
    {
        this.transform.position = respawnPoint;
        this.transform.forward = Vector3.forward;
    }
}
