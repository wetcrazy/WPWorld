﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCollide : MonoBehaviour {

    [SerializeField]
    private Vector3 MoveAmount;
    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private Vector3 MovePosition;

    private Vector3 OrgPosition;

    private bool IsMoving = false;

	// Use this for initialization
	void Start () {
        OrgPosition = transform.localPosition;
        MovePosition = transform.localPosition + MoveAmount;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsMoving)
        {
            if(Vector3.Distance(transform.localPosition, MovePosition) > transform.localScale.magnitude * 0.01f)
                this.transform.localPosition = Vector3.Lerp(transform.localPosition, MovePosition, Time.deltaTime * MoveSpeed);
            else
                this.transform.localPosition = MovePosition;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            IsMoving = true;
    }

    public void Reset()
    {
        transform.localPosition = OrgPosition;
        IsMoving = false;
    }
}
