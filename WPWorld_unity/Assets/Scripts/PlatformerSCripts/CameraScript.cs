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

    [SerializeField]
    private bool RestrictMovement;
    [SerializeField]
    private RESTRICTMOVE CurrRestriction;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(this.transform.position, FocusTarget.transform.position) > DistanceToWait)
        {
            if(RestrictMovement)
            {
                Vector3 NewPos = Vector3.Lerp(this.transform.position, FocusTarget.transform.position, Time.deltaTime * FollowSpeed);
                NewPos.z = this.transform.position.z;
                if(CurrRestriction == RESTRICTMOVE.X_Axis)
                {
                    NewPos.x = this.transform.position.x;
                }
                else if (CurrRestriction == RESTRICTMOVE.Y_Axis)
                {
                    NewPos.y = this.transform.position.y;
                }
                this.transform.position = NewPos;
            }
            else
            {
                Vector3 NewPos = Vector3.Lerp(this.transform.position, FocusTarget.transform.position, Time.deltaTime * FollowSpeed);
                NewPos.z = this.transform.position.z;
                this.transform.position = NewPos;
            }
        }
	}
}
