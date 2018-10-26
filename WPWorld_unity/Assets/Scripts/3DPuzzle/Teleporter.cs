using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField]
    private GameObject teleport;
    [SerializeField]
    private GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Player")
        {
            player.transform.position = teleport.transform.position; //teleports player to that specific place.
            Debug.Log("HI TELEPORT!");
        }
    }
}
