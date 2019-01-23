using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    // Variables to grab
    private Text TextRef;
    private TPSLogic PlayerRef;
    private GameObject[] PlayerInstances;

	// Use this for initialization
	void Start () {
        TextRef = GetComponent<Text>();

        PlayerInstances = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < PlayerInstances.Length; i++)
        {
            if (!PlayerInstances[i].GetComponent<TPSLogic>().isMine())
                continue;
            PlayerRef = PlayerInstances[i].GetComponent<TPSLogic>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(PlayerRef != null)
            TextRef.text = " : " + PlayerRef.CurrPointsPub.ToString();
    }
}
