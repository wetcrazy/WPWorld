using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSceneManage : MonoBehaviour {
    private Vector3 PlayerStartpos; // original player position
    private GameObject Players;
    private GameObject Traps;
    private GameObject dummer;
    [SerializeField]
    private AudioClip Youarealreadydead;
    // Use this for initialization
    void Start () {
        Players = GameObject.FindGameObjectWithTag("Player");
        PlayerStartpos = Players.transform.position;
        Traps = GameObject.FindGameObjectWithTag("PuzzleTrap");
        dummer = GameObject.FindGameObjectWithTag("DumbAI");
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
        for (int j = 0; j < dummer.transform.childCount; ++j)
        {
            DumbAI Dumb = dummer.transform.GetChild(j).gameObject.GetComponent<DumbAI>();

            if (Dumb.GetCollideAI())
            {
                ResetValues();
                break;
            }
            else  
            {
                Dumb.UpdateTheThing();
            }
        }
       // DumbAI Dumb = dummer.gameObject.GetComponent<DumbAI>();

        //if(Dumb.GetCollideAI())
        //{
        //    ResetValues();
        //}
        //else
        //{
        //    Dumb.UpdateTheThing();
        //}
    }

    void ResetValues()
    {
        Players.transform.position = PlayerStartpos;
        Players.transform.forward = Vector3.forward;
        GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(Youarealreadydead);
        for (int i = 0; i < Traps.transform.childCount; ++i)
        {
            TrapCollider theTrap = Traps.transform.GetChild(i).gameObject.GetComponent<TrapCollider>();
            theTrap.transform.position = theTrap.GetTrapPos();
            theTrap.Set_RenderOut(false);
            theTrap.Set_isCollided(false);
            theTrap.gameObject.SetActive(true);
            theTrap.Set_mooved(false);
        }
        for (int j = 0; j < dummer.transform.childCount; ++j)
        {
            DumbAI Dumb = dummer.transform.GetChild(j).gameObject.GetComponent<DumbAI>();
            Dumb.transform.position = Dumb.OriginalPos;
            Dumb.SetCollideAI(false);
            Dumb.CurrState = "";
        }
    }
}
