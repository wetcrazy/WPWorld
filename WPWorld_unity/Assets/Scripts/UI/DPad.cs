using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPad : MonoBehaviour {

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
        PlayerObject.SendMessage("GetDPadInput", Vector3.zero);
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
