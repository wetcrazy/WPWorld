﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {

    [SerializeField]
    Button JoystickBall;
    [SerializeField]
    Image JoystickBackground;
    [SerializeField]
    bool is4Directional = false;

    bool isDraggingJoystick = false;

    public float JoystickBallDragLengthLimit = 70;
    Vector3 Up, Right;

    public void OnJoystickDown()
    {
        isDraggingJoystick = true;
    }

    public void OnJoyStickUp()
    {
        isDraggingJoystick = false;
        JoystickBall.transform.position = JoystickBackground.transform.position;

        //Send a message to player that joystick input has stopped
        PlayerObject.SendMessage("GetJoystickInput", Vector4.zero);

        joystickDirection = JoystickDirection.DIR_NONE;
    }

    GameObject PlayerObject;
    // Use this for initialization
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        Up = new Vector3(0, JoystickBallDragLengthLimit, 0);
        Right = new Vector3(JoystickBallDragLengthLimit, 0, 0);
    }
    

    private void Update()
    {
        if (!isDraggingJoystick)
        {
            return;
        }

        Vector3 JoystickBackgroundPosition = JoystickBackground.transform.position;
        Vector3 InputPos = Vector3.zero, DragDirection;

        if (Application.platform == RuntimePlatform.Android && Input.touchCount > 0)
        {
            InputPos = Input.GetTouch(0).position;        
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            InputPos = Input.mousePosition;
        }
        
        DragDirection = (InputPos - JoystickBackgroundPosition);
        float DragAngle = -Vector3.SignedAngle(Up, DragDirection.normalized, JoystickBackground.transform.forward);

        if (Vector3.Distance(InputPos, JoystickBackgroundPosition) != 0)
        {
            if (DragAngle < 0)
            {
                DragAngle += 360;
            }
        }

        if (is4Directional)
        {
            ContrainedJoystick(InputPos, JoystickBackgroundPosition, DragDirection, DragAngle);
        }
        else
        {
            FreeMoveJoystick(InputPos, JoystickBackgroundPosition, DragDirection, DragAngle);
        }
    }
    
    void FreeMoveJoystick(Vector3 InputPos, Vector3 JoystickBackgroundPosition, Vector3 DragDirection, float DragAngle)
    {
        if (Vector3.Distance(InputPos, JoystickBackgroundPosition) < JoystickBallDragLengthLimit)
        {
            //Snap the joystick ball pos to the cursor if within the joystick background space
            JoystickBall.transform.position = InputPos;
        }
        else
        {
            JoystickBall.transform.position = JoystickBackgroundPosition + (Quaternion.AngleAxis(-DragAngle, Vector3.forward) * Up);
        }

        Vector4 DataPacket = new Vector4(DragDirection.x, DragDirection.y, DragDirection.z, DragAngle);

        //Send the dragging direction and angle to the player
        PlayerObject.SendMessage("GetJoystickInput", DataPacket);
    }

    enum JoystickDirection
    {
        DIR_FORWARD,
        DIR_RIGHT,
        DIR_LEFT,
        DIR_BACK,

        DIR_NONE
    }

    JoystickDirection joystickDirection = JoystickDirection.DIR_NONE;

    void ContrainedJoystick(Vector3 InputPos, Vector3 JoystickBackgroundPosition, Vector3 DragDirection, float DragAngle)
    {
        float HalfLength = JoystickBallDragLengthLimit * 0.5f;

        if (Vector3.Distance(InputPos, JoystickBackgroundPosition) < JoystickBallDragLengthLimit)
        {
            Vector3 OldPos = JoystickBall.transform.position;

            //Determine which axis the ball is travelling on
            if (joystickDirection == JoystickDirection.DIR_NONE)
            {
                if(Vector3.Angle(Up, InputPos) <= 45)
                {
                    joystickDirection = JoystickDirection.DIR_FORWARD;
                }
                else if (Vector3.Angle(-Up, InputPos) <= 45)
                {
                    joystickDirection = JoystickDirection.DIR_BACK;
                }
                else if (Vector3.Angle(Right, InputPos) <= 45)
                {
                    joystickDirection = JoystickDirection.DIR_RIGHT;
                }
                else if (Vector3.Angle(-Right, InputPos) <= 45)
                {
                    joystickDirection = JoystickDirection.DIR_LEFT;
                }


                //if (JoystickBall.transform.position.y > JoystickBackgroundPosition.y)
                //{
                //    joystickDirection = JoystickDirection.DIR_FORWARD;
                //}
                //else if (JoystickBall.transform.position.y < JoystickBackgroundPosition.y)
                //{
                //    joystickDirection = JoystickDirection.DIR_BACK;
                //}
                //else if (JoystickBall.transform.position.x > JoystickBackgroundPosition.x)
                //{
                //    joystickDirection = JoystickDirection.DIR_RIGHT;
                //}
                //else if (JoystickBall.transform.position.x < JoystickBackgroundPosition.x)
                //{
                //    joystickDirection = JoystickDirection.DIR_LEFT;
                //}
            }
            else
            {
                if (InputPos.y > JoystickBackgroundPosition.y + HalfLength)
                {
                    joystickDirection = JoystickDirection.DIR_FORWARD;
                }
                else if (InputPos.y < JoystickBackgroundPosition.y - HalfLength)
                {
                    joystickDirection = JoystickDirection.DIR_BACK;
                }
                else if (InputPos.x > JoystickBackgroundPosition.x + HalfLength)
                {
                    joystickDirection = JoystickDirection.DIR_RIGHT;
                }
                else if (InputPos.x < JoystickBackgroundPosition.x - HalfLength)
                {
                    joystickDirection = JoystickDirection.DIR_LEFT;
                }
            }


            //Constrain the joysstick ball to the x and y axes only
            if (joystickDirection == JoystickDirection.DIR_LEFT || joystickDirection == JoystickDirection.DIR_RIGHT)
            {
                JoystickBall.transform.position.Set(InputPos.x, OldPos.y, OldPos.z);
            }
            else
            {
                JoystickBall.transform.position.Set(OldPos.x, InputPos.y, OldPos.z);
            }
        }

        Vector4 DataPacket = new Vector4(DragDirection.x, DragDirection.y, DragDirection.z, DragAngle);

        //Send the dragging direction and angle to the player
        PlayerObject.SendMessage("GetJoystickInput", DataPacket);
    }
}
