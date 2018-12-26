using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrap : MonoBehaviour {

    [SerializeField]
    private float PushForce;

    [SerializeField]
    private float TimeDelay;
    private float TimeElapsed;

    [SerializeField]
    private GameObject ParticleToDrop;
    private float TimeToSpawn;
    private float TimeElapsedObj;

    private Vector3 OrgPos;
    private Rigidbody RigidRef;

	// Use this for initialization
	void Start () {
        OrgPos = transform.localPosition;

        RigidRef = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.y <= OrgPos.y)
        {
            RigidRef.constraints = RigidbodyConstraints.FreezePositionY;
            transform.localPosition = OrgPos;
            TimeElapsed += Time.deltaTime;

            if(TimeElapsed >= TimeDelay)
            {
                RigidRef.constraints = RigidbodyConstraints.None;
                RigidRef.AddForce(transform.up * PushForce, ForceMode.VelocityChange);
                TimeElapsed = 0;
            }
        }

        if(TimeElapsedObj >= TimeToSpawn)
        {
            TimeElapsedObj = 0;

            Instantiate(ParticleToDrop, transform);
        }
        else
        {
            TimeElapsedObj += Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Water"))
        {
            Debug.Log("Entered Water, Input Particle Here");
        }
    }
}
