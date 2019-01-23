using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanPowerupBase : MonoBehaviour
{
    // GameObject PlayerTarget == Collector
    // Bool isCollected == Powerup is been collected by player
   
    public bool isCollected { get; set; }
    [HideInInspector]
    public GameObject PlayerTarget = null; 

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
            Effect();    

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
            PlayerTarget = other.gameObject;
            isCollected = true;
        }
    }
}
