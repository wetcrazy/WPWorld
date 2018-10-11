using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TETRISbehaviour : MonoBehaviour {

   // private bool maystop;
    private bool firstcollision;
    private bool collided;
    private bool magic;
    List<Vector3> directions;
    int dirnum;

    Vector3 myleft;
    Vector3 myright;
    Vector3 myfront;
    Vector3 myback;
    //Vector3 mydown;
    // Use this for initialization
    void Start () {
        collided = false;
        firstcollision = false;
        magic = false;

        directions = new List<Vector3>();
        myleft = Vector3.left;
        myright = Vector3.right;
        myfront = Vector3.forward;
        myback = Vector3.back;
       // mydown = Vector3.down;
        
        SetDirections();
        //StartCoroutine(Stall());
    }
	
	// Update is called once per frame
	void Update () {
        if (!collided &&!firstcollision) 
        {
         GetComponent<Rigidbody>().MovePosition(transform.position + Vector3.down * 0.5f * Time.deltaTime);
        }
        else if(firstcollision && collided && !magic)
        {
            AdjustTransform();
            magic = true;
           // StartCoroutine(Stall());
           // Debug.Log("blob");
        }
        //else if (magic)
        //{
        //    AdjustTransform();
        //    StartCoroutine(Stall());
        //}

    }
   
    IEnumerator Stall()
    {
        yield return new WaitForSeconds(5);
       // magic = true;
       // RandomTranslation();
        //this.gameObject.transform.Translate(directions[dirnum] *0.1f ,Space.World);
      //  Debug.Log("tried moving");
        //  collided = false;
        StartCoroutine(Stall());
        // maystop = true;
        // print(Time.time);
        // StartCoroutine(Example());
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Block"))
    //    {
    //        if (firstcollision)
    //        {
    //            Debug.Log("Im still colliding on trigger");
    //        }
    //        else
    //        {

    //            Debug.Log("imcolliding for the first time on trigger");
    //        }
    //        collided = true;
    //        //maystop = true;
    //        firstcollision = true;
    //        //Debug.Log("I AM GAY"  + collision.gameObject.name);
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if(firstcollision)
            {
                Debug.Log("Im still colliding");
            }
            else
            {

                Debug.Log("imcolliding for the first time");
            }
            collided = true;
            //maystop = true;
            firstcollision = true;
             

              //Debug.Log("I AM GAY"  + collision.gameObject.name);
        }
        if(collision.gameObject.CompareTag("OOB"))//out of bounds
        {
            Debug.Log("hi im outside");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("oh snap stay");
        }
    }

    void AdjustTransform()
    {
       // firstcollision = true;
        float mypos = this.gameObject.transform.position.y;
        // Debug.Log(mypos);
        float fix;
        fix = Mathf.Round(this.gameObject.transform.position.y * 10f) / 10f;
        float mynewpos = fix - mypos;
        //Debug.Log("HEEELLLLOOOO     " + mypos);
        this.gameObject.transform.Translate( 0, mynewpos,0,Space.World);
       // Debug.Log(this.gameObject.transform.position.y);
    }

    void SetDirections()
    {
        directions.Add( myleft  );
        directions.Add( myright );
        directions.Add( myfront );
        directions.Add( myback  );
        //directions.Add(mydown);
    }
    void RandomTranslation()
    {
         dirnum = Random.Range(0, 4);
       // Debug.Log("tried moving "+gameObject.name+"   " + dirnum + "   " + Time.time);
    }
}
