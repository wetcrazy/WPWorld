using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Does the spawning of the ball
/// </summary>
public class BallSpawner : MonoBehaviour
{
    // Prefab for ball
    public GameObject ballPrefab;
    public int MAX_balls;
    public float MAX_TIMER;

    //private float MAX_row, MAX_col;
    private float curr_Timer;

    // Intialize 
    private void Awake()
    {
        // MAX_row = transform.parent.localScale.z;
        //MAX_col = transform.parent.localScale.x;
        curr_Timer = 0;
    }

    private void Update()
    {
        // If there is no prefab being used
        if (ballPrefab == null)
        {
            Debug.Log("ERROR: Fail Spawn " + ballPrefab.name);
            return;
        }

        int _currballs = GameObject.FindGameObjectsWithTag(ballPrefab.tag).Length;

        // Stop spawning when max balls
        if (_currballs >= MAX_balls)
        {
            return;
        }

        if(curr_Timer < MAX_TIMER)
        {
            curr_Timer += 0.1f;
            return;
        }
        curr_Timer = 0;

        // Spawns the ball

        //float _row = Random.Range(-MAX_row, MAX_row);
        //float _col = Random.Range(-MAX_col, MAX_col);

        //float _height = transform.localPosition.y;

        var _newPos = Random.insideUnitSphere * 1.0f;
        _newPos.y = transform.position.y;

        // var _newPos = new Vector3(_col/2, _height/2, _row/2);
        var _newOBJ = Instantiate(ballPrefab, _newPos, Quaternion.identity, transform);
        Debug.Log("Spawned " + _newOBJ.name);
    }

}
