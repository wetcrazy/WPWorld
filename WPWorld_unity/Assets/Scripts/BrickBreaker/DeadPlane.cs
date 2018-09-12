using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script destorys anything that touches it
/// </summary>
/// 
public class DeadPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }
}
