using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnCollided : MonoBehaviour {
    private Rigidbody RigidRef;
    // Use this for initialization
    void Start () {
        RigidRef = GetComponent<Rigidbody>();
        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.Rotate(0, 0, 90);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.Rotate(0, 0, 90);
        }
    }
}
