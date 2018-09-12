using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrisrotatetokill : MonoBehaviour {

    public GameObject A;

    private bool startrotation;
    private void Start()
    {
        startrotation = false;
    }
    private void Update()
    {
        if(startrotation)
        {
         A.gameObject.transform.Rotate(0,0,Time.deltaTime*-5*10);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //rotate
            startrotation = true;
        }
    }
}
