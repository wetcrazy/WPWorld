using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBlock : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private Vector3 FirstPoint;
    private Vector3 FirstPatrolPoint;

    [SerializeField]
    private Vector3 SecondPoint;
    private Vector3 SecondPatrolPoint;

    private bool TravelToSecond = true;

    [SerializeField]
    private bool CollideToStart;

    private bool HasCollided;

    [SerializeField]
    private Vector3 LocalScale;
    [SerializeField]
    private Vector3 OrgScale;

	// Use this for initialization
	void Start () {
        FirstPatrolPoint = this.transform.position + FirstPoint;
        SecondPatrolPoint = this.transform.position + SecondPoint;
	}
	
	// Update is called once per frame
	void Update () {
        if (CollideToStart && !HasCollided)
        {
            return;
        }

        if (TravelToSecond)
        {
            if(Vector3.Distance(transform.position, SecondPatrolPoint) > transform.lossyScale.magnitude)
            {
                transform.position = Vector3.Lerp(transform.position, SecondPatrolPoint, Time.deltaTime * MovementSpeed);
            }
            else
            {
                TravelToSecond = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, FirstPatrolPoint) > transform.lossyScale.magnitude)
            {
                transform.position = Vector3.Lerp(transform.position, FirstPatrolPoint, Time.deltaTime * MovementSpeed);
            }
            else
            {
                TravelToSecond = true;
            }
        }
	}

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(CollideToStart)
                HasCollided = true;

            if(OrgScale == Vector3.zero)
            {
                OrgScale = collision.gameObject.transform.lossyScale;
            }
            if(LocalScale == Vector3.zero && collision.gameObject.transform.lossyScale != collision.gameObject.transform.localScale)
            {
                LocalScale = collision.gameObject.transform.localScale;
            }

            collision.gameObject.transform.parent = transform;

            if(Vector3.Distance(collision.gameObject.transform.localScale, LocalScale) < 0.001f)
            {
                collision.gameObject.transform.localScale = LocalScale;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
            collision.gameObject.transform.localScale = OrgScale;
        }
    }
}
