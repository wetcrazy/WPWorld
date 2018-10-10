using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour {

    [SerializeField]
    private MovementAvaliability RestrictUponLeave;

    [SerializeField]
    private float SetMovementSpeed;

    [SerializeField]
    private bool AfterLeaveX;

    [SerializeField]
    private bool AfterLeaveZ;

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
            other.GetComponent<PlayerMovement>().SetRestriction(MovementAvaliability.NONE);
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

            Vector3 PlayerPos = other.transform.position;
            if(AfterLeaveX)
                PlayerPos.x = transform.position.x;
            if(AfterLeaveZ)
                PlayerPos.z = transform.position.z;
            other.transform.position = PlayerPos;

            GetComponent<Collider>().isTrigger = false;
        }
    }
}
