using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCollider : MonoBehaviour {
    public GameObject player;
    public GameObject Trap2;
    public GameObject Trap3;
    public GameObject Trap4;

    //stage 1
    public SendTrigger2 render;
    public SendTrigger2 render2;
    public SendTrigger2 render3;
    public SendTrigger2 render4;

    //stage 2
    public SendTrigger rrender;
    public SendTrigger rrender2;
    public SendTrigger rrender3;
    public SendTrigger rrender4;

    private Vector3 PlayerStartpos;
    private Vector3 TrapPos;
    private Vector3 Trap2Pos;
    private Vector3 Trap3Pos;
    private Vector3 Trap4Pos;

    Scene CurrentScene;
    // Use this for initialization
    void Start () {
        PlayerStartpos = player.transform.position;
        TrapPos = transform.position;
        Trap2Pos = Trap2.transform.position;
        Trap3Pos = Trap3.transform.position;
        Trap4Pos = Trap4.transform.position;
        CurrentScene = SceneManager.GetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.transform.position = PlayerStartpos;
            Debug.Log("YOU DIED");
            transform.position = TrapPos;
            Trap2.transform.position = Trap2Pos;
            Trap3.transform.position = Trap3Pos;
            Trap4.transform.position = Trap4Pos;

            if (CurrentScene.name == "3DPuzzle")
            {
                render.renderout.enabled = false;
                render2.renderout.enabled = false;
                render3.renderout.enabled = false;
                render4.renderout.enabled = false;
            }
            if (CurrentScene.name == "3DPuzzle2")
            {
                rrender.renderout.enabled = false;
                rrender2.renderout.enabled = false;
                rrender3.renderout.enabled = false;
                rrender4.renderout.enabled = false;
            }

        }
    }
}
