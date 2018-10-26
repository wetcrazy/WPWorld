using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggercheck : MonoBehaviour {
    public GameObject a;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            a.SendMessage("mayIsmash");
    
        }
        else
        {
            a.SendMessage("returntosender");
        }
    }
}
