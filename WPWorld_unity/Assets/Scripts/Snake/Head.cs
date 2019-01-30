using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Head : MonoBehaviour
{
    Vector3 scalingoffset;

    public Text dispos;
    bool space = false;
    bool isInput = false;
    public Text ScoreDisplay;
    public Text LifeDisplay;
    public Text WLconditionDisplay;
    public Text MultiplierDisplay;
    float I_score = 0;
    bool isstreak = false;
    int streakcounter = 0;
    float minmult = 1;
    float multiplier = 1;
    float addmult = 0.2f;
    float max_mult = 5;
    int Lives = 5;
    int HighScore = 0;
    public Button UP;
    public Button DOWN;
    public Button LEFT;
    public Button RIGHT;
    public int Goal;
    public enum STATE_FACING
    {
        STATE_FORWARD,
        STATE_BACKWARD,
        STATE_RIGHT,
        STATE_LEFT,
        STATE_EMPTY,
    }
    [SerializeField]
    private float m_Speed = 0.01f;
    [SerializeField]
    private GameObject bodyPartObj;
    [SerializeField]
    private List<GameObject> Children = new List<GameObject>();
    private Vector3 mainDirection = new Vector3();
    private STATE_FACING facingState;
    Vector3 prev = new Vector3();
    bool once = false;
    float normalspeed;
    float timer = 5;
    bool hit = false;
   // float blinking = 2.5f;
    private void Start()
    {
        scalingoffset = new Vector3(this.gameObject.transform.parent.localScale.x, this.gameObject.transform.parent.localScale.y, this.gameObject.transform.parent.localScale.z);

        facingState = STATE_FACING.STATE_EMPTY;
        mainDirection = this.gameObject.transform.forward;

        // Vector3 temp = SetVectortoint(this.gameObject.transform.position);
        ////Vector3 temp = this.gameObject.transform.position;
        //    this.gameObject.transform.Translate(temp - this.gameObject.transform.position);
        //    prev = temp;

        normalspeed = 0.01f;
        //button
        UP.onClick.AddListener(Inputup);
        DOWN.onClick.AddListener(Inputdown);
        LEFT.onClick.AddListener(Inputleft);
        RIGHT.onClick.AddListener(Inputright);
        UP.onClick.AddListener(delegate { TaskWithParameters("Up"); });
        DOWN.onClick.AddListener(delegate { TaskWithParameters("Down"); });
        LEFT.onClick.AddListener(delegate { TaskWithParameters("Left"); });
        RIGHT.onClick.AddListener(delegate { TaskWithParameters("Right"); });


        isInput = false;
        ScoreDisplay.text = "";
        LifeDisplay.text = "";
        WLconditionDisplay.text = "";
        hit = false;
        Goal = 100;

        popmypos();
    }

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


    public void Setspeed(float speed)
    {
        m_Speed = speed;
    }
    private void Update()
    {
        
        hit = this.gameObject.transform.GetChild(0).GetComponent<Nose>().deathcollided;

        ScoreDisplay.text = " Score : " + I_score;
        LifeDisplay.text = " Lives : " + Lives;

        if (I_score >= Goal)
        {
            //WLconditionDisplay.text = " WINNER ";
            //hit = true;
        }
        else if (Lives < 1 || hit)
        {
            WLconditionDisplay.text = "GAME OVER";
        }
      
        if(m_Speed!= normalspeed)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            m_Speed = normalspeed;
            timer = 5;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
          
            Stun();
        }
        //if (Input.GetKeyDown(KeyCode.Space)&& space == false)
        //{
        //    Debug.Log("SpaceDown");
        //    space = true;
        //}
        //else if(space == true && Input.GetKeyUp(KeyCode.Space))
        //{
        //    Debug.Log("spaceup");
        //    space = false;
        //}

        // Player movement
        if ((!hit))
        {
            PlayerControl();
            //if (Vector3.Distance(prev, this.gameObject.transform.position) > 0.08f)
            //{
            //    popmypos();
            //}

            if (once)
            {
                this.gameObject.transform.position += this.gameObject.transform.forward * m_Speed;
            }

           


            // Children Movement
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
                        childScript.turningPos.Dequeue();

                        if (childScript.turningDirection.Peek() == STATE_FACING.STATE_FORWARD)
                        {
                            child.transform.forward = Quaternion.AngleAxis(0, gameObject.transform.up) * mainDirection;
                        }
                        else if (childScript.turningDirection.Peek() == STATE_FACING.STATE_BACKWARD)
                        {
                            child.transform.forward = Quaternion.AngleAxis(180, gameObject.transform.up) * mainDirection;
                        }
                        else if (childScript.turningDirection.Peek() == STATE_FACING.STATE_RIGHT)
                        {
                            child.transform.forward = Quaternion.AngleAxis(90, gameObject.transform.up) * mainDirection;
                        }
                        else if (childScript.turningDirection.Peek() == STATE_FACING.STATE_LEFT)
                        {
                            child.transform.forward = Quaternion.AngleAxis(270, gameObject.transform.up) * mainDirection;
                        }

                        childScript.turningDirection.Dequeue();
                    }
                }
            }
        }
        float test = Settofixnumber(this.gameObject.transform.position.x);
        // dispos.text = test.ToString();
        MultiplierDisplay.text = multiplier.ToString();
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

    float specialx;
    float specialy;
    float specialz;
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
    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            AddBody();
        }
    } 
    //score goes up
    public void AddAppleAte()
    {
        //default score of apple
        float applecost = 10;

        isstreak = true;
        streakcounter++;


        multiplier = (minmult+(streakcounter * addmult));













        I_score+= (float)applecost *multiplier;


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
            newPosition = Children[Children.Count - 1].transform.position - (Children[Children.Count - 1].transform.forward * transform.parent.localScale.x);
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
    //void TaskOnClick()
    //{
    //    //Output this to console when Button1 or Button3 is clicked
    //    Debug.Log("You have clicked the button!");
    //}
    //void ButtonClicked(int buttonNo)
    //{
    //    //Output this to console when the Button3 is clicked
    //    Debug.Log("Button clicked = " + buttonNo);
    //}
    //used for mobile ui touches
    public void Inputup()
    {
        // float check = Vector3.Distance(prev, this.gameObject.transform.position);

        if (facingState != STATE_FACING.STATE_BACKWARD && facingState != STATE_FACING.STATE_FORWARD)
        {
            gameObject.transform.forward = Quaternion.AngleAxis(0, gameObject.transform.up) * mainDirection;
            facingState = STATE_FACING.STATE_FORWARD;
            once = true;
            isInput = true;
        }
    }
    public void Inputdown()
    {
        if (facingState != STATE_FACING.STATE_FORWARD && facingState != STATE_FACING.STATE_BACKWARD)
        {
            gameObject.transform.forward = Quaternion.AngleAxis(180, gameObject.transform.up) * mainDirection;
            facingState = STATE_FACING.STATE_BACKWARD;
            once = true;
            isInput = true;
        }
    }
    public void Inputleft()
    {
        if (facingState != STATE_FACING.STATE_RIGHT && facingState != STATE_FACING.STATE_LEFT)
        {
            gameObject.transform.forward = Quaternion.AngleAxis(270, gameObject.transform.up) * mainDirection;
            facingState = STATE_FACING.STATE_LEFT;
            once = true;
            isInput = true;
        }
    }
    public void Inputright()
    {
        if (facingState != STATE_FACING.STATE_LEFT && facingState != STATE_FACING.STATE_LEFT)
        {
            gameObject.transform.forward = Quaternion.AngleAxis(90, gameObject.transform.up) * mainDirection;
            facingState = STATE_FACING.STATE_RIGHT;
            once = true;
            isInput = true;
        }
    }


    void Stun()
    {
        /*

         //if hit = true
         hit==false

         children clear

         speed=normal


          */
        Debug.Log("Stunned");
        m_Speed = 0;
        timer = 2;
        ClearBody();

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
