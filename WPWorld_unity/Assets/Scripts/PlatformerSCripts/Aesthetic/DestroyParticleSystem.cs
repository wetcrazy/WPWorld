using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour {

    private ParticleSystem PSRef;

	// Use this for initialization
	void Start () {
        PSRef = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!PSRef.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
