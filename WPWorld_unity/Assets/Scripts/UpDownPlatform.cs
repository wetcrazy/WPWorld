using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour {

    public Transform MovePlatform;
    public Transform Startpos;
    public Transform EndPos;
    public Vector3 Newpos;
    public string CurrState;
    public float time;
    public float resettime ;
	// Use this for initialization
	void Start () {
        ChangeTarget();
        
	}
    private void FixedUpdate()
    {
        MovePlatform.position = Vector3.Lerp(MovePlatform.position, Newpos, time * Time.deltaTime); // move to designated position
    }

    // Update is called once per frame
   void ChangeTarget()
    {
        if(CurrState == "Moving to Up")
        {
            //change to down
            CurrState = "Moving to Down";
            Newpos = Startpos.position;
        }
        else if (CurrState == "Moving to Down")
        {
            //change to up after new pos is down
            CurrState = "Moving to Up";
            Newpos = EndPos.position;
        }

        else if (CurrState == "")
        {
            // if empty assume going up
            CurrState = "Moving to Up";
            Newpos = EndPos.position;
        }
        Invoke("ChangeTarget", resettime);
    }
}
