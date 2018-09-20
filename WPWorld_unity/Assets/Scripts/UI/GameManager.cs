using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject PlayerOBJ;

    private void Awake()
    {
        PlayerOBJ = GameObject.FindGameObjectWithTag("Player");
    }

    public void SendPlayerReset()
    {
        PlayerOBJ.SendMessage("ResetPlayer");
    }

    public void DeleteSelf()
    {
        Destroy(transform.parent.gameObject);
    }

}
