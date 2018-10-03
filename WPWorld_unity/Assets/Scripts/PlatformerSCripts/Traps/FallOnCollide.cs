using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnCollide : MonoBehaviour {

    private Rigidbody RigidRef;
    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();

        RenderRef = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
        {
            Debug.Log("Stopped");
            RenderRef.enabled = false;
            RigidRef.useGravity = false;

            RigidRef.constraints = RigidbodyConstraints.FreezeAll;

            foreach(Transform n_Child in transform)
            {
                n_Child.GetComponent<Renderer>().enabled = false;
                Physics.IgnoreCollision(n_Child.GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
            }

            Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y - collision.gameObject.transform.lossyScale.y / 2
                >= transform.position.y + transform.lossyScale.y / 2)
            {
                RigidRef.useGravity = true;

                RigidRef.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        else if (collision.gameObject.tag != "Enemy")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
