using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TexttoBlocks : MonoBehaviour {

    public string helloworlds;

    //Symbols
    public GameObject AA  ;
    public GameObject BB  ;
    public GameObject CC  ;
    public GameObject DD  ;
    public GameObject EE  ;
    public GameObject FF  ;
    public GameObject AAGG;
    public GameObject BBHH;
    public GameObject CCII;
    public GameObject CCII2;
    public GameObject DDJJ;
    public GameObject EEKK;
    public GameObject FFLL;
    public GameObject GGMM;
    public GameObject HHNN;
    public GameObject IIOO;
    public GameObject JJPP;
    public GameObject KKQQ;
    public GameObject LLRR;
    public GameObject MMSS;
    public GameObject NNTT;
    public GameObject OOUU;
    public GameObject PPVV;
    public GameObject QQWW;
    public GameObject RRXX;
    public GameObject SSYY;
    public GameObject TTZZ;
    public GameObject UU  ;
    public GameObject VV  ;
    public GameObject WW  ;
    public GameObject XX  ;
    public GameObject YY  ;
    public GameObject ZZ  ;

    //Alphabets
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;
    public GameObject F;
    public GameObject G;
    public GameObject H;
    public GameObject I;
    public GameObject J;
    public GameObject K;
    public GameObject L;
    public GameObject M;
    public GameObject N;
    public GameObject O;
    public GameObject P;
    public GameObject Q;
    public GameObject R;
    public GameObject S;
    public GameObject T;
    public GameObject U;
    public GameObject V;
    public GameObject W;
    public GameObject X;
    public GameObject Y;
    public GameObject Z;

    public GameObject nill;

    public GameObject containwer;

    List<GameObject> alphabets = new List<GameObject>();
    StreamReader a;
    string line;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        alphabets.Add(AA);
        alphabets.Add(BB  );
        alphabets.Add(CC  );
        alphabets.Add(DD  );
        alphabets.Add(EE  );
        alphabets.Add(FF  );
        alphabets.Add(AAGG);
        alphabets.Add(BBHH);
        alphabets.Add(CCII);
        alphabets.Add(CCII2);
        alphabets.Add(DDJJ);
        alphabets.Add(EEKK);
        alphabets.Add(FFLL);
        alphabets.Add(GGMM);
        alphabets.Add(HHNN);
        alphabets.Add(IIOO);
        alphabets.Add(JJPP);
        alphabets.Add(KKQQ);
        alphabets.Add(LLRR);
        alphabets.Add(MMSS);
        alphabets.Add(NNTT);
        alphabets.Add(OOUU);
        alphabets.Add(PPVV);
        alphabets.Add(QQWW);
        alphabets.Add(RRXX);
        alphabets.Add(SSYY);
        alphabets.Add(TTZZ);
        alphabets.Add(UU  );
        alphabets.Add(VV  );
        alphabets.Add(WW  );
        alphabets.Add(XX  );
        alphabets.Add(YY  );
        alphabets.Add(ZZ  );

        alphabets.Add(A);
        alphabets.Add(B);
        alphabets.Add(C);
        alphabets.Add(D);
        alphabets.Add(E);
        alphabets.Add(F);
        alphabets.Add(G);
        alphabets.Add(H);
        alphabets.Add(I);
        alphabets.Add(J);
        alphabets.Add(K);
        alphabets.Add(L);
        alphabets.Add(M);
        alphabets.Add(N);
        alphabets.Add(O);
        alphabets.Add(P);
        alphabets.Add(Q);
        alphabets.Add(R);
        alphabets.Add(S);
        alphabets.Add(T);
        alphabets.Add(U);
        alphabets.Add(V);
        alphabets.Add(W);
        alphabets.Add(X);
        alphabets.Add(Y);
        alphabets.Add(Z);

        coroutine = WaitAndPrint(5.0f);

        a = new StreamReader("Assets/Resources/script.txt");
        line = null;

        StartCoroutine(coroutine);

       
       // Debug.Log();
	}



    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        { 
            while ((line = a.ReadLine()) != null)
            {
                line = line.ToUpper();
                yield return new WaitForSeconds(waitTime);


                for (int i = 0; i < line.Length; i++)
                {
                     if((line[i].GetHashCode()-32 >0 ) || (line[i].GetHashCode() - 32 < 59))
                    Instantiate(alphabets[line[i].GetHashCode() - 32], this.gameObject.transform.position + (new Vector3(i, 0, 0) * 6), transform.rotation);
                     else
                         Instantiate(nill, this.gameObject.transform.position + (new Vector3(i, 0, 0) * 6), transform.rotation);
                }


            }
        }
    }
    



}
