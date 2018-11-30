using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 lookDir;

    private Joystick joyStick;
    private Rigidbody rb;
    
	void Start ()
    {
        joyStick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        lookDir = new Vector3();
	}
	
	
	void FixedUpdate ()
    {
		
	}

    public void GetJoystickInput(Vector4 DragInfo)
    {
        if (DragInfo == Vector4.zero)
        {          
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
    }
}
