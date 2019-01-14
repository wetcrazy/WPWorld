using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingPoint : MonoBehaviour {

    [SerializeField]
    private float RaycastDistance;

    [SerializeField]
    private GameObject SpawnedPoint;

    [SerializeField]
    private GameObject CrosshairUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, RaycastDistance))
        {
            Vector3 n_Pos = hit.point;

            // See if it's aligned with z axis or x axis and lock one of the positions;

            if (SpawnedPoint != null)
                SpawnedPoint.transform.position = n_Pos;
            else
                SpawnedPoint = Instantiate(SpawnedPoint, n_Pos, Quaternion.identity);
        }

        Debug.DrawRay(transform.position, transform.forward * RaycastDistance);
	}
}
