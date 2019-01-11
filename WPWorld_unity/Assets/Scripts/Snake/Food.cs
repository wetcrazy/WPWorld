using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.gameObject.tag == "Speedy")
            {
                other.gameObject.GetComponent<Head>().Setspeed(1);
            }
            Destroy(this.gameObject);
            
        }

    }
}
