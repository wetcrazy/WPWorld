using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPad : MonoBehaviour {

    //For flat DPad surface movement
    //Send the movement direction vectors to playermovement script
    public void OnMoveUp()
    {
        PlayerObject.SendMessage("GetDPadInput", Vector3.forward);
    }

    public void OnMoveDown()
    {
        PlayerObject.SendMessage("GetDPadInput", -Vector3.forward);
    }

    public void OnMoveLeft()
    {
        PlayerObject.SendMessage("GetDPadInput", -Vector3.right);
    }

    public void OnMoveRight()
    {
        PlayerObject.SendMessage("GetDPadInput", Vector3.right);
    }

    public void OnDpadKeyUp()
    {
        //Tell Player to stop moving
        PlayerObject.SendMessage("GetDPadInput", Vector3.zero);
    }

    //-----For sphere planet level only-----//
    public void OnSphereMoveUp()
    {
        PlayerObject.SendMessage("GetDPadInput", "Up");
    }

    public void OnSphereMoveDown()
    {
        PlayerObject.SendMessage("GetDPadInput", "Down");
    }

    public void OnSphereMoveLeft()
    {
        PlayerObject.SendMessage("GetDPadInput", "Left");
    }

    public void OnSphereMoveRight()
    {
        PlayerObject.SendMessage("GetDPadInput", "Right");
    }

    public void OnSphereDpadKeyUp()
    {
        //Tell Player to stop moving
        PlayerObject.SendMessage("GetDPadInput", "None");
    }

    GameObject PlayerObject;
    // Use this for initialization
    void Start () {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }
}
