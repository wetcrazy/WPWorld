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

    [SerializeField]
    private float Bounces;
    [SerializeField]
    private float BounceSpeed;

    private bool FinishedFalling = false;
    [SerializeField]
    private Vector3 RotateAngle;

    private Rigidbody2D RigidRef;
    private RectTransform PosRef;

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

        if(!FinishedFalling)
        {
            WinText.transform.localEulerAngles = Vector3.MoveTowards(WinText.transform.localEulerAngles, RotateAngle, 15 * Time.deltaTime);

            // Drops the Win Text
            if (PosRef.anchoredPosition.y > DropSpot.y)
            {
                RigidRef.AddForce(-Vector3.up * DropSpeed);
            }
            else
            {
                PosRef.anchoredPosition = DropSpot;
                RigidRef.velocity = Vector3.zero;

                if (Bounces != 0)
                {
                    RigidRef.AddForce(Vector3.up * BounceSpeed * Bounces);
                    Bounces--;

                    if(Bounces == 1)
                    {
                        RotateAngle = Vector3.zero;
                    }
                    else
                    {
                        if(Bounces % 2 == 1)
                        {
                            RotateAngle = new Vector3(0, 0, 15);
                        }
                        else
                        {
                            RotateAngle = new Vector3(0, 0, -15);
                        }
                    }

                    if (Bounces == 0)
                    {
                        FinishedFalling = true;
                        RigidRef.velocity = Vector3.zero;
                        PosRef.anchoredPosition = DropSpot;
                        RigidRef.constraints = RigidbodyConstraints2D.FreezeAll;
                    }
                }
            }
        }
        else
        {

        }
	}
}
