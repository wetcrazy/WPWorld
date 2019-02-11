using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenUI : MonoBehaviour {

    [Header("Win Text Settings")]
    [SerializeField]
    private GameObject WinText;
    [SerializeField]
    private Vector3 DropSpot;
    [SerializeField]
    private float DropSpeed;

    private bool FinishedFalling = false;

    private Rigidbody2D RigidRef;
    private RectTransform PosRef;

    [SerializeField]
    private float Bounces;

    [SerializeField]
    private float BounceSpeed;

    [Header("Score Text Settings")]
    [SerializeField]
    private GameObject ScoreText;

    [Header("Exit Butto Settings")]
    [SerializeField]
    private GameObject ExitButton;

    [Header("Leaderboard Button Settings")]
    [SerializeField]
    private GameObject LeaderboardButton;

	// Use this for initialization
	void Start () {
        RigidRef = WinText.GetComponent<Rigidbody2D>();
        PosRef = WinText.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

        // Drops the Win Text
        if (PosRef.anchoredPosition.y > DropSpot.y)
        {
            RigidRef.AddForce(-Vector3.up * DropSpeed);
        }
        else
        {
            PosRef.anchoredPosition = DropSpot;
            RigidRef.velocity = Vector3.zero;

            if (!FinishedFalling && Bounces != 0)
            {
                RigidRef.AddForce(Vector3.up * BounceSpeed * 25 * Bounces);
                Bounces--;
            }
        }
	}
}
