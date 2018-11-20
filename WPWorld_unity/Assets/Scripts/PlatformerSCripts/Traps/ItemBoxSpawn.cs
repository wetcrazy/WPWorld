using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxSpawn : MonoBehaviour {

    private Vector3 TargetLocation;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        TargetLocation = transform.position;
        TargetLocation.y += transform.lossyScale.y;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().detectCollisions = false;
            GetComponent<Rigidbody>().useGravity = false;
        }
        if (GetComponent<Enemy>())
            GetComponent<Enemy>().enabled = false;
        if (GetComponent<Collider>())
            GetComponent<Collider>().enabled = false;
        if (GetComponent<GivePowerUpOnCollide>())
            GetComponent<GivePowerUpOnCollide>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(TargetLocation, transform.position) < transform.lossyScale.y * 0.05f || TargetLocation.y < transform.position.y)
        {
            transform.position = TargetLocation;
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().detectCollisions = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
            if (GetComponent<Enemy>())
                GetComponent<Enemy>().enabled = true;
            if (GetComponent<Collider>())
                GetComponent<Collider>().enabled = true;
            if (GetComponent<GivePowerUpOnCollide>())
                GetComponent<GivePowerUpOnCollide>().enabled = true;

            this.enabled = false;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, TargetLocation, 3.5f * Time.deltaTime);
        }
	}
}
