using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Objective : MonoBehaviour {

    // Use this for initialization
    private Renderer Renderout;
    
    void Start () {
        //set the sphere to false
        //gameobject.setactive(false);
        Renderout = GetComponent<Renderer>();
        Renderout.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//check if the coins are collected
        
            var foundobjs = FindObjectsOfType<DestroyWhenCollide>();
            Debug.Log(foundobjs + " : " + foundobjs.Length);

            if (foundobjs.Length == 0)
            {
                Renderout.enabled = true;
            }
       

        

        //gameobject.setactive(True);
	}
}
