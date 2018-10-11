using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockspawner : MonoBehaviour {
   
    public GameObject aref;
    public GameObject bref;
    public GameObject cref;
    public GameObject dref;
    public GameObject eref;
    public GameObject fref;
    public GameObject gref;
    public GameObject href;
    public GameObject iref;
    //blocks
    List<GameObject> Blockrefs = new List<GameObject>();

    List<float> floorchecker = new List<float>();

    float lowest;
    float mid;
    float highest;
    float untouchable;
    //int myrotation;//select which rotation

    float scaleof;//0.1f

    bool Transnegx;
    bool Transposx;
    bool Transnegz;
    bool Transposz;
    Quaternion RotationOFF;
    
    
    //rotations
    List<Vector3> HoliestGrail = new List<Vector3>();
    Vector3 myfinaltranslation;


    private void Start()
    {
        scaleof = 0.1f;
        ResetmyBool();
        //checkmyfloor();
        Setfixedrotations();
       // SetmyGrail();
        SetmyGameobjectsRef();
        StartCoroutine(Example());
       
    }

    private void Update()
    {
        
       
    }



    void ResetmyBool()
    {
        Transnegx = false;
        Transposx = false;
        Transnegz = false;
        Transposz = false;
    }
    void checkmyfloor()
    {
        List<float> floorchecker = new List<float>();
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        //Debug.Log(this.gameObject.transform.position);

        //greater the distance from my point the lower it is

        lowest = 0.0f;
        mid = 0.0f;
        highest = 999.9f;
        untouchable = 9999.9f;
        


        if (Physics.Raycast(transform.position + new Vector3(-1, 10, 1) * 0.1f, Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
            //print("Found an object Vector3(-1, 0,1)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position+ new Vector3(0, 10, 1) * 0.1f  , Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(0, 0, 1)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(1, 10, 1) * 0.1f, Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(1, 0, 1)  - distance: " + hit.distance);

        if (Physics.Raycast(transform.position+ new Vector3(-1, 10, 0) * 0.1f , Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(-1, 0,0)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position+ new Vector3(0, 10, 0) * 0.1f  , Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(0, 0, 0)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position+ new Vector3(1, 10, 0) * 0.1f  , Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(1, 0, 0)  - distance: " + hit.distance);

        if (Physics.Raycast(transform.position+ new Vector3(-1, 10, -1) * 0.1f, Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(-1, 0,-1) - distance: " + hit.distance);
        if (Physics.Raycast(transform.position+ new Vector3(0, 10, -1) * 0.1f , Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(0, 0,-1)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position+ new Vector3(1, 10, -1) * 0.1f , Vector3.down, out hit))
        {
            Testmyhighest(hit.distance);
            Testmylowest(hit.distance);
        }
        //print("Found an object Vector3(1, 0,-1)  - distance: " + hit.distance);


        ////////////////////////////
        if (Physics.Raycast(transform.position + new Vector3(-1, 10, 1) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(hit.transform.position.y);
        }
        //print("Found an object Vector3(-1, 0,1)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(0, 10, 1) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(0, 0, 1)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(1, 10, 1) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(1, 0, 1)  - distance: " + hit.distance);

        if (Physics.Raycast(transform.position + new Vector3(-1, 10, 0) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(-1, 0,0)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(0, 10, 0) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(0, 0, 0)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(1, 10, 0) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(1, 0, 0)  - distance: " + hit.distance);

        if (Physics.Raycast(transform.position + new Vector3(-1, 10, -1) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(-1, 0,-1) - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(0, 10, -1) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
        //print("Found an object Vector3(0, 0,-1)  - distance: " + hit.distance);
        if (Physics.Raycast(transform.position + new Vector3(1, 10, -1) * 0.1f, Vector3.down, out hit))
        {
            floorchecker.Add(lowest - hit.distance);
        }
       for(int i = 0;i<9;i++)
        {
            Debug.Log(floorchecker[i]);
        }

        //print("Found an object Vector3(1, 0,-1)  - distance: " + hit.distance);

        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        //{

        //}
        //Debug.DrawRay(transform.position + new Vector3(1, 0, 1) * 0.1f  ,   transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(0, 0, 1) * 0.1f  ,   transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(-1, 0, 1) * 0.1f ,  transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(1, 0, 0) * 0.1f  ,   transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(0, 0, 0) * 0.1f  ,   transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(-1, 0, 0) * 0.1f ,  transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(1, 0, -1) * 0.1f ,  transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(0, 0, -1) * 0.1f ,  transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //Debug.DrawRay(transform.position + new Vector3(-1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        // Does the ray intersect any objects excluding the player layer
        ////if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        ////{
        ////    Debug.DrawRay(transform.position + new Vector3(1, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(0, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(-1, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(0, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(-1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(0, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(-1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    //Debug.Log("Did Hit");
        ////    //Debug.Log(Physics.Raycast)
        ////}
        ////else
        ////{
        ////    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    //  Debug.Log("Did not Hit"); Debug.DrawRay(transform.position + new Vector3(1,0,1 ) * 0.1f, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        ////    Debug.DrawRay(transform.position + new Vector3(0, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(-1, 0, 1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(0, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(-1, 0, 0) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(0, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////    Debug.DrawRay(transform.position + new Vector3(-1, 0, -1) * 0.1f, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        ////}
    }
    void Testmylowest(float lowt)
    {
        if (lowt > lowest)
        {
            lowest = lowt;
        }
    }
    void Testmyhighest(float hight)
    {
        if (hight > highest)
        {
            highest = hight;
        }
    }
    //void Setmyfloor()
    //{
    //    int layerMask = 1 << 8;

    //    // This would cast rays only against colliders in layer 8.
    //    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
    //    layerMask = ~layerMask;

    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position + new Vector3(-1, 10, 1) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(-1, 0,1)  - distance: " + hit.distance);
    //    if (Physics.Raycast(transform.position + new Vector3(0, 10, 1) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(0, 0, 1)  - distance: " + hit.distance);
    //    if (Physics.Raycast(transform.position + new Vector3(1, 10, 1) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(1, 0, 1)  - distance: " + hit.distance);

    //    if (Physics.Raycast(transform.position + new Vector3(-1, 10, 0) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(-1, 0,0)  - distance: " + hit.distance);
    //    if (Physics.Raycast(transform.position + new Vector3(0, 10, 0) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(0, 0, 0)  - distance: " + hit.distance);
    //    if (Physics.Raycast(transform.position + new Vector3(1, 10, 0) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(1, 0, 0)  - distance: " + hit.distance);

    //    if (Physics.Raycast(transform.position + new Vector3(-1, 10, -1) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(-1, 0,-1) - distance: " + hit.distance);
    //    if (Physics.Raycast(transform.position + new Vector3(0, 10, -1) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(0, 0,-1)  - distance: " + hit.distance);
    //    if (Physics.Raycast(transform.position + new Vector3(1, 10, -1) * 0.1f, Vector3.down, out hit))
    //        print("Found an object Vector3(1, 0,-1)  - distance: " + hit.distance);
    //}
    void Setfixedrotations()
    {                                                                                                                              
        // all rotations                                        L ::   ::       ||  thumb   ::       ||  z       ::       ||    t    ::        ||      u    ::       ||    block
        Vector3 baa = new Vector3( 90  , 0    , 0  );   //   0345 ::   ::       ||  t 01345 ::       ||  z 0145  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        Vector3 dca = new Vector3( 270  , 180  , 0 );   //   2345 ::   ::       ||  t 12345 ::       ||  z 1234  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345

        Vector3 dba = new Vector3( 270  , 90   , 0 );   //   0147 ::   ::       ||  t 01347 ::       ||  z 0347  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        Vector3 bda = new Vector3( 90  , 270  , 0  );   //   1467 ::   ::       ||  t 13467 ::       ||  z 1346  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467

        Vector3 bba = new Vector3( 90  , 90   , 0  );   //   1247 ::   ::       ||  t 12457 ::       ||  z 2457  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        Vector3 dda = new Vector3( 270  , 270  , 0 );   //   1478 ::   ::       ||  t 14578 ::       ||  z 1458  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578

        Vector3 bca = new Vector3( 90  , 180  , 0  );   //   3458 ::   ::       ||  t 34578 ::       ||  z 3478  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        Vector3 daa = new Vector3( 270  , 0    , 0 );   //   3456 ::   ::       ||  t 34567 ::       ||  z 4567  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678

        Vector3 aba = new Vector3( 0  , 90   , 0   );   //   147  ::   ::       ||  t 147   ::       ||  z  47   ::1      ||  t 147  ::        ||   u 147   ::       ||  b 147
        Vector3 ada = new Vector3( 0  , 270  , 0   );   //   147  ::   ::       ||  t 147   ::       ||  z  14   ::7      ||  t 147  ::        ||   u 147   ::       ||  b 147

        Vector3 cba = new Vector3( 180  , 90   , 0 );   //   1    ::47 ::       ||  t 14    ::7      ||  z  14   ::7      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        Vector3 cda = new Vector3( 180  , 270  , 0 );   //   7    ::14 ::       ||  t 47    ::1      ||  z  47   ::1      ||  t 4    ::17      ||   u 17    ::4      ||  b 147

        Vector3 aaa = new Vector3( 0  , 0    , 0   );   //   345  ::   ::       ||  t 345   ::       ||  z  45   ::3      ||  t 345  ::        ||   u 345   ::       ||  b 345
        Vector3 aca = new Vector3( 0  , 180  , 0   );   //   345  ::   ::       ||  t 345   ::       ||  z  34   ::5      ||  t 345  ::        ||   u 345   ::       ||  b 345

        Vector3 caa = new Vector3( 180  , 0    , 0 );   //   3    ::45 ::       ||  t 34    ::5      ||  z  34   ::5      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        Vector3 cca = new Vector3( 180  , 180  , 0 );   //   5    ::34 ::       ||  t 45    ::3      ||  z  45   ::3      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
                                                                                                                                                      
                                                                                                                                                   
                                                                                                                                                        
                                                                                                                                                       
        Vector3 aab = new Vector3( 0  , 0    , 90  );   //   34   ::   ::       ||  t 34    ::       ||  z 3     ::4      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        Vector3 acd = new Vector3( 0  , 180  , 270 );   //   4    ::   ::3      ||  t 4     ::3      ||  z 4     ::3      ||  t 4    ::3       ||   u 34    ::       ||  b 34

        Vector3 abb = new Vector3( 0  , 90   , 90  );   //   14   ::   ::       ||  t 14    ::       ||  z 1     ::4      ||  t 4    ::1       ||   u 14    ::       ||  b 14
        Vector3 add = new Vector3( 0  , 270  , 270 );   //   4    ::   ::1      ||  t 4     ::1      ||  z 4     ::1      ||  t 4    ::1       ||   u 14    ::       ||  b 14 

        Vector3 acb = new Vector3( 0  , 180  , 90  );   //   45   ::   ::       ||  t 45    ::       ||  z 5     ::4      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        Vector3 aad = new Vector3( 0  , 0    , 270 );   //   4    ::   ::5      ||  t 4     ::5      ||  z 4     ::5      ||  t 4    ::5       ||   u 45    ::       ||  b 45

        Vector3 adb = new Vector3( 0  , 270  , 90  );   //   47   ::   ::       ||  t 47    ::       ||  z 7     ::4      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        Vector3 abd = new Vector3( 0  , 90   , 270 );   //   4    ::   ::7      ||  t 4     ::7      ||  z 4     ::7      ||  t 4    ::7       ||   u 47    ::       ||  b 47
                                                                                                                                                       
                                                                                                                    



















       Vector3 a = new Vector3(90f, 90f, 0f);     //1     1
       Vector3 b = new Vector3(90f, 0f, 90f);     //2       2
       Vector3 c = new Vector3(90f, 0f, 0f);      //3         3
       Vector3 d = new Vector3(90f, 0f, 180f);    //4           4
       Vector3 e = new Vector3(0f, 90f, 0f);      //5     1,2
       Vector3 f = new Vector3(0f, 90f, 180f);    //6     1,2
       Vector3 g = new Vector3(0f, 0f, 0f);       //7         3,4
       Vector3 h = new Vector3(0f, 0f, 180f);     //8         3,4
       Vector3 i = new Vector3(0f, 90f, 90f);     //9     1,2,3
       Vector3 j = new Vector3(0f, 90f, 270f);    //10    1,2,  4
       Vector3 k = new Vector3(0f, 0f, 270f);     //11    1,  3,4
       Vector3 l = new Vector3(0f, 0f, 90f);      //12      2,3,4


        HoliestGrail.Add(baa);
        HoliestGrail.Add(dca);
        HoliestGrail.Add(dba);

        HoliestGrail.Add(bda);
        HoliestGrail.Add(bba);
        HoliestGrail.Add(dda);

        HoliestGrail.Add(bca);
        HoliestGrail.Add(daa);
        




        //HoliestGrail.Add(a);
        //HoliestGrail.Add(b);
        //HoliestGrail.Add(c);
        //HoliestGrail.Add(d);
        //HoliestGrail.Add(e);
        //HoliestGrail.Add(f);
        //HoliestGrail.Add(g);
        //HoliestGrail.Add(h);
        //HoliestGrail.Add(i);
        //HoliestGrail.Add(j);
        //HoliestGrail.Add(k);
        //HoliestGrail.Add(l);
        //HoliestGrail.Add()
    }
    //void SetmyGrail()
    //{
    //    HoliestGrail.Add(a);
    //    HoliestGrail.Add(b);
    //    HoliestGrail.Add(c);
    //    HoliestGrail.Add(d);
    //    HoliestGrail.Add(e);
    //    HoliestGrail.Add(f);
    //    HoliestGrail.Add(g);
    //    HoliestGrail.Add(h);
    //    HoliestGrail.Add(i);
    //    HoliestGrail.Add(j);
    //    HoliestGrail.Add(k);
    //    HoliestGrail.Add(l);

    //}
    void SetmyGameobjectsRef()
    {
        Blockrefs.Add(aref);
        Blockrefs.Add(bref);
        Blockrefs.Add(cref);
        Blockrefs.Add(dref);
        Blockrefs.Add(eref);
        Blockrefs.Add(fref);
        Blockrefs.Add(gref);
        Blockrefs.Add(href);
        Blockrefs.Add(iref);
    }
    IEnumerator Example()
    {
        Wheretofall();
       // checkmyfloor();
        MoreRegret();
        //Instantiate(a, this.gameObject.transform.position + OFFs,rotOFFS);
        //Instantiate(a, this.gameObject.transform.position + OFFs,rotOFFS);
       // Debug.Log("inexample");
      //  yield return new WaitForSeconds(3);
        yield return new WaitForSeconds(2);
        // print(Time.time);
        StartCoroutine(Example());
    }
    void Wheretofall()
    {
        float gamex = Random.Range(-0.84f, 0.95f);
        float gamez = Random.Range(-0.84f, 0.95f);
        Vector3 currpos = this.gameObject.transform.position;
        this.gameObject.transform.Translate(-currpos.x, 0, -currpos.z, Space.World);
        this.gameObject.transform.Translate(gamex, 0, gamez, Space.World);
    }
    void MoreRegret()
    {
        int myrotation = Random.Range(0, 8);
        //switch (myrotation)
        //{
        ////Vector3 a = new Vector3(90, 90, 0);     //1     1
        ////Vector3 b = new Vector3(90, 0, 90);     //2       2
        ////Vector3 c = new Vector3(90, 0, 0);      //3         3
        ////Vector3 d = new Vector3(90, 0, 180);    //4           4
        ////Vector3 e = new Vector3(0, 90, 0);      //5     1,2
        ////Vector3 f = new Vector3(0, 90, 180);    //6     1,2
        ////Vector3 g = new Vector3(0, 0, 0);       //7         3,4
        ////Vector3 h = new Vector3(0, 0, 180);     //8         3,4
        ////Vector3 i = new Vector3(0, 90, 90);     //9     1,2,3
        ////Vector3 j = new Vector3(0, 90, 270);    //10    1,2,  4
        ////Vector3 k = new Vector3(0, 0, 270);     //11    1,  3,4
        ////Vector3 l = new Vector3(0, 0, 90);      //12      2,3,4
        //    case 0:                    //1
        //        // a;
        //        ResetmyBool();
        //        Transnegx = true;
        //        break;
        //    case 1:                    //2
        //        //b;
        //        ResetmyBool();
        //        Transposx = true;
        //        break;
        //    case 2:                    //3
        //        //c;
        //        ResetmyBool();
        //        Transnegz = true;
        //        break;
        //    case 3:                    //4
        //        //d;
        //        ResetmyBool();
        //        Transposz = true;
        //        break;
        //    case 4:                    //1,2
        //        //e;
        //        ResetmyBool();
        //        Transnegx = true;
        //        Transposx = true;
        //        break;
        //    case 5:                  //1,2
        //        //f;
        //        ResetmyBool();
        //        Transnegx = true;
        //        Transposx = true;
        //        break;
        //    case 6:                    //3,4
        //        //g;
        //        ResetmyBool();
        //        Transnegz = true;
        //        Transposz = true;
        //        break; 
        //    case 7:                     //3,4
        //        //h;
        //        ResetmyBool();
        //        Transnegz = true;
        //        Transposz = true;
        //        break;
        //    case 8:                   //1,2,3
        //        //i;
        //        ResetmyBool();
        //        Transnegx = true;
        //        Transposx = true;
        //        Transnegz = true;
        //        break;
        //    case 9:                      //1,2,4
        //        //j;
        //        ResetmyBool();
        //        Transnegx = true;
        //        Transposx = true;

        //        Transposz = true;
        //        break;
        //    case 10:                   //1,3,4
        //        //k;
        //        ResetmyBool();
        //        Transnegx = true;
        //        Transnegz = true;
        //        Transposz = true;
        //        break;
        //    case 11:          //2,3,4
        //        //l;
        //        ResetmyBool();
        //        Transposx = true;
        //        Transnegz = true;
        //        Transposz = true;
        //        break;
        //}
        Vector3 temp = HoliestGrail[myrotation];
        RotationOFF = Quaternion.Euler(temp);
        //Debug.Log("myrot   " + temp);
        Instantiatemyblock();
    }

    void Instantiatemyblock()
    {
        int blocknum =  Random.Range( 0,9);
        SetmyTranslations();
        Roundoffandsetmypos();
       // this.gameObject.transform.Translate(mynewpos, 0, 0, Space.World);
        Instantiate(Blockrefs[blocknum], this.gameObject.transform.position + myfinaltranslation, RotationOFF);
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
    void Roundoffandsetmypos()
    {
        float myposx = this.gameObject.transform.position.x;
        float fixx;
        fixx = Mathf.Round(this.gameObject.transform.position.x * 10f) / 10f;
        float mynewposx = fixx - myposx;

        float myposz = this.gameObject.transform.position.z;
        float fixz;
        fixz = Mathf.Round(this.gameObject.transform.position.z * 10f) / 10f;
        float mynewposz = fixz - myposz;
        this.gameObject.transform.Translate(mynewposx, 0, mynewposz, Space.World);
    }
}
