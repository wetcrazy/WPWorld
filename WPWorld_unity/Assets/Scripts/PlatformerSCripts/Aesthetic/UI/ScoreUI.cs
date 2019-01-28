using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    [SerializeField]
    private float ActualScore;
    [SerializeField]
    private float ScoreToShow;

    [SerializeField]
    private float PulseIntervals;
    private float TimeElapsed;

    [SerializeField]
    private GameObject Icon;

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
        // ActualScore = PlayerRef.CurrPointsPub;

        TextRef.text = " : " + ScoreToShow.ToString("F0");

        if (Mathf.Abs(ActualScore - ScoreToShow) > 0.5f)
        {
            if(TimeElapsed > 0)
            {
                TimeElapsed -= Time.deltaTime;
            }
            else
            {
                Icon.GetComponent<CoinFeedbackUI>().Pulse();

                TimeElapsed = PulseIntervals;
            }

            ScoreToShow = Mathf.MoveTowards(ScoreToShow, ActualScore, 50 * Time.deltaTime);
        }
        else
        {
            ScoreToShow = ActualScore;
        }
    }
}
