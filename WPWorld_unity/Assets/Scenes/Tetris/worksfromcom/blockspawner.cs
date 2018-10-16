﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockspawner : MonoBehaviour {

    //============================================================================================
    //********************
    //DO NOT TOUCH SECTION
    //********************
    public GameObject aref;//L            |0
    public GameObject bref;//thumb        |1
    public GameObject cref;//Z            |2
    public GameObject dref;//T            |3
    public GameObject eref;//U            |4
    public GameObject fref;//block        |5
  

    //public GameObject aref;//2x2 left     |0
    //public GameObject bref;//2x2 right    |1
    //public GameObject cref;//block 2x3    |2
    //public GameObject dref;//L            |3
    //public GameObject eref;//Reverse L    |4
    //public GameObject fref;//Reverse S    |5
    //public GameObject gref;//S            |6
    //public GameObject href;//small t      |7
    //public GameObject iref;//U shape      |8
    //blocks
    List<GameObject> Blockrefs = new List<GameObject>();//list of blocks conatainer
    void SetmyGameobjectsRef()//  6/9objects
    {
        Blockrefs.Add(aref);
        Blockrefs.Add(bref);
        Blockrefs.Add(cref);
        Blockrefs.Add(dref);
        Blockrefs.Add(eref);
        Blockrefs.Add(fref);
        //Blockrefs.Add(gref);
        //Blockrefs.Add(href);
        //Blockrefs.Add(iref);
    }
    //rotations
    List<Vector3> Rotations = new List<Vector3>();// list of rotations container
    Quaternion RotationOFF;
    void Setfixedrotations()//  24 rotations
    {                                                                                                                              
        // all rotations                                  num|      L ::   ::       ||  thumb   ::       ||  z       ::       ||    t    ::        ||      u    ::       ||    block
        Vector3 aaa = new Vector3( 0  , 0    , 0   );   //0  |   345  ::   ::       ||  t 345   ::       ||  z  45   ::3      ||  t 345  ::        ||   u 345   ::       ||  b 345
        Vector3 aba = new Vector3( 0  , 90   , 0   );   //1  |   147  ::   ::       ||  t 147   ::       ||  z  47   ::1      ||  t 147  ::        ||   u 147   ::       ||  b 147
        Vector3 aca = new Vector3( 0  , 180  , 0   );   //2  |   345  ::   ::       ||  t 345   ::       ||  z  34   ::5      ||  t 345  ::        ||   u 345   ::       ||  b 345
        Vector3 ada = new Vector3( 0  , 270  , 0   );   //3  |   147  ::   ::       ||  t 147   ::       ||  z  14   ::7      ||  t 147  ::        ||   u 147   ::       ||  b 147
        Vector3 baa = new Vector3( 90  , 0    , 0  );   //4  |   0345 ::   ::       ||  t 01345 ::       ||  z 0145  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        Vector3 bba = new Vector3( 90  , 90   , 0  );   //5  |   1247 ::   ::       ||  t 12457 ::       ||  z 2457  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        Vector3 bca = new Vector3( 90  , 180  , 0  );   //6  |   3458 ::   ::       ||  t 34578 ::       ||  z 3478  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        Vector3 bda = new Vector3( 90  , 270  , 0  );   //7  |   1467 ::   ::       ||  t 13467 ::       ||  z 1346  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        Vector3 caa = new Vector3( 180  , 0    , 0 );   //8  |   3    ::45 ::       ||  t 34    ::5      ||  z  34   ::5      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        Vector3 cba = new Vector3( 180  , 90   , 0 );   //9  |   1    ::47 ::       ||  t 14    ::7      ||  z  14   ::7      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        Vector3 cca = new Vector3( 180  , 180  , 0 );   //10 |   5    ::34 ::       ||  t 45    ::3      ||  z  45   ::3      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        Vector3 cda = new Vector3( 180  , 270  , 0 );   //11 |   7    ::14 ::       ||  t 47    ::1      ||  z  47   ::1      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        Vector3 daa = new Vector3( 270  , 0    , 0 );   //12 |   3456 ::   ::       ||  t 34567 ::       ||  z 4567  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        Vector3 dba = new Vector3( 270  , 90   , 0 );   //13 |   0147 ::   ::       ||  t 01347 ::       ||  z 0347  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        Vector3 dca = new Vector3( 270  , 180  , 0 );   //14 |   2345 ::   ::       ||  t 12345 ::       ||  z 1234  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        Vector3 dda = new Vector3( 270  , 270  , 0 );   //15 |   1478 ::   ::       ||  t 14578 ::       ||  z 1458  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        Vector3 aab = new Vector3( 0  , 0    , 90  );   //16 |   34   ::   ::       ||  t 34    ::       ||  z 3     ::4      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        Vector3 abb = new Vector3( 0  , 90   , 90  );   //17 |   14   ::   ::       ||  t 14    ::       ||  z 1     ::4      ||  t 4    ::1       ||   u 14    ::       ||  b 14
        Vector3 acb = new Vector3( 0  , 180  , 90  );   //18 |   45   ::   ::       ||  t 45    ::       ||  z 5     ::4      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        Vector3 adb = new Vector3( 0  , 270  , 90  );   //19 |   47   ::   ::       ||  t 47    ::       ||  z 7     ::4      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        Vector3 aad = new Vector3( 0  , 0    , 270 );   //20 |   4    ::   ::5      ||  t 4     ::5      ||  z 4     ::5      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        Vector3 abd = new Vector3( 0  , 90   , 270 );   //21 |   4    ::   ::7      ||  t 4     ::7      ||  z 4     ::7      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        Vector3 acd = new Vector3( 0  , 180  , 270 );   //22 |   4    ::   ::3      ||  t 4     ::3      ||  z 4     ::3      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        Vector3 add = new Vector3( 0  , 270  , 270 );   //23 |   4    ::   ::1      ||  t 4     ::1      ||  z 4     ::1      ||  t 4    ::1       ||   u 14    ::       ||  b 14 
        

       //Vector3 a = new Vector3(90f, 90f, 0f);     //1     1
       //Vector3 b = new Vector3(90f, 0f, 90f);     //2       2
       //Vector3 c = new Vector3(90f, 0f, 0f);      //3         3
       //Vector3 d = new Vector3(90f, 0f, 180f);    //4           4
       //Vector3 e = new Vector3(0f, 90f, 0f);      //5     1,2
       //Vector3 f = new Vector3(0f, 90f, 180f);    //6     1,2
       //Vector3 g = new Vector3(0f, 0f, 0f);       //7         3,4
       //Vector3 h = new Vector3(0f, 0f, 180f);     //8         3,4
       //Vector3 i = new Vector3(0f, 90f, 90f);     //9     1,2,3
       //Vector3 j = new Vector3(0f, 90f, 270f);    //10    1,2,  4
       //Vector3 k = new Vector3(0f, 0f, 270f);     //11    1,  3,4
       //Vector3 l = new Vector3(0f, 0f, 90f);      //12      2,3,4


        Rotations.Add(aaa);
        Rotations.Add(aba);
        Rotations.Add(aca);
        Rotations.Add(ada);
        Rotations.Add(baa);
        Rotations.Add(bba);
        Rotations.Add(bca);
        Rotations.Add(bda);
        Rotations.Add(caa);
        Rotations.Add(cba);
        Rotations.Add(cca);
        Rotations.Add(cda);
        Rotations.Add(daa);
        Rotations.Add(dba);
        Rotations.Add(dca);
        Rotations.Add(dda);
        Rotations.Add(aab);
        Rotations.Add(abb);
        Rotations.Add(acb);
        Rotations.Add(adb);
        Rotations.Add(aad);
        Rotations.Add(abd);
        Rotations.Add(acd);
        Rotations.Add(add);

    }
    //RAY SCAN
    List<Vector3> Floorposchecker = new List<Vector3>();//the vector position offset of rays
    List<float> Floorchecker;
    List<Vector3> Floorpos;
    void Setraypos()//3x3 scanray
    { 
        ////my 3x3 scan
        Floorposchecker.Add(new Vector3(-1, 10, 1));
        Floorposchecker.Add(new Vector3(0, 10, 1));
        Floorposchecker.Add(new Vector3(1, 10, 1));
        Floorposchecker.Add(new Vector3(-1, 10, 0));
        Floorposchecker.Add(new Vector3(0, 10, 0));
        Floorposchecker.Add(new Vector3(1, 10, 0));
        Floorposchecker.Add(new Vector3(-1, 10, -1));
        Floorposchecker.Add(new Vector3(0, 10, -1));
        Floorposchecker.Add(new Vector3(1, 10, -1));
    }

    bool Transnegx;
    bool Transposx;
    bool Transnegz;
    bool Transposz;
    void ResetmyBool()//bool check
    {
        Transnegx = false;
        Transposx = false;
        Transnegz = false;
        Transposz = false;
    }

  
    float scaleof;//0.1f set manually to fit game

    // This would cast rays only against colliders in layer 8.
    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
    int layerMask;
    RaycastHit hit;
    void Settermyvoids()
    {
        scaleof = 0.1f;//DO NOT TOUCH
        layerMask = 1 << 8;
        layerMask = ~layerMask;
        ResetmyBool();//ignore WILLCHECK THIS LATER
        Setfixedrotations();// my 24 possible rotations
        SetmyGameobjectsRef();//my 6/9 objects
        Setraypos();//ray offset on 3x3
    }

    int counter = 0;
    //==============================================================================================




    float lowest;
  //  float mid;
    float highest;
    //float untouchable;
    //int myrotation;//select which rotation

    Vector3 myfinaltranslation;

    bool Theresablockhit;

    private void Start()
    {
        Theresablockhit = false;
        Settermyvoids ();//no need look at this 
        StartCoroutine(Example());//START SPAWNING
        
    }
    IEnumerator Example()
    {
        do
        {
        Wheretofall();//Random position on where to fall

        Checkmyfloor();//scan the floor if got block and what the terrain is
        }
        while (Theresablockhit);

        // find all possible outcomes
        //MoreRegret();
        PossibleRotations();

        //Instantiatemyblock();
        yield return new WaitForSeconds(2);//timer how long to wait

        
        StartCoroutine(Example());
    }
    private void Update()
    {
        for (int i = 0; i < Floorposchecker.Count; i++)
        {
            if (Physics.Raycast(transform.position + Floorposchecker[i] * scaleof, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position + Floorposchecker[i] * scaleof, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            }
        }
    }
    //step1 set random position of the spawner
    void Wheretofall()
    {
        float gamex = Random.Range(-0.84f, 0.95f);
        float gamez = Random.Range(-0.84f, 0.95f);
        Vector3 currpos = this.gameObject.transform.position;
        this.gameObject.transform.Translate(-currpos.x, 0, -currpos.z, Space.World);
        this.gameObject.transform.Translate(gamex, 0, gamez, Space.World);
        
    }

    //step2 check the floor if theres a falling object and scan the terrain
    void Checkmyfloor()
    {
        Floorchecker = new List<float>();//numbering system of terrain
        Floorpos = new List<Vector3>();
        Theresablockhit = false;
        // floorpos = new List<Vector3>();
        hit = new RaycastHit();
        //greater the distance from my point the lower it is
        lowest = 0.0f;
        // mid = 0.0f;
        highest = 999.9f;
        // untouchable = 9999.9f;

        for (int rpos = 0; rpos < Floorposchecker.Count; rpos++)
        {
            if (Physics.Raycast(transform.position + Floorposchecker[rpos] * scaleof, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
            {
                Testing(hit);//test to see if there is a falling block in the way

            }
        }

        if (!Theresablockhit)
        {


            //////////////////////////////

            //there are no falling blocks 
           // Debug.Log("my lowest is" + lowest+"   my highest  "+ highest);// + "    and this is my position  " + this.gameObject.transform.position);
            for (int rpos = 0; rpos < Floorposchecker.Count; rpos++)
            {
                if (Physics.Raycast(transform.position + Floorposchecker[rpos] * scaleof, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
                {

                    float temp = lowest - hit.distance;
                  
                    float adjusted = Mathf.Round(temp * 10f);
                    
                    Floorchecker.Add(adjusted);
                }
            }

          // Debug.Log(Floorchecker[0] + ", " + Floorchecker[1] + ", " + Floorchecker[2] + "     " + Floorchecker[3] + ", " + Floorchecker[4] + ", " + Floorchecker[5] + "    " + Floorchecker[6] + ", " + Floorchecker[7] + ", " + Floorchecker[8]);
            //Debug.Log(Floorchecker[3] + ", " + Floorchecker[4]+", " + Floorchecker[5]);
            //Debug.Log(Floorchecker[6] + ", " + Floorchecker[7]+", " + Floorchecker[8]);
            //Debug.Log("---------------------------------------------------------------------------------------------");

        }
    }
    //step3 with the given terrain find all possible and allowed rotations
    void PossibleRotations()
    {
        List<GameObject> PossibleBlocks = new List<GameObject>();

        List<Vector3> BlockPossibleRots = new List<Vector3>();//done

        List<Vector3> LPossibleRots = new List<Vector3>();

        List<Vector3> UPossibleRots = new List<Vector3>();
        List<Vector3> TPossibleRots = new List<Vector3>();
        List<Vector3> ZPossibleRots = new List<Vector3>();
        List<Vector3> ThumbPossibleRots = new List<Vector3>();

        List<Vector3> PossiblePOS = new List<Vector3>();
        //   // all rotations                                 num|      L ::   ::       ||  thumb   ::       ||  z       ::       ||    t    ::        ||      u    ::       ||    block
        //  Vector3 aaa = new Vector3( 0  , 0    , 0   );   //0  |   345  ::   ::       ||  t 345   ::       ||  z  45   ::3      ||  t 345  ::        ||   u 345   ::       ||  b 345
        //  Vector3 aca = new Vector3( 0  , 180  , 0   );   //2  |   345  ::   ::       ||  t 345   ::       ||  z  34   ::5      ||  t 345  ::        ||   u 345   ::       ||  b 345
        //  Vector3 caa = new Vector3( 180  , 0    , 0 );   //8  |   3    ::45 ::       ||  t 34    ::5      ||  z  34   ::5      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        //  Vector3 cca = new Vector3( 180  , 180  , 0 );   //10 |   5    ::34 ::       ||  t 45    ::3      ||  z  45   ::3      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        //  Vector3 aab = new Vector3( 0  , 0    , 90  );   //16 |   34   ::   ::       ||  t 34    ::       ||  z 3     ::4      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        //  Vector3 acd = new Vector3( 0  , 180  , 270 );   //22 |   4    ::   ::3      ||  t 4     ::3      ||  z 4     ::3      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        //  Vector3 acb = new Vector3( 0  , 180  , 90  );   //18 |   45   ::   ::       ||  t 45    ::       ||  z 5     ::4      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        //  Vector3 aad = new Vector3( 0  , 0    , 270 );   //20 |   4    ::   ::5      ||  t 4     ::5      ||  z 4     ::5      ||  t 4    ::5       ||   u 45    ::       ||  b 45

        //  Vector3 aba = new Vector3( 0  , 90   , 0   );   //1  |   147  ::   ::       ||  t 147   ::       ||  z  47   ::1      ||  t 147  ::        ||   u 147   ::       ||  b 147
        //  Vector3 ada = new Vector3( 0  , 270  , 0   );   //3  |   147  ::   ::       ||  t 147   ::       ||  z  14   ::7      ||  t 147  ::        ||   u 147   ::       ||  b 147
        //  Vector3 cba = new Vector3( 180  , 90   , 0 );   //9  |   1    ::47 ::       ||  t 14    ::7      ||  z  14   ::7      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        //  Vector3 cda = new Vector3( 180  , 270  , 0 );   //11 |   7    ::14 ::       ||  t 47    ::1      ||  z  47   ::1      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        //  Vector3 abb = new Vector3( 0  , 90   , 90  );   //17 |   14   ::   ::       ||  t 14    ::       ||  z 1     ::4      ||  t 4    ::1       ||   u 14    ::       ||  b 14
        //  Vector3 add = new Vector3( 0  , 270  , 270 );   //23 |   4    ::   ::1      ||  t 4     ::1      ||  z 4     ::1      ||  t 4    ::1       ||   u 14    ::       ||  b 14 
        //  Vector3 adb = new Vector3( 0  , 270  , 90  );   //19 |   47   ::   ::       ||  t 47    ::       ||  z 7     ::4      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        //  Vector3 abd = new Vector3( 0  , 90   , 270 );   //21 |   4    ::   ::7      ||  t 4     ::7      ||  z 4     ::7      ||  t 4    ::7       ||   u 47    ::       ||  b 47


        //  Vector3 baa = new Vector3( 90  , 0    , 0  );   //4  |   0345 ::   ::       ||  t 01345 ::       ||  z 0145  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        //  Vector3 dca = new Vector3( 270  , 180  , 0 );   //14 |   2345 ::   ::       ||  t 12345 ::       ||  z 1234  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        //  Vector3 bca = new Vector3( 90  , 180  , 0  );   //6  |   3458 ::   ::       ||  t 34578 ::       ||  z 3478  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        //  Vector3 daa = new Vector3( 270  , 0    , 0 );   //12 |   3456 ::   ::       ||  t 34567 ::       ||  z 4567  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        //  Vector3 bda = new Vector3( 90  , 270  , 0  );   //7  |   1467 ::   ::       ||  t 13467 ::       ||  z 1346  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        //  Vector3 dba = new Vector3( 270  , 90   , 0 );   //13 |   0147 ::   ::       ||  t 01347 ::       ||  z 0347  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        //  Vector3 bba = new Vector3( 90  , 90   , 0  );   //5  |   1247 ::   ::       ||  t 12457 ::       ||  z 2457  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        //  Vector3 dda = new Vector3( 270  , 270  , 0 );   //15 |   1478 ::   ::       ||  t 14578 ::       ||  z 1458  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578


        //0,1,2
        //3,4,5
        //6,7,8
        Vector3 topleft = new Vector3(-1,0,1);
        Vector3 top = new Vector3(0,0,1);
        Vector3 topright = new Vector3(1,0,1);
        Vector3 left = new Vector3(-1,0,0);
        Vector3 center = new Vector3(0,0,0);
        Vector3 right = new Vector3(1,0,0);
        Vector3 botleft = new Vector3(-1,0,-1);
        Vector3 bot = new Vector3(0,0,-1);
        Vector3 botright = new Vector3(-1,0,-1);



        //if horizontal line
        if (((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0)) ||
            ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0)) ||
            ((Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))  )
        {
            //if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0))
            //{
            //    PossiblePOS.Add(top);
            //}
            //if((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
            //{
            //    PossiblePOS.Add(center);
            //}
            //if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
            //{
            //    PossiblePOS.Add(bot);
            //}
                //block 
            BlockPossibleRots.Add(Rotations[0]);
            BlockPossibleRots.Add(Rotations[2]);
            BlockPossibleRots.Add(Rotations[8]);
            BlockPossibleRots.Add(Rotations[10]);
            //L
            LPossibleRots.Add(Rotations[0]);
            LPossibleRots.Add(Rotations[2]);
                                        
        }
        //2 spacing horizontal
        if (((Floorchecker[0] == 0) && (Floorchecker[1] == 0)) || ((Floorchecker[1] == 0) && (Floorchecker[2] == 0)) ||
            ((Floorchecker[3] == 0) && (Floorchecker[4] == 0)) || ((Floorchecker[4] == 0) && (Floorchecker[5] == 0)) ||
            ((Floorchecker[6] == 0) && (Floorchecker[7] == 0)) || ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))  )
        {
            //Block
            BlockPossibleRots.Add(Rotations[16]);
            BlockPossibleRots.Add(Rotations[22]);
            BlockPossibleRots.Add(Rotations[18]);
            BlockPossibleRots.Add(Rotations[20]);
            //L
            LPossibleRots.Add(Rotations[16]);
            LPossibleRots.Add(Rotations[18]);
           
        }

        //L BLOCK
        //0,1,1 horizontal
        if (((Floorchecker[0] == 0) && (Floorchecker[1] == 1) && (Floorchecker[2] == 1)) ||
            ((Floorchecker[3] == 0) && (Floorchecker[4] == 1) && (Floorchecker[5] == 1)) ||
            ((Floorchecker[6] == 0) && (Floorchecker[7] == 1) && (Floorchecker[8] == 1)))
        {
            //L
            LPossibleRots.Add(Rotations[8]);

        }
        //1,1,0 horizontal
        if (((Floorchecker[0] == 1) && (Floorchecker[1] == 1) && (Floorchecker[2] == 0)) ||
            ((Floorchecker[3] == 1) && (Floorchecker[4] == 1) && (Floorchecker[5] == 0)) ||
            ((Floorchecker[6] == 1) && (Floorchecker[7] == 1) && (Floorchecker[8] == 0)))
        {
            //L
            LPossibleRots.Add(Rotations[10]);

        }

        //Vertical
        if (((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0)) ||
            ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0)) ||
            ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0)))
        {
            //Block
            BlockPossibleRots.Add(Rotations[1]);
            BlockPossibleRots.Add(Rotations[3]);
            BlockPossibleRots.Add(Rotations[9]);
            BlockPossibleRots.Add(Rotations[11]);
            //L
            LPossibleRots.Add(Rotations[1]);
            LPossibleRots.Add(Rotations[3]);
        }
        //2 spacing vertical
        if (((Floorchecker[0] == 0) && (Floorchecker[3] == 0)) || ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))||
            ((Floorchecker[1] == 0) && (Floorchecker[4] == 0)) || ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))||
            ((Floorchecker[2] == 0) && (Floorchecker[5] == 0)) || ((Floorchecker[5] == 0) && (Floorchecker[8] == 0)))
        {
            //block
            BlockPossibleRots.Add(Rotations[17]);
            BlockPossibleRots.Add(Rotations[23]);
            BlockPossibleRots.Add(Rotations[19]);
            BlockPossibleRots.Add(Rotations[21]);
            //L
            LPossibleRots.Add(Rotations[17]);
            LPossibleRots.Add(Rotations[19]);
        }

        //L BLOCK
        //0,1,1 Vertical
        if (((Floorchecker[0] == 0) && (Floorchecker[3] == 1) && (Floorchecker[6] == 1)) ||
            ((Floorchecker[1] == 0) && (Floorchecker[4] == 1) && (Floorchecker[7] == 1)) ||
            ((Floorchecker[2] == 0) && (Floorchecker[5] == 1) && (Floorchecker[8] == 1)))
        {
            //L
            LPossibleRots.Add(Rotations[9]);

        }
        //1,1,0 Vertical
        if (((Floorchecker[0] == 1) && (Floorchecker[3] == 1) && (Floorchecker[6] == 0)) ||
            ((Floorchecker[1] == 1) && (Floorchecker[4] == 1) && (Floorchecker[7] == 0)) ||
            ((Floorchecker[2] == 1) && (Floorchecker[5] == 1) && (Floorchecker[8] == 0)))
        {
            //L
            LPossibleRots.Add(Rotations[11]);

        }

        //012345
        //345678

        //speacial***
        //moveforward and back  
        if ( ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))||
             ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0)) )
        {
            //block
            BlockPossibleRots.Add(Rotations[4 ]);
            BlockPossibleRots.Add(Rotations[14]);
            BlockPossibleRots.Add(Rotations[6 ]);
            BlockPossibleRots.Add(Rotations[12]);
        }
        //013467
        //124578
        //move leftright
        if (((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0)) ||
            ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0)))
        {
            //block
            BlockPossibleRots.Add(Rotations[7 ]);
            BlockPossibleRots.Add(Rotations[13]);
            BlockPossibleRots.Add(Rotations[5 ]);
            BlockPossibleRots.Add(Rotations[15]);
        }


       

        




        int rotpos;
        int myRandomblock = 5;
        switch (myRandomblock)
        {
            case 0://L
                {
                    if (LPossibleRots.Count == 0)
                    {
                        //theres no rotations
                        break;
                    }
                    else
                    {
                        rotpos = Random.Range(0, LPossibleRots.Count);

                    }
                    break;
                }
            case 1://Thumb
                {

                    break;
                }
            case 2://z
                {
                    break;
                }
            case 3://T
                {
                    break;
                }
            case 4://U
                {
                    break;
                }
            case 5://Block
                {
                    if (BlockPossibleRots.Count == 0)
                    {
                        //theres no rotations
                        break;
                    }
                    else
                    {
                        rotpos = Random.Range(0, BlockPossibleRots.Count);

                        if ( (BlockPossibleRots.Equals(Rotations[0]))||
                             (BlockPossibleRots.Equals(Rotations[2]))||
                             (BlockPossibleRots.Equals(Rotations[8]))||
                             (BlockPossibleRots.Equals(Rotations[10])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {

                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                       

                        if ((BlockPossibleRots.Equals(Rotations[16])) ||
                            (BlockPossibleRots.Equals(Rotations[22])) ||
                            (BlockPossibleRots.Equals(Rotations[18])) ||
                            (BlockPossibleRots.Equals(Rotations[20])))
                        {

                        }
                            break;
                    }

                }
        }

    }
    void MoreRegret()
    {
        int myrotation = Random.Range(0,24);
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
        Vector3 temp = Rotations[myrotation];
        RotationOFF = Quaternion.Euler(temp);
        Instantiatemyblock();
    }
    //step4 with the given possible outcomes have a random check what possibilty to spawn and spawn it
    void Instantiatemyblock()
    {
        int blocknum =  Random.Range( 0,9);
        SetmyTranslations();
        Roundoffandsetmypos();
      
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

    void Testing(RaycastHit hit)
    {
       
        if (hit.transform.gameObject.GetComponent<TETRISbehaviour>())//THERE IS A spawn BLOCK
        {
            if (hit.transform.gameObject.GetComponent<TETRISbehaviour>().firstcollision.Equals(true))
            {

                Testmylowest(hit.distance);
                Testmyhighest(hit.distance);
                Debug.Log("HEY IM A BLOCK");
               
            }
            else//block is grounded
            {
              
                counter++;
                Debug.Log("RESTART RANDOM SPAWN - " + counter);
                Theresablockhit = true;
            }
        }
        else//this is the ground
        {
            Testmylowest(hit.distance);
            Testmyhighest(hit.distance);
        }
       
      
    }
    //dont bother this yet
    void Testmylowest(float lowt)
    {
        
        if (lowest < lowt)
        {
            lowest = lowt;
        }
    }
    void Testmyhighest(float hight)
    {
        if (highest > hight)
        {
            highest = hight;
        }
    }
}
