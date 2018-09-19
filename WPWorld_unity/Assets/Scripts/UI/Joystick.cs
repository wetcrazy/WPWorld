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

    public void OnJoystickDown()
    {
        isDraggingJoystick = true;
    }

    public void OnJoyStickUp()
    {
        isDraggingJoystick = false;
        JoystickBall.transform.position = JoystickBackground.transform.position;
    }

    GameObject PlayerObject;
    // Use this for initialization
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
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

            //Send the dragging direction to the player
            PlayerObject.SendMessage("GetJoystickInput", MousePos - JoystickBackgroundPosition);

            if (Vector3.Distance(MousePos, JoystickBackgroundPosition) < JoystickBallDragLengthLimit)
            {
                //Snap the joystick ball pos to the cursor if within the joystick background space
                MousePos.y = JoystickBall.transform.position.y;
                //Keep the joystick ball in the same y-pos
                JoystickBall.transform.position = MousePos;
            }
        }
    }
    
}
