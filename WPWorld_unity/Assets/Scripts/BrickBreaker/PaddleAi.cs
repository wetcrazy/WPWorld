using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Paddle Ai Script
/// </summary>
public class PaddleAi : MonoBehaviour
{
    public GameObject TargetPrefab;
    public float speed = 1;

    private GameObject[] arr_TargetOBJ;

    private void Update()
    {
        arr_TargetOBJ = GameObject.FindGameObjectsWithTag(TargetPrefab.tag);

        if (arr_TargetOBJ == null || arr_TargetOBJ.Length == 0)
        {
            return;
        }

        RaycastHit _hit;
        RaycastHit _closestHit = new RaycastHit();
        for (int i = 0; i < arr_TargetOBJ.Length; i++)
        {
            if (Physics.Raycast(transform.position, transform.up, out _hit))
            {
                if (_closestHit.transform == null)
                {
                    _closestHit = _hit;
                    return;
                }

                if (_hit.distance < _closestHit.distance)
                {
                    _closestHit = _hit;
                    return;
                }
            }
        }

        transform.LookAt(_closestHit.transform);
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
}
