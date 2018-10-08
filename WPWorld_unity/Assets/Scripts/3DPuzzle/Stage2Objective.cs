using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Objective : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//set the sphere to false
        //gameobject.setactive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//check if the coins are collected
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var foundobjs = FindObjectsOfType<CollectOnCollide>();
            Debug.Log(foundobjs + " : " + foundobjs.Length);
        }
        //gameobject.setactive(True);
	}
}
