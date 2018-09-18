using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPad : MonoBehaviour {

    public void OnMoveUp()
    {
        PlayerObject.SendMessage("GetDPadInput", "Vertical");
    }

    public void OnMoveDown()
    {
        PlayerObject.SendMessage("GetDPadInput", "Vertical");
    }

    public void OnMoveLeft()
    {
        PlayerObject.SendMessage("GetDPadInput", "Horizontal");
    }

    public void OnMoveRight()
    {
        PlayerObject.SendMessage("GetDPadInput", "Horizontal");
    }

    public void OnDpadKeyUp()
    {
        PlayerObject.SendMessage("GetDPadInput", "None");
    }

    GameObject PlayerObject;
    // Use this for initialization
    void Start () {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
