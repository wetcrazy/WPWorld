using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonGameController : MonoBehaviour {

    [Header("Scene Objects")]
    [SerializeField]
    GameObject LeaderboardUI;
    [SerializeField]
    GameObject LeaderboardHeaderUI;
    [SerializeField]
    GameObject LeaderboardEntryUI;

    [Header("Debug Stuff")]
    [SerializeField]
    Text PlayerPoints;
    

    private void Awake()
    {
        GameSparks.Api.Messages.NewHighScoreMessage.Listener += HighScoreMessageHandler;
    }

    // Use this for initialization
    void Start () {
        
        LeaderboardUI.SetActive(false);
    }
	
    public void SubmitScore()
    {
        int ScoreToSubmit = 0;
        string EventShortCode = "";

        switch (SceneManagerHelper.ActiveSceneName)
        {
            case "BomberMan":
                {
                    ScoreToSubmit = PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().PlayerScore;
                    EventShortCode = "SUBMIT_SCORE_BOMBERMAN";
                    break;
                }
            case "Platformer":
                {
                    ScoreToSubmit = PlayerMovement.LocalPlayerInstance.GetComponent<TPSLogic>().CurrPointsPub;
                    EventShortCode = "SUBMIT_SCORE_PLATFORMER";
                    break;
                }

            default:
                break;
        }

        new GameSparks.Api.Requests.LogEventRequest().SetEventKey(EventShortCode).SetEventAttribute("SCORE", ScoreToSubmit)
            .Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("Score Posted Successfully...");
            }
            else
            {
                Debug.Log("Error Posting Score...");
            }
        });
    }

    public void EditScore(bool isAdd)
    {
        int Amount = 1000;

        if(!isAdd)
        {
            Amount = -Amount;
        }

        PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().PlayerScore += Amount;
        PlayerPoints.text = PlayerMovement.LocalPlayerInstance.GetComponent<BomberManPlayer>().PlayerScore.ToString();
    }

    public void GetLeaderboard()
    {
        if(LeaderboardUI.activeSelf)
        {
            LeaderboardUI.SetActive(false);
            return;
        }

        LeaderboardUI.SetActive(true);

        string LeaderboardShortCode = "";
        switch (SceneManagerHelper.ActiveSceneName)
        {
            case "BomberMan":
                {
                    LeaderboardShortCode = "LEADERBOARD_BOMBERMAN";
                    break;
                }
            case "Platformer":
                {
                    LeaderboardShortCode = "LEADERBOARD_PLATFORMER";
                    break;
                }
            default:
                break;
        }

        new GameSparks.Api.Requests.LeaderboardDataRequest()
            .SetEntryCount(10)
            .SetLeaderboardShortCode(LeaderboardShortCode)
            .Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("Found Leaderboard Data...");
                foreach (GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data)
                {
                    CreateLeaderboardEntryUI((int)entry.Rank, entry.UserName, entry.JSONData["SCORE"].ToString());

                    //string rank = entry.Rank.ToString();
                    //string playerName = entry.UserName;
                    //string score = entry.JSONData["SCORE"].ToString();
                    //Debug.Log("Rank:" + rank + " Name:" + playerName + " \n Score:" + score);
                }
            }
            else
            {
                Debug.Log("Error Retrieving Leaderboard Data...");
            }
        });
    }

    public void CreateLeaderboardEntryUI(int Rank, string PlayerName, string Score)
    {
        GameObject NewLeaderboardEntry = Instantiate(LeaderboardEntryUI, LeaderboardHeaderUI.transform);

        NewLeaderboardEntry.transform.GetChild(0).GetComponent<Text>().text = Rank.ToString();
        NewLeaderboardEntry.transform.GetChild(1).GetComponent<Text>().text = PlayerName;
        NewLeaderboardEntry.transform.GetChild(2).GetComponent<Text>().text = Score;

        //Adjust the entry position based on rank number
        if (Rank > 1)
        {
            Vector3 EntryPos = NewLeaderboardEntry.transform.localPosition;
            EntryPos.y *= Rank;

            NewLeaderboardEntry.transform.localPosition = EntryPos;
        }
    }
    
    //<summary>Listener for when player gets a new high score</summary>
    void HighScoreMessageHandler(GameSparks.Api.Messages.NewHighScoreMessage _message)
    {
        Debug.Log("NEW HIGH SCORE \n " + _message.LeaderboardName);
    }
}
