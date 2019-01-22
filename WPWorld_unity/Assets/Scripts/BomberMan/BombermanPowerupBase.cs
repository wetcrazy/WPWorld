using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPowerupBase : MonoBehaviour
{
    private bool isCollected;
  
    // Constructor
    public BombermanPowerupBase()
    {
        isCollected = false;
    }

    private void Update()
    {
        // Power up collected destory it
        if(isCollected)
        {
            Destroy(this.gameObject);
        }
    }

    // Constant Random Rotation
    public void Rotate()
    {
        
    }

    // Empty Function
    public virtual void Effect()
    {

    }

    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isCollected = true;
        }
    }
}
