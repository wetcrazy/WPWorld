using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBlock : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private Vector3 FirstPoint;
    [SerializeField]
    private Vector3 FirstPatrolPoint;

    [SerializeField]
    private Vector3 SecondPoint;
    [SerializeField]
    private Vector3 SecondPatrolPoint;

    private bool TravelToSecond = true;

    [SerializeField]
    private bool CollideToStart;

    private bool HasCollided;

    private Transform OrgParent;

	// Use this for initialization
	void Start () {
        FirstPatrolPoint = this.transform.localPosition + FirstPoint;
        SecondPatrolPoint = this.transform.localPosition + SecondPoint;
	}
	
	// Update is called once per frame
	void Update () {
        if (CollideToStart && !HasCollided)
        {
            return;
        }

        if (TravelToSecond)
        {
            if(Vector3.Distance(transform.localPosition, SecondPatrolPoint) > transform.lossyScale.magnitude * 0.5f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, SecondPatrolPoint, Time.deltaTime * MovementSpeed);
            }
            else
            {
                TravelToSecond = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.localPosition, FirstPatrolPoint) > transform.lossyScale.magnitude * 0.5f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, FirstPatrolPoint, Time.deltaTime * MovementSpeed);
            }
            else
            {
                TravelToSecond = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (CollideToStart)
                HasCollided = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if(CollidedObject.tag == "Player")
        {
            if(CollidedObject.transform.position.y - CollidedObject.transform.lossyScale.y / 2 >= transform.position.y + transform.lossyScale.y / 2)
            {
                if (CollideToStart)
                    HasCollided = true;

                OrgParent = CollidedObject.transform.parent;

                CollidedObject.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.tag == "Player")
        {
            CollidedObject.transform.parent = OrgParent;
        }
    }
}
