using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TexttoBlocks : MonoBehaviour {

    //public string helloworlds;

    public GameObject nill;

    public GameObject container;

    List<GameObject> alphabets = new List<GameObject>();
    StreamReader a;
    string line;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
  

        for (int i = 0; i < container.transform.childCount; i++)
        {
            GameObject letter = container.transform.GetChild(i).gameObject;
            alphabets.Add(letter);
        }
       // Instantiate(lmao, this.gameObject.transform.position + (new Vector3(-15, 0, 0)), transform.rotation);
        // containwer.gameObject.;
        //for(int i = 0; i<containwer.)
        coroutine = WaitAndPrint(3f);

        a = new StreamReader("Assets/Resources/script.txt");
        line = null;

        StartCoroutine(coroutine);

       
       // Debug.Log();
	}



    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
      
            while ((line = a.ReadLine()) != "end")
            {
                line = line.ToUpper();
                yield return new WaitForSeconds(waitTime);


                for (int i = 0; i < line.Length; i++)
                {
                     if((line[i].GetHashCode()-31 >0 ) || (line[i].GetHashCode() - 31 < 58))
                    Instantiate(alphabets[line[i].GetHashCode() - 31], this.gameObject.transform.position + (new Vector3(i, 0, 0) * 6), transform.rotation);
                     else
                         Instantiate(nill, this.gameObject.transform.position + (new Vector3(i, 0, 0) * 6), transform.rotation);
                }


            }
        
    }
    



}
