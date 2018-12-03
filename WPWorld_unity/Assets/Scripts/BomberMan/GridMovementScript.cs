using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Block;
    [SerializeField]
    private Vector3 lookDir;
    [SerializeField]
    private float movementSpeed = 1.0f;
    [SerializeField]
    private float movementMultiplier = 0.0f;

    private Joystick joyStick;
    private Rigidbody rb;
    private Vector3 target;
    
	void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        joyStick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        lookDir = new Vector3();
        target = new Vector3();
	}


    void FixedUpdate()
    {    
        //Vector3 dir = target - this.transform.position;
        //dir.Normalize();
        //this.transform.position += dir * movementSpeed * movementMultiplier * Time.fixedDeltaTime;

        //Vector3 newPosition = rb.position + lookDir * movementSpeed * movementMultiplier * Time.fixedDeltaTime;
        //rb.MovePosition(newPosition); 
    }

    public void GetJoystickInput(Vector4 DragInfo)
    {
        if (DragInfo == Vector4.zero)
        {
            this.transform.position = target;
            lookDir = Vector3.zero;
            return;
        }
    
        switch ((Joystick.JoystickDirection)DragInfo.w)
        {
            case Joystick.JoystickDirection.DIR_FORWARD:
                lookDir = Vector3.forward;
                break;
            case Joystick.JoystickDirection.DIR_BACK:
                lookDir = -Vector3.forward;
                break;
            case Joystick.JoystickDirection.DIR_RIGHT:
                lookDir = Vector3.right;
                break;
            case Joystick.JoystickDirection.DIR_LEFT:
                lookDir = -Vector3.right;
                break;
        }

        this.transform.forward = lookDir;
        target = this.transform.position + lookDir * Block.transform.localScale.x;
    }

    public void SetMovementMultiplier(float n_Multiplier)
    {
        if (n_Multiplier > 1)
        {
            n_Multiplier = 1;
        }
        movementMultiplier = n_Multiplier;
    }
}
