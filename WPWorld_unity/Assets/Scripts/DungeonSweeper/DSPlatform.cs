using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlatform : MonoBehaviour
{
    [SerializeField]
    private bool isExpanding;
    private bool isRotated;

    public bool m_isExpanding
    {
        set { isExpanding = value; }
        get { return isExpanding; }
    }

    public bool m_isRotated
    {
        set { isRotated = value; }
        get { return isRotated; }
    }

    private void Awake()
    {
        isRotated = false;
        isExpanding = true;
    }

    private void OnCollisionEnter(Collision _col)
    {
        if(_col.gameObject.tag == "Blocks")
        {
            isExpanding = false;
        }
    }

    // 000000000000000000000000000000000000000000
    //              PUBLIC METHOD
    // 000000000000000000000000000000000000000000

    public void Expand(Vector3 _vector3) // Expands the size of the cube
    {
        transform.localScale += _vector3;
    }

    public void Rotate()
    {             
        transform.Rotate(transform.up, 90);
        isRotated = true;
    }
}
