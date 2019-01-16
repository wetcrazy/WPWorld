using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour {

    private Vector3 Curr_Rot;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = Vector3.Lerp(transform.forward, Vector3.forward, Time.deltaTime);
	}
}
