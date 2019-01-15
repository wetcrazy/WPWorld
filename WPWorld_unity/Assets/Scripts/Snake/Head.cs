using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Head : MonoBehaviour
{
    bool isInput = false;
    bool checkbuttons = false;
    public Text ScoreDisplay;
    public Text LifeDisplay;
    public Text WLconditionDisplay;

    bool hitself = false;
    int I_score = 0;
    int Lives = 5;

    public Button UP;
    public Button DOWN;
    public Button LEFT;
    public Button RIGHT;
 
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
    private void Start()
    {
        facingState = STATE_FACING.STATE_EMPTY;
        mainDirection = this.gameObject.transform.forward;
       // Vector3 temp = SetVectortoint(this.gameObject.transform.position);
        //this.gameObject.transform.Translate(  temp -this.gameObject.transform.position);
        // prev = this.gameObject.transform.position;
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
        hitself = false;
        hit = false;
    }
    public void Setspeed(float speed)
    {
        m_Speed = speed;
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button2 is clicked
        Debug.Log(message);
    }

    void ButtonClicked(int buttonNo)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Button clicked = " + buttonNo);
    }

    private void Update()
    {
        hit = this.gameObject.transform.GetChild(0).GetComponent<Nose>().deathcollided;

        ScoreDisplay.text = " Score : " + I_score;
        LifeDisplay.text = " Lives : " + Lives;
        
        if(I_score>5)
        {
            WLconditionDisplay.text = " WINNER ";
        }
        else if(Lives<1)
        {
            WLconditionDisplay.text = "GAMEOVER";
        }
      
        if(m_Speed!= normalspeed)
        {
            timer -= Time.deltaTime;
        }

        if(timer<=0)
        {
            m_Speed = normalspeed;
        }
        // Player movement
        if (!hit)
        {
            PlayerControl();


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
       // var kidsbod = this.gameObject.GetComponentsInChildren<Rigidbody>();
       // if(kidsbod.)
    }
 
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

    // Inputs
    private void PlayerControl()
    {
       
        float check = Vector3.Distance(prev, this.gameObject.transform.position);

         
        // bool isInput = false;
        //if (check >= 1f || !once)
        //{
        //    Vector3 temp = SetVectortoint(this.gameObject.transform.position);
        //    this.gameObject.transform.Translate(temp - this.gameObject.transform.position);
        //    prev = this.gameObject.transform.position;
        if (Input.GetKey(KeyCode.UpArrow) && facingState != STATE_FACING.STATE_BACKWARD && facingState != STATE_FACING.STATE_FORWARD)
            {
                gameObject.transform.forward = Quaternion.AngleAxis(0, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_FORWARD;
            once = true;
            isInput = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && facingState != STATE_FACING.STATE_FORWARD && facingState != STATE_FACING.STATE_BACKWARD)
            {
                gameObject.transform.forward = Quaternion.AngleAxis(180, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_BACKWARD;
            once = true;
            isInput = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && facingState != STATE_FACING.STATE_RIGHT && facingState != STATE_FACING.STATE_LEFT)
            {
                gameObject.transform.forward = Quaternion.AngleAxis(270, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_LEFT;
            once = true;
            isInput = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && facingState != STATE_FACING.STATE_LEFT && facingState != STATE_FACING.STATE_LEFT)
            {
                gameObject.transform.forward = Quaternion.AngleAxis(90, gameObject.transform.up) * mainDirection;
                facingState = STATE_FACING.STATE_RIGHT;
            once = true;
            isInput = true;
            }

       
           
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

    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            AddBody();
        }
       
    } 

    //returns a vector3 with int values
    Vector3 SetVectortoint(Vector3 vecpos)
    {
        int Vx = Mathf.RoundToInt(vecpos.x);
        int Vy = Mathf.RoundToInt(vecpos.y);
        int Vz = Mathf.RoundToInt(vecpos.z);
        Vector3 temp = new Vector3(Vx,Vy,Vz);
        return temp;
    }
    //score goes up
    public void AddAppleAte()
    {
        I_score++;
    }
  
}
