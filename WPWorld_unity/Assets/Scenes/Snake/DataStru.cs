using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataStru : MonoBehaviour {
    public GameObject a;
    public GameObject b;
    Vector3 prevpos;
    Queue<GameObject> myQueue = new Queue<GameObject>();
    Queue<GameObject> tempqueue = new Queue<GameObject>();
	// Use this for initialization
	void Start () {
        myQueue.Enqueue(this.gameObject);
        myQueue.Enqueue(a);
        myQueue.Enqueue(b);
        prevpos = RoundoffFix(this.gameObject.transform.position);
    }
    // Update is called once per frame
    void Update ()
    {
        Vector3 distance = prevpos - this.gameObject.transform.position;
        if (distance.magnitude >= 1 )
        {
            QUeueOrder();
        }
    }
    Vector3 RoundoffFix(Vector3 T)
    {
        var posx = Mathf.RoundToInt(T.x);
        var posy = Mathf.RoundToInt(T.y);
        var posz = Mathf.RoundToInt(T.z);
        Vector3 S = new Vector3(posx, posy, posz);
        return S;
    }
    private void QUeueOrder()
    {
        Debug.Log(myQueue.Count);
        while(myQueue.Count>0)
        {
            var V = myQueue.Dequeue();
            if (!V.GetComponent<MyplayerScript>())
            {
             
                Vector3 temp = RoundoffFix(V.transform.position);
                V.transform.Translate(prevpos - V.transform.position);
                prevpos = temp;
            }
            tempqueue.Enqueue(V);


        }
        prevpos = RoundoffFix(this.gameObject.transform.position);
        while(tempqueue.Count>0)
        {
            var V = tempqueue.Dequeue();
            myQueue.Enqueue(V);
        }
    }
}
