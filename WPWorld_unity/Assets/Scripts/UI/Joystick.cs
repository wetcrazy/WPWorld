using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {

    [SerializeField]
    Button JoystickBall;
    [SerializeField]
    Image JoystickBackground;

    bool isDraggingJoystick = false;

    public float JoystickBallDragLengthLimit = 70;
    Vector3 Up;

    public void OnJoystickDown()
    {
        isDraggingJoystick = true;
    }

    public void OnJoyStickUp()
    {
        isDraggingJoystick = false;
        JoystickBall.transform.position = JoystickBackground.transform.position;

        PlayerObject.SendMessage("GetJoystickInput", Vector4.zero);
    }

    GameObject PlayerObject;
    // Use this for initialization
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        Up = new Vector3(0, JoystickBallDragLengthLimit, 0);
    }
    

    private void Update()
    {
        if (!isDraggingJoystick)
        {
            return;
        }

        Vector3 JoystickBackgroundPosition = JoystickBackground.transform.position;

        if (Application.platform == RuntimePlatform.Android)
        {
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Vector3 MousePos = Input.mousePosition;
            Vector3 DragDirection = (MousePos - JoystickBackgroundPosition);

            float DragAngle = -Vector3.SignedAngle(Up, DragDirection.normalized, JoystickBackground.transform.forward);

            if (Vector3.Distance(MousePos, JoystickBackgroundPosition) > 25)
            {
                if(DragAngle < 0)
                {
                    DragAngle += 360;
                }

                Vector4 DataPacket = new Vector4(DragDirection.x, DragDirection.y, DragDirection.z, DragAngle);

                //Send the dragging direction and angle to the player
                PlayerObject.SendMessage("GetJoystickInput", DataPacket);
            }

            if (Vector3.Distance(MousePos, JoystickBackgroundPosition) < JoystickBallDragLengthLimit)
            {
                //Snap the joystick ball pos to the cursor if within the joystick background space
                JoystickBall.transform.position = MousePos;
            }
            else
            {
                JoystickBall.transform.position = JoystickBackgroundPosition + (Quaternion.AngleAxis(-DragAngle, Vector3.forward) * Up);
            }
        }
    }
    
}
