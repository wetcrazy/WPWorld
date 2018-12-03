using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyplayerScript : MonoBehaviour {

    float Speed;
    bool Horizontal;

    float FixX;
    float FixZ;
    int appleCount;
    public Text debugX;
    public Text debugZ;
    public Text debugsnakelength;
    public Text SpointDist;

    bool spawn;
    public Vector3 S_point;
    Vector3 temp;


    void Start () {
        spawn = false;
        appleCount = 0;
        Horizontal = false;
        Speed = 0.01f;

        SetpopPos();
    }
    
    
    void Update () {
        if (temp.magnitude > 1f)
        {
            SetpopPos();
        }
        //detect button press
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //with the button press move in that direction
        if ((temp.magnitude > 0.8f) && (temp.magnitude < 1.2f) || !spawn)
        {
            if (x > 0 && !Horizontal)
            {
                FixX = Speed;
                FixZ = 0;
                Horizontal = true;
                spawn = true;

                SetpopPos();
            }
            else if (x < 0 && !Horizontal)
            {
                FixX = -Speed;
                FixZ = 0;
                Horizontal = true;
                spawn = true;

                SetpopPos();
            }
            else if (z > 0 && Horizontal)
            {
                FixZ = Speed;
                FixX = 0;
                Horizontal = false;
                spawn = true;

                SetpopPos();
            }
            else if (z < 0 && Horizontal)
            {
                FixZ = -Speed;
                FixX = 0;
                Horizontal = false;
                spawn = true;

                SetpopPos();
            }
        }
        //move in that direction
        transform.Translate(FixX, 0, FixZ);
       
        temp = S_point - this.transform.position;
        TExtDebugging();
    }


    //void Distance_checker()
    //{
    //}

    //void Direction(float x,float z)
    //{
    //    if (x > 0 && !Horizontal)
    //    {
    //        FixX = Speed;
    //        FixZ = 0;
    //        Horizontal = true;
    //    }
    //    //move left
    //    else if (x < 0 && !Horizontal)
    //    {
    //        FixX = -Speed;
    //        FixZ = 0;
    //        Horizontal = true;
    //    }


    //    else if (z > 0 && Horizontal)
    //    {
    //        FixZ = Speed;
    //        FixX = 0;
    //        Horizontal = false;
    //    }
    //    else if (z < 0 && Horizontal)
    //    {
    //        FixZ = -Speed;
    //        FixX = 0;
    //        Horizontal = false;

    //    }

    //}

    Vector3 RoundoffFix(Vector3 T)
    {
        var posx = Mathf.RoundToInt(T.x);
        var posy = Mathf.RoundToInt(T.y);
        var posz = Mathf.RoundToInt(T.z);
        Vector3 S = new Vector3(posx, posy, posz);
        return S;
    }
    void SetRoundOffFix(Vector3 T)
    {
        Vector3 temp = RoundoffFix(T);
        transform.Translate(temp - T);
    }

    void SetpopPos()
    {
        SetRoundOffFix(transform.position);
        S_point = this.transform.position;
    }
    void TExtDebugging()
    {
        debugX.text = " x : " + FixX.ToString();
        debugZ.text = " z : " + FixZ.ToString();
        debugsnakelength.text = " eaten apples: " + appleCount.ToString();
        SpointDist.text = " distance : " + S_point.ToString() + " to object pos " + transform.position + " = " + temp.magnitude;
    }




    //get apple
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Apple"))
        {
            appleCount++;
        }
    }
}
