using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSPlatform : MonoBehaviour
{
    [SerializeField]
    private bool isExpanding;

    public bool m_isExpanding
    {
        set { isExpanding = value; }
        get { return isExpanding; }
    }

    private void Awake()
    {    
        isExpanding = true;
    }

    private void OnCollisionEnter(Collision _col)
    {
        if(_col.gameObject.tag == "Blocks")
        {
            isExpanding = false;          
        }
    }  
}
