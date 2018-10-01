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
            TrapCollider theTrap = Traps.transform.GetChild(i).gameObject.GetComponent<TrapCollider>();

            if (theTrap.Get_isCollided())
            {
                ResetValues();
                break;
            }
            else if (theTrap.gameObject.activeSelf)
            {
                theTrap.TrapUpdate();
            }
        }
    }

    void ResetValues()
    {
        Players.transform.position = PlayerStartpos;
        Players.transform.forward = Vector3.forward;
        for (int i = 0; i < Traps.transform.childCount; ++i)
        {
            TrapCollider theTrap = Traps.transform.GetChild(i).gameObject.GetComponent<TrapCollider>();
            theTrap.transform.position = theTrap.GetTrapPos();
            theTrap.Set_RenderOut(false);
            theTrap.Set_isCollided(false);
            theTrap.gameObject.SetActive(true);
            theTrap.Set_mooved(false);
        }
        
    }
}
