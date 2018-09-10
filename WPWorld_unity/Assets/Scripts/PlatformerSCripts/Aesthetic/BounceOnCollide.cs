using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnCollide : MonoBehaviour {

    private Rigidbody RigidRef;
    private Vector3 OrgPos;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();

        OrgPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(RigidRef.useGravity)
        {
            TimeElapsed += Time.deltaTime;

            if(TimeElapsed >= 0.1f)
            {
                if (Vector3.Distance(this.transform.position, OrgPos) < 0.01f)
                {
                    RigidRef.constraints = RigidbodyConstraints.FreezeAll;
                    RigidRef.useGravity = false;

                    this.transform.position = OrgPos;
                    TimeElapsed = 0;
                }
            }
        }

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y + collision.gameObject.transform.lossyScale.y / 2
                <= transform.position.y - transform.lossyScale.y / 2)
            {
                // Push Up
                RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                RigidRef.AddForce(new Vector3(0, 50, 0));
                RigidRef.useGravity = true;
            }
        }
    }
}
