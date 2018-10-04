using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockspawner : MonoBehaviour {
   
    public GameObject aref;
    int myrotation;//select which rotation
    Vector3 a;
    Vector3 b;
    Vector3 c;
    Vector3 d;
    Vector3 e;
    Vector3 f;
    Vector3 g;
    Vector3 h;
    Vector3 i;
    Vector3 j;
    Vector3 k;
    Vector3 l;
    float scaleof;

    bool Transnegx;
    bool Transposx;
    bool Transnegz;
    bool Transposz;
    Quaternion RotationOFF;
    
    Vector3 temp;
    List<Vector3> HoliestGrail = new List<Vector3>();
    Vector3 myfinaltranslation;
    private void Start()
    {
        scaleof = 0.1f;
        ResetmyBool();
         a = new Vector3(90f, 90f, 0f);     //1     1
         b = new Vector3(90f, 0f, 90f);     //2       2
         c = new Vector3(90f, 0f, 0f);      //3         3
         d = new Vector3(90f, 0f, 180f);    //4           4
         e = new Vector3(0f , 90f, 0f);      //5     1,2
         f = new Vector3(0f , 90f, 180f);    //6     1,2
         g = new Vector3(0f , 0f, 0f);       //7         3,4
         h = new Vector3(0f , 0f, 180f);     //8         3,4
         i = new Vector3(0f , 90f, 90f);     //9     1,2,3
         j = new Vector3(0f , 90f, 270f);    //10    1,2,  4
         k = new Vector3(0f , 0f, 270f);     //11    1,  3,4
         l = new Vector3(0f , 0f, 90f);      //12      2,3,4
        SetmyGrail();
        StartCoroutine(Example());
        
    }

    private void Update()
    {
        
        Example();
    }

    IEnumerator Example()
    {
        float gamex = Random.Range(-0.84f, 0.95f);
        float gamez = Random.Range(-0.84f, 0.95f);
        Vector3 currpos = this.gameObject.transform.position;
        this.gameObject.transform.Translate(-currpos.x,0,-currpos.z,Space.World);
        this.gameObject.transform.Translate(gamex, 0, gamez, Space.World);
        //print(Time.time);
        //setposnrot();
        MoreRegret();
        //Instantiate(a, this.gameObject.transform.position + OFFs,rotOFFS);
        //Instantiate(a, this.gameObject.transform.position + OFFs,rotOFFS);
       // Debug.Log("inexample");
        yield return new WaitForSeconds(3);
        // print(Time.time);
        StartCoroutine(Example());
    }
    void SetmyGrail()
    {
        HoliestGrail.Add(a);
        HoliestGrail.Add(b);
        HoliestGrail.Add(c);
        HoliestGrail.Add(d);
        HoliestGrail.Add(e);
        HoliestGrail.Add(f);
        HoliestGrail.Add(g);
        HoliestGrail.Add(h);
        HoliestGrail.Add(i);
        HoliestGrail.Add(j);
        HoliestGrail.Add(k);
        HoliestGrail.Add(l);

    }

    void MoreRegret()
    {
        myrotation = Random.Range(0, 12);
        switch (myrotation)
        {
        //Vector3 a = new Vector3(90, 90, 0);     //1     1
        //Vector3 b = new Vector3(90, 0, 90);     //2       2
        //Vector3 c = new Vector3(90, 0, 0);      //3         3
        //Vector3 d = new Vector3(90, 0, 180);    //4           4
        //Vector3 e = new Vector3(0, 90, 0);      //5     1,2
        //Vector3 f = new Vector3(0, 90, 180);    //6     1,2
        //Vector3 g = new Vector3(0, 0, 0);       //7         3,4
        //Vector3 h = new Vector3(0, 0, 180);     //8         3,4
        //Vector3 i = new Vector3(0, 90, 90);     //9     1,2,3
        //Vector3 j = new Vector3(0, 90, 270);    //10    1,2,  4
        //Vector3 k = new Vector3(0, 0, 270);     //11    1,  3,4
        //Vector3 l = new Vector3(0, 0, 90);      //12      2,3,4
            case 0:                    //1
                // a;
                ResetmyBool();
                Transnegx = true;
                break;
            case 1:                    //2
                //b;
                ResetmyBool();
                Transposx = true;
                break;
            case 2:                    //3
                //c;
                ResetmyBool();
                Transnegz = true;
                break;
            case 3:                    //4
                //d;
                ResetmyBool();
                Transposz = true;
                break;
            case 4:                    //1,2
                //e;
                ResetmyBool();
                Transnegx = true;
                Transposx = true;
                break;
            case 5:                  //1,2
                //f;
                ResetmyBool();
                Transnegx = true;
                Transposx = true;
                break;
            case 6:                    //3,4
                //g;
                ResetmyBool();
                Transnegz = true;
                Transposz = true;
                break; 
            case 7:                     //3,4
                //h;
                ResetmyBool();
                Transnegz = true;
                Transposz = true;
                break;
            case 8:                   //1,2,3
                //i;
                ResetmyBool();
                Transnegx = true;
                Transposx = true;
                Transnegz = true;
                break;
            case 9:                      //1,2,4
                //j;
                ResetmyBool();
                Transnegx = true;
                Transposx = true;

                Transposz = true;
                break;
            case 10:                   //1,3,4
                //k;
                ResetmyBool();
                Transnegx = true;
                Transnegz = true;
                Transposz = true;
                break;
            case 11:          //2,3,4
                //l;
                ResetmyBool();
                Transposx = true;
                Transnegz = true;
                Transposz = true;
                break;
        }
        temp = HoliestGrail[myrotation];
        RotationOFF = Quaternion.Euler(temp);
        //Debug.Log("myrot   " + temp);
        Instantiatemyblock();
    }

    void ResetmyBool()
    {
        Transnegx = false;
        Transposx = false;
        Transnegz = false;
        Transposz = false;
    }

    void Instantiatemyblock()
    {
        SetmyTranslations();
        float myposx = this.gameObject.transform.position.x;
        float fixx;
        fixx = Mathf.Round(this.gameObject.transform.position.x * 10f) / 10f;
        float mynewposx = fixx - myposx;

        float myposz = this.gameObject.transform.position.z;
        float fixz;
        fixz = Mathf.Round(this.gameObject.transform.position.z * 10f) / 10f;
        float mynewposz = fixz - myposz;
        this.gameObject.transform.Translate( mynewposx,0, mynewposz, Space.World);
       // this.gameObject.transform.Translate(mynewpos, 0, 0, Space.World);
        Instantiate(aref, this.gameObject.transform.position + myfinaltranslation, RotationOFF);
    }

   
    void SetmyTranslations()
    {
        Vector3 trantonegx = new Vector3(-1 * scaleof, 0, 0);
        Vector3 trantoposx = new Vector3(1 * scaleof, 0, 0);
        Vector3 nomove = new Vector3(0, 0, 0);
        Vector3 trantonegz = new Vector3(0,0,-1 * scaleof);
        Vector3 trantoposz = new Vector3(0,0,1 * scaleof);
      
        Vector3 tempx = new Vector3(0, 0, 0); 
        Vector3 tempz = new Vector3(0, 0, 0); 
        if (Transnegx && Transposx)
        {
            
            int leftorright = Random.Range(0, 3);
            switch (leftorright)
            {

                case 0: //move left
                    tempx = trantonegx;
                    break;
                case 1://stay center
                    tempx = nomove;
                    break;
                case 2://move right
                    tempx = trantoposx;
                    break;
            }
          
        }
        else if (Transposx)
        {
            tempx = trantoposx;
        }
        else if(Transnegx)
        {
            tempx = trantonegx;
        }

        if (Transnegz && Transposz)
        {
            int forwardorback = Random.Range(0, 3);
            switch (forwardorback)
            {

                case 0: //move forward
                    tempz = trantonegz;
                    break;
                case 1://stay center
                    tempz = nomove;
                    break;
                case 2://move back
                    tempz = trantoposz;
                    break;
            }
        }
        else if(Transnegz)
        {
            tempz = trantonegz;
        }
        else if (Transposz)
        {
            tempz = trantoposz;
        }



        myfinaltranslation = tempx + tempz;
    }
}
