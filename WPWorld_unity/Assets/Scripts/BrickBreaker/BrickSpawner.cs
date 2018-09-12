using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts spawns the bricks to break
/// </summary>
public class BrickSpawner : MonoBehaviour
{
    /// <summary>
    /// Prefab of the bricks
    /// </summary>
    public GameObject bricksPrefab;
 
    private bool isCheckedExistanceBricks = false;
    private const float MAX_ROW = 4.4f, MAX_COL = 4.4f;
    private const int MAX_HEIGHT = 20; 

    private void Update()
    {
        // Safety check for any null gameobject that is not place in the inspector
        if(bricksPrefab == null)
        {
            Debug.Log("ERROR : Fail Spawn");
            return;
        }
        
        // If checked do not run the check code again to reset
        if(isCheckedExistanceBricks)
        {
            return;
        }

        // First time spawn / Reset Spawn
        if (!CheckBricKExistence())
        {
            /*
            for (int height = (int)transform.position.y; height <= MAX_HEIGHT; height++)
            {
                for (float row = -MAX_ROW; row <= MAX_ROW;)
                {
                    for (float col = -MAX_COL; col <= MAX_COL;)
                    {
                        GameObject _temp = bricksPrefab;
                        _temp.transform.position = new Vector3(col, height, row);
                        Instantiate(_temp, _temp.transform);
                        col += 1.1f;
                    }
                    row += 1.1f;
                }
            }
            */
            Debug.Log("SPAWNED");
            for (float height = transform.localPosition.y; height <= MAX_HEIGHT;)
            {
                for (float row = -MAX_ROW; row <= MAX_ROW;)
                {
                    for (float col = -MAX_COL; col <= MAX_COL;)
                    {
                        Vector3 _pos = new Vector3(col / 10, height / 10, row / 10);
                        var _newGameObject = Instantiate(bricksPrefab, _pos, Quaternion.identity, transform);
                        col += 1.1f;
                    }
                    row += 1.1f;
                }

                height += 1.1f;
                //Instantiate(bricksPrefab, transform.localPosition,Quaternion.identity);
            }

            isCheckedExistanceBricks = true;
        }
        else
        {
            DestoryBrickExistence();
        }

    }
    
    /// <summary>
    /// Checks for brick objects in the game
    /// </summary>
    private bool CheckBricKExistence()
    {
        GameObject[] _gameobject;
        _gameobject = GameObject.FindGameObjectsWithTag("Bricks");
        for (int i = 0; i < _gameobject.Length; i++)
        {
            if (_gameobject[i] == null)
            {
                return true;
            }
        }
       

        return false;
    }

    private void DestoryBrickExistence()
    {
        GameObject[] _gameobject;
        _gameobject = GameObject.FindGameObjectsWithTag("Bricks");
        for(int i= 0;i<_gameobject.Length;i++ )
        {
            Destroy(_gameobject[i]);
        }
    }
}
