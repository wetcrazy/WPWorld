using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {

    [SerializeField]
    Button JoystickBall;
    [SerializeField]
    Image JoystickBackground;

    public Vector3 JoystickDragDirection = Vector3.zero;
    bool isDraggingJoystick = false;

    
    Vector3 Up = new Vector3(0, 100, 0);
    public float FacingAngle;

    public void OnJoystickDown()
    {
        isDraggingJoystick = true;
    }

    public void OnJoyStickUp()
    {
        isDraggingJoystick = false;
        JoystickBall.transform.position = JoystickBackground.transform.position;
        JoystickDragDirection = Vector3.zero;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!isDraggingJoystick)
        {
            return;
        }

        Vector3 JoystickBackgroundPosition = JoystickBackground.transform.position;

        if (Application.platform == RuntimePlatform.Android)
        {
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector3 MousePos = Input.mousePosition;
            if (Vector3.Distance(MousePos, JoystickBackgroundPosition) < 100)
            {
                JoystickBall.transform.position = MousePos;
            }
            else
            {
                JoystickDragDirection = (MousePos - JoystickBackgroundPosition).normalized;
                FacingAngle = -Vector3.SignedAngle(JoystickDragDirection, Up, JoystickBall.transform.forward);

                JoystickBall.transform.position = JoystickBackgroundPosition + (Quaternion.AngleAxis(
                    FacingAngle, 
                    Vector3.forward) * Up);
            }
        }
    }
    
}
