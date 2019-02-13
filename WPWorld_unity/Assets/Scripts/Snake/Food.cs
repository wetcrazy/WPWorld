using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    bool fall = true;
    private void Update()
    {
        if (fall)
        {
            gameObject.transform.position += (-(gameObject.transform.up) * 0.01f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fall = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fall = false;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player")
    //    {
    //        if(this.gameObject.tag == "Speedy")
    //        {
    //            other.gameObject.GetComponent<Head>().Setspeed(0.035f);
    //        }
    //        else if (this.gameObject.tag == "Food")
    //        {
    //            other.gameObject.GetComponent<Head>().AddAppleAte();
    //        }
    //        Destroy(this.gameObject);

    //    }

    //}
}
