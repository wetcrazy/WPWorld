using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyplayerScript : MonoBehaviour {

    public Text debugX;
    public Text debugZ;
    public int gridsize;


    bool spawn;
    Vector3 temp;
    private Vector3 S_point;
    float Speed;
    bool Horizontal;
    float FixX;
    float FixZ;
    public int appleCount;

   
    void Start () {
        spawn = false;
        appleCount = 0;
        Horizontal = false;
        Speed = 0.01f;
        SetpopPos();
    }
    
    
    void Update () {

        if (temp.magnitude >= 1f)
        {
            SetpopPos();
            
        }
        ////detect button press
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        ////with the button press move in that direction
        if ((temp.magnitude > 0.9f) && (temp.magnitude < 1f) || !spawn)
        {
             Direction(x, z);
        }

        ////move in that direction
        transform.Translate(FixX, 0, FixZ);
        temp = S_point - this.transform.position;
        TExtDebugging();
    }

    //Direction to translate
    void Direction(float x, float z)
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

    //translates and set Spoint
    void SetpopPos()
    {
        SetRoundOffFix(transform.position);
        S_point = this.transform.position;
    }
    //translates this object to the rounded off of its position
    void SetRoundOffFix(Vector3 T)
    {
        Vector3 temp = RoundoffFix(T);
        transform.Translate(temp - T);
    }
    //Round off given vector to int and returns the rounded off vector
    Vector3 RoundoffFix(Vector3 T)
    {
        var posx = Mathf.RoundToInt(T.x);
        var posy = Mathf.RoundToInt(T.y);
        var posz = Mathf.RoundToInt(T.z);
        Vector3 S = new Vector3(posx, posy, posz);
        return S;
    }
    //to display
    void TExtDebugging()
    {
        debugX.text = " x : " + FixX.ToString();
        debugZ.text = " z : " + FixZ.ToString();
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
