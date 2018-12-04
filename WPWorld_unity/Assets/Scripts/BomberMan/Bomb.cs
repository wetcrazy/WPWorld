using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject BombFirePrefab;

    private float currTimer;
    private const float MAX_TIMER = 3.0f;
    private int firePower;

    private void Start()
    {
        currTimer = 0.0f;
    }

    private void Update()
    {
        if(currTimer > MAX_TIMER)
        {
            BlowUp();
        }
        else
        {
            currTimer += 1.0f * Time.deltaTime;
        }
    }

    public void BlowUp()
    {
        
        Destroy(this);
    }

    public void SetBombPower(int _newPower)
    {
        firePower = _newPower;
    }
}
