using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private Rigidbody RigidRef;

    private bool Triggered;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();

        RigidRef.AddForce(transform.right * 40);
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Triggered)
            return;

        GameObject CollisionRef = collision.gameObject;

        if(CollisionRef.tag == "Player")
        {
            Destroy(this.gameObject);
            CollisionRef.GetComponent<TPSLogic>().Death();
            return;
        }

        RigidRef.velocity = Vector3.zero;

        if(Mathf.Abs(CollisionRef.transform.position.y - transform.position.y) < CollisionRef.transform.lossyScale.y / 2)
        {
            transform.eulerAngles -= new Vector3(0, 180, 0);
            RigidRef.AddForce(transform.right * 40);
        }
        else
        {
            RigidRef.AddForce(transform.right * 40);
            RigidRef.AddForce(transform.up * 40);
        }

        Triggered = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!Triggered)
            return;

        Triggered = false;
    }
}
