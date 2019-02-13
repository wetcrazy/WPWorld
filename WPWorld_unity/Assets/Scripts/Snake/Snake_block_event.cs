using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_block_event : MonoBehaviour {
    float life_cooldown;
    bool life_decay;
	// Use this for initialization
	void Start () {

        life_cooldown = 5.0f;
        life_decay = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        //this.gameObject.transform.Translate()
        gameObject.transform.position += (-(gameObject.transform.up) * 0.01f);
        if (life_decay)
        {

            life_cooldown -= Time.deltaTime;
            if (life_cooldown <= 0)
            {
                    Destroy(this.gameObject);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blocks"))
        {
            life_decay = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blocks"))
        {
            life_decay = true;
        }
    }
}
