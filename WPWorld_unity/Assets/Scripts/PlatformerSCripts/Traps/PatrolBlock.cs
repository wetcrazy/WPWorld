using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBlock : MonoBehaviour {

    [SerializeField]
    private float MovementSpeed;

    [SerializeField]
    private GameObject FirstPatrolPoint;

    [SerializeField]
    private GameObject SecondPatrolPoint;

    private bool TravelToSecond = true;

    [SerializeField]
    private bool CollideToStart;

    private bool HasCollided;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CollideToStart && !HasCollided)
        {
            return;
        }

        if (TravelToSecond)
        {
            if(Vector3.Distance(transform.position, SecondPatrolPoint.transform.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, SecondPatrolPoint.transform.position, Time.deltaTime * MovementSpeed);
            }
            else
            {
                TravelToSecond = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, FirstPatrolPoint.transform.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, FirstPatrolPoint.transform.position, Time.deltaTime * MovementSpeed);
            }
            else
            {
                TravelToSecond = true;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HasCollided = true;
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.transform.parent = null;
    }
}
