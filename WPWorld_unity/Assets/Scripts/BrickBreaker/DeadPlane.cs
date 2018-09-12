using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script destorys anything that touches it
/// </summary>
/// 
public class DeadPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
