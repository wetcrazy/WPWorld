using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {

    [SerializeField]
    private Vector3 TurnAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.eulerAngles = TurnAngle;
        }
    }
}
