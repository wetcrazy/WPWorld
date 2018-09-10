using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private GameObject FocusTarget;

    [SerializeField]
    private float DistanceToWait;

    [SerializeField]
    private float FollowSpeed;

	// Use this for initialization
	void Start () {
		if(FocusTarget != null)
        {
            Vector3 NewPos = FocusTarget.transform.position;
            NewPos.x = this.transform.position.x;
            NewPos.z = this.transform.position.z;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(this.transform.position, FocusTarget.transform.position) > DistanceToWait)
        {
            float Temp_Z = this.transform.position.z;
            Vector3 NewPos = Vector3.Lerp(this.transform.position, FocusTarget.transform.position, Time.deltaTime * FollowSpeed);
            NewPos.z = Temp_Z;
            this.transform.position = NewPos;
        }
	}
}
