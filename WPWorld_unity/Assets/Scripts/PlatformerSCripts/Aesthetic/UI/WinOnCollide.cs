using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnCollide : MonoBehaviour
{

    private bool WonGame = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
            WonGame = true;
    }

    public void CheckIsWon(ref bool isWon)
    {
        isWon = WonGame;
    }
}
