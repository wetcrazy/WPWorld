using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlocksBehaviour : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            Destroy(this.gameObject);
        }

    }
   
    private void Update()
    {
        this.gameObject.transform.Translate(Vector3.up*0.55f);
       
    }


}

