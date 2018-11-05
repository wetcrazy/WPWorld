using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnOnCollide : MonoBehaviour {

    private ParticleSystem PSRef;
    private List<ParticleCollisionEvent> PSCollisions;

    [SerializeField]
    private ParticleSystem PSToSpawn;

	// Use this for initialization
	void Start () {
        PSRef = GetComponent<ParticleSystem>();
        PSCollisions = new List<ParticleCollisionEvent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Killbox")
            return;
        int numCollisionEvents = PSRef.GetCollisionEvents(other, PSCollisions);
        int i = 0;

        while (i < numCollisionEvents)
        {
            ParticleSystem n_PS = Instantiate(PSToSpawn, PSCollisions[i].intersection, transform.rotation);
            n_PS.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            i++;
        }
    }

    private void OnParticleTrigger()
    {
        Debug.Log("PArticle Trigger Hit");
    }
}
