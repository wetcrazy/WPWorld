﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCollider : MonoBehaviour
{

    private Vector3 PlayerStartpos; // original player position
    private Vector3 TrapPos; // original trap position
    private bool isCollided = false;
    public float speed = 1;
    private Renderer Renderout;
    private bool mooveout = false;
    public Vector3 GetTrapPos()
    {
        return TrapPos;
    }

    // Use this for initialization
    void Start()
    {
        Renderout = gameObject.GetComponent<Renderer>();
        Renderout.enabled = false;
        TrapPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("YOU DIED");
            isCollided = true;
        }
    }
    public void TrapUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, TrapPos) > 2)
        {
            gameObject.SetActive(false);
            mooveout = false;
        }
        else if (mooveout)
        {
            gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);

        }
    }
    // 0000000000000000000000000000000
    //        Public Methods
    // 0000000000000000000000000000000

    public void Set_RenderOut(bool _bool)
    {
        Renderout.enabled = _bool;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mooveout = true;
            Renderout.enabled = true;
            Debug.Log("Surprise.");
        }
    }

    public bool Get_RenderOut()
    {
        return Renderout.enabled;
    }

    public bool Get_isCollided()
    {
        return isCollided;
    }

    public void Set_isCollided(bool collide)
    {
        isCollided = collide;
    }


    public void Set_mooved(bool mooved)
    {
        mooveout = mooved;
    }

    public bool Get_mooved()
    {
        return mooveout;
    }

}
