using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
        {
            other.GetComponent<TPSLogic>().Death();
        }

        if(other.name.Contains("Enemy"))
        {
            other.GetComponent<Enemy>().AirDeath();
        }

        if(other.GetComponent<Fireball>() != null)
        {
            other.GetComponent<Fireball>().CreateExplosion(other.transform.position);
            Destroy(other.gameObject);
        }
    }
}
