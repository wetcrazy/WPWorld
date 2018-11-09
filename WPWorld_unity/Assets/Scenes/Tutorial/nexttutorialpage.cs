using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nexttutorialpage : MonoBehaviour {

    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;
    public GameObject f;
    public GameObject g;
    public GameObject h;
    public GameObject I;
    public float WaitTime;

    List<GameObject> Pages = new List<GameObject>();
    // Use this for initialization
    private IEnumerator coroutine;

    void Start()
    {
        
        Pages.Add(a);
        Pages.Add(b);
        Pages.Add(c);
        Pages.Add(d);
        Pages.Add(e);
        Pages.Add(f);
        for(int i = 0; i< Pages.Count; i++)
        {
            Pages[i].SetActive(false);
        }
        // - After 0 seconds, prints "Starting 0.0"
        // - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
        // - After 2 seconds, prints "WaitAndPrint 2.0"
        // print("Starting " + Time.time);
        if(WaitTime< 1)
        {
            WaitTime = 2f;
        }
        // Start function WaitAndPrint as a coroutine.

        coroutine = WaitAndPrint(WaitTime);
        StartCoroutine(coroutine);

        //print("Before WaitAndPrint Finishes " + Time.time);
    }

    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            for (int i = 0; i < Pages.Count; i++)
            {
                Pages[i].SetActive(true);
                yield return new WaitForSeconds(waitTime);
                Pages[i].SetActive(false);
            }
                // print("WaitAndPrint " + Time.time);
        }
    }

    //IEnumerator Start()
    //{
    //    // - After 0 seconds, prints "Starting 0.0"
    //    // - After 2 seconds, prints "WaitAndPrint 2.0"
    //    // - After 2 seconds, prints "Done 2.0"
    //    print("Starting " + Time.time);

    //    // Start function WaitAndPrint as a coroutine. And wait until it is completed.
    //    // the same as yield WaitAndPrint(2.0);
    //    yield return StartCoroutine(WaitAndPrint(2.0F));
    //    print("Done " + Time.time);
    //}

    //// suspend execution for waitTime seconds
    //IEnumerator WaitAndPrint(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    print("WaitAndPrint " + Time.time);
    //}
    // Update is called once per frame
    void Update () {
		
	}
}

