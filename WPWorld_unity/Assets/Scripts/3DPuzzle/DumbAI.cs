using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAI : MonoBehaviour {

    public Transform MovePlatform;
    public Transform Startpos;
    public Transform EndPos;
    public Vector3 Newpos;
    public Vector3 OriginalPos;
    public string CurrState;
    public float time;
    public float resettime;
    public bool collideAI;
    // Use this for initialization
    void Start()
    {
        ChangeTarget();
        OriginalPos = gameObject.transform.position;
    }
    private void FixedUpdate()
    {
         
    }

    public void UpdateTheThing()
    {
        //MovePlatform.position = Vector3.Lerp(MovePlatform.position, Newpos, time * Time.deltaTime); // move to designated position
        MovePlatform.position = Vector3.MoveTowards(MovePlatform.position, Newpos, time * Time.deltaTime); // move to designated position
        if (MovePlatform.transform.position == Newpos)
        {
            ChangeTarget();
        }
    }

    // Update is called once per frame
    void ChangeTarget()
    {
        if (CurrState == "Moving to right")
        {
            //change to down
            CurrState = "Moving to left";
            Newpos = Startpos.position;
        }
        else if (CurrState == "Moving to left")
        {
            //change to up after new pos is down
            CurrState = "Moving to right";
            Newpos = EndPos.position;
        }

        else if (CurrState == "")
        {
            // if empty assume going up
            CurrState = "Moving to right";
            Newpos = EndPos.position;
        }
        //Invoke("ChangeTarget", resettime);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //call reset
            Debug.Log("Oh ho, too bad");
            collideAI = true;
        }
    }

    public bool GetCollideAI()
    {
        return collideAI;
    }

    public void SetCollideAI(bool Collided)
    {
        collideAI = Collided;
    }

    public Transform GetStartPos()
    {
        return Startpos;
    }

    public void SetStartPos(Transform _Startpos)
    {
        Startpos = _Startpos;
    }

    public void SetEndPos(Transform _endPos)
    {
        EndPos = _endPos;
    }

    public Transform GetEndPos()
    {
        return EndPos;
    }
}
