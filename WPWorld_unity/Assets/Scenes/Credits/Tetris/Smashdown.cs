using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashdown : MonoBehaviour {

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public GameObject b;

    private Vector3 startingpos;
    bool startsmash;
	// Use this for initialization
	void Start () {
        startsmash = false;
        posOffset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	    if(startsmash)
        {
            transform.Translate(Vector3.down*0.05f,Space.World);
        }
        else
        {

        //transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
        }
        
	}


   
    public void mayIsmash()
    {
        startsmash = true;
        b.SetActive(true);
    }

    void returntosender()
    {
        this.transform.position = startingpos;
        startsmash = false;
        b.SetActive(false);
    }
}
