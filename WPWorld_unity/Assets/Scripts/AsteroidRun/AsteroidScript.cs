using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {

    [SerializeField]
    public float AsteroidSpeed;
    [SerializeField]
    AudioClip AsteroidHitSFX = null;

    SoundSystem soundSystem;

    private void Start()
    {
        soundSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Planet")
        {
            //Destroy the asteroid if it collides with the planet
            Destroy(gameObject);

            //Play the sound effect
            soundSystem.PlaySFX(AsteroidHitSFX);
        }
        else if (other.tag == "Player")
        {
            //Play the sound effect
            soundSystem.PlaySFX(AsteroidHitSFX);
        }
    }
}
