using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {
    public Transform spawnpos;
    public GameObject spawnobject;
	// Update is called once per frame
	void Update () {

        Instantiate(spawnobject, spawnpos.position, spawnpos.rotation);
	}
}
