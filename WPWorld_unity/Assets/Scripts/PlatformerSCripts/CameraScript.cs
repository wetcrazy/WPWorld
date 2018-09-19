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

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 NewPos = Vector3.Lerp(this.transform.position, FocusTarget.transform.position, Time.deltaTime * FollowSpeed);
        NewPos.z = this.transform.position.z;
        this.transform.position = NewPos;
    }
}
