using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class BomberManPlayer : MonoBehaviourPun
{
    // Player Properties
    private int firePower;
    private int currLives;
    private const int MAX_Lives = 3;
    private bool isDead;
    private int MAX_NUMBOMB;
    private int currNUMBomb;
    // private Vector3 respawnPt;
    private Vector3 OrignScale;

    // For Respawning Cool Down
    private float currTimer;
    private const float MAX_TIMER = 3.0f;

    // Lose Condition
    private bool isLose;

    // Highscore
    private int Score = 0;
    public int PlayerScore
    {
        get { return Score; }
        set { Score = value; }
    }

    // Invurnable Frame (Shouldn't be in reset function)
    private const float MAX_invurnTime = 2.0f;
    private float curr_invurnTime = 0.0f;
    private bool isDmgtaken = false;
    private bool isBlinking = false;

    // Heart Container
    private GameObject HeartContainer;
    SendOptions sendOptions = new SendOptions { Reliability = true };

    private void Awake()
    {
        //if(photonView.IsMine)
        //{
        //    respawnPt = this.transform.position;
        //}
    }

    private void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        Reset();
        OrignScale = this.transform.localScale;
       
        //this.gameObject.GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        // Death Respawn
        if (isDead)
        {
            if (currTimer > MAX_TIMER)
            {
                currTimer = 0.0f;
                if (currLives <= 0)
                {
                    isLose = true;
                }
                else
                {
                    Reset();
                }
            }
            else
            {
                currTimer += 1.0f * Time.deltaTime;
            }
        }
           
        // Invurnable Frame
        if (isDmgtaken)
        {        
            InvurnablePlayer();
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        ////Send other players our data
        //if (stream.IsWriting)
        //{
        //    stream.SendNext(transform.localPosition);
        //}
        //else //Receive data from other players
        //{
        //    foreach (GameObject player in PlayerObjects)
        //    {
        //        if (player.GetPhotonView().OwnerActorNr == info.Sender.ActorNumber)
        //        {
        //            player.transform.localPosition = (Vector3)stream.ReceiveNext();
        //            break;
        //        }
        //    }
        //}
    }

    // Bomb Button
    public void onBombButtonDown()
    {    
        if(currNUMBomb >= MAX_NUMBOMB)
        {
            return;
        }

        if (ARMultiplayerController.isSinglePlayer)
        {
            GameObject.FindGameObjectWithTag("BombermanManager").GetComponent<BombermanManager>().SpawnBomb(this.transform.localPosition, firePower, this.gameObject);         
        }
        else
        {
            object[] content = new object[]
            {
                transform.localPosition,
                firePower
            };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.BOMBER_EVENT_DROP_BOMB, content, raiseEventOptions, sendOptions);
        }

        currNUMBomb += 1;
    }
    // Respawn the player
    //public void Respawn()
    //{
    //    this.transform.position = respawnPt;
    //}
    // Reset function
    public void Reset()
    {
        isDead = false;
        isLose = true;
        firePower = 3; // Default 3, can be increased
        currLives = MAX_Lives;
        currTimer = 0.0f;
        MAX_NUMBOMB = 1;
        currNUMBomb = 0;

        Respawn();
    }
    // Player blinking
    public void InvurnablePlayer()
    {     
        if (curr_invurnTime > MAX_invurnTime)
        {          
            isDmgtaken = false;        
            curr_invurnTime = 0.0f;
            this.transform.localScale = OrignScale;
            return;
        }
        else
        {         
            curr_invurnTime += 1.0f * Time.deltaTime;          
        }

        if (isBlinking)
        {
            // GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>().text = "Invurn Start";
            this.transform.localScale = new Vector3(0, 0, 0);
            isBlinking = false;  
        }
        else
        {
            // GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>().text = "Invurn over";
            this.transform.localScale = OrignScale;
            isBlinking = true;
        }

    }

    // Collision
    private void OnTriggerEnter(Collider other)
    {
        // Fire
        if (other.gameObject.tag == "BombFire")
        {
            if (!isDmgtaken)
            {
                // GameObject.FindGameObjectWithTag("Debug").GetComponent<Text>().text = "Dmg Taken";
                // currLives -= 1;
                isDmgtaken = true;
            }
        }
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<Collider>().enabled = true;
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
        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.BOMBER_EVENT_PLAYER_DEATH, null, REO, sendOptions);

        // Adding Score
        photonView.RPC("PlayerAddPoints", Bomb_Owner, BombermanManager.PointsForKilling);
    }

    // Setter
    public void SetisDead(bool _boolvalue)
    {
        isDead = _boolvalue;
    }

    public void AddBombPower()
    {
        firePower += 1;
    }

    public void AddNumBomb()
    {
        MAX_NUMBOMB += 1;
    }

    public void OnBombDestoryed()
    {
        currNUMBomb -= 1;
    }

    // Getter
    public int GetBombPower()
    {
        return firePower;
    }
    public int GetMaxBombCount()
    {
        return MAX_NUMBOMB;
    }


    // Highscore
    [PunRPC]
    private void PlayerAddPoints(int PointsToAdd)
    {
        PlayerScore += PointsToAdd;
    }
}
