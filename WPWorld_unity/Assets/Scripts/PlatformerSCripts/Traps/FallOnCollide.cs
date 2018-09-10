using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnCollide : MonoBehaviour {

    private Rigidbody RigidRef;
    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        RigidRef.constraints = RigidbodyConstraints.FreezeAll;

        RenderRef = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerRef = GameObject.FindGameObjectWithTag("Player");
        GameObject[] EnemyRefs = GameObject.FindGameObjectsWithTag("Enemy");

        if (!RenderRef.isVisible)
        {
            Physics.IgnoreCollision(PlayerRef.GetComponent<Collider>(), GetComponent<Collider>());
            foreach(GameObject Enemy in EnemyRefs)
            {
                Physics.IgnoreCollision(Enemy.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
        else
        {
            Physics.IgnoreCollision(PlayerRef.GetComponent<Collider>(), GetComponent<Collider>(), false);
            foreach (GameObject Enemy in EnemyRefs)
            {
                Physics.IgnoreCollision(Enemy.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }
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
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            }
        }
        if(collision.gameObject.tag == "Killbox")
        {
            RenderRef.enabled = false;
        }
    }
}
