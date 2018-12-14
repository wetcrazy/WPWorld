using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberManPlayer : MonoBehaviour
{
    public GameObject Bombprefab;

    private int firePower;
    private int Lives;
    private bool isDead;
    private int MAX_NUMBOMB;
    [SerializeField]
    private int currNUMBomb;
    private float currTimer;
    private float MAX_TIMER;

    private bool isLose;

    private void Start()
    {
        isDead = false;
        isLose = true;
        firePower = 10; // Default 1, can be increased
        Lives = 3;
        currTimer = 0.0f;
        MAX_TIMER = 3.0f;
        MAX_NUMBOMB = 1;
        currNUMBomb = 0;
    }

    private void Update()
    {
        if(isDead)
        {
            if(currTimer > MAX_TIMER)
            {
                currTimer = 0.0f;
                if(Lives <= 0)
                {
                    isLose = true;
                }
                else
                {
                    Respawn();
                }
            }
            else
            {
                currTimer += 1.0f * Time.deltaTime;
            }
        }
    }

    public void SpawnBomb()
    {
        if(isLose && currNUMBomb < MAX_NUMBOMB)
        {
            currNUMBomb += 1;
            var newBomb = Bombprefab;
            newBomb.GetComponent<Bomb>().SetBombPower(firePower);
            newBomb.GetComponent<Bomb>().SetBombOwner(this.transform.gameObject);
            Instantiate(newBomb, this.transform.position, Quaternion.identity, this.transform.parent);
        }   
    }

    public void Respawn()
    {
        this.transform.GetComponent<GridMovementScript>().Respawn();
    }

    public void SetisDead(bool _boolvalue)
    {
        isDead = _boolvalue;
    }

    public void OneBombDestory()
    {
        currNUMBomb -= 1;
    }
}
