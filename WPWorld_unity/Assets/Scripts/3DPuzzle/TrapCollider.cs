using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCollider : MonoBehaviour {

    private Vector3 PlayerStartpos; // original player position
    private Vector3 TrapPos; // original trap position
    public bool isCollided = false;
    public SendTrigger2 render;

    public Vector3 GetTrapPos()
    {
        return TrapPos;
    }

    Scene CurrentScene;
    // Use this for initialization
    void Start () {
        TrapPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("YOU DIED");
            isCollided = true;
        }
    }
}
