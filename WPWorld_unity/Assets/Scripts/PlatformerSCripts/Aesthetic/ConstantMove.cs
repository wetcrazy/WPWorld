using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MOVEDIRECTION
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT
}

public class ConstantMove : MonoBehaviour {

    [SerializeField]
    private MOVEDIRECTION CurrMove;


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
            other.GetComponent<PlayerMovement>().SetRestriction(MovementRestrict.BOTH);
            other.GetComponent<TPSLogic>().SetJumpRestrict(true);
            switch(CurrMove)
            {
                case (MOVEDIRECTION.FORWARD):
                    other.GetComponent<Rigidbody>().MovePosition(other.GetComponent<Rigidbody>().position + other.transform.forward * other.GetComponent<PlayerMovement>().GetMovementSpeed() * Time.fixedDeltaTime);
                    break;
                case (MOVEDIRECTION.BACK):
                    other.GetComponent<Rigidbody>().MovePosition(other.GetComponent<Rigidbody>().position - other.transform.forward * other.GetComponent<PlayerMovement>().GetMovementSpeed() * Time.fixedDeltaTime);
                    break;
                case (MOVEDIRECTION.LEFT):
                    other.GetComponent<Rigidbody>().MovePosition(other.GetComponent<Rigidbody>().position - other.transform.right * other.GetComponent<PlayerMovement>().GetMovementSpeed() * Time.fixedDeltaTime);
                    break;
                case (MOVEDIRECTION.RIGHT):
                    other.GetComponent<Rigidbody>().MovePosition(other.GetComponent<Rigidbody>().position + other.transform.right * other.GetComponent<PlayerMovement>().GetMovementSpeed() * Time.fixedDeltaTime);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().SetRestriction(MovementRestrict.X_ONLY);
            other.GetComponent<TPSLogic>().SetJumpRestrict(false);
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
