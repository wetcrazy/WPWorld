using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour {

    [Header("Loading Settings")]
    [SerializeField]
    private float TimeIntervals;
    [SerializeField]
    private string TextToAdd;
    [SerializeField]
    private int TimesToAdd;

    private float TimeElapsed;
    private float CurrTimesToAdd;

    private string OrgText;
    private Text TextRef;

	// Use this for initialization
	void Start () {
        TextRef = GetComponent<Text>();
        OrgText = TextRef.text;
	}
	
	// Update is called once per frame
	void Update () {
        if(TimeIntervals > TimeElapsed)
        {
            TimeElapsed += Time.deltaTime;
        }
        else
        {
            if(CurrTimesToAdd == TimesToAdd)
            {
                CurrTimesToAdd = 0;
                TextRef.text = OrgText;
            }
            else
            {
                CurrTimesToAdd++;
                TextRef.text += TextToAdd;
            }

            TimeElapsed = 0;
        }
	}
}
