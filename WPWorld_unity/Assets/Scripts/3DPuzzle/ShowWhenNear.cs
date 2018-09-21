using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWhenNear : MonoBehaviour {
    public GameObject InvisWall;
    public Renderer torender;
	// Use this for initialization
	void Start () {
        torender.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        torender.enabled = true;
    }
}
