using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour {

    private TPSLogic PlayerRef;
    private Text TextRef;

	// Use this for initialization
	void Start () {
        PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<TPSLogic>();
        TextRef = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        TextRef.text = " x " + PlayerRef.GetDeaths();
	}
}
