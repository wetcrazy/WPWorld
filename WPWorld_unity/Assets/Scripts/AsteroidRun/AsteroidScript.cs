using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    public float AsteroidSpeed;
    [SerializeField]
    AudioClip AsteroidHitSFX = null;

    SoundSystem soundSystem;
    SceneControlFinal SceneControllerScript = null;

    private void Start()
    {
        soundSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
        SceneControllerScript = GameObject.Find("Scripts").GetComponent<SceneControlFinal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Planet")
        {
            //Destroy the asteroid if it collides with the planet
            Destroy(gameObject);

            //Play the sound effect
            soundSystem.PlaySFX("Explosion");
        }
        else if (other.tag == "Player")
        {
            //Play the sound effect
            soundSystem.PlaySFX("Explosion");
            //Reset level
            SceneControllerScript.Reset_Level();
        }
    }
}