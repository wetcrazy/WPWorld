using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnCollide : MonoBehaviour {

    private Rigidbody RigidRef;
    private Renderer RenderRef;
    private Collider ColliderRef;

    private bool Falling;
    private Vector3 OrgPos;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        RenderRef = GetComponent<Renderer>();
        ColliderRef = GetComponent<Collider>();

        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
        RigidRef.useGravity = true;
        OrgPos = transform.position;
        Falling = false;
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
        {
            RenderRef.enabled = false;
            RigidRef.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, transform.localScale.y)
            || Physics.Raycast(transform.position, transform.up + transform.forward * 0.5f, out hit, transform.localScale.y)
            || Physics.Raycast(transform.position, transform.up - transform.forward * 0.5f, out hit, transform.localScale.y)
            || Physics.Raycast(transform.position, transform.up + transform.right * 0.5f, out hit, transform.localScale.y)
            || Physics.Raycast(transform.position, transform.up - transform.right * 0.5f, out hit, transform.localScale.y))
        {
            if (hit.transform.tag == "Player")
            {
                ColliderRef.isTrigger = true;
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    public void Reset()
    {
        ColliderRef.isTrigger = false;
        RenderRef.enabled = true;
        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = OrgPos;
    }
}
