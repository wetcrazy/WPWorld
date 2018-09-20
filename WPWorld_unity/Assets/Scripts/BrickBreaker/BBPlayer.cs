using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Class for Break Brick
/// </summary>

public class BBPlayer : MonoBehaviour
{
    private Vector3 StartingPos;

    private void Awake()
    {
        StartingPos = transform.position;
    }

    public void ResetPlayer()
    {
        transform.position = StartingPos;
    }

}
