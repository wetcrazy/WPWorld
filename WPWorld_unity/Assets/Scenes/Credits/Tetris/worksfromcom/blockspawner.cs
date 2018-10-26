using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockspawner : MonoBehaviour
{

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
    }

    //rotations
    // all rotations       num|      L ::   ::       ||  thumb       ::       ||  z       ::       ||    t    ::        ||      u    ::       ||    block
    Vector3 aaa;         //0  |   345  ::   ::       ||  thumb 345   ::       ||  z  45   ::3      ||  t 345  ::        ||   u 345   ::       ||  b 345
    Vector3 aba;         //1  |   147  ::   ::       ||  thumb 147   ::       ||  z  47   ::1      ||  t 147  ::        ||   u 147   ::       ||  b 147
    Vector3 aca;         //2  |   345  ::   ::       ||  thumb 345   ::       ||  z  34   ::5      ||  t 345  ::        ||   u 345   ::       ||  b 345
    Vector3 ada;         //3  |   147  ::   ::       ||  thumb 147   ::       ||  z  14   ::7      ||  t 147  ::        ||   u 147   ::       ||  b 147

    Vector3 baa;         //4  |   0345 ::   ::       ||  thumb 01345 ::       ||  z 0145  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
    Vector3 bba;         //5  |   1247 ::   ::       ||  thumb 12457 ::       ||  z 2457  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
    Vector3 bca;         //6  |   3458 ::   ::       ||  thumb 34578 ::       ||  z 3478  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
    Vector3 bda;         //7  |   1467 ::   ::       ||  thumb 13467 ::       ||  z 1346  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467

    Vector3 caa;         //8  |   3    ::45 ::       ||  thumb 34    ::5      ||  z  34   ::5      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
    Vector3 cba;         //9  |   1    ::47 ::       ||  thumb 14    ::7      ||  z  14   ::7      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
    Vector3 cca;         //10 |   5    ::34 ::       ||  thumb 45    ::3      ||  z  45   ::3      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
    Vector3 cda;         //11 |   7    ::14 ::       ||  thumb 47    ::1      ||  z  47   ::1      ||  t 4    ::17      ||   u 17    ::4      ||  b 147

    Vector3 daa;         //12 |   3456 ::   ::       ||  thumb 34567 ::       ||  z 4567  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
    Vector3 dba;         //13 |   0147 ::   ::       ||  thumb 01347 ::       ||  z 0347  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
    Vector3 dca;         //14 |   2345 ::   ::       ||  thumb 12345 ::       ||  z 1234  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
    Vector3 dda;         //15 |   1478 ::   ::       ||  thumb 14578 ::       ||  z 1458  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578

    Vector3 aab;         //16 |   34   ::   ::       ||  thumb 34    ::       ||  z 3     ::4      ||  t 4    ::3       ||   u 34    ::       ||  b 34
    Vector3 abb;         //17 |   14   ::   ::       ||  thumb 14    ::       ||  z 1     ::4      ||  t 4    ::1       ||   u 14    ::       ||  b 14
    Vector3 acb;         //18 |   45   ::   ::       ||  thumb 45    ::       ||  z 5     ::4      ||  t 4    ::5       ||   u 45    ::       ||  b 45
    Vector3 adb;         //19 |   47   ::   ::       ||  thumb 47    ::       ||  z 7     ::4      ||  t 4    ::7       ||   u 47    ::       ||  b 47

    Vector3 aad;         //20 |   4    ::   ::5      ||  thumb 4     ::5      ||  z 4     ::5      ||  t 4    ::5       ||   u 45    ::       ||  b 45
    Vector3 abd;         //21 |   4    ::   ::7      ||  thumb 4     ::7      ||  z 4     ::7      ||  t 4    ::7       ||   u 47    ::       ||  b 47
    Vector3 acd;         //22 |   4    ::   ::3      ||  thumb 4     ::3      ||  z 4     ::3      ||  t 4    ::3       ||   u 34    ::       ||  b 34
    Vector3 add;         //23 |   4    ::   ::1      ||  thumb 4     ::1      ||  z 4     ::1      ||  t 4    ::1       ||   u 14    ::       ||  b 14 

    List<Vector3> Rotations = new List<Vector3>();// list of rotations container
    Quaternion RotationOFF;
    void Setfixedrotations()//  24 rotations
    {
        aaa = new Vector3(0, 0, 0);
        aba = new Vector3(0, 90, 0);
        aca = new Vector3(0, 180, 0);
        ada = new Vector3(0, 270, 0);

        baa = new Vector3(90, 0, 0);
        bba = new Vector3(90, 90, 0);
        bca = new Vector3(90, 180, 0);
        bda = new Vector3(90, 270, 0);

        caa = new Vector3(180, 0, 0);
        cba = new Vector3(180, 90, 0);
        cca = new Vector3(180, 180, 0);
        cda = new Vector3(180, 270, 0);

        daa = new Vector3(270, 0, 0);
        dba = new Vector3(270, 90, 0);
        dca = new Vector3(270, 180, 0);
        dda = new Vector3(270, 270, 0);

        aab = new Vector3(0, 0, 90);
        abb = new Vector3(0, 90, 90);
        acb = new Vector3(0, 180, 90);
        adb = new Vector3(0, 270, 90);

        aad = new Vector3(0, 0, 270);
        abd = new Vector3(0, 90, 270);
        acd = new Vector3(0, 180, 270);
        add = new Vector3(0, 270, 270);



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
    //
    void ResetmyBool()//bool check
    {
        Transnegx = false;
        Transposx = false;
        Transnegz = false;
        Transposz = false;
    }

    float scaleof;//0.1f set manually to fit game

    int layerMask;
    RaycastHit hit;
    // This would cast rays only against colliders in layer 8.
    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
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
    float lowest;
    
    float highest;
    List<GameObject> PossibleBlocks = new List<GameObject>();
    
    List<Vector3> LPossibleRots = new List<Vector3>();
    List<Vector3> ThumbPossibleRots = new List<Vector3>();
    List<Vector3> ZPossibleRots = new List<Vector3>();
    List<Vector3> TPossibleRots = new List<Vector3>();
    List<Vector3> UPossibleRots = new List<Vector3>();
    List<Vector3> BlockPossibleRots = new List<Vector3>();

    List<Vector3> PossiblePOS = new List<Vector3>();
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
    bool Theresablockhit;
    void Checkmyfloor()
    {
        Floorchecker = new List<float>();//numbering system of terrain
                                         //  Floorpos = new List<Vector3>();
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
            for (int rpos = 0; rpos < Floorposchecker.Count; rpos++)
            {
                if (Physics.Raycast(transform.position + Floorposchecker[rpos] * scaleof, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
                {

                    float temp = lowest - hit.distance;

                    float adjusted = Mathf.Round(temp * 10f);

                    Floorchecker.Add(adjusted);
                }
            }
        }
    }
    //==============================================================================================


    
    

    Vector3 myfinaltranslation;


    private void Start()
    {
        Theresablockhit = false;
        Settermyvoids();//no need look at this 
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
         //ScanfloortoRot();
        PossibleRotations();

        //Instantiatemyblock();
        yield return new WaitForSeconds(1);//timer how long to wait


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


    //step3 with the given terrain find all possible and allowed rotations
    void ScanfloortoRot()
    {
      
        //// all rotations     //num|      L ::   ::       ||  thumb       ::       ||  z       ::       ||    t    ::        ||      u    ::       ||    block
        //Vector3 aaa;         //0  |   345  ::   ::       ||  thumb 345   ::       ||  z  45   ::3      ||  t 345  ::        ||   u 345   ::       ||  b 345
        //Vector3 aba;         //1  |   147  ::   ::       ||  thumb 147   ::       ||  z  47   ::1      ||  t 147  ::        ||   u 147   ::       ||  b 147
        //Vector3 aca;         //2  |   345  ::   ::       ||  thumb 345   ::       ||  z  34   ::5      ||  t 345  ::        ||   u 345   ::       ||  b 345
        //Vector3 ada;         //3  |   147  ::   ::       ||  thumb 147   ::       ||  z  14   ::7      ||  t 147  ::        ||   u 147   ::       ||  b 147
        //Vector3 baa;         //4  |   0345 ::   ::       ||  thumb 01345 ::       ||  z 0145  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        //Vector3 bba;         //5  |   1247 ::   ::       ||  thumb 12457 ::       ||  z 2457  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        //Vector3 bca;         //6  |   3458 ::   ::       ||  thumb 34578 ::       ||  z 3478  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        //Vector3 bda;         //7  |   1467 ::   ::       ||  thumb 13467 ::       ||  z 1346  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        //Vector3 caa;         //8  |   3    ::45 ::       ||  thumb 34    ::5      ||  z  34   ::5      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        //Vector3 cba;         //9  |   1    ::47 ::       ||  thumb 14    ::7      ||  z  14   ::7      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        //Vector3 cca;         //10 |   5    ::34 ::       ||  thumb 45    ::3      ||  z  45   ::3      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        //Vector3 cda;         //11 |   7    ::14 ::       ||  thumb 47    ::1      ||  z  47   ::1      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        //Vector3 daa;         //12 |   3456 ::   ::       ||  thumb 34567 ::       ||  z 4567  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        //Vector3 dba;         //13 |   0147 ::   ::       ||  thumb 01347 ::       ||  z 0347  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        //Vector3 dca;         //14 |   2345 ::   ::       ||  thumb 12345 ::       ||  z 1234  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        //Vector3 dda;         //15 |   1478 ::   ::       ||  thumb 14578 ::       ||  z 1458  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        //Vector3 aab;         //16 |   34   ::   ::       ||  thumb 34    ::       ||  z 3     ::4      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        //Vector3 abb;         //17 |   14   ::   ::       ||  thumb 14    ::       ||  z 1     ::4      ||  t 4    ::1       ||   u 14    ::       ||  b 14
        //Vector3 acb;         //18 |   45   ::   ::       ||  thumb 45    ::       ||  z 5     ::4      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        //Vector3 adb;         //19 |   47   ::   ::       ||  thumb 47    ::       ||  z 7     ::4      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        //Vector3 aad;         //20 |   4    ::   ::5      ||  thumb 4     ::5      ||  z 4     ::5      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        //Vector3 abd;         //21 |   4    ::   ::7      ||  thumb 4     ::7      ||  z 4     ::7      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        //Vector3 acd;         //22 |   4    ::   ::3      ||  thumb 4     ::3      ||  z 4     ::3      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        //Vector3 add;         //23 |   4    ::   ::1      ||  thumb 4     ::1      ||  z 4     ::1      ||  t 4    ::1       ||   u 14    ::       ||  b 14 



        //num|      L ::   ::  
        //0  |  L 345  ::   ::  
        //2  |  L 345  ::   ::  
        //0  |  thumb 345   ::  
        //2  |  thumb 345   ::  
        //0  |  t 345  ::   
        //2  |  t 345  ::   
        //0  |  u 345   ::   
        //2  |  u 345   ::   
        //0  |  b 345
        //2  |  b 345
        //8  |  b 345
        //10 |  b 345
        if ((Floorchecker[3] == 0) &&
     (Floorchecker[4] == 0) &&
     (Floorchecker[5] == 0))
        {
            LPossibleRots.Add(Rotations[0]);
            LPossibleRots.Add(Rotations[2]);
            ThumbPossibleRots.Add(Rotations[0]);
            ThumbPossibleRots.Add(Rotations[2]);
            TPossibleRots.Add(Rotations[0]);
            TPossibleRots.Add(Rotations[2]);
            UPossibleRots.Add(Rotations[0]);
            UPossibleRots.Add(Rotations[2]);
            BlockPossibleRots.Add(Rotations[0]);
            BlockPossibleRots.Add(Rotations[2]);
            BlockPossibleRots.Add(Rotations[8]);
            BlockPossibleRots.Add(Rotations[10]);
        }
        //1  |  L 147  ::   ::  
        //3  |  L 147  ::   ::  
        //1  |  thumb 147   ::  
        //3  |  thumb 147   ::  
        //1  |  t 147  ::   
        //3  |  t 147  ::   
        //1  |  u 147   ::   
        //3  |  u 147   ::   
        //1  |  b 147
        //3  |  b 147
        //9  |  b 147
        //11 |  b 147
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[7] == 0))
        {
            LPossibleRots.Add(Rotations[1]);
            LPossibleRots.Add(Rotations[3]);
            ThumbPossibleRots.Add(Rotations[1]);
            ThumbPossibleRots.Add(Rotations[3]);
            TPossibleRots.Add(Rotations[1]);
            TPossibleRots.Add(Rotations[3]);
            UPossibleRots.Add(Rotations[1]);
            UPossibleRots.Add(Rotations[3]);
            BlockPossibleRots.Add(Rotations[1]);
            BlockPossibleRots.Add(Rotations[3]);
            BlockPossibleRots.Add(Rotations[9]);
            BlockPossibleRots.Add(Rotations[11]);
        }

        //16 |  L 34        ::   ::  
        //16 |  thumb 34    ::  
        //16 |  u 34        ::   
        //22 |  u 34        ::   
        //16 |  b 34
        //22 |  b 34
        if ((Floorchecker[3] == 0) &&
          (Floorchecker[4] == 0))
        {
            LPossibleRots.Add(Rotations[16]);
            ThumbPossibleRots.Add(Rotations[16]);
            UPossibleRots.Add(Rotations[16]);
            UPossibleRots.Add(Rotations[22]);
            BlockPossibleRots.Add(Rotations[16]);
            BlockPossibleRots.Add(Rotations[22]);
        }
        //18 |  L 45        ::   ::  
        //18 |  thumb 45    ::  
        //18 |  u 45        ::   
        //20 |  u 45        ::   
        //18 |  b 45
        //20 |  b 45
        if ((Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0))
        {
            LPossibleRots.Add(Rotations[18]);
            ThumbPossibleRots.Add(Rotations[18]);
            UPossibleRots.Add(Rotations[18]);
            UPossibleRots.Add(Rotations[20]);
            BlockPossibleRots.Add(Rotations[18]);
            BlockPossibleRots.Add(Rotations[20]);
        }
        //17 |  L 14        ::   ::  
        //17 |  thumb 14    ::  
        //17 |  u 14        ::   
        //23 |  u 14        ::   
        //17 |  b 14
        //23 |  b 14 
        if ((Floorchecker[1] == 0) &&
       (Floorchecker[4] == 0))
        {
            LPossibleRots.Add(Rotations[17]);
            ThumbPossibleRots.Add(Rotations[17]);
            UPossibleRots.Add(Rotations[17]);
            UPossibleRots.Add(Rotations[23]);
            BlockPossibleRots.Add(Rotations[17]);
            BlockPossibleRots.Add(Rotations[23]);
        }
        //19 |  L 47        ::   ::  
        //19 |  thumb 47    ::  
        //19 |  u 47        ::   
        //21 |  u 47        ::   
        //19 |  b 47
        //21 |  b 47
        if ((Floorchecker[4] == 0) &&
       (Floorchecker[7] == 0))
        {
            LPossibleRots.Add(Rotations[19]);
            ThumbPossibleRots.Add(Rotations[19]);
            UPossibleRots.Add(Rotations[19]);
            UPossibleRots.Add(Rotations[21]);
            BlockPossibleRots.Add(Rotations[19]);
            BlockPossibleRots.Add(Rotations[21]);
        }
        //8  |  thumb 34    ::5 
        //2  |  z  34       ::5 
        //8  |  z  34       ::5 
        if ((Floorchecker[3] == 0) &&
          (Floorchecker[4] == 0) &&
          (Floorchecker[5] == 1))
        {

            ThumbPossibleRots.Add(Rotations[8]);
            ZPossibleRots.Add(Rotations[2]);
            ZPossibleRots.Add(Rotations[8]);

        }
        //10 |  thumb 45    ::3 
        //0  |  z  45       ::3 
        //10 |  z  45       ::3
        if ((Floorchecker[3] == 1) &&
        (Floorchecker[4] == 0) &&
        (Floorchecker[5] == 0))
        {

            ThumbPossibleRots.Add(Rotations[10]);
            ZPossibleRots.Add(Rotations[0]);
            ZPossibleRots.Add(Rotations[10]);

        }
        //9  |  thumb 14    ::7 
        //3  |  z  14       ::7 
        //9  |  z  14       ::7 
        if ((Floorchecker[1] == 0) &&
        (Floorchecker[4] == 0) &&
        (Floorchecker[7] == 1))
        {

            ThumbPossibleRots.Add(Rotations[9]);
            ZPossibleRots.Add(Rotations[3]);
            ZPossibleRots.Add(Rotations[9]);

        }
        //11 |  thumb 47    ::1 
        //1  |  z  47       ::1 
        //11 |  z  47       ::1 
        if ((Floorchecker[1] == 1) &&
        (Floorchecker[4] == 0) &&
        (Floorchecker[7] == 0))
        {
            ThumbPossibleRots.Add(Rotations[11]);
            ZPossibleRots.Add(Rotations[1]);
            ZPossibleRots.Add(Rotations[11]);
        }
        //8  |  u 35    ::4  
        //10 |  u 35    ::4  
        if ((Floorchecker[3] == 0) &&
        (Floorchecker[4] == 1) &&
        (Floorchecker[5] == 0))
        {
            UPossibleRots.Add(Rotations[8]);
            UPossibleRots.Add(Rotations[10]);
        }
        //9  |  u 17    ::4  
        //11 |  u 17    ::4  
        if ((Floorchecker[1] == 0) &&
        (Floorchecker[4] == 1) &&
        (Floorchecker[7] == 0))
        {
            UPossibleRots.Add(Rotations[9]);
            UPossibleRots.Add(Rotations[11]);
        }
        //8  |  t 4    ::35 
        //10 |  t 4    ::35 
        if ((Floorchecker[3] == 1) &&
       (Floorchecker[4] == 0) &&
       (Floorchecker[5] == 1))
        {
            TPossibleRots.Add(Rotations[8]);
            TPossibleRots.Add(Rotations[10]);
        }
        //9  |  t 4    ::17 
        //11 |  t 4    ::17 
        if ((Floorchecker[1] == 1) &&
      (Floorchecker[4] == 0) &&
      (Floorchecker[7] == 1))
        {
            TPossibleRots.Add(Rotations[9]);
            TPossibleRots.Add(Rotations[11]);
        }
        //20 |  thumb 4     ::5 
        //20 |  z 4         ::5 
        //18 |  t 4         ::5  
        //20 |  t 4         ::5  
        if ((Floorchecker[4] == 0) &&
            (Floorchecker[5] == 1))
        {
            ThumbPossibleRots.Add(Rotations[20]);

            ZPossibleRots.Add(Rotations[20]);

            TPossibleRots.Add(Rotations[18]);
            TPossibleRots.Add(Rotations[20]);
        }
        //23 |  thumb 4     ::1 
        //23 |  z 4         ::1 
        //17 |  t 4         ::1  
        //23 |  t 4         ::1  
        if ((Floorchecker[4] == 0) &&
           (Floorchecker[1] == 1))
        {
            ThumbPossibleRots.Add(Rotations[23]);

            ZPossibleRots.Add(Rotations[23]);

            TPossibleRots.Add(Rotations[17]);
            TPossibleRots.Add(Rotations[23]);
        }
        //21 |  thumb 4     ::7 
        //21 |  z 4         ::7 
        //19 |  t 4         ::7  
        //21 |  t 4         ::7  
        if ((Floorchecker[4] == 0) &&
           (Floorchecker[7] == 1))
        {
            ThumbPossibleRots.Add(Rotations[21]);

            ZPossibleRots.Add(Rotations[21]);

            TPossibleRots.Add(Rotations[19]);
            TPossibleRots.Add(Rotations[21]);
        }
        //22 |  thumb 4     ::3 
        //22 |  z 4         ::3 
        //16 |  t 4         ::3  
        //22 |  t 4         ::3  
        if ((Floorchecker[4] == 0) &&
           (Floorchecker[3] == 1))
        {
            ThumbPossibleRots.Add(Rotations[22]);

            ZPossibleRots.Add(Rotations[22]);

            TPossibleRots.Add(Rotations[16]);
            TPossibleRots.Add(Rotations[22]);
        }






        //8  |  L 3    ::45 ::  
        if ((Floorchecker[3] == 0) &&
           (Floorchecker[4] == 1) &&
           (Floorchecker[5] == 1))
        {
            LPossibleRots.Add(Rotations[8]);
        }
        //10 |  L 5    ::34 ::  
        if ((Floorchecker[3] == 1) &&
         (Floorchecker[4] == 1) &&
         (Floorchecker[5] == 0))
        {
            LPossibleRots.Add(Rotations[10]);
        }
        //9  |  L 1    ::47 ::  
        if ((Floorchecker[1] == 0) &&
       (Floorchecker[4] == 1) &&
       (Floorchecker[7] == 1))
        {
            LPossibleRots.Add(Rotations[9]);
        }
        //11 |  L 7    ::14 ::  
        if ((Floorchecker[1] == 1) &&
     (Floorchecker[4] == 1) &&
     (Floorchecker[7] == 0))
        {
            LPossibleRots.Add(Rotations[11]);
        }
        //20 |  L 4    ::   ::5 
        if ((Floorchecker[4] == 0) &&
         (Floorchecker[5] == 2))
        {
            LPossibleRots.Add(Rotations[20]);
        }
        //22 |  L 4    ::   ::3 
        if ((Floorchecker[4] == 0) &&
      (Floorchecker[3] == 2))
        {
            LPossibleRots.Add(Rotations[22]);
        }
        //21 |  L 4    ::   ::7 
        if ((Floorchecker[4] == 0) &&
      (Floorchecker[7] == 2))
        {
            LPossibleRots.Add(Rotations[21]);
        }
        //23 |  L 4    ::   ::1 
        if ((Floorchecker[4] == 0) &&
      (Floorchecker[1] == 2))
        {
            LPossibleRots.Add(Rotations[23]);
        }
        //16 |  z 3     ::4 
        if ((Floorchecker[4] == 1) &&
      (Floorchecker[3] == 0))
        {
            ZPossibleRots.Add(Rotations[16]);
        }
        //17 |  z 1     ::4 
        if ((Floorchecker[4] == 1) &&
     (Floorchecker[1] == 0))
        {
            ZPossibleRots.Add(Rotations[17]);
        }
        //18 |  z 5     ::4 
        if ((Floorchecker[4] == 1) &&
     (Floorchecker[5] == 0))
        {
            ZPossibleRots.Add(Rotations[18]);
        }
        //19 |  z 7     ::4 
        if ((Floorchecker[4] == 1) &&
     (Floorchecker[7] == 0))
        {
            ZPossibleRots.Add(Rotations[19]);
        }
        //--------------------------------------------------------------------

        //4  |  L 0345 ::   ::  
        if ((Floorchecker[3] == 0) &&
       (Floorchecker[4] == 0) &&
       (Floorchecker[5] == 0) &&
       (Floorchecker[0] == 0))
        {
            LPossibleRots.Add(Rotations[4]);
        }
        //14 |  L 2345 ::   ::  
        if ((Floorchecker[3] == 0) &&
       (Floorchecker[4] == 0) &&
       (Floorchecker[5] == 0) &&
       (Floorchecker[2] == 0))
        {
            LPossibleRots.Add(Rotations[14]);
        }
        //6  |  L 3458 ::   ::  
        if ((Floorchecker[3] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0) &&
         (Floorchecker[8] == 0))
        {
            LPossibleRots.Add(Rotations[6]);
        }
        //12 |  L 3456 ::   ::  
        if ((Floorchecker[3] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0) &&
         (Floorchecker[6] == 0))
        {
            LPossibleRots.Add(Rotations[12]);
        }

        //13 |  L 0147 ::   ::  
        if ((Floorchecker[1] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[7] == 0) &&
         (Floorchecker[0] == 0))
        {
            LPossibleRots.Add(Rotations[13]);
        }
        //5  |  L 1247 ::   ::  
        if ((Floorchecker[1] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[7] == 0) &&
         (Floorchecker[2] == 0))
        {
            LPossibleRots.Add(Rotations[5]);
        }
        //7  |  L 1467 ::   ::  
        if ((Floorchecker[1] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[6] == 0) &&
         (Floorchecker[7] == 0))
        {
            LPossibleRots.Add(Rotations[7]);
        }
        //15 |  L 1478 ::   ::  
        if ((Floorchecker[1] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[7] == 0) &&
         (Floorchecker[8] == 0))
        {
            LPossibleRots.Add(Rotations[15]);
        }


        //num|  thumb       ::  
        //4  |  thumb 01345 ::
        if ((Floorchecker[3] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0) &&
         (Floorchecker[0] == 0) &&
         (Floorchecker[1] == 0))
        {
            ThumbPossibleRots.Add(Rotations[4]);
        }
        //5  |  thumb 12457 ::
        if ((Floorchecker[1] == 0) &&
         (Floorchecker[2] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0) &&
         (Floorchecker[7] == 0))
        {
            ThumbPossibleRots.Add(Rotations[5]);
        }
        //6  |  thumb 34578 ::  
        if ((Floorchecker[3] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0) &&
         (Floorchecker[7] == 0) &&
         (Floorchecker[8] == 0))
        {
            ThumbPossibleRots.Add(Rotations[6]);
        }
        //7  |  thumb 13467 ::
        if ((Floorchecker[1] == 0) &&
         (Floorchecker[3] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[6] == 0) &&
         (Floorchecker[7] == 0))
        {
            ThumbPossibleRots.Add(Rotations[7]);
        }
        //12 |  thumb 34567 ::  
        if ((Floorchecker[3] == 0) &&
         (Floorchecker[4] == 0) &&
         (Floorchecker[5] == 0) &&
         (Floorchecker[6] == 0) &&
         (Floorchecker[7] == 0))
        {
            ThumbPossibleRots.Add(Rotations[12]);
        }
        //13 |  thumb 01347 ::  
        if ((Floorchecker[0] == 0) &&
            (Floorchecker[1] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[7] == 0))
        {
            ThumbPossibleRots.Add(Rotations[13]);
        }
        //14 |  thumb 12345 ::  
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[2] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0))
        {
            ThumbPossibleRots.Add(Rotations[14]);
        }
        //15 |  thumb 14578 ::  
        if ((Floorchecker[1] == 0) &&
           (Floorchecker[4] == 0) &&
           (Floorchecker[5] == 0) &&
           (Floorchecker[7] == 0) &&
           (Floorchecker[8] == 0))
        {
            ThumbPossibleRots.Add(Rotations[15]);
        }


        //num|  z       ::  
        //4  |  z 0145  ::  
        if ((Floorchecker[0] == 0) &&
           (Floorchecker[1] == 0) &&
           (Floorchecker[4] == 0) &&
           (Floorchecker[5] == 0))
        {
            ZPossibleRots.Add(Rotations[4]);
        }
        //5  |  z 2457  ::  
        if ((Floorchecker[2] == 0) &&
           (Floorchecker[4] == 0) &&
           (Floorchecker[5] == 0) &&
           (Floorchecker[7] == 0))
        {
            ZPossibleRots.Add(Rotations[5]);
        }
        //6  |  z 3478  ::  
        if ((Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[7] == 0) &&
            (Floorchecker[8] == 0))
        {
            ZPossibleRots.Add(Rotations[6]);
        }
        //7  |  z 1346  :: 
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[6] == 0))
        {
            ZPossibleRots.Add(Rotations[7]);
        }
        //12 |  z 4567  ::  
        if ((Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0) &&
            (Floorchecker[6] == 0) &&
            (Floorchecker[7] == 0))
        {
            ZPossibleRots.Add(Rotations[12]);
        }
        //13 |  z 0347  ::  
        if ((Floorchecker[0] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[7] == 0))
        {
            ZPossibleRots.Add(Rotations[13]);
        }
        //14 |  z 1234  ::  
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[2] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0))
        {
            ZPossibleRots.Add(Rotations[14]);
        }
        //15 |  z 1458  ::  
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0) &&
            (Floorchecker[8] == 0))
        {
            ZPossibleRots.Add(Rotations[15]);
        }


        //num|    t    ::   
        //4  |  t 1345 ::   
        //14 |  t 1345 ::  
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0) &&
            (Floorchecker[3] == 0))
        {
            TPossibleRots.Add(Rotations[4]);
            TPossibleRots.Add(Rotations[14]);
        }
        //5  |  t 1457 ::   
        //15 |  t 1457 ::   
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0) &&
            (Floorchecker[7] == 0))
        {
            TPossibleRots.Add(Rotations[5]);
            TPossibleRots.Add(Rotations[15]);
        }
        //6  |  t 3457 ::   
        //12 |  t 3457 ::   
        if ((Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0) &&
            (Floorchecker[7] == 0))
        {
            TPossibleRots.Add(Rotations[6]);
            TPossibleRots.Add(Rotations[12]);
        }
        //7  |  t 1347 ::   
        //13 |  t 1347 ::   
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[7] == 0))
        {
            TPossibleRots.Add(Rotations[7]);
            TPossibleRots.Add(Rotations[13]);
        }

        //num|     u    ::   
        //4  |  u 02345 ::   
        //14 |  u 02345 ::   
        if ((Floorchecker[0] == 0) &&
        (Floorchecker[2] == 0) &&
        (Floorchecker[3] == 0) &&
        (Floorchecker[4] == 0) &&
        (Floorchecker[5] == 0))
        {
            UPossibleRots.Add(Rotations[4]);
            UPossibleRots.Add(Rotations[14]);
        }
        //5  |  u 12478 ::   
        //15 |  u 12478 ::   
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[2] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[7] == 0) &&
            (Floorchecker[8] == 0))
        {
            UPossibleRots.Add(Rotations[5]);
            UPossibleRots.Add(Rotations[15]);
        }
        //6  |  u 34568 ::   
        //12 |  u 34568 ::   
        if ((Floorchecker[6] == 0) &&
            (Floorchecker[8] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0))
        {
            UPossibleRots.Add(Rotations[6]);
            UPossibleRots.Add(Rotations[12]);
        }
        //7  |  u 01467 ::   
        //13 |  u 01467 ::   
        if ((Floorchecker[0] == 0) &&
            (Floorchecker[1] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[6] == 0) &&
            (Floorchecker[7] == 0))
        {
            UPossibleRots.Add(Rotations[7]);
            UPossibleRots.Add(Rotations[13]);
        }



        //num|    block
        //4  |  b 012345
        //14 |  b 012345
        if ((Floorchecker[0] == 0) &&
            (Floorchecker[1] == 0) &&
            (Floorchecker[2] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0))
        {
            BlockPossibleRots.Add(Rotations[4]);
            BlockPossibleRots.Add(Rotations[14]);
        }
        //5  |  b 124578
        //15 |  b 124578
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[2] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0) &&
            (Floorchecker[7] == 0) &&
            (Floorchecker[8] == 0))
        {
            BlockPossibleRots.Add(Rotations[5]);
            BlockPossibleRots.Add(Rotations[15]);
        }
        //6  |  b 345678
        //12 |  b 345678
        if ((Floorchecker[6] == 0) &&
            (Floorchecker[7] == 0) &&
            (Floorchecker[8] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[5] == 0))
        {
            BlockPossibleRots.Add(Rotations[4]);
            BlockPossibleRots.Add(Rotations[14]);
        }
        //7  |  b 013467
        //13 |  b 013467
        if ((Floorchecker[1] == 0) &&
            (Floorchecker[0] == 0) &&
            (Floorchecker[4] == 0) &&
            (Floorchecker[3] == 0) &&
            (Floorchecker[7] == 0) &&
            (Floorchecker[6] == 0))
        {
            BlockPossibleRots.Add(Rotations[7]);
            BlockPossibleRots.Add(Rotations[13]);
        }
    }
    void PossibleRotations()
    {
        PossiblePOS = new List<Vector3>();
        LPossibleRots = new List<Vector3>();
        ThumbPossibleRots = new List<Vector3>();
        ZPossibleRots = new List<Vector3>();
        TPossibleRots = new List<Vector3>();
        UPossibleRots = new List<Vector3>();
        BlockPossibleRots = new List<Vector3>();
        //// all rotations                          //num| L      ::   ::       ||  thumb       ::       ||  z       ::       ||    t    ::        ||      u    ::       ||    block
        //Vector3 aaa = new Vector3(0, 0, 0);       //0  | L 345  ::   ::       ||  thumb 345   ::       ||  z  45   ::3      ||  t 345  ::        ||   u 345   ::       ||  b 345
        //Vector3 aba = new Vector3(0, 90, 0);      //1  | L 147  ::   ::       ||  thumb 147   ::       ||  z  47   ::1      ||  t 147  ::        ||   u 147   ::       ||  b 147
        //Vector3 aca = new Vector3(0, 180, 0);     //2  | L 345  ::   ::       ||  thumb 345   ::       ||  z  34   ::5      ||  t 345  ::        ||   u 345   ::       ||  b 345
        //Vector3 ada = new Vector3(0, 270, 0);     //3  | L 147  ::   ::       ||  thumb 147   ::       ||  z  14   ::7      ||  t 147  ::        ||   u 147   ::       ||  b 147
        //Vector3 baa = new Vector3(90, 0, 0);      //4  | L 0345 ::   ::       ||  thumb 01345 ::       ||  z 0145  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        //Vector3 bba = new Vector3(90, 90, 0);     //5  | L 1247 ::   ::       ||  thumb 12457 ::       ||  z 2457  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        //Vector3 bca = new Vector3(90, 180, 0);    //6  | L 3458 ::   ::       ||  thumb 34578 ::       ||  z 3478  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        //Vector3 bda = new Vector3(90, 270, 0);    //7  | L 1467 ::   ::       ||  thumb 13467 ::       ||  z 1346  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        //Vector3 caa = new Vector3(180, 0, 0);     //8  | L 3    ::45 ::       ||  thumb 34    ::5      ||  z  34   ::5      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        //Vector3 cba = new Vector3(180, 90, 0);    //9  | L 1    ::47 ::       ||  thumb 14    ::7      ||  z  14   ::7      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        //Vector3 cca = new Vector3(180, 180, 0);   //10 | L 5    ::34 ::       ||  thumb 45    ::3      ||  z  45   ::3      ||  t 4    ::35      ||   u 35    ::4      ||  b 345
        //Vector3 cda = new Vector3(180, 270, 0);   //11 | L 7    ::14 ::       ||  thumb 47    ::1      ||  z  47   ::1      ||  t 4    ::17      ||   u 17    ::4      ||  b 147
        //Vector3 daa = new Vector3(270, 0, 0);     //12 | L 3456 ::   ::       ||  thumb 34567 ::       ||  z 4567  ::       ||  t 3457 ::        ||   u 34568 ::       ||  b 345678
        //Vector3 dba = new Vector3(270, 90, 0);    //13 | L 0147 ::   ::       ||  thumb 01347 ::       ||  z 0347  ::       ||  t 1347 ::        ||   u 01467 ::       ||  b 013467
        //Vector3 dca = new Vector3(270, 180, 0);   //14 | L 2345 ::   ::       ||  thumb 12345 ::       ||  z 1234  ::       ||  t 1345 ::        ||   u 02345 ::       ||  b 012345
        //Vector3 dda = new Vector3(270, 270, 0);   //15 | L 1478 ::   ::       ||  thumb 14578 ::       ||  z 1458  ::       ||  t 1457 ::        ||   u 12478 ::       ||  b 124578
        //Vector3 aab = new Vector3(0, 0, 90);      //16 | L 34   ::   ::       ||  thumb 34    ::       ||  z 3     ::4      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        //Vector3 abb = new Vector3(0, 90, 90);     //17 | L 14   ::   ::       ||  thumb 14    ::       ||  z 1     ::4      ||  t 4    ::1       ||   u 14    ::       ||  b 14
        //Vector3 acb = new Vector3(0, 180, 90);    //18 | L 45   ::   ::       ||  thumb 45    ::       ||  z 5     ::4      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        //Vector3 adb = new Vector3(0, 270, 90);    //19 | L 47   ::   ::       ||  thumb 47    ::       ||  z 7     ::4      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        //Vector3 aad = new Vector3(0, 0, 270);     //20 | L 4    ::   ::5      ||  thumb 4     ::5      ||  z 4     ::5      ||  t 4    ::5       ||   u 45    ::       ||  b 45
        //Vector3 abd = new Vector3(0, 90, 270);    //21 | L 4    ::   ::7      ||  thumb 4     ::7      ||  z 4     ::7      ||  t 4    ::7       ||   u 47    ::       ||  b 47
        //Vector3 acd = new Vector3(0, 180, 270);   //22 | L 4    ::   ::3      ||  thumb 4     ::3      ||  z 4     ::3      ||  t 4    ::3       ||   u 34    ::       ||  b 34
        //Vector3 add = new Vector3(0, 270, 270);   //23 | L 4    ::   ::1      ||  thumb 4     ::1      ||  z 4     ::1      ||  t 4    ::1       ||   u 14    ::       ||  b 14 


        //0,1,2
        //3,4,5
        //6,7,8

        //lowest
        //(Floorchecker[]==0)

        //theres something there
        //(Floorchecker[]==1)

        //can place something 
        //(Floorchecker[]==2)

        //ttoo high cant reach 0 
        //(Floorchecker[]==3)
        Vector3 topleft = new Vector3(-1, 0, 1);
        Vector3 top = new Vector3(0, 0, 1);
        Vector3 topright = new Vector3(1, 0, 1);
        Vector3 left = new Vector3(-1, 0, 0);
        Vector3 center = new Vector3(0, 0, 0);
        Vector3 right = new Vector3(1, 0, 0);
        Vector3 botleft = new Vector3(-1, 0, -1);
        Vector3 bot = new Vector3(0, 0, -1);
        Vector3 botright = new Vector3(-1, 0, -1);

        ScanfloortoRot();

        int myRandomblock = Random.Range(0, 6);
        //select my block
        switch (myRandomblock)
        {
            case 0://L   //------------------------------------------------check this
                {


                    //num| L      ::   :: 


                    //-------------------------------------------------------------------------------HORIZONTAL

                    //0  | L 345  ::   ::       //Vector3 aaa = new Vector3(0, 0, 0);    
                    //2  | L 345  ::   ::       //Vector3 aca = new Vector3(0, 180, 0);  

                    //8  | L 3    ::45 ::       //Vector3 caa = new Vector3(180, 0, 0);  
                    //10 | L 5    ::34 ::       //Vector3 cca = new Vector3(180, 180, 0);

                    //4  | L 0345 ::   ::       //Vector3 baa = new Vector3(90, 0, 0);   
                    //6  | L 3458 ::   ::       //Vector3 bca = new Vector3(90, 180, 0); 

                    //12 | L 3456 ::   ::       //Vector3 daa = new Vector3(270, 0, 0);  
                    //14 | L 2345 ::   ::       //Vector3 dca = new Vector3(270, 180, 0);

                    //16 | L 34   ::   ::       //Vector3 aab = new Vector3(0, 0, 90);   
                    //18 | L 45   ::   ::       //Vector3 acb = new Vector3(0, 180, 90); 

                    //20 | L 4    ::   ::5      //Vector3 aad = new Vector3(0, 0, 270);  
                    //22 | L 4    ::   ::3      //Vector3 acd = new Vector3(0, 180, 270);

                    //------------------------------------------------------------------------------------vERTICAL

                    //1  | L 147  ::   ::       //Vector3 aba = new Vector3(0, 90, 0);   
                    //3  | L 147  ::   ::       //Vector3 ada = new Vector3(0, 270, 0);  

                    //5  | L 1247 ::   ::       //Vector3 bba = new Vector3(90, 90, 0);  
                    //7  | L 1467 ::   ::       //Vector3 bda = new Vector3(90, 270, 0); 

                    //9  | L 1    ::47 ::       //Vector3 cba = new Vector3(180, 90, 0); 
                    //11 | L 7    ::14 ::       //Vector3 cda = new Vector3(180, 270, 0);

                    //13 | L 0147 ::   ::       //Vector3 dba = new Vector3(270, 90, 0); 
                    //15 | L 1478 ::   ::       //Vector3 dda = new Vector3(270, 270, 0);

                    //17 | L 14   ::   ::       //Vector3 abb = new Vector3(0, 90, 90);  
                    //19 | L 47   ::   ::       //Vector3 adb = new Vector3(0, 270, 90); 

                    //21 | L 4    ::   ::7      //Vector3 abd = new Vector3(0, 90, 270); 
                    //23 | L 4    ::   ::1      //Vector3 add = new Vector3(0, 270, 270);

                    if (LPossibleRots.Count != 0)
                    {
                         int rotpos = Random.Range(0, LPossibleRots.Count);
                        //theres no rotations

                        //num|       L::   :: 
                        //0  |   345  ::   :: 
                        //2  |   345  ::   :: 

                        if ((LPossibleRots[rotpos].Equals(Rotations[0])) ||
                           (LPossibleRots[rotpos].Equals(Rotations[2])))
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
                        //8  |   3    ::45 :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[8]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 1) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 1) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 1) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        //10 |   5    ::34 :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[10]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 1) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //16 |   34   ::   :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[16]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }

                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        //22 |   4    ::   ::3
                        if (LPossibleRots[rotpos].Equals(Rotations[22]))
                        {
                            if ((Floorchecker[0] == 2) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 2) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }

                            if ((Floorchecker[3] == 2) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 2) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                            if ((Floorchecker[6] == 2) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 2) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }

                        //18 |   45   ::   :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[18]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }

                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);

                            }
                        }
                        //20 |   4    ::   ::5
                        if (LPossibleRots[rotpos].Equals(Rotations[20]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 2))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 2))
                            {
                                PossiblePOS.Add(top);
                            }

                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 2))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 2))
                            {
                                PossiblePOS.Add(center);
                            }

                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 2))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 2))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //1  |   147  ::   :: 
                        //3  |   147  ::   :: 
                        if ((LPossibleRots[rotpos].Equals(Rotations[1])) ||
                        (LPossibleRots[rotpos].Equals(Rotations[3])))
                        {

                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                        }
                        //9  |   1    ::47 :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[9]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 1) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 1) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 1) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        //11 |   7    ::14 :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[11]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 1) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }



                        //17 |   14   ::   :: 
                        //23 |   4    ::   ::1
                        if (LPossibleRots[rotpos].Equals(Rotations[17]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }

                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }

                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }

                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[23]))
                        {
                            if ((Floorchecker[0] == 2) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[3] == 2) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }

                            if ((Floorchecker[1] == 2) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 2) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }

                            if ((Floorchecker[2] == 2) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[5] == 2) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        //19 |   47   ::   :: 
                        //21 |   4    ::   ::7
                        if (LPossibleRots[rotpos].Equals(Rotations[19]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }

                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[21]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 2))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 2))
                            {
                                PossiblePOS.Add(left);
                            }

                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 2))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 2))
                            {
                                PossiblePOS.Add(center);
                            }

                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 2))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 2))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //4  |   0345 ::   :: 
                        //14 |   2345 ::   :: 
                        //6  |   3458 ::   :: 
                        //12 |   3456 ::   :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[4]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[14]))
                        {
                            if ((Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[6]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[12]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }

                        //7  |   1467 ::   :: 
                        //13 |   0147 ::   :: 
                        //5  |   1247 ::   :: 
                        //15 |   1478 ::   :: 
                        if (LPossibleRots[rotpos].Equals(Rotations[7]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[13]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[5]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }
                        if (LPossibleRots[rotpos].Equals(Rotations[15]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }


                        int posran = Random.Range(0, PossiblePOS.Count);

                        Vector3 hey = PossiblePOS[posran];
                        Vector3 hi = LPossibleRots[rotpos];
                        INstanmyBLCK(myRandomblock, hey, hi);
                    }
                    break;
                }
            case 1://Thumb 
                {
                    //num|||  thumb       ::    

                    //-----------------------------------------------------------------------------------------------HORIZONTAL

                    //0  |||  thumb 345   ::        //Vector3 aaa = new Vector3(0, 0, 0);    
                    //2  |||  thumb 345   ::        //Vector3 aca = new Vector3(0, 180, 0);  

                    //8  |||  thumb 34    ::5       //Vector3 caa = new Vector3(180, 0, 0);  
                    //10 |||  thumb 45    ::3       //Vector3 cca = new Vector3(180, 180, 0);

                    //16 |||  thumb 34    ::        //Vector3 aab = new Vector3(0, 0, 90);   
                    //18 |||  thumb 45    ::        //Vector3 acb = new Vector3(0, 180, 90); 

                    //20 |||  thumb 4     ::5       //Vector3 aad = new Vector3(0, 0, 270);  
                    //22 |||  thumb 4     ::3       //Vector3 acd = new Vector3(0, 180, 270);

                    //4  |||  thumb 01345 ::        //Vector3 baa = new Vector3(90, 0, 0);   
                    //6  |||  thumb 34578 ::        //Vector3 bca = new Vector3(90, 180, 0); 

                    //12 |||  thumb 34567 ::        //Vector3 daa = new Vector3(270, 0, 0);  
                    //14 |||  thumb 12345 ::        //Vector3 dca = new Vector3(270, 180, 0);

                    //----------------------------------------------------------------------------------------------vERTICAL

                    //1  |||  thumb 147   ::        //Vector3 aba = new Vector3(0, 90, 0);   
                    //3  |||  thumb 147   ::        //Vector3 ada = new Vector3(0, 270, 0);  

                    //9  |||  thumb 14    ::7       //Vector3 cba = new Vector3(180, 90, 0); 
                    //11 |||  thumb 47    ::1       //Vector3 cda = new Vector3(180, 270, 0);

                    //17 |||  thumb 14    ::        //Vector3 abb = new Vector3(0, 90, 90);  
                    //19 |||  thumb 47    ::        //Vector3 adb = new Vector3(0, 270, 90); 

                    //21 |||  thumb 4     ::7       //Vector3 abd = new Vector3(0, 90, 270); 
                    //23 |||  thumb 4     ::1       //Vector3 add = new Vector3(0, 270, 270);

                    //5  |||  thumb 12457 ::        //Vector3 bba = new Vector3(90, 90, 0);  
                    //7  |||  thumb 13467 ::        //Vector3 bda = new Vector3(90, 270, 0); 

                    //13 |||  thumb 01347 ::        //Vector3 dba = new Vector3(270, 90, 0); 
                    //15 |||  thumb 14578 ::        //Vector3 dda = new Vector3(270, 270, 0);





                    if (ThumbPossibleRots.Count != 0)
                    {
                        int rotpos = Random.Range(0, ThumbPossibleRots.Count);
                        //theres no rotations


                        //num|||     thumb::
                        //0  |||  t 345   ::  
                        //2  |||  t 345   ::  
                        if ((ThumbPossibleRots[rotpos].Equals(Rotations[0])) ||
                            (ThumbPossibleRots[rotpos].Equals(Rotations[2])))
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

                        //8  |||  t 34    ::5 
                        //10 |||  t 45    ::3 
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[8]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[10]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        //16 |||  t 34    ::  
                        //22 |||  t 4     ::3 
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[16]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[22]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        //18 |||  t 45    ::  
                        //20 |||  t 4     ::5 
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[18]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[20]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 1))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //1  |||  t 147   ::  
                        //3  |||  t 147   ::  
                        if ((ThumbPossibleRots[rotpos].Equals(Rotations[1])) ||
                            (ThumbPossibleRots[rotpos].Equals(Rotations[3])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        //9  |||  t 14    ::7 
                        //11 |||  t 47    ::1 
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[9]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[11]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //17 |||  t 14    ::  
                        //23 |||  t 4     ::1 
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[17]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[23]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[5] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        //19 |||  t 47    ::  
                        //21 |||  t 4     ::7 
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[19]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[21]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 1))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }


                        //4  |||  t 01345 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[4]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }

                        }
                        //14 |||  t 12345 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[14]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }

                        }

                        //6  |||  t 34578 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[6]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }
                        //12 |||  t 34567 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[12]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }

                        //7  |||  t 13467 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[7]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                        }
                        //13 |||  t 01347 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[13]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                        }

                        //5  |||  t 12457 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[5]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }


                        }
                        //15 |||  t 14578 ::  
                        if (ThumbPossibleRots[rotpos].Equals(Rotations[15]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }


                        int posran = Random.Range(0, PossiblePOS.Count);

                        Vector3 hey = PossiblePOS[posran];
                        Vector3 hi = ThumbPossibleRots[rotpos];
                        INstanmyBLCK(myRandomblock, hey, hi);
                    }
                    break;
                }
            case 2://z            
                {
                    //num|||  z       ::   

                    //---------------------------------------------------------------------HORIZONTAL

                    //0  |||  z  45   ::3         //Vector3 aaa = new Vector3(0, 0, 0);    
                    //10 |||  z  45   ::3         //Vector3 cca = new Vector3(180, 180, 0);
                    //
                    //2  |||  z  34   ::5         //Vector3 aca = new Vector3(0, 180, 0);  
                    //8  |||  z  34   ::5         //Vector3 caa = new Vector3(180, 0, 0);  
                    //
                    //16 |||  z 3     ::4         //Vector3 aab = new Vector3(0, 0, 90);   
                    //18 |||  z 5     ::4         //Vector3 acb = new Vector3(0, 180, 90); 
                    //
                    //20 |||  z 4     ::5         //Vector3 aad = new Vector3(0, 0, 270);  
                    //22 |||  z 4     ::3         //Vector3 acd = new Vector3(0, 180, 270);
                    //
                    //4  |||  z 0145  ::          //Vector3 baa = new Vector3(90, 0, 0);   
                    //6  |||  z 3478  ::          //Vector3 bca = new Vector3(90, 180, 0); 

                    //5  |||  z 2457  ::          //Vector3 bba = new Vector3(90, 90, 0);  
                    //7  |||  z 1346  ::          //Vector3 bda = new Vector3(90, 270, 0); 



                    //------------------------------------------------------------------------------VERTICAL

                    //1  |||  z  47   ::1         //Vector3 aba = new Vector3(0, 90, 0);   
                    //11 |||  z  47   ::1         //Vector3 cda = new Vector3(180, 270, 0);

                    //3  |||  z  14   ::7         //Vector3 ada = new Vector3(0, 270, 0);  
                    //9  |||  z  14   ::7         //Vector3 cba = new Vector3(180, 90, 0); 

                    //17 |||  z 1     ::4         //Vector3 abb = new Vector3(0, 90, 90);  
                    //23 |||  z 4     ::1         //Vector3 add = new Vector3(0, 270, 270);
                    //19 |||  z 7     ::4         //Vector3 adb = new Vector3(0, 270, 90); 
                    //21 |||  z 4     ::7         //Vector3 abd = new Vector3(0, 90, 270); 


                    //12 |||  z 4567  ::          //Vector3 daa = new Vector3(270, 0, 0);  
                    //14 |||  z 1234  ::          //Vector3 dca = new Vector3(270, 180, 0);

                    //13 |||  z 0347  ::          //Vector3 dba = new Vector3(270, 90, 0); 
                    //15 |||  z 1458  ::          //Vector3 dda = new Vector3(270, 270, 0);

                    if (ZPossibleRots.Count != 0)
                    {
                        int rotpos = Random.Range(0, ZPossibleRots.Count);
                        //theres no rotations


                        //num|||  z       :: 
                        //0  |||  z  45   ::3
                        //10 |||  z  45   ::3
                        if ((ZPossibleRots[rotpos].Equals(Rotations[0])) ||
                            (ZPossibleRots[rotpos].Equals(Rotations[10])))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }


                        //2  |||  z  34   ::5
                        //8  |||  z  34   ::5
                        if ((ZPossibleRots[rotpos].Equals(Rotations[2])) ||
                            (ZPossibleRots[rotpos].Equals(Rotations[8])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //16 |||  z 3     ::4
                        //22 |||  z 4     ::3
                        if (ZPossibleRots[rotpos].Equals(Rotations[16]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[22]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        //18 |||  z 5     ::4
                        //20 |||  z 4     ::5
                        if (ZPossibleRots[rotpos].Equals(Rotations[18]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[20]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 1))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }


                        //1  |||  z  47   ::1
                        //11 |||  z  47   ::1
                        if ((ZPossibleRots[rotpos].Equals(Rotations[1])) ||
                            (ZPossibleRots[rotpos].Equals(Rotations[11])))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        //3  |||  z  14   ::7
                        //9  |||  z  14   ::7
                        if ((ZPossibleRots[rotpos].Equals(Rotations[3])) ||
                            (ZPossibleRots[rotpos].Equals(Rotations[9])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //17 |||  z 1     ::4
                        //23 |||  z 4     ::1
                        if (ZPossibleRots[rotpos].Equals(Rotations[17]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[23]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[5] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }
                        //21 |||  z 4     ::7
                        //19 |||  z 7     ::4
                        if (ZPossibleRots[rotpos].Equals(Rotations[21]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 1))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[19]))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }



                        //4  |||  z 0145  :: 
                        //6  |||  z 3478  :: 
                        if (ZPossibleRots[rotpos].Equals(Rotations[4]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[6]))
                        {
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                        }

                        //14 |||  z 1234  :: 
                        //12 |||  z 4567  :: 
                        if (ZPossibleRots[rotpos].Equals(Rotations[14]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[12]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }

                        //7  |||  z 1346  :: 
                        //5  |||  z 2457  :: 
                        if (ZPossibleRots[rotpos].Equals(Rotations[7]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[5]))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }
                        //13 |||  z 0347  :: 
                        //15 |||  z 1458  :: 
                        if (ZPossibleRots[rotpos].Equals(Rotations[13]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        if (ZPossibleRots[rotpos].Equals(Rotations[15]))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }


                        int posran = Random.Range(0, PossiblePOS.Count);

                        Vector3 hey = PossiblePOS[posran];
                        Vector3 hi = ZPossibleRots[rotpos];
                        INstanmyBLCK(myRandomblock, hey, hi);
                    }
                    break;
                }
            case 3://T
                {
                    //num|||    t    ::   

                    //-----------------------------------------------------------------HORIZONTAL

                    //0  |||  t 345  ::         //Vector3 aaa = new Vector3(0, 0, 0);    
                    //2  |||  t 345  ::         //Vector3 aca = new Vector3(0, 180, 0);  

                    //1  |||  t 147  ::         //Vector3 aba = new Vector3(0, 90, 0);   
                    //3  |||  t 147  ::         //Vector3 ada = new Vector3(0, 270, 0);  

                    //4  |||  t 1345 ::         //Vector3 baa = new Vector3(90, 0, 0);   
                    //14 |||  t 1345 ::         //Vector3 dca = new Vector3(270, 180, 0);

                    //6  |||  t 3457 ::         //Vector3 bca = new Vector3(90, 180, 0); 
                    //12 |||  t 3457 ::         //Vector3 daa = new Vector3(270, 0, 0);  

                    //5  |||  t 1457 ::         //Vector3 bba = new Vector3(90, 90, 0);  
                    //15 |||  t 1457 ::         //Vector3 dda = new Vector3(270, 270, 0);

                    //7  |||  t 1347 ::         //Vector3 bda = new Vector3(90, 270, 0); 
                    //13 |||  t 1347 ::         //Vector3 dba = new Vector3(270, 90, 0); 

                    //8  |||  t 4    ::35       //Vector3 caa = new Vector3(180, 0, 0);  
                    //10 |||  t 4    ::35       //Vector3 cca = new Vector3(180, 180, 0);

                    //9  |||  t 4    ::17       //Vector3 cba = new Vector3(180, 90, 0); 
                    //11 |||  t 4    ::17       //Vector3 cda = new Vector3(180, 270, 0);

                    //16 |||  t 4    ::3        //Vector3 aab = new Vector3(0, 0, 90);   
                    //22 |||  t 4    ::3        //Vector3 acd = new Vector3(0, 180, 270);

                    //18 |||  t 4    ::5        //Vector3 acb = new Vector3(0, 180, 90); 
                    //20 |||  t 4    ::5        //Vector3 aad = new Vector3(0, 0, 270);  

                    //17 |||  t 4    ::1        //Vector3 abb = new Vector3(0, 90, 90);  
                    //23 |||  t 4    ::1        //Vector3 add = new Vector3(0, 270, 270);

                    //19 |||  t 4    ::7        //Vector3 adb = new Vector3(0, 270, 90); 
                    //21 |||  t 4    ::7        //Vector3 abd = new Vector3(0, 90, 270); 
                    if (TPossibleRots.Count != 0)
                    {
                        int rotpos = Random.Range(0, TPossibleRots.Count);
                        //theres no rotations

                        //num|||    t    ::        
                        //0  |||  t 345  ::        
                        //2  |||  t 345  ::        
                        if ((TPossibleRots[rotpos].Equals(Rotations[0])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[2])))
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

                        //8  |||  t 4    ::35      
                        //10 |||  t 4    ::35      
                        if ((TPossibleRots[rotpos].Equals(Rotations[8])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[10])))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //16 |||  t 4    ::3       
                        //22 |||  t 4    ::3       
                        if ((TPossibleRots[rotpos].Equals(Rotations[16])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[22])))
                        {

                            if ((Floorchecker[0] == 1) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }

                        //18 |||  t 4    ::5       
                        //20 |||  t 4    ::5       
                        if ((TPossibleRots[rotpos].Equals(Rotations[18])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[20])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 1))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //1  |||  t 147  ::        
                        //3  |||  t 147  ::        
                        if ((TPossibleRots[rotpos].Equals(Rotations[1])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[3])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        //9  |||  t 4    ::17      
                        //11 |||  t 4    ::17      
                        if ((TPossibleRots[rotpos].Equals(Rotations[9])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[11])))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //17 |||  t 4    ::1       
                        //23 |||  t 4    ::1       
                        if ((TPossibleRots[rotpos].Equals(Rotations[17])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[23])))
                        {
                            if ((Floorchecker[0] == 1) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[3] == 1) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }

                            if ((Floorchecker[1] == 1) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }

                            if ((Floorchecker[2] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[5] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }

                        //19 |||  t 4    ::7       
                        //21 |||  t 4    ::7       
                        if ((TPossibleRots[rotpos].Equals(Rotations[19])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[21])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 1))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 1))
                            {
                                PossiblePOS.Add(left);
                            }

                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 1))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 1))
                            {
                                PossiblePOS.Add(bot);
                            }

                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 1))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 1))
                            {
                                PossiblePOS.Add(right);
                            }
                        }


                        //4  |||  t 1345 ::        
                        //14 |||  t 1345 ::        
                        if ((TPossibleRots[rotpos].Equals(Rotations[4])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[14])))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //6  |||  t 3457 ::        
                        //12 |||  t 3457 ::                   
                        if ((TPossibleRots[rotpos].Equals(Rotations[6])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[12])))
                        {

                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                        }

                        //7  |||  t 1347 ::                   
                        //13 |||  t 1347 ::                   
                        if ((TPossibleRots[rotpos].Equals(Rotations[7])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[13])))
                        {

                            if ((Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                        }

                        //5  |||  t 1457 ::                   
                        //15 |||  t 1457 ::                   
                        if ((TPossibleRots[rotpos].Equals(Rotations[5])) ||
                            (TPossibleRots[rotpos].Equals(Rotations[15])))
                        {
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                        }



                        int posran = Random.Range(0,PossiblePOS.Count);

                        Vector3 hey = PossiblePOS[posran];
                        Vector3 hi = TPossibleRots[rotpos];
                        INstanmyBLCK(myRandomblock, hey, hi);
                    }
                    break;
                }
            case 4://U
                {
                    //num| ||      u    :: 

                    //---------------------------Horizontal

                    //0  | ||   u 345   ::         //Vector3 aaa = new Vector3(0, 0, 0);    
                    //2  | ||   u 345   ::         //Vector3 aca = new Vector3(0, 180, 0);  

                    //8  | ||   u 35    ::4        //Vector3 caa = new Vector3(180, 0, 0);  
                    //10 | ||   u 35    ::4        //Vector3 cca = new Vector3(180, 180, 0);

                    //16 | ||   u 34    ::         //Vector3 aab = new Vector3(0, 0, 90);   
                    //22 | ||   u 34    ::         //Vector3 acd = new Vector3(0, 180, 270);

                    //18 | ||   u 45    ::         //Vector3 acb = new Vector3(0, 180, 90); 
                    //20 | ||   u 45    ::         //Vector3 aad = new Vector3(0, 0, 270);  

                    //4  | ||   u 02345 ::         //Vector3 baa = new Vector3(90, 0, 0);   
                    //14 | ||   u 02345 ::         //Vector3 dca = new Vector3(270, 180, 0);

                    //6  | ||   u 34568 ::         //Vector3 bca = new Vector3(90, 180, 0); 
                    //12 | ||   u 34568 ::         //Vector3 daa = new Vector3(270, 0, 0);  

                    //-----------------------------Vertical

                    //1  | ||   u 147   ::         //Vector3 aba = new Vector3(0, 90, 0);   
                    //3  | ||   u 147   ::         //Vector3 ada = new Vector3(0, 270, 0);  

                    //9  | ||   u 17    ::4        //Vector3 cba = new Vector3(180, 90, 0); 
                    //11 | ||   u 17    ::4        //Vector3 cda = new Vector3(180, 270, 0);

                    //17 | ||   u 14    ::         //Vector3 abb = new Vector3(0, 90, 90);  
                    //23 | ||   u 14    ::         //Vector3 add = new Vector3(0, 270, 270); 

                    //19 | ||   u 47    ::         //Vector3 adb = new Vector3(0, 270, 90); 
                    //21 | ||   u 47    ::         //Vector3 abd = new Vector3(0, 90, 270); 

                    //5  | ||   u 12478 ::         //Vector3 bba = new Vector3(90, 90, 0);  
                    //15 | ||   u 12478 ::         //Vector3 dda = new Vector3(270, 270, 0);

                    //7  | ||   u 01467 ::         //Vector3 bda = new Vector3(90, 270, 0); 
                    //13 | ||   u 01467 ::         //Vector3 dba = new Vector3(270, 90, 0);                     

                    if (UPossibleRots.Count != 0)
                    {
                       int rotpos = Random.Range(0, UPossibleRots.Count);
                        //theres no rotations

                        //num|||      u    ::       
                        //0  |||   u 345   ::       
                        //2  |||   u 345   ::       

                        if ((UPossibleRots[rotpos].Equals(Rotations[0])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[2])))
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
                        //8  |||   u 35    ::4      
                        //10 |||   u 35    ::4      
                        if ((UPossibleRots[rotpos].Equals(Rotations[8])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[10])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 1) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 1) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //16 |||   u 34    ::       
                        //22 |||   u 34    ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[16])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[22])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }

                        //18 |||   u 45    ::       
                        //20 |||   u 45    ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[18])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[20])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }
                        //1  |||   u 147   ::       
                        //3  |||   u 147   ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[1])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[3])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }
                        //9  |||   u 17    ::4      
                        //11 |||   u 17    ::4      
                        if ((UPossibleRots[rotpos].Equals(Rotations[9])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[11])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 1) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 1) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 1) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //17 |||   u 14    ::       
                        //23 |||   u 14    ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[17])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[23])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }

                        //19 |||   u 47    ::       
                        //21 |||   u 47    ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[19])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[21])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }


                        //4  |||   u 02345 ::       
                        //14 |||   u 02345 ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[4])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[14])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {

                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }

                        }

                        //6  |||   u 34568 ::       
                        //12 |||   u 34568 ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[6])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[12])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }

                        //7  |||   u 01467 ::       
                        //13 |||   u 01467 ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[7])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[13])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }

                        }
                        //5  |||   u 12478 ::       
                        //15 |||   u 12478 ::       
                        if ((UPossibleRots[rotpos].Equals(Rotations[5])) ||
                            (UPossibleRots[rotpos].Equals(Rotations[15])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }

                        }


                        int posran = Random.Range(0, PossiblePOS.Count);

                        Vector3 hey = PossiblePOS[posran];
                        Vector3 hi = UPossibleRots[rotpos];
                        INstanmyBLCK(myRandomblock, hey, hi);
                    }
                    break;
                }
            case 5://Block
                {
                    //num||    block

                    //--------------------------------------------------------------Horizontal

                    //0  ||  b 345             //Vector3 aaa = new Vector3(0, 0, 0);    
                    //2  ||  b 345             //Vector3 aca = new Vector3(0, 180, 0);  
                    //8  ||  b 345             //Vector3 caa = new Vector3(180, 0, 0);  
                    //10 ||  b 345             //Vector3 cca = new Vector3(180, 180, 0);

                    //16 ||  b 34              //Vector3 aab = new Vector3(0, 0, 90);   
                    //22 ||  b 34              //Vector3 acd = new Vector3(0, 180, 270);

                    //18 ||  b 45              //Vector3 acb = new Vector3(0, 180, 90); 
                    //20 ||  b 45              //Vector3 aad = new Vector3(0, 0, 270);  

                    //4  ||  b 012345          //Vector3 baa = new Vector3(90, 0, 0);   
                    //14 ||  b 012345          //Vector3 dca = new Vector3(270, 180, 0);

                    //6  ||  b 345678          //Vector3 bca = new Vector3(90, 180, 0); 
                    //12 ||  b 345678          //Vector3 daa = new Vector3(270, 0, 0);  

                    //-----------------------------------------------------------------------Vertical

                    //1  ||  b 147             //Vector3 aba = new Vector3(0, 90, 0);   
                    //3  ||  b 147             //Vector3 ada = new Vector3(0, 270, 0);  
                    //9  ||  b 147             //Vector3 cba = new Vector3(180, 90, 0); 
                    //11 ||  b 147             //Vector3 cda = new Vector3(180, 270, 0);

                    //17 ||  b 14              //Vector3 abb = new Vector3(0, 90, 90);  
                    //23 ||  b 14              //Vector3 add = new Vector3(0, 270, 270);

                    //19 ||  b 47              //Vector3 adb = new Vector3(0, 270, 90); 
                    //21 ||  b 47              //Vector3 abd = new Vector3(0, 90, 270); 

                    //5  ||  b 124578          //Vector3 bba = new Vector3(90, 90, 0);  
                    //15 ||  b 124578          //Vector3 dda = new Vector3(270, 270, 0);

                    //7  ||  b 013467          //Vector3 bda = new Vector3(90, 270, 0); 
                    //13 ||  b 013467          //Vector3 dba = new Vector3(270, 90, 0); 

                    if (BlockPossibleRots.Count != 0)
                    {
                        int rotpos = Random.Range(0, BlockPossibleRots.Count);

                        //0  ||  b 345             //Vector3 aaa = new Vector3(0, 0, 0);    
                        //2  ||  b 345             //Vector3 aca = new Vector3(0, 180, 0);  
                        //8  ||  b 345             //Vector3 caa = new Vector3(180, 0, 0);  
                        //10 ||  b 345             //Vector3 cca = new Vector3(180, 180, 0);

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[0])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[2])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[8])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[10])))
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

                        //16 ||  b 34              //Vector3 aab = new Vector3(0, 0, 90);   
                        //22 ||  b 34              //Vector3 acd = new Vector3(0, 180, 270);

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[16])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[22])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }

                        }

                        //18 ||  b 45              //Vector3 acb = new Vector3(0, 180, 90); 
                        //20 ||  b 45              //Vector3 aad = new Vector3(0, 0, 270);  

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[18])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[20])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //4  ||  b 012345          //Vector3 baa = new Vector3(90, 0, 0);   
                        //14 ||  b 012345          //Vector3 dca = new Vector3(270, 180, 0);

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[4])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[14])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                        }

                        //6  ||  b 345678          //Vector3 bca = new Vector3(90, 180, 0); 
                        //12 ||  b 345678          //Vector3 daa = new Vector3(270, 0, 0);  

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[6])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[12])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }

                        //------------------------------------------------------------------------------

                        //1  ||  b 147             //Vector3 aba = new Vector3(0, 90, 0);   
                        //3  ||  b 147             //Vector3 ada = new Vector3(0, 270, 0);  
                        //9  ||  b 147             //Vector3 cba = new Vector3(180, 90, 0); 
                        //11 ||  b 147             //Vector3 cda = new Vector3(180, 270, 0);

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[1])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[3])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[9])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[11])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //17 ||  b 14              //Vector3 abb = new Vector3(0, 90, 90);  
                        //23 ||  b 14              //Vector3 add = new Vector3(0, 270, 270);

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[17])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[23])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(botleft);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(bot);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(botright);
                            }
                        }

                        //19 ||  b 47              //Vector3 adb = new Vector3(0, 270, 90); 
                        //21 ||  b 47              //Vector3 abd = new Vector3(0, 90, 270); 

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[19])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[21])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[3] == 0))
                            {
                                PossiblePOS.Add(topleft);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[4] == 0))
                            {
                                PossiblePOS.Add(top);
                            }
                            if ((Floorchecker[2] == 0) && (Floorchecker[5] == 0))
                            {
                                PossiblePOS.Add(topright);
                            }
                            if ((Floorchecker[3] == 0) && (Floorchecker[6] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[4] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[5] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //7  ||  b 013467          //Vector3 bda = new Vector3(90, 270, 0); 
                        //13 ||  b 013467          //Vector3 dba = new Vector3(270, 90, 0); 

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[7])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[13])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(right);
                            }
                        }

                        //5  ||  b 124578          //Vector3 bba = new Vector3(90, 90, 0);  
                        //15 ||  b 124578          //Vector3 dda = new Vector3(270, 270, 0);

                        if ((BlockPossibleRots[rotpos].Equals(Rotations[5])) ||
                            (BlockPossibleRots[rotpos].Equals(Rotations[15])))
                        {
                            if ((Floorchecker[0] == 0) && (Floorchecker[1] == 0) && (Floorchecker[3] == 0) && (Floorchecker[4] == 0) && (Floorchecker[6] == 0) && (Floorchecker[7] == 0))
                            {
                                PossiblePOS.Add(left);
                            }
                            if ((Floorchecker[1] == 0) && (Floorchecker[2] == 0) && (Floorchecker[4] == 0) && (Floorchecker[5] == 0) && (Floorchecker[7] == 0) && (Floorchecker[8] == 0))
                            {
                                PossiblePOS.Add(center);
                            }
                        }

                        int posran = Random.Range(0, PossiblePOS.Count);

                        Vector3 hey = PossiblePOS[posran];
                        Vector3 hi = BlockPossibleRots[rotpos];
                        INstanmyBLCK(myRandomblock, hey, hi);
                    }

                    break;
                }
        }
        //  int rotpos = Random.Range(0,Po;

    }
    //this block was selected
    //void BLOCKSelected()//2x3
    //{

    //}
    //void TSelected()
    //{

    //}
    //void ZSelected()
    //{

    //}
    //void USelected()
    //{

    //}
    //void LSelected()
    //{

    //}
    //void THUMBSelected()
    //{

    //}
    //void MoreRegret()
    //{
    //    int myrotation = Random.Range(0, 24);
    //    //switch (myrotation)
    //    //{
    //    ////Vector3 a = new Vector3(90, 90, 0);     //1     1
    //    ////Vector3 b = new Vector3(90, 0, 90);     //2       2
    //    ////Vector3 c = new Vector3(90, 0, 0);      //3         3
    //    ////Vector3 d = new Vector3(90, 0, 180);    //4           4
    //    ////Vector3 e = new Vector3(0, 90, 0);      //5     1,2
    //    ////Vector3 f = new Vector3(0, 90, 180);    //6     1,2
    //    ////Vector3 g = new Vector3(0, 0, 0);       //7         3,4
    //    ////Vector3 h = new Vector3(0, 0, 180);     //8         3,4
    //    ////Vector3 i = new Vector3(0, 90, 90);     //9     1,2,3
    //    ////Vector3 j = new Vector3(0, 90, 270);    //10    1,2,  4
    //    ////Vector3 k = new Vector3(0, 0, 270);     //11    1,  3,4
    //    ////Vector3 l = new Vector3(0, 0, 90);      //12      2,3,4
    //    //    case 0:                    //1
    //    //        // a;
    //    //        ResetmyBool();
    //    //        Transnegx = true;
    //    //        break;
    //    //    case 1:                    //2
    //    //        //b;
    //    //        ResetmyBool();
    //    //        Transposx = true;
    //    //        break;
    //    //    case 2:                    //3
    //    //        //c;
    //    //        ResetmyBool();
    //    //        Transnegz = true;
    //    //        break;
    //    //    case 3:                    //4
    //    //        //d;
    //    //        ResetmyBool();
    //    //        Transposz = true;
    //    //        break;
    //    //    case 4:                    //1,2
    //    //        //e;
    //    //        ResetmyBool();
    //    //        Transnegx = true;
    //    //        Transposx = true;
    //    //        break;
    //    //    case 5:                  //1,2
    //    //        //f;
    //    //        ResetmyBool();
    //    //        Transnegx = true;
    //    //        Transposx = true;
    //    //        break;
    //    //    case 6:                    //3,4
    //    //        //g;
    //    //        ResetmyBool();
    //    //        Transnegz = true;
    //    //        Transposz = true;
    //    //        break; 
    //    //    case 7:                     //3,4
    //    //        //h;
    //    //        ResetmyBool();
    //    //        Transnegz = true;
    //    //        Transposz = true;
    //    //        break;
    //    //    case 8:                   //1,2,3
    //    //        //i;
    //    //        ResetmyBool();
    //    //        Transnegx = true;
    //    //        Transposx = true;
    //    //        Transnegz = true;
    //    //        break;
    //    //    case 9:                      //1,2,4
    //    //        //j;
    //    //        ResetmyBool();
    //    //        Transnegx = true;
    //    //        Transposx = true;

    //    //        Transposz = true;
    //    //        break;
    //    //    case 10:                   //1,3,4
    //    //        //k;
    //    //        ResetmyBool();
    //    //        Transnegx = true;
    //    //        Transnegz = true;
    //    //        Transposz = true;
    //    //        break;
    //    //    case 11:          //2,3,4
    //    //        //l;
    //    //        ResetmyBool();
    //    //        Transposx = true;
    //    //        Transnegz = true;
    //    //        Transposz = true;
    //    //        break;
    //    //}
    //    Vector3 temp = Rotations[myrotation];
    //    RotationOFF = Quaternion.Euler(temp);
    //    Instantiatemyblock();
    //}
    //step4 with the given possible outcomes have a random check what possibilty to spawn and spawn it
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
    void INstanmyBLCK(int blocknum, Vector3 pos, Vector3 rots)
    {
        RotationOFF = Quaternion.Euler(rots);
        //  Instantiatemyblock();
        Roundoffandsetmypos();
        Instantiate(Blockrefs[blocknum], this.gameObject.transform.position + (pos * scaleof), RotationOFF);
    }
    //void Instantiatemyblock()
    //{
    //    int blocknum = Random.Range(0, 9);
    //    SetmyTranslations();
    //    Roundoffandsetmypos();

    //    Instantiate(Blockrefs[blocknum], this.gameObject.transform.position + myfinaltranslation, RotationOFF);
    //}
    //--------------------------------------------------------------------------------
    //void SetmyTranslations()
    //{
    //    Vector3 trantonegx = new Vector3(-1 * scaleof, 0, 0);
    //    Vector3 trantoposx = new Vector3(1 * scaleof, 0, 0);
    //    Vector3 nomove = new Vector3(0, 0, 0);
    //    Vector3 trantonegz = new Vector3(0, 0, -1 * scaleof);
    //    Vector3 trantoposz = new Vector3(0, 0, 1 * scaleof);

    //    Vector3 tempx = new Vector3(0, 0, 0);
    //    Vector3 tempz = new Vector3(0, 0, 0);
    //    if (Transnegx && Transposx)
    //    {

    //        int leftorright = Random.Range(0, 3);
    //        switch (leftorright)
    //        {

    //            case 0: //move left
    //                tempx = trantonegx;
    //                break;
    //            case 1://stay center
    //                tempx = nomove;
    //                break;
    //            case 2://move right
    //                tempx = trantoposx;
    //                break;
    //        }

    //    }
    //    else if (Transposx)
    //    {
    //        tempx = trantoposx;
    //    }
    //    else if (Transnegx)
    //    {
    //        tempx = trantonegx;
    //    }

    //    if (Transnegz && Transposz)
    //    {
    //        int forwardorback = Random.Range(0, 3);
    //        switch (forwardorback)
    //        {

    //            case 0: //move forward
    //                tempz = trantonegz;
    //                break;
    //            case 1://stay center
    //                tempz = nomove;
    //                break;
    //            case 2://move back
    //                tempz = trantoposz;
    //                break;
    //        }
    //    }
    //    else if (Transnegz)
    //    {
    //        tempz = trantonegz;
    //    }
    //    else if (Transposz)
    //    {
    //        tempz = trantoposz;
    //    }



    //    myfinaltranslation = tempx + tempz;
    //}

    //------------------------------------------------------
    void Testing(RaycastHit hit)
    {

        if (hit.transform.gameObject.GetComponent<TETRISbehaviour>())//THERE IS A spawn BLOCK
        {
            if (hit.transform.gameObject.GetComponent<TETRISbehaviour>().firstcollision.Equals(true))
            {

                Testmylowest(hit.distance);
                Testmyhighest(hit.distance);
              //  Debug.Log("HEY IM A BLOCK");

            }
            else//block is grounded
            {

                counter++;
              //  Debug.Log("RESTART RANDOM SPAWN - " + counter);
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


//public GameObject aref;//2x2 left     |0
//public GameObject bref;//2x2 right    |1
//public GameObject cref;//block 2x3    |2
//public GameObject dref;//L            |3
//public GameObject eref;//Reverse L    |4
//public GameObject fref;//Reverse S    |5
//public GameObject gref;//S            |6
//public GameObject href;//small t      |7
//public GameObject iref;//U shape      |8