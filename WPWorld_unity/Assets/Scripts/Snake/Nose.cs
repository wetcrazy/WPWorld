using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose : MonoBehaviour {
    public bool deathcollided = false;
    public void Restart()
    {
        deathcollided = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("okay");
            deathcollided = true;
        }
        else if(other.name == "Body(Clone)")
        {
            deathcollided = true;
        }
    }
}
