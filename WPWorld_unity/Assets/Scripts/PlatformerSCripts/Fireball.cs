using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private Rigidbody RigidRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();

        RigidRef.AddForce(transform.forward * 10);
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollisionRef = collision.gameObject;

        if(CollisionRef.tag == "Player" || CollisionRef.name.Contains("Enemy"))
        {
            Destroy(this.gameObject);
            if (CollisionRef.tag == "Player")
                CollisionRef.GetComponent<TPSLogic>().Death();
            else
            {
                CollisionRef.GetComponent<Enemy>().AirborneDeath();
            }
        }
        else
        {
            RigidRef.velocity = Vector3.zero;
            if(Mathf.Abs(transform.position.y - CollisionRef.transform.position.y) < CollisionRef.transform.localScale.y / 2.5f)
            {
                Debug.Log(CollisionRef.name + " LR, " + Mathf.Abs(transform.position.y - CollisionRef.transform.position.y) + " - " + CollisionRef.transform.localScale.y / 2.5f);
                transform.forward = -transform.forward;
            }
            else
            {
                Debug.Log(CollisionRef.name + " UD, " + Mathf.Abs(transform.position.y - CollisionRef.transform.position.y) + " - " + CollisionRef.transform.localScale.y / 2.5f);
                
                if(transform.position.y > CollisionRef.transform.position.y) // Down
                {
                    RigidRef.AddForce(transform.up * 20);
                }
                else // Up
                {
                    RigidRef.AddForce(-transform.up * 20);
                }
            }
            RigidRef.AddForce(transform.forward * 20);
        }
    }
}
