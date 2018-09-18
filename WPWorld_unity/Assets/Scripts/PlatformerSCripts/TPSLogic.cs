using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSLogic : MonoBehaviour {

    [SerializeField]
    private AudioClip JumpSFX;

    [SerializeField]
    private int Points = 0;

    [SerializeField]
    private int DeathCounter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GetComponent<PlayerMovement>().GetGrounded())
            {
                if(JumpSFX != null)
                    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(JumpSFX);
            }
        }
	}

    public void SetPoints(int n_Points)
    {
        Points = n_Points;
    }

    public int GetPoints()
    {
        return Points;
    }

    public int GetDeaths()
    {
        return DeathCounter;
    }

    public void Death()
    {
        DeathCounter++;
        GetComponent<PlayerMovement>().Respawn();
    }
}
