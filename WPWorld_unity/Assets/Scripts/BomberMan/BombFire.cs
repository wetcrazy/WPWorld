using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFire : MonoBehaviour
{  
    private float SCALING_SPEED = 1.0f;
    private Vector3 subtractScaling;

    private void Start()
    {
        subtractScaling = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void Update()
    {
        if(this.transform.localScale.x <= 0 || this.transform.localScale.y <= 0 || this.transform.localScale.z <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.localScale -= subtractScaling * SCALING_SPEED * Time.deltaTime;
        }
    }
}
