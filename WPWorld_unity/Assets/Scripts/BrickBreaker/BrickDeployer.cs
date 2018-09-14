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

    private float MAX_row, MAX_col, MAX_height, brickOffset;
    private bool is_spawn = false;

    // Intialize 
    private void Awake()
    {
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
                    // Spawn Object
                    var _newPos = new Vector3(col / 2, height / 2, row / 2);
                    var _newOBJ = Instantiate(bricksPrefab, _newPos, Quaternion.identity, transform);                   
                    Debug.Log("Spawned " + _newOBJ.name);
                }
            }
        }
        is_spawn = true;    
    }
}
