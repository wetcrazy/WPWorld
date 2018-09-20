using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script destorys anything that touches it
/// </summary>

public class DeadPlane : MonoBehaviour
{
    public Text UI_text;
    public float MAX_TIMER;

    private float curr_timer;

    private void Awake()
    {
        curr_timer = 0;
    }

    private void OnCollisionEnter(Collision _col)
    {
        if(_col.transform.tag  == "Player")
        {
            UI_text.enabled = true;
        }
    }
}
