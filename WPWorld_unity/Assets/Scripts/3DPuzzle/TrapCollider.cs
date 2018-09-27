using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCollider : MonoBehaviour {
    public GameObject player;
    //public GameObject Trap2;
    //public GameObject Trap3;
    //public GameObject Trap4;

    //stage 1
    public SendTrigger2 render;
    //public SendTrigger2 render2;
    //public SendTrigger2 render3;
    //public SendTrigger2 render4;

    //stage 2
    //public SendTrigger rrender;
    //public SendTrigger rrender2;
    //public SendTrigger rrender3;
    //public SendTrigger rrender4;

    private Vector3 PlayerStartpos; // original player position
    private Vector3 TrapPos; // original trap position
    public bool isCollided = false;

    public Vector3 GetTrapPos()
    {
        return TrapPos;
    }

    Scene CurrentScene;
    // Use this for initialization
    void Start () {
        TrapPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
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
