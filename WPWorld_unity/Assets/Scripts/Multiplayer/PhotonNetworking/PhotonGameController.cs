﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonGameController : MonoBehaviour {

    [Header("Scene Objects")]
    [SerializeField]
    GameObject PlayerObjectPrefab;
    [SerializeField]
    GameObject LeaderboardUI;
    [SerializeField]
    GameObject LeaderboardHeaderUI;
    [SerializeField]
    GameObject LeaderboardEntryUI;
    
    float DistanceBetweenLeaderboardEntries;

    private void Awake()
    {
        GameSparks.Api.Messages.NewHighScoreMessage.Listener += HighScoreMessageHandler;
    }

    // Use this for initialization
    void Start () {

        if(PlayerController.LocalPlayerInstance == null)
        {
           var thePlayer = PhotonNetwork.Instantiate(this.PlayerObjectPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
            thePlayer.transform.GetChild(0).GetComponent<TextMesh>().text = thePlayer.GetComponent<PlayerController>().photonView.Owner.NickName;
        }

        DistanceBetweenLeaderboardEntries = Vector2.Distance(LeaderboardHeaderUI.transform.position, LeaderboardEntryUI.transform.position);

        LeaderboardUI.SetActive(false);
    }
	
    public void SubmitScore()
    {
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SUBMIT_SCORE").SetEventAttribute("SCORE", PlayerController.LocalPlayerInstance.GetComponent<PlayerController>().PlayerScore)
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

    public void GetLeaderboard()
    {
        new GameSparks.Api.Requests.LeaderboardDataRequest().SetLeaderboardShortCode("SUBMIT_SCORE").SetEntryCount(100).Send((response) => {
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
        GameObject NewLeaderboardEntry = Instantiate(LeaderboardEntryUI, LeaderboardEntryUI.transform.parent);

        NewLeaderboardEntry.transform.GetChild(0).GetComponent<Text>().text = Rank.ToString();
        NewLeaderboardEntry.transform.GetChild(1).GetComponent<Text>().text = PlayerName;
        NewLeaderboardEntry.transform.GetChild(2).GetComponent<Text>().text = Score;

        //Adjust the entry position based on rank number
        if (Rank > 1)
        {
            Vector3 EntryPos = NewLeaderboardEntry.transform.position;
            EntryPos.y -= (Rank - 1) * DistanceBetweenLeaderboardEntries;

            NewLeaderboardEntry.transform.position = EntryPos;
        }
    }
    
    //<summary>Listener for when player gets a new high score</summary>
    void HighScoreMessageHandler(GameSparks.Api.Messages.NewHighScoreMessage _message)
    {
        Debug.Log("NEW HIGH SCORE \n " + _message.LeaderboardName);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
