using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIconScript : MonoBehaviour {

    [SerializeField]
    private GameObject TimerTextRef;

    private TimerUI TimerRef;

	// Use this for initialization
	void Start () {
        TimerRef = TimerTextRef.GetComponent<TimerUI>();
	}
	
	// Update is called once per frame
	void Update () {
		if(TimerTextRef != null)
        {
            float Z_Rot = TimerRef.StartingTime % 100 * 3.6f;
            Vector3 new_Rot = transform.localEulerAngles;
            new_Rot.z = Z_Rot;
            transform.localEulerAngles = new_Rot;
        }
	}
}
