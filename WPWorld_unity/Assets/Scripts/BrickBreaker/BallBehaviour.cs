using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public GameObject BrickPrefab;

    private GameObject[] arr_BrickOBJ;


    private void Update()
    {
        arr_BrickOBJ = GameObject.FindGameObjectsWithTag(BrickPrefab.tag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ball Touched");
        for (int i = 0; i < arr_BrickOBJ.Length; i++)
        {
            if (collision.gameObject == arr_BrickOBJ[i])
            {
                Destroy(collision.gameObject);
            }
        }

    }
}
