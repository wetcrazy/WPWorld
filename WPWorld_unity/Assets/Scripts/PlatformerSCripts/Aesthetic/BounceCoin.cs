using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCoin : MonoBehaviour {

    private Rigidbody RigidRef;

    private Vector3 OrgPos;

    [SerializeField]
    private float UpwardForce;

    private float TimeElapsed;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
        OrgPos = transform.position;

        RigidRef.AddForce(transform.up * UpwardForce, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
        TimeElapsed += Time.deltaTime;

        if(TimeElapsed > 0.1f)
        {
            if(Vector3.Distance(OrgPos, transform.position) < 0.01f || OrgPos.y > transform.position.y)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
