using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour, IOnEventCallback
{
    //Text
    [Header("Canvas Text")]
    public Text ScoreDisplay;
    public Text LifeDisplay;
    public Text WLconditionDisplay;
    public Text MultiplierDisplay;

    [SerializeField]
    private GameObject LevelDesign;

    [SerializeField]
    private GameObject foodprefab = null;
    [SerializeField]
    private GameObject blockprefab = null;
    [SerializeField]
    private GameObject Speedyprefab = null;

    public GameObject Level;
    //public GameObject Body;

    [SerializeField]
    private int MAX_Food = 1;
    [SerializeField]
    private int MAX_Blocks = 6;

    private int Blockcount = 0;
    public int Foodcount = 0;
    private List<GameObject> arr_Blocks = new List<GameObject>();
    private List<GameObject> arr_BODY = new List<GameObject>();

    private float score = 0;
   // private float lives = 0;
    Head PlayerHeadComponent;
    
    public static SendOptions sendOptions = new SendOptions { Reliability = true };
    public static RaiseEventOptions raiseEventAll = new RaiseEventOptions { Receivers = ReceiverGroup.All };
    public Dictionary<int, GameObject> PlayerGoDict = new Dictionary<int, GameObject>();

    private void Start()
    {
        //texts
        WLconditionDisplay.text = "";

        var arr_ChildLvl = LevelDesign.GetComponentsInChildren<Transform>();
        foreach(Transform child in arr_ChildLvl)
        {
            if(child.gameObject.tag == "Blocks")
            {
                arr_Blocks.Add(child.gameObject);
            }
        }
        var arr_body = Level.GetComponentsInChildren<Transform>();
        foreach (Transform child in arr_body)
        {
            if (child.gameObject.tag == "Body" || child.gameObject.tag == "Player")
            {
                arr_BODY.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (PlayerHeadComponent == null)
        {
            PlayerHeadComponent = Head.LocalPlayerInstance.GetComponent<Head>();
        }

        
        if (PhotonNetwork.IsMasterClient && ARMultiplayerController.isPlayerSpawned && GameObject.FindGameObjectsWithTag("Player").Length == PhotonNetwork.PlayerList.Length)
        {
            if (Foodcount < MAX_Food)
            {
                ++Foodcount;
                FoodSpawner();
            }

            if (GameObject.FindGameObjectsWithTag("Speedy").Length <= 0)
            {
                Food_stunSpawner();
            }
        }

        if (PlayerHeadComponent.spawn_block)
        {
            BlockSpawner();
            PlayerHeadComponent.spawn_block = false;
        }
    }

    public void FoodSpawner()
    {
        //bool notcolliding;
        Vector3 newPosition;
        
        newPosition = arr_Blocks[Random.Range(0, arr_Blocks.Count)].transform.localPosition;

        newPosition.y += 5;
        var newFood = Instantiate(foodprefab, Vector3.zero, Quaternion.identity, transform.parent);
        newFood.transform.localPosition = newPosition;

        object[] content = new object[]
               {
                        newPosition
               };
        PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_SPAWNFOOD, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
    }

    public void FoodSpawner(Vector3 FoodPos)
    {
        var newFood = Instantiate(foodprefab, Vector3.zero, Quaternion.identity, transform.parent);
        newFood.transform.localPosition = FoodPos;
    }

    public void BlockSpawner()
    {
        Vector3 newPosition;

        newPosition = arr_Blocks[Random.Range(0, arr_Blocks.Count)].transform.localPosition;

        newPosition.y += 5;

        var newBlock = Instantiate(blockprefab, Vector3.zero, Quaternion.Euler(Vector3.zero),transform.parent);
        newBlock.transform.localPosition = newPosition;
    }

    public void Food_stunSpawner()
    {
        if (score % 30 == 0)
        {
            Vector3 newPosition;
            newPosition = arr_Blocks[Random.Range(0, arr_Blocks.Count)].transform.localPosition;

            newPosition.y += 5;
            var newFood = Instantiate(Speedyprefab, Vector3.zero, Quaternion.identity, transform.parent);
            newFood.transform.localPosition = newPosition;

            object[] content = new object[]
               {
                        newPosition
               };
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_SPAWNSTUN, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
        }
    }
    public void Food_stunSpawner(Vector3 StunPos)
    {
        var newFood = Instantiate(Speedyprefab, Vector3.zero, Quaternion.identity, transform.parent);
        newFood.transform.localPosition = StunPos;
    }

    //UI Text Updaters
    public void UpdateMultiplierText(float amount)
    {
        MultiplierDisplay.text = "x" + amount;
    }

    public void UpdateScoreText(float amount)
    {
        ScoreDisplay.text = " Score : " + amount;
        score = amount;
    }

    public void UpdateLivesText(float amount)
    {
        LifeDisplay.text = " Lives : " + amount;
        //lives = amount;
    }

    public void UpdateWLConditionText(string text)
    {
        WLconditionDisplay.text = text;
    }

    //Player Movement
    public void Movement_Up()
    {
        PlayerHeadComponent.Inputup();

        //object[] contentPos = new object[Head.LocalPlayerInstance.transform.childCount];
        //object[] contentRot = new object[contentPos.Length];

        //for (int i = 0; i < Head.LocalPlayerInstance.transform.childCount; ++i)
        //{
        //    contentPos[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localPosition;
        //    contentRot[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localEulerAngles;
        //}

        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_POS, contentPos, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_ROT, contentRot, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
    }

    public void Movement_Down()
    {
        PlayerHeadComponent.Inputdown();

        //object[] contentPos = new object[Head.LocalPlayerInstance.transform.childCount];
        //object[] contentRot = new object[contentPos.Length];

        //for (int i = 0; i < Head.LocalPlayerInstance.transform.childCount; ++i)
        //{
        //    contentPos[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localPosition;
        //    contentRot[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localEulerAngles;
        //}

        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_POS, contentPos, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_ROT, contentRot, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
    }

    public void Movement_Left()
    {
        PlayerHeadComponent.Inputleft();

        //object[] contentPos = new object[Head.LocalPlayerInstance.transform.childCount];
        //object[] contentRot = new object[contentPos.Length];

        //for (int i = 0; i < Head.LocalPlayerInstance.transform.childCount; ++i)
        //{
        //    contentPos[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localPosition;
        //    contentRot[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localEulerAngles;
        //}

        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_POS, contentPos, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_ROT, contentRot, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
    }

    public void Movement_Right()
    {
        PlayerHeadComponent.Inputright();

        //object[] contentPos = new object[Head.LocalPlayerInstance.transform.childCount];
        //object[] contentRot = new object[contentPos.Length];

        //for (int i = 0; i < Head.LocalPlayerInstance.transform.childCount; ++i)
        //{
        //    contentPos[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localPosition;
        //    contentRot[i] = Head.LocalPlayerInstance.transform.GetChild(i).transform.localEulerAngles;
        //}

        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_POS, contentPos, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
        //PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_ROT, contentRot, Photon.Realtime.RaiseEventOptions.Default, sendOptions);
    }

    //Adds this script as one of the callback targets
    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (!PlayerGoDict.ContainsKey(photonEvent.Sender))
        {
            GameObject[] PlayerGoList = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in PlayerGoList)
            {
                if (player.GetPhotonView().OwnerActorNr == photonEvent.Sender)
                {
                    PlayerGoDict.Add(photonEvent.Sender, player);
                    break;
                }
            }
        }

        switch ((EventCodes.EVENT_CODES)photonEvent.Code)
        {
            case EventCodes.EVENT_CODES.SNAKE_EVENT_BLOCKS_POP_UP:
                {
                    PlayerHeadComponent.Block_Pop_up();
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_STUN:
                {
                    PlayerHeadComponent.Stun();
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_EATFOOD:
                {
                    Destroy(GameObject.FindGameObjectWithTag("Food"));

                    if (photonEvent.Sender == PhotonNetwork.LocalPlayer.ActorNumber)
                    {
                        PlayerHeadComponent.AddAppleAte();
                    }
                    else
                    {
                        PlayerGoDict[photonEvent.Sender].GetComponent<Head>().AddBody();
                        GameObject.Find("DebugText").GetComponent<Text>().text = PlayerGoDict[photonEvent.Sender].GetComponent<Head>().Children.Count.ToString();
                    }

                    if (PhotonNetwork.IsMasterClient)
                    {
                        --Foodcount;
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_SPAWNFOOD:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    FoodSpawner((Vector3)data[0]);
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_SPAWNSTUN:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    Food_stunSpawner((Vector3)data[0]);
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_POS:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    Head theplayer = PlayerGoDict[photonEvent.Sender].GetComponent<Head>();
                    
                    for (int i = 0; i < data.Length; ++i)
                    {
                        theplayer.Children[i].transform.localPosition = (Vector3)data[i];
                    }

                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_BODY_ROT:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    Head theplayer = PlayerGoDict[photonEvent.Sender].GetComponent<Head>();

                    for (int i = 0; i < data.Length; ++i)
                    {
                        theplayer.Children[i].transform.localEulerAngles = (Vector3)data[i];
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_PLAYER_DEATH:
                {
                    Head theplayer = PlayerGoDict[photonEvent.Sender].GetComponent<Head>();

                    foreach (GameObject bodypart in theplayer.Children)
                    {
                        Destroy(bodypart);
                    }
                    break;
                }
            default:
                break;
        }
    }
}

