using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollisionRef = collision.gameObject;

        if(CollisionRef.tag == "Player")
        {
            CollisionRef.GetComponent<TPSLogic>().Death();
        }
    }
}
