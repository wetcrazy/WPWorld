using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberManPlayer : MonoBehaviour
{
    public GameObject Bombprefab;

    private int firePower;
    private int Lives;

    private void Start()
    {
        firePower = 1;
        Lives = 3;
    }

    public void SpawnBomb()
    {
        var newBomb = Bombprefab;
        newBomb.GetComponent<Bomb>().SetBombPower(firePower);
        Instantiate(newBomb, this.transform.position, Quaternion.identity, this.transform.parent);
    }

    public void Respawn()
    {
        this.transform.GetComponent<GridMovementScript>().Respawn();
    }

    // Inputs
    public void GetBombSpawnInput()
    {
        SpawnBomb();
    }

  
}
