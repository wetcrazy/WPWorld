using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectpush : MonoBehaviour {


    bool letmepush;
	// Use this for initialization
	void Start () {
        letmepush = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (letmepush)
        {
            this.gameObject.transform.Translate(Vector3.back * 0.01f, Space.World);
        }
        else
        {
            this.gameObject.transform.Translate(Vector3.down * 0.01f, Space.World);
        }
    }

    public void pushmealr()
    {
        letmepush = true;
    }
}
