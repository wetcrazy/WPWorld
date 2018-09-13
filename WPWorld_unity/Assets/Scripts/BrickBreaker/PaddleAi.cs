﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Paddle Ai Script
/// </summary>
public class PaddleAi : MonoBehaviour
{
    public GameObject TargetPrefab;
    public float speed = 1;

    [SerializeField]
    private GameObject[] arr_TargetOBJ;
   
    private void Update()
    {
        arr_TargetOBJ = GameObject.FindGameObjectsWithTag(TargetPrefab.tag);

        if (arr_TargetOBJ == null || arr_TargetOBJ.Length == 0)
        {
            return;
        }

        //RaycastHit _hit;
        //RaycastHit _closestHit = new RaycastHit();
        /*
        for (int i = 0; i < arr_TargetOBJ.Length; i++)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit))
            {
                if(_hit.transform.name == arr_TargetOBJ[i].name)
                {
                    Debug.Log("test worked");
                    return;
                }
                Debug.Log("Checking for closest ball");
                if (_closestHit.transform == null)
                {
                    _closestHit = _hit;                    
                }
                if (_hit.distance < _closestHit.distance)
                {
                    _closestHit = _hit;                 
                }
            }
        }
        */

        /*
        if(Physics.Raycast(transform.position,Vector3.up,out _hit))
        {
            for (int i = 0; i < arr_TargetOBJ.Length; i++)
            {
                if(_hit.transform.gameObject == arr_TargetOBJ[i])
                {
                    if (_hit.distance < _closestHit.distance)
                    {
                        _closestHit = _hit;
                    }
                }
            }
        }
        */

        //Debug.Log("Before translation " + _closestHit.collider.name);      
        //transform.LookAt(_closestHit.transform);
        //transform.Translate(transform.right * speed * Time.deltaTime);
        //Vector3 _newPos = new Vector3(_closestHit.transform.position.x, 0, _closestHit.transform.position.z);
        //transform.Translate(_newPos);

        /*
        RaycastHit[] _hits;
        _hits = Physics.RaycastAll(transform.position,transform.right,1000);
        Vector3 _newPos = new Vector3();
        // Check all hits
        for (int i = 0; i < _hits.Length; i++)
        {
            // Check all targets
            for (int n=0;n<arr_TargetOBJ.Length;n++)
            {
                // Check if the hit object is the target object
                if(_hits[i].transform.gameObject == arr_TargetOBJ[n])
                {
                    // Check for null pos
                    if(_newPos == null)
                    {
                        _newPos = _hits[i].transform.position;
                    }
                    else if(Vector3.Distance(transform.position,_hits[i].transform.position)< Vector3.Distance(transform.position, _newPos))
                    {
                        _newPos = _hits[i].transform.position;
                    }
                  
                }
            }
        }

        Debug.Log(_newPos);
        _newPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, _newPos,speed);
        //transform.Translate(_newPos * speed * Time.deltaTime);
    */

        Transform _ClosestOBJ = null;
        for (int n = 0; n < arr_TargetOBJ.Length; n++)
        {
            if(_ClosestOBJ == null)
            {
                _ClosestOBJ = arr_TargetOBJ[n].transform;                   
            }

            else if (Vector3.Distance(_ClosestOBJ.transform.position, transform.position) > Vector3.Distance(arr_TargetOBJ[n].transform.position, transform.position))
            {
                _ClosestOBJ = arr_TargetOBJ[n].transform;             
            }
        }

        //transform.Translate(_ClosestOBJ.position * Time.deltaTime);
        //Debug.Log(_ClosestOBJ.name + " He is closer to me " + _ClosestOBJ.localPosition);
        //Debug.Log("my position is " + transform.localPosition);  
        //Destroy(_ClosestOBJ); 


        //transform.LookAt(_ClosestOBJ.transform);
        Vector3 MoveToPos = _ClosestOBJ.position;
        MoveToPos.y = this.transform.position.y;
        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(this.transform.position, MoveToPos, Time.deltaTime * speed));
    }
}
