using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_block_event : MonoBehaviour {
    float spawn_cooldown;
	// Use this for initialization
	void Start () {

        spawn_cooldown = 5.0f;
		
	}
	
	// Update is called once per frame
	void Update () {

        //this.gameObject.transform.Translate()
        spawn_cooldown -= Time.deltaTime;
        if(spawn_cooldown<=0)
        {
        //    Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blocks"))
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blocks"))
        {

        }
    }
}
