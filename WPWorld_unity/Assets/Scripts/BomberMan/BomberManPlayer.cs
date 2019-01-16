using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class BomberManPlayer : MonoBehaviourPun, IPunObservable
{
    // Player Properties
    private int firePower;
    private int Lives;
    private bool isDead;
    private int MAX_NUMBOMB;
    private int currNUMBomb;

    // For Respawning Cool Down
    private float currTimer;
    private float MAX_TIMER;

    private bool isLose;

    public static GameObject LocalPlayerInstance;
    private int Score = 0;

    public int PlayerScore
    {
        get { return Score; }
        set { Score = value; }
    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
            LocalPlayerInstance.transform.parent = ARMultiplayerController._GroundObject.transform;
        }
    }

    private void Start()
    {
        isDead = false;
        isLose = true;
        firePower = 3; // Default 3, can be increased
        Lives = 3;
        currTimer = 0.0f;
        MAX_TIMER = 3.0f;
        MAX_NUMBOMB = 1;
        currNUMBomb = 0;

        //Setting the username text that is above the player objects
        if (photonView.IsMine)
        {
            LocalPlayerInstance.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = photonView.Owner.NickName;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }

        // Death Respawn
        if (isDead)
        {
            if (currTimer > MAX_TIMER)
            {
                currTimer = 0.0f;
                if (Lives <= 0)
                {
                    isLose = true;
                }
                else
                {
                    Respawn();
                }
            }
            else
            {
                currTimer += 1.0f * Time.deltaTime;
            }
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Send other players our data
        if (stream.IsWriting)
        {

        }
        else //Receive data from other players
        {

        }
    }

    // Bomb Button
    public void onBombButtonDown()
    {    
        if(currNUMBomb >= MAX_NUMBOMB)
        {
            return;
        }

        if (!PhotonNetwork.IsConnected)
        {
            GameObject.FindGameObjectWithTag("BombermanManager").GetComponent<BombermanManager>().SpawnBomb(this.transform.position, firePower, this.gameObject);         
        }
        else
        {
            object[] content = new object[]
            {
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                firePower
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well

            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.BOMBER_EVENT_DROP_BOMB, content, raiseEventOptions, sendOptions);
        }

        currNUMBomb += 1;
    }

    // Respawn the player
    public void Respawn()
    {
        this.transform.GetComponent<GridMovementScript>().Respawn();
    }

    // Setter
    public void SetisDead(bool _boolvalue)
    {
        isDead = _boolvalue;
    }

    public void OnBombDestoryed()
    {
        currNUMBomb -= 1;
    }


    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BombFire")
        {
            if (!isDead)
            {
                LocalPlayerDeathEvent(other.gameObject.transform.parent.GetComponent<Bomb>().GetOwnerPUN());
            }
        }
    }

    // Death Event
    private void LocalPlayerDeathEvent(Player Bomb_Owner)
    {
        // Set local player death
        isDead = true;

        // Singleplayer == True
        if(!PhotonNetwork.IsConnected)
        {
            return;
        }

        // Ask for mourning session
        RaiseEventOptions REO = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        SendOptions SO = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.BOMBER_EVENT_PLAYER_DEATH, null, REO, SO);

        // Adding Score
        photonView.RPC("PlayerAddPoints", Bomb_Owner, BombermanManager.PointsForKilling);
    }

    // Highscore
    [PunRPC]
    private void PlayerAddPoints(int PointsToAdd)
    {
        PlayerScore += PointsToAdd;
    }

}
