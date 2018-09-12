using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

    [SerializeField]
    public float AsteroidSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Planet")
        {
            //Destroy the asteroid if it collides with the planet
            Destroy(gameObject);
        }
    }
}
