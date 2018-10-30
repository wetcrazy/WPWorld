﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnCollide : MonoBehaviour {

    private Renderer RenderRef;
    [SerializeField]
    private AudioClip CollectSFX;

    [SerializeField]
    private int PointsToAdd;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 5, 0));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(RenderRef.isVisible)
            {
                RenderRef.enabled = false;
                other.GetComponent<TPSLogic>().SetPoints(other.GetComponent<TPSLogic>().GetPoints() + PointsToAdd);

                //if(CollectSFX != null && GameObject.Find("Sound System") != null)
                //    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(CollectSFX);
            }
        }
    }
}
