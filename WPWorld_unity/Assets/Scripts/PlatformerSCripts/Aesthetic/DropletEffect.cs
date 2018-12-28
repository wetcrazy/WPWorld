using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Block"))
        {
            Destroy(this.gameObject);
        }
    }
}
