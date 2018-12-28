using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    public bool IsButton;

    // Button Variables
    public float ButtonTimeDelay;
    private float TimeElapsed;

    // Shared Variables
    private bool HasInteracted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(HasInteracted)
        {
            if (IsButton)
            {

            }
            else
            {

            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        
    }
}
