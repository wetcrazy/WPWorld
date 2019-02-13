using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScaleUI : MonoBehaviour {

    [Header("Size Settings")]
    [SerializeField]
    private Vector3 PulseScale;
    [SerializeField]
    private float PulseSpeed;

    private Vector3 OrgScale;
    private bool Expanding = true;

	// Use this for initialization
	void Start () {
        OrgScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(Expanding)
        {
            if(Vector3.Distance(transform.localScale,PulseScale) > 0.01f)
                transform.localScale = Vector3.MoveTowards(transform.localScale, PulseScale, PulseSpeed * Time.deltaTime);
            else
                Expanding = !Expanding;
        }
        else
        {
            if (Vector3.Distance(transform.localScale, OrgScale) > 0.01f)
                transform.localScale = Vector3.MoveTowards(transform.localScale, OrgScale, PulseSpeed * Time.deltaTime);
            else
                Expanding = !Expanding;
        }
	}
}
