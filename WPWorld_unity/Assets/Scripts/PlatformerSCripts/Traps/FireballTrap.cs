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
    [SerializeField]
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
            RigidRef.constraints = RigidbodyConstraints.FreezeAll;
            transform.localPosition = OrgPos;
            TimeElapsed += Time.deltaTime;

            if(TimeElapsed >= TimeDelay)
            {
                RigidRef.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                RigidRef.AddForce(Vector3.up * PushForce, ForceMode.VelocityChange);
                TimeElapsed = 0;
            }
        }
        else
        {
            if (TimeElapsedObj >= TimeToSpawn && RigidRef.velocity.y > 0 && ParticleToDrop != null)
            {
                TimeElapsedObj = 0;

                Instantiate(ParticleToDrop,
                    transform.position + new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x) * 0.25f, 0, Random.Range(-transform.localScale.x, transform.localScale.x) * 0.25f),
                    Quaternion.identity);
            }
        }

        if (RigidRef.velocity.y < 0)
            transform.eulerAngles = new Vector3(0, 0, -180) + transform.parent.parent.eulerAngles;
        else
            transform.eulerAngles = transform.parent.parent.eulerAngles;

        if (ParticleToDrop != null)
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
