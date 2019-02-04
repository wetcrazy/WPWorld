using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIconScript : MonoBehaviour {

    [SerializeField]
    private GameObject TimerTextRef;

    [SerializeField]
    private GameObject SecondHandRef;

    [SerializeField]
    private GameObject FillRef;

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

            if(FillRef != null)
            {
                FillRef.GetComponent<Image>().fillAmount = 1 - TimerRef.StartingTime % 100 / 100;
            }
            if(SecondHandRef != null)
            {
                Z_Rot = TimerRef.StartingTime % 100 * 36;
                new_Rot = SecondHandRef.transform.localEulerAngles;
                new_Rot.z = Z_Rot;
                SecondHandRef.transform.localEulerAngles = new_Rot;
            }
        }
	}
}
