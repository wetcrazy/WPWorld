using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendYouUp : MonoBehaviour {
    public GameObject player;
    public float speed;
   
    bool collided;
	// Use this for initialization
	void Start () {
        collided = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(collided)
        player.transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collided = true;
            Debug.Log("HAHAHA.");
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        other.attachedRigidbody.useGravity = false;
    //        collided = true;
    //        Debug.Log("HAHAHAHHA");
    //    }
    //}
}
