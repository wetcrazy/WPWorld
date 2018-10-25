using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    bool startflying;
    // Use this for initialization
    void Start () {
         startflying = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(startflying)
        {
        this.gameObject.transform.Translate(0, 0.01f, 0);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            startflying = true;
        }
    }

    
}
