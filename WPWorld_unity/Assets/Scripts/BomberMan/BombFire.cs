using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFire : MonoBehaviour
{
    //private float currTimer;
    private float MAX_TIMER = 3.0f;

    private void Update()
    {
        if(this.transform.localScale.x <= 0 || this.transform.localScale.y <= 0 || this.transform.localScale.z <= 0)
        {
            Destroy(this);
        }
        else
        {
            Vector3.Lerp(this.transform.localScale, Vector3.zero, MAX_TIMER);
        }
    }
}
