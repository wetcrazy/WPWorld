using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TETRISbehaviour : MonoBehaviour {

    private bool maystop;
    private bool once;
	// Use this for initialization
	void Start () {
        maystop = false;
        once = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!maystop)
        {
         //   this.gameObject.transform.Translate(transform.position + Vector3.down * 0.01f * Time.deltaTime);
         GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.down * 0.5f * Time.deltaTime);

        }
        else if(!once)
        {
            enditalr();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            maystop = true;
            //Debug.Log("I AM GAY"  + collision.gameObject.name);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            maystop = true;
              //Debug.Log("I AM GAY"  + collision.gameObject.name);
        }
    }

    void enditalr()
    {
        once = true;
        float mypos = this.gameObject.transform.position.y;
        // Debug.Log(mypos);
        float fix;
        fix = Mathf.Round(this.gameObject.transform.position.y * 10f) / 10f;
        float mynewpos = fix - mypos;
        //Debug.Log("HEEELLLLOOOO     " + mypos);
        this.gameObject.transform.Translate( 0, mynewpos,0,Space.World);
       // Debug.Log(this.gameObject.transform.position.y);
    }


}
