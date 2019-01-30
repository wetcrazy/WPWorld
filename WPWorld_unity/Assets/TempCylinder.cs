using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCylinder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localPosition = ARMultiplayerController._GroundObject.transform.forward;
	}
}
