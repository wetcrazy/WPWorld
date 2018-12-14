using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBasedOffRB : MonoBehaviour {

    private Rigidbody RigidRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(RigidRef.velocity);
	}
}
