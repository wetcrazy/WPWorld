using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    [SerializeField]
    private float ActualScore;
    private float ScoreToShow;

    // Variables to grab
    private Text TextRef;
    private TPSLogic PlayerRef;
    private GameObject[] PlayerInstances;

	// Use this for initialization
	void Start () {
        TextRef = GetComponent<Text>();

        PlayerInstances = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < PlayerInstances.Length; i++)
        {
            if (!PlayerInstances[i].GetComponent<TPSLogic>().isMine())
                continue;
            PlayerRef = PlayerInstances[i].GetComponent<TPSLogic>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        TextRef.text = " : " + ScoreToShow.ToString("F0");

        ScoreToShow = Mathf.Lerp(ScoreToShow, ActualScore, Time.deltaTime);
    }
}
