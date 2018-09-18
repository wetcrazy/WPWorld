using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script spawns the bricks
/// </summary>
public class BrickDeployer : MonoBehaviour
{
    // Prefab for brick
    public GameObject bricksPrefab;
    // Prefab for GoalBrick
    public GameObject goalbrickPrefab;

    private float MAX_row, MAX_col, MAX_height, brickOffset;
    private int GoalRNG, GoalSpawnNUM;
    private bool is_spawn = false, is_goalSpawned = false;

    // Intialize 
    private void Awake()
    {
        GoalRNG = 0;
        GoalSpawnNUM = 0;
        brickOffset = transform.parent.lossyScale.x;
        MAX_height = transform.localPosition.y + brickOffset;
        MAX_row = transform.parent.localScale.z - brickOffset;
        MAX_col = transform.parent.localScale.x  - brickOffset;
    }

    private void Update()
    {
        // If there is no prefab being used
        if (bricksPrefab == null)
        {
            Debug.Log("ERROR: Fail Spawn " + bricksPrefab.name);
            return;
        }

        // Skips the updated if the bricks has already spawned 
        if (is_spawn)
        {
            return;
        }
        
        // Randoms at the start to find when the Goal should be place
        if(GoalRNG == 0)
        {
            GoalRNG = (int)Random.Range(1, ((MAX_row * 2) * transform.localScale.z * 10) * ((MAX_col * 2) * transform.localScale.z * 10) * (MAX_height + transform.position.y - 1)); // Need to multiply 2 as this is position on the game space which includes negative and positive (except height) 
            // Do not want to spawn the goal at the top (cause i am a jerk)
            Debug.Log("Goal Randomed " + GoalRNG);
        }

        // Spawns the bricks 
        // Height
        for (float height = transform.position.y; height <= MAX_height; height += brickOffset)
        {
            // Row
            for (float row = -MAX_row; row <= MAX_row; row += brickOffset)
            {
                // Col
                for (float col = -MAX_col; col <= MAX_col; col += brickOffset)
                {
                    // Spawn POSITION                 
                    var _newPos = new Vector3(col / 2, height / 2, row / 2);  
                    // Spawn GOAL
                    if(GoalSpawnNUM == GoalRNG)
                    {
                        GoalSpawnNUM++; // Offset the goalSpawnNUM by 1 to stop spawning
                        is_goalSpawned = true;
                        var _newOBJ = Instantiate(goalbrickPrefab, _newPos, Quaternion.identity, transform);
                        Debug.Log("Spawned " + _newOBJ.name);
                    }
                    // Spawn BRICKS
                    else
                    {
                        GoalSpawnNUM++; // Updates the GoalSpawnNUM
                        var _newOBJ = Instantiate(bricksPrefab, _newPos, Quaternion.identity, transform);
                        Debug.Log("Spawned " + _newOBJ.name);
                    }               
                }
            }
        }
        is_spawn = true;    
    }
}
