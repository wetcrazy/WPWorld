using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class BomberManPlayer : MonoBehaviourPun, IPunObservable, IOnEventCallback
{
    public GameObject Bombprefab;

    private int firePower;
    private int Lives;
    private bool isDead;
    private int MAX_NUMBOMB;
    [SerializeField]
    private int currNUMBomb;
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
        }
    }

    private void Start()
    {
        isDead = false;
        isLose = true;
        firePower = 10; // Default 1, can be increased
        Lives = 3;
        currTimer = 0.0f;
        MAX_TIMER = 3.0f;
        MAX_NUMBOMB = 1;
        currNUMBomb = 0;

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

        if (isDead)
        {
            if(currTimer > MAX_TIMER)
            {
                currTimer = 0.0f;
                if(Lives <= 0)
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

    private void SpawnBomb(GameObject BombData, Bomb BombDataScript)
    {
        currNUMBomb++;
        Instantiate(BombData, BombDataScript.GetOwner().transform.position, Quaternion.identity, BombDataScript.GetOwner().transform.parent);
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

    public void OnEvent(EventData photonEvent)
    {
        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.EVENT_DROP_BOMB:
                {
                    GameObject theBomb = (GameObject)photonEvent.CustomData;
                    SpawnBomb(theBomb, theBomb.GetComponent<Bomb>());
                    break;
                }

            default:
                break;
        }
    }

    public void onBombButtonDown()
    {
        if (isLose && currNUMBomb < MAX_NUMBOMB)
        {
            var newBomb = Bombprefab;
            newBomb.GetComponent<Bomb>().SetBombPower(firePower);
            newBomb.GetComponent<Bomb>().SetBombOwner(gameObject);
            newBomb.GetComponent<Bomb>().SetBombOwnerPUN(PhotonNetwork.LocalPlayer);

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.EVENT_DROP_BOMB, newBomb, raiseEventOptions, sendOptions);
        }
    }

    public void Respawn()
    {
        this.transform.GetComponent<GridMovementScript>().Respawn();
    }

    public void SetisDead(bool _boolvalue)
    {
        isDead = _boolvalue;
    }

    public void OneBombDestory()
    {
        currNUMBomb -= 1;
    }
}
