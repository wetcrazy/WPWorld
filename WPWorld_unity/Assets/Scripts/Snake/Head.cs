using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Head : MonoBehaviourPun, IPunObservable, IOnEventCallback
{

    //public Text dispos;
   // Vector3 SpawnPoint;
    float spawntime = 3.0f;

    //Facing(for rotation of "Head" object of the snake)
    public enum STATE_FACING
    {
        STATE_FORWARD,
        STATE_BACKWARD,
        STATE_RIGHT,
        STATE_LEFT,
        STATE_EMPTY,
    }
    private STATE_FACING facingState;
    private Vector3 mainDirection;// = new Vector3();
    [SerializeField]
    private float m_Speed;// = 0.01f;
    [SerializeField]
    private GameObject bodyPartObj;
    [SerializeField]
    private List<GameObject> Children;//= new List<GameObject>();
    //-----------------------------
    bool isInput = false;
    bool once = false;
    bool hit = false;

    // Player Local Instance
    public static GameObject LocalPlayerInstance;
    //SceneObject
    GameController gameController;
    
    SendOptions sendOptions = new SendOptions { Reliability = true };

    Dictionary<int, GameObject> PlayerGoDict = new Dictionary<int, GameObject>();

    private void Awake()
    {
        //Set the level as the parent
        gameObject.transform.SetParent(ARMultiplayerController._GroundObject.transform, true);

        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
      //  SpawnPoint = this.gameObject.transform.localPosition;
    }

    // float blinking = 2.5f;

    float I_score = 0;
    float timer = 5;
    //variables need testing------------------------------------
    public int Goal;
    bool space = false;
    bool isstreak = false;
    bool Isstunned = false;
    bool IsSpeedup = false;
    bool IsSpeedDown = false;
    bool IsBlockUp = false;
    float normalspeed;
    float minmult;
    float multiplier;
    float addmult;
    float max_mult ;
    int streakcounter;
    int Lives;
    int HighScore;
    Vector3 prev = new Vector3();
   // Vector3 scalingoffset;
    float starting_speed;
    float specialx;
    float specialy;
    float specialz;
    public bool spawn_block;
   // float blinking = 2.5f;
    //-------------------------------------------------------------
    //start***************************************************************************************************

    private void Start()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        //Set the pos
        gameObject.transform.localPosition = ARMultiplayerController.SpawnPoint;

        gameObject.transform.localEulerAngles = Vector3.zero;

        //Get the GameController
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //scalingoffset = new Vector3(this.gameObject.transform.parent.localScale.x, this.gameObject.transform.parent.localScale.y, this.gameObject.transform.parent.localScale.z);

        //object rotation
        facingState = STATE_FACING.STATE_EMPTY;
        //mainDirection = gameObject.transform.loc;
        //mainDirection = new Vector3(0, 0, 1);
       
        //set variables
        isInput = false;
        hit = false;
        Goal = 100;
        normalspeed = 0.01f;
        minmult = 1;
        multiplier = 1;
        addmult = 0.2f;
        max_mult = 5;
        streakcounter = 0;
        Lives = 5;
        HighScore = 0;
        starting_speed = 0.01f;
        spawn_block = false;
        //popmypos();

        //Init the text
        gameController.UpdateLivesText(Lives);
        gameController.UpdateMultiplierText(multiplier);
        gameController.UpdateScoreText(I_score);
        
        if (PhotonNetwork.IsMasterClient)
        {
            gameController.FoodSpawner();
        }
    }
    //update*********************************************************************************************

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!ARMultiplayerController.isSinglePlayer)
        {
            //Update position on other client
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_POSITION_UPDATE, gameObject.transform.localPosition, RaiseEventOptions.Default, sendOptions);

            //Update your rotation on other clients
            PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLAYER_ROTATION_UPDATE, gameObject.transform.localRotation, RaiseEventOptions.Default, sendOptions);
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            UpdateBody();
            return;
        }

        //check collision of nose
        hit = this.gameObject.transform.GetChild(0).GetComponent<Nose>().deathcollided;


        if (I_score >= Goal)
        {
            //WLconditionDisplay.text = " WINNER ";
            //hit = true;
        }
        else if (Lives < 1 || hit)
        {
           //gameController.UpdateWLConditionText("GAME OVER");
        }

        if (m_Speed != normalspeed)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            m_Speed = normalspeed;
            timer = 5;
        }
        
        

        // Player movement
        if (!hit)
        {
            PlayerControl();
        
            if (once)
            {
                gameObject.transform.position += gameObject.transform.forward * m_Speed;
            }

            // Children Movement
            UpdateBody();
        }
        else
        {
            spawntime -= Time.deltaTime;
        }

        if(spawntime<=0)
        {
           
            gameObject.transform.localPosition = ARMultiplayerController.SpawnPoint;
            this.gameObject.transform.GetChild(0).GetComponent<Nose>().deathcollided = false;
            Lives--;
            once = false;
            multiplier = minmult;
            streakcounter = 0;
            spawntime = 3.0f;
            isInput = false;
            m_Speed = normalspeed;
            gameController.UpdateLivesText(Lives);
            Stun();
        }

        //float test = Settofixnumber(this.gameObject.transform.position.x);
        // dispos.text = test.ToString();
    }

    void UpdateBody()
    {
        foreach (GameObject child in Children)
        {
            var childScript = child.GetComponent<Body>();

            if (childScript.turningPos.Count == 0)
            {
                child.gameObject.transform.position += child.gameObject.transform.forward * m_Speed;
            }
            else
            {
                child.gameObject.transform.position = Vector3.MoveTowards(child.gameObject.transform.position, childScript.turningPos.Peek(), m_Speed);

                if (childScript.turningPos.Peek() == child.transform.position)
                {

                    if (childScript.turningDirection.Peek() == STATE_FACING.STATE_FORWARD)
                    {
                        child.transform.localEulerAngles = Vector3.zero;
                    }
                    else if (childScript.turningDirection.Peek() == STATE_FACING.STATE_BACKWARD)
                    {
                        child.transform.localEulerAngles = new Vector3(0, 180, 0);
                    }
                    else if (childScript.turningDirection.Peek() == STATE_FACING.STATE_RIGHT)
                    {
                        child.transform.localEulerAngles = new Vector3(0, 90, 0);
                    }
                    else if (childScript.turningDirection.Peek() == STATE_FACING.STATE_LEFT)
                    {
                        child.transform.localEulerAngles = new Vector3(0, 270, 0);
                    }
                    childScript.turningPos.Dequeue();
                    childScript.turningDirection.Dequeue();
                }
            }
        }
    }

    // Inputs
    private void PlayerControl()
    {
       
        //float check = Vector3.Distance(prev, this.gameObject.transform.position);

         
        // bool isInput = false;
        //if (check >= 1f || !once)
        //{
        //    Vector3 temp = SetVectortoint(this.gameObject.transform.position);
        //    this.gameObject.transform.Translate(temp - this.gameObject.transform.position);
        //    prev = this.gameObject.transform.position;

        
        //if(((Vector3.Distance(prev, this.gameObject.transform.position)>= 0.08f)&& (Vector3.Distance(prev, this.gameObject.transform.position) <= 0.12f)) || (!once))
        //{

        //Rotate playerhead
            if (Input.GetKey(KeyCode.UpArrow) &&
                facingState != STATE_FACING.STATE_BACKWARD && 
                facingState != STATE_FACING.STATE_FORWARD)
            {
                gameObject.transform.forward = Quaternion.AngleAxis(0, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_FORWARD;

                once = true;
                isInput = true;
               // popmypos();
            }
            else if (Input.GetKey(KeyCode.DownArrow) && 
                     facingState != STATE_FACING.STATE_FORWARD && 
                     facingState != STATE_FACING.STATE_BACKWARD)
            {
            gameObject.transform.forward = Quaternion.AngleAxis(180, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_BACKWARD;

                once = true;
                isInput = true;
                //popmypos();
            }


            if (Input.GetKey(KeyCode.LeftArrow) && 
                facingState != STATE_FACING.STATE_RIGHT &&
                facingState != STATE_FACING.STATE_LEFT)
            {
            gameObject.transform.forward = Quaternion.AngleAxis(270, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_LEFT;

                once = true;
                isInput = true;
               // popmypos();
            }
            else if (Input.GetKey(KeyCode.RightArrow) &&
                     facingState != STATE_FACING.STATE_LEFT &&
                     facingState != STATE_FACING.STATE_LEFT)
            {
            gameObject.transform.forward = Quaternion.AngleAxis(90, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_RIGHT;

                once = true;
                isInput = true;
                //popmypos();
            }

        //    Debug.Log("hi");
        //}
           
            if (isInput)
            {
                once = true;
               
                foreach (GameObject child in Children)
                {
                    var childScript = child.GetComponent<Body>();
                    childScript.turningDirection.Enqueue(GetComponent<Head>().facingState);
                    childScript.turningPos.Enqueue(gameObject.transform.position);
                }
                isInput = false;
            }

        
       // }
    }

    // Collision
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Food")
    //    {
    //        AddBody();
    //    }
    //    else if(other.gameObject.CompareTag("Food_Block"))
    //    {
    //        Block_Pop_up();
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Food")
    //    {
    //        AddBody();
    //    }
    //    else if (collision.gameObject.CompareTag("Food_Block"))
    //    {
    //        Block_Pop_up();
    //    }
    //}

    //score goes up
    public void AddAppleAte()
    {
        //default score of apple
        float applecost = 10;

        isstreak = true;
        streakcounter++;

        multiplier = (minmult+(streakcounter * addmult));
        gameController.UpdateMultiplierText(multiplier);

        I_score+= (float)applecost * multiplier;
        gameController.UpdateScoreText(I_score);
        AddBody();
    }

    // Add body parts
    public void AddBody()
    {
        Vector3 newPosition = new Vector3();

        if (Children.Count == 0)
        {
            newPosition = this.transform.position - (this.transform.forward * transform.parent.localScale.x);
            var child = Instantiate(bodyPartObj, newPosition, this.gameObject.transform.rotation, transform.parent);
            Children.Add(child);
        }
        else
        {
            newPosition = Children[Children.Count - 1].transform.position - (Children[Children.Count - 1].transform.forward * (transform.parent.localScale.x)*0.5f);
            var child = Instantiate(bodyPartObj, newPosition, Children[Children.Count - 1].transform.rotation, transform.parent);                  
            Children.Add(child);

            var childScript = Children[Children.Count - 1].GetComponent<Body>();

            // Save turning points
            childScript.turningPos = new Queue<Vector3>(Children[Children.Count - 2].GetComponent<Body>().turningPos);
            childScript.turningDirection = new Queue<STATE_FACING>(Children[Children.Count - 2].GetComponent<Body>().turningDirection);                  
        }

    }
    //clear body parts
    public void ClearBody()
    {
        if (Children.Count > 0)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Destroy(Children[i]);
            }
            Children.Clear();
        }
    }
    //when button is pressed
    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }
    //used for mobile ui touches
    public void Inputup()
    {
        // float check = Vector3.Distance(prev, this.gameObject.transform.position);
        
        if (facingState != STATE_FACING.STATE_BACKWARD && facingState != STATE_FACING.STATE_FORWARD)
        {
            gameObject.transform.localEulerAngles = Vector3.zero;
            facingState = STATE_FACING.STATE_FORWARD;
            once = true;
            isInput = true;
        }
    }
    public void Inputdown()
    {
        if (facingState != STATE_FACING.STATE_FORWARD && facingState != STATE_FACING.STATE_BACKWARD)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 180, 0);
            facingState = STATE_FACING.STATE_BACKWARD;
            once = true;
            isInput = true;
        }
    }
    public void Inputleft()
    {
        if (facingState != STATE_FACING.STATE_RIGHT && facingState != STATE_FACING.STATE_LEFT)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 270, 0);
            facingState = STATE_FACING.STATE_LEFT;
            once = true;
            isInput = true;
        }
    }
    public void Inputright()
    {
        if (facingState != STATE_FACING.STATE_LEFT && facingState != STATE_FACING.STATE_RIGHT)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
            facingState = STATE_FACING.STATE_RIGHT;
            once = true;
            isInput = true;
        }
    }
    
    //EVENTS=======================================================================================================
    //SNAKE_EVENT_STUN,
    public void Stun()
    { 
        Debug.Log("Stunned");
        m_Speed = 0;
        timer = 2;
        ClearBody();
    }
    public void Setspeed(float speed)
    {
        m_Speed = speed;
    }
    //SNAKE_EVENT_SPEEDUP,
    void Speed_up()
    {
        Debug.Log("SpeedUP");
        Setspeed(0);
    }
    //SNAKE_EVENT_SLOWDOWN,
    void Speed_down()
    {
        Debug.Log("SpeedDown");
        Setspeed(0);
    }
    //SNAKE_EVENT_BLOCKS_POP_UP,
    public void Block_Pop_up()
    {
        spawn_block = true;
        //var block = Instantiate(bodyPartObj, newPosition, this.gameObject.transform.rotation, transform.parent);
    }

    //Getter
    public float GetPlayerScore()
    {
        return I_score;
    }

    //area need testing--------------------------------------------------------
    public void popmypos()
    {
        Vector3 test = SetVectortoint(this.gameObject.transform.position);
        //Vector3 tester = this.gameObject.transform.position;
        //Vector3 tesetesr = tester - test;
        //(0,0.1,0 - (0.4,0.1,0.2)
        //(-0.4,0,-0.2)
        this.gameObject.transform.Translate(test - this.gameObject.transform.position);
        prev = this.gameObject.transform.position;
    }

    //returns a vector3 with int values
    Vector3 SetVectortoint(Vector3 vecpos)
    {
        float Vx = (Mathf.RoundToInt(vecpos.x*10)*0.1f);
        float Vy = (Mathf.RoundToInt(vecpos.y*10)*0.1f);
        float Vz = (Mathf.RoundToInt(vecpos.z*10)*0.1f);

        Vector3 temp = new Vector3(Vx, Vy, Vz);
        return temp;
    }

    float Settofixnumber(float num)
    {
        num = Mathf.RoundToInt(num) * 0.1f;
        return num;
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
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
                    Block_Pop_up();
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_STUN:
                {
                    Stun();
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_EATFOOD:
                {
                    Destroy(GameObject.FindGameObjectWithTag("Food"));
                    GameObject theplayer = PlayerGoDict[photonEvent.Sender];

                    if(theplayer.GetPhotonView().IsMine)
                    {
                        AddAppleAte();
                    }
                    else
                    {
                        AddBody();
                    }

                    if(PhotonNetwork.IsMasterClient)
                    {
                        gameController.FoodSpawner();
                    }
                    break;
                }
            case EventCodes.EVENT_CODES.SNAKE_EVENT_SPAWNFOOD:
                {
                    object[] data = (object[])photonEvent.CustomData;
                    gameController.FoodSpawner((Vector3)data[0]);
                    break;
                }

            default:
                break;
        }
        
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
}


/* 
    vector prevpos
    vector position

    set prevpos as position
    moves position in 1 direction * the speed

    if prevpos
    set pos into roundoff

 */ 
