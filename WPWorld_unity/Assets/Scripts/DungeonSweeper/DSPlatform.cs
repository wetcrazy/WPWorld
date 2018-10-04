using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlatform : MonoBehaviour
{
    
    public void Expand(Vector3 _vector3) // Expands the size of the cube
    {
        transform.localScale += _vector3;
    }

    public void Rotate()
    {
        transform.Rotate(Vector3.up, 90);
    }
}
