﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Paddle Ai Script
/// </summary>
public class PaddleAi : MonoBehaviour
{
    /// <summary>
    /// The target prefab
    /// </summary>
    public GameObject TargetPrefab;

     /// <summary>
    /// Array of targeted object
    /// </summary>
    private GameObject[] arr_TargetOBJ;
    private float speed = 0.4f;

    private void Update()
    {
        // Find all the targets in the scene
        arr_TargetOBJ = GameObject.FindGameObjectsWithTag(TargetPrefab.tag);

        // If we dont find any targets just skip the update
        if (arr_TargetOBJ == null || arr_TargetOBJ.Length == 0)
        {
            return;
        }

        // Find the closes target to this attached gameobject
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

        // Update the position
        Vector3 MoveToPos = _ClosestOBJ.position;
        MoveToPos.y = transform.position.y;      
        float step = speed + Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, MoveToPos, step);
    }
}
