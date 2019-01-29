﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

    public float StartingTime;

    private Text TextRef;

	// Use this for initialization
	void Start () {
		TextRef = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(StartingTime > 0)
        {
            StartingTime -= Time.deltaTime;
        }
        else
        {
            StartingTime = 0;
        }

        TextRef.text = ": " + StartingTime.ToString("F0");
	}
}
