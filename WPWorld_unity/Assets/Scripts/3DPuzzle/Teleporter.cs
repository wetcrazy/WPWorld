using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField]
    private GameObject teleport;
    [SerializeField]
    private GameObject player;
    float timer;
    bool teleported;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(teleported)
        {
            timer += Time.deltaTime;
            Debug.Log("timer now yo.");
        }
        if(timer >= 10)
        {
            teleported = false;
            timer = 0;
            Debug.Log("COOLDOWN OVER");
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !teleported)
        {
            player.transform.position = teleport.transform.position; //teleports player to that specific place.
            Debug.Log("HI TELEPORT!");
            teleported = true;
        }
    }
}
