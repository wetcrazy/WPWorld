using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
  
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

    //private int gridsize = 1;
    Vector3 prev = new Vector3();
    bool once = false;
    float normalspeed;
    float timer = 5;
    private void Start()
    {
        facingState = STATE_FACING.STATE_EMPTY;
        mainDirection = this.gameObject.transform.forward;
       // Vector3 temp = SetVectortoint(this.gameObject.transform.position);
        //this.gameObject.transform.Translate(  temp -this.gameObject.transform.position);
        // prev = this.gameObject.transform.position;
        normalspeed = 0.01f;
    }
    public void Setspeed(float speed)
    {
        m_Speed = speed;
    }
    private void Update()
    {
        
        //var headspeed = m_Speed * 0.6f;
        //bool turning = false;
        if(m_Speed!= normalspeed)
        {
            timer -= Time.deltaTime;
        }

        if(timer<=0)
        {
            m_Speed = normalspeed;
        }
        // Player movement
        PlayerControl();

        //if(Children.Count > 2)
        //{
        //    if (Children[0].GetComponent<Body>().turningPos.Count == 0)
        //    {
        //        turning = false;
        //    }
        //    else if (Children[0].GetComponent<Body>().turningPos.Count > 3)
        //    {
        //        turning = true;
        //    }
        //}
        // Player Movement
        //if(turning)
        //{
        //    this.gameObject.transform.position += this.gameObject.transform.forward * headspeed * Time.deltaTime;
        //}
        //else
        //{
        if(once)
        {
            this.gameObject.transform.position += this.gameObject.transform.forward * m_Speed ;
        }

        //}     
      

        // Children Movement
        foreach (GameObject child in Children)
        {
            var childScript = child.GetComponent<Body>();

            if (childScript.turningPos.Count == 0)
            {
                child.gameObject.transform.position += child.gameObject.transform.forward * m_Speed ;             
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

    // Inputs
    private void PlayerControl()
    {
       
        float check = Vector3.Distance(prev, this.gameObject.transform.position);
       
            bool isInput = false;
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
    
    //Vector3 Snaptoposition(Vector3 V)
    //{
    //    Vector3 blob = SetVectortoint(V);
    //    V.Set(blob.x, blob.y, blob.z);
    //    return V;
    //}
}
