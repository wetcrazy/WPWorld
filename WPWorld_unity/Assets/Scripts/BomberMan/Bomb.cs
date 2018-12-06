using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject BombFirePrefab;
    public GameObject BlockPrefab;

    [SerializeField]
    private int firePower;

    private float currTimer;
    private float MAX_TIMER = 3.0f;

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
        var newBomb = BombFirePrefab;
        Instantiate(newBomb, this.transform.position, Quaternion.identity, this.transform.parent);
        // + X
        RaycastHit hit;
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, Vector3.right, out hit, BlockPrefab.transform.localScale.x * i))
            {
                if(hit.transform.gameObject.tag != "BombFire")
                {
                    continue;
                }             
            }
            else
            {
                var _newBomb = BombFirePrefab;
                Instantiate(newBomb, this.transform.position + Vector3.right * (BlockPrefab.transform.localScale.x * i), Quaternion.identity, this.transform.parent);
            }
        }
        // - X
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, -Vector3.right, out hit, BlockPrefab.transform.localScale.x * i))
            {
                if (hit.transform.gameObject.tag != "BombFire")
                {
                    continue;
                }
            }
            else
            {
                var _newBomb = BombFirePrefab;
                Instantiate(newBomb, this.transform.position + -Vector3.right * (BlockPrefab.transform.localScale.x * i), Quaternion.identity, this.transform.parent);
            }
        }
        // + Z
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, Vector3.forward, out hit, BlockPrefab.transform.localScale.x * i))
            {
                if (hit.transform.gameObject.tag != "BombFire")
                {
                    continue;
                }
            }
            else
            {
                var _newBomb = BombFirePrefab;
                Instantiate(newBomb, this.transform.position + Vector3.forward * (BlockPrefab.transform.localScale.x * i), Quaternion.identity, this.transform.parent);
            }
        }
        // - Z
        for (int i = 1; i <= firePower; i++)
        {
            if (Physics.Raycast(this.transform.position, -Vector3.forward, out hit, BlockPrefab.transform.localScale.x * i))
            {
                if (hit.transform.gameObject.tag != "BombFire")
                {
                    continue;
                }
            }
            else
            {
                var _newBomb = BombFirePrefab;
                Instantiate(newBomb, this.transform.position + -Vector3.forward * (BlockPrefab.transform.localScale.x * i), Quaternion.identity, this.transform.parent);
            }
        }
        Destroy(this.gameObject);
    }

    public void SetBombPower(int _newPower)
    {
        firePower = _newPower;
    }

    public void SetBombTimer(int _newTime)
    {
        MAX_TIMER = _newTime;
    }
}
