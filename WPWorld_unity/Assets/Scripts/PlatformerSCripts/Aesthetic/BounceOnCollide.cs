﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnCollide : MonoBehaviour {

    private Rigidbody RigidRef;
    private Vector3 OrgPos;

    [SerializeField]
    private AudioClip BounceSFX;

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
                if (Vector3.Distance(this.transform.position, OrgPos) < 0.01f || this.transform.position.y < OrgPos.y)
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
        GameObject CollidedObject = collision.gameObject;

        if(CollidedObject.tag == "Player")
        {
            if (CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y / 2
                <= transform.position.y - transform.lossyScale.y / 2 && Mathf.Abs(CollidedObject.transform.position.x - transform.position.x) < transform.lossyScale.x / 2)
            {
                if (Vector3.Distance(this.transform.position, OrgPos) < 0.05f)
                {
                    if(!collision.gameObject.GetComponent<TPSLogic>().GetGrounded())
                    {
                        // Push Up
                        RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        RigidRef.AddForce(new Vector3(0, 50, 0));
                        RigidRef.useGravity = true;

                        if(BounceSFX != null)
                            GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(BounceSFX);
                    }
                }
            }
        }
    }
}
