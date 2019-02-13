using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour {

    // Background Panel Settings
    private Image B_ImageRef;
    private Color B_PanelColor;

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
    private Vector3 RotateAngle;

    private Rigidbody2D W_RigidRef;
    private RectTransform W_PosRef;

    [Header("Main Panel Settings")]
    [SerializeField]
    private GameObject MainPanel;
    private Image M_ImageRef;
    private Color M_PanelColor;
    private bool M_PanelActivate = false;

    public Text PlayerOneText;
    public Text PlayerOneScore;
    public Text PlayerTwoText;
    public Text PlayerTwoScore;

    [SerializeField]
    GameObject NewHighscoreText;

    [SerializeField]
    private GameObject[] Dividers;

    private Color A_Color;
    private bool A_TextShow;
    private Color B_Color;
    private bool B_TextShow;

    [Header("Pointer Score Settings")]
    [SerializeField]
    private GameObject Pointer;
    [SerializeField]
    private float RotateSpeed;
    [SerializeField]
    private float TimeToRotate;
    private float TimeElapsed;
    private Image P_ImageRef;
    private Color P_ImageColor;
    private bool P_PointerActivate;

    [Header("Exit Button Settings")]
    [SerializeField]
    private GameObject ExitButton;
    [SerializeField]
    private Vector3 ExitMovePos;

    [Header("Leaderboard Button Settings")]
    [SerializeField]
    private GameObject LeaderboardButton;
    [SerializeField]
    private Vector3 LeaderMovePos;

    [Header("Scripts")]
    [SerializeField]
    PhotonGameController MultiplayerController;

    // Use this for initialization
    void Start () {

        //Post the score to database
        MultiplayerController.SubmitScore();

        //Send your data to other players
        object[] content = new object[]
            {
                PhotonNetwork.NickName,
                MultiplayerController.PlayerScore
            };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.INFO_OTHER_PLAYER, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

        PlayerOneScore.text = MultiplayerController.PlayerScore.ToString();
        NewHighscoreText.SetActive(false);

        W_RigidRef = WinText.GetComponent<Rigidbody2D>();
        W_PosRef = WinText.GetComponent<RectTransform>();

        B_ImageRef = GetComponent<Image>();
        B_PanelColor = B_ImageRef.color;
        B_PanelColor.a = 0;

        M_ImageRef = MainPanel.GetComponent<Image>();
        M_PanelColor = M_ImageRef.color;
        M_PanelColor.a = 0;

        P_ImageRef = Pointer.GetComponent<Image>();
        P_ImageColor = P_ImageRef.color;
        P_ImageColor.a = 0;

        A_Color = PlayerTwoText.color;
        A_Color.a = 0;
        B_Color = PlayerTwoText.color;
        B_Color.a = 0;
	}

    public void UpdateOtherPlayerData(string OtherPlayerName, int OtherPlayerScore)
    {
        PlayerTwoText.text = OtherPlayerName + "'s Score";
        PlayerTwoScore.text = OtherPlayerScore.ToString();
    }

    public void HighScoreCheck()
    {
        NewHighscoreText.SetActive(true);
    }

    public void ExitToRoom()
    {
        PhotonNetwork.LoadLevel("PhotonLobbyRoom");
    }

	// Update is called once per frame
	void Update () {

        // Panel Transparencies
        {
            if (B_PanelColor.a < 0.5f)
                B_PanelColor.a += 0.01f;
            B_ImageRef.color = B_PanelColor;

            if(M_PanelActivate)
                if (M_PanelColor.a < 0.5f)
                    M_PanelColor.a += 0.01f;
            M_ImageRef.color = M_PanelColor;

            M_ImageRef.transform.GetChild(0).GetComponent<Image>().color = M_PanelColor;

            if (P_PointerActivate)
                if (P_ImageColor.a < 1.0f)
                    P_ImageColor.a += 0.05f;
            P_ImageRef.color = P_ImageColor;

            if(A_TextShow)
                if (A_Color.a < 1.0f)
                    A_Color.a += 0.05f;
            PlayerOneText.color = A_Color;
            PlayerTwoText.color = A_Color;

            for(int i = 0; i < Dividers.Length; i++)
            {
                Dividers[i].GetComponent<Image>().color = A_Color;
            }

            if (B_TextShow)
                if (B_Color.a < 1.0f)
                    B_Color.a += 0.05f;
            PlayerOneScore.color = B_Color;
            PlayerTwoScore.color = B_Color;
        }

        // Win Text Bounce
        if(!FinishedFalling)
        {
            WinText.transform.localEulerAngles = Vector3.MoveTowards(WinText.transform.localEulerAngles, RotateAngle, 15 * Time.deltaTime);

            // Drops the Win Text
            if (W_PosRef.anchoredPosition.y > DropSpot.y)
            {
                W_RigidRef.AddForce(-Vector3.up * DropSpeed);
            }
            else
            {

                M_PanelActivate = true;
                A_TextShow = true;
                W_PosRef.anchoredPosition = DropSpot;
                W_RigidRef.velocity = Vector3.zero;

                if (Bounces != 0)
                {
                    W_RigidRef.AddForce(Vector3.up * BounceSpeed * Bounces);
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
                        W_RigidRef.velocity = Vector3.zero;
                        W_PosRef.anchoredPosition = DropSpot;
                        W_RigidRef.constraints = RigidbodyConstraints2D.FreezeAll;
                        P_PointerActivate = true;
                    }
                }
            }
        }

        // Pointer
        if(P_PointerActivate)
        {
            if(TimeElapsed > TimeToRotate)
            {
                Vector3 TargetRotation;

                // Look at Player One
                if (int.Parse(PlayerOneScore.text) > int.Parse(PlayerTwoScore.text))
                    TargetRotation = Vector3.zero;
                // Look at Player Two
                else
                    TargetRotation = new Vector3(0,0,180);

                if(Vector3.Distance(Pointer.transform.localEulerAngles, TargetRotation) < 1.0f)
                {
                    B_TextShow = true;
                    Pointer.transform.localEulerAngles = TargetRotation;
                }
                else
                {
                    Pointer.transform.localEulerAngles = Vector3.Lerp(Pointer.transform.localEulerAngles, TargetRotation, 3 * Time.deltaTime);
                }
            }
            else
            {
                TimeElapsed += Time.deltaTime;
                Pointer.transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
            }
        }

        // Buttons
        if(A_TextShow && B_TextShow)
        {
            LeaderboardButton.transform.localPosition = Vector3.Lerp(LeaderboardButton.transform.localPosition, LeaderMovePos, 2 * Time.deltaTime);
            ExitButton.transform.localPosition = Vector3.Lerp(ExitButton.transform.localPosition, ExitMovePos, 2 * Time.deltaTime);
        }
	}
}
