using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Filereader : MonoBehaviour {

    
    StreamReader a;
    string line;
    private IEnumerator coroutine;
    void Start() {
        // - After 0 seconds, prints "Starting 0.0"
        // - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
        // - After 2 seconds, prints "WaitAndPrint 2.0"
        // print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine.

        coroutine = WaitAndPrint(5.0f);
       

     

       
        
       
       a = new StreamReader("Assets/Resources/script.txt");
         line = null;

                StartCoroutine(coroutine);
        
    }


    
    // every 2 seconds perform the print()
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            while ((line = a.ReadLine()) != "END")
            {
                Debug.Log("waiting");
            yield return new WaitForSeconds(waitTime);
                Debug.Log(line);
            print("WaitAndPrint " + Time.time);
            }
        }
    }
}
