using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    private bool Interacted;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(Interacted)
        {
            if(Vector3.Distance(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, new Vector3(1, 0.1f, 0)) < 0.1f)
            {
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = new Vector3(1, 0.1f, 0);
            }
            else
            {
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = Vector3.Lerp(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, new Vector3(1, 0.1f, 0), Time.deltaTime);
            }
            transform.GetChild(0).GetComponent<Cloth>().randomAcceleration = Vector3.zero;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Interacted = true;
            other.GetComponent<PlayerMovement>().SetRespawn(this.transform.position);
        }
    }
}
