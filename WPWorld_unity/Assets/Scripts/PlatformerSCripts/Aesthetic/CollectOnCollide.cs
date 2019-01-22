﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnCollide : MonoBehaviour {

    // Score Variables
    [Header("Score Settings")]
    [SerializeField]
    private int PointsToAdd;
    public bool HasCollected;

    [Space]
    [Header("Sound Settings")]
    [SerializeField]
    private string CollectSFX;

    private Renderer RenderRef;
    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        HasCollected = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!RenderRef.isVisible)
            return;
        transform.Rotate(new Vector3(0, 5, 0));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
        {
            if(RenderRef.isVisible)
            {
                Collect();

                // Add points to the player who collects the coin
                    other.GetComponent<TPSLogic>().CurrPointsPub += PointsToAdd;
            }
        }
    }

    public void Collect()
    {
        RenderRef.enabled = false;

        HasCollected = true;

        if (CollectSFX != "")
            SoundSystemRef.PlaySFX(CollectSFX);
    }

    public void Reset()
    {
        RenderRef.enabled = true;
    }
}
