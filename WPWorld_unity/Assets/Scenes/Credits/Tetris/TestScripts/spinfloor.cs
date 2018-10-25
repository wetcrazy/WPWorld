using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinfloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(0, Time.deltaTime * 50, 0);
	}
}
