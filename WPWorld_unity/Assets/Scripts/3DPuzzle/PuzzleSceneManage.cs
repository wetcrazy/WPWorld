using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSceneManage : MonoBehaviour {
    private Vector3 PlayerStartpos; // original player position
    private GameObject Players;
    private GameObject Traps;
    // Use this for initialization
    void Start () {
        Players = GameObject.FindGameObjectWithTag("Player");
        PlayerStartpos = Players.transform.position;
        Traps = GameObject.FindGameObjectWithTag("PuzzleTrap");

	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < Traps.transform.childCount; ++i)
        {
            Transform theTrap = Traps.transform.GetChild(i);
            if(theTrap.GetComponent<TrapCollider>().isCollided)
            {
                ResetValues();
                break;
            }
        }
    }

    void ResetValues()
    {
        Players.transform.position = PlayerStartpos;
        Players.transform.forward = Vector3.forward;
        for (int i = 0; i < Traps.transform.childCount; ++i)
        {
            Transform theTrap = Traps.transform.GetChild(i);
            theTrap.transform.position = theTrap.GetComponent<TrapCollider>().GetTrapPos();
            theTrap.GetComponent<TrapCollider>().isCollided = false;
            theTrap.GetComponent<TrapCollider>().render.renderout.enabled = false;
        }
        
    }
}
