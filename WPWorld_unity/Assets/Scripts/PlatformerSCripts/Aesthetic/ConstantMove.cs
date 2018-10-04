using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour {

    [SerializeField]
    private MovementRestrict RestrictUponLeave;

    [SerializeField]
    private float SetMovementSpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().SetRestriction(MovementRestrict.NONE);
            other.GetComponent<TPSLogic>().SetJumpRestrict(true);

            other.GetComponent<Rigidbody>().MovePosition(other.GetComponent<Rigidbody>().position + other.transform.forward * other.GetComponent<PlayerMovement>().GetMovementSpeed() * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().SetRestriction(RestrictUponLeave);
            other.GetComponent<TPSLogic>().SetJumpRestrict(false);

            GetComponent<Collider>().isTrigger = false;
        }
    }
}
