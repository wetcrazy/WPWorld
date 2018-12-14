using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowHead : MonoBehaviour {
    //public GameObject P;
    //   public GameObject C;
    //  // private GameObject Parent;
    //   private GameObject Child;
    //   private GameObject newChild;

    //   public Text debugdistance;
    //   public Text Headpos;
    //   public Text Bodypos;

    //   public Text objectcondition;

    //   private FollowHead Childfh;
    //   private FollowHead newCHfh;
    //   Vector3 temp;


    //   Vector3 positionto;
    //   // Use this for initialization
    //   void Start () {
    //      // SetParent(P);
    //       SetChild(C);

    //           //huh = Parent.GetComponent<MyplayerScript>();
    //       Childfh = Child.GetComponent<FollowHead>();
    //       newCHfh = newChild.GetComponent<FollowHead>();
    //       positionto = new Vector3();
    //       temp = new Vector3();
    //}

    //// Update is called once per frame
    //void Update () {

    //       objectcondition.text = this.gameObject.name + " at " + this.gameObject.transform.position.ToString();


    //       Vector3 mag = new Vector3();
    //     //  var m = huh.S_point;

    //     //  mag = Parent.transform.position - m;
    //     //  temp =  m-transform.position;

    //       debugdistance.text = " distance:  "+ temp.ToString();
    //       Bodypos.text = "BodyPos: " + transform.position.ToString();
    //     //  Headpos.text = "HeadPos: " + Parent.transform.position.ToString();

    //       // Vector3 offset = new Vector3(1, 0, 0);

    //       if (mag.magnitude > 0.8f) 
    //       {
    //           transform.Translate(temp);
    //       }
    //}

    //   //pop root child push new root child(this new root child's =old root child 
    //   //and update
    //   void Newchild(GameObject NEWCH)
    //   {
    //       if(this.gameObject.GetComponent<MyplayerScript>())
    //       {
    //           newCHfh.SetChild(Child);
    //           SetChild(NEWCH);
    //       }
    //   }

    //   void SetParent(GameObject P)
    //   {
    //       if (P)
    //       {
    //           //Parent = P;
    //       }
    //   }
    //   void SetChild(GameObject C)
    //   {
    //       if (C)
    //       {
    //           Child = C;
    //       }
    //   }

    public GameObject Child1;
    public GameObject Parent1;
    private GameObject C;
    private GameObject P;

    public int count;
    public Text Pos1;
    public Text Pos2;
    Vector3 endpos;
    Vector3 prevpos;

    Vector3 temp;
    Vector3 S_point;
    public void SetMyChild(GameObject Child)
    {
        C = Child;
    }
    public void SetMyParent(GameObject Parent)
    {
        P = Parent;
    }
     public void SetMyEndPos(Vector3 pos)
    {
        prevpos = endpos;
        endpos = pos;
        
    }
    public void Addcount()
    {
        count++;
    }
    private void Start()
    {
        SetMyChild(Child1);
        SetMyParent(Parent1);
        endpos = new Vector3(0, 0, 0);
        
    }

    void SetpopPos()
    {
        SetRoundOffFix(transform.position);
        S_point = this.transform.position;
    }
    //translates this object to the rounded off of its position
    void SetRoundOffFix(Vector3 T)
    {
        Vector3 temp = RoundoffFix(T);
        transform.Translate(temp - T);
    }
    //Round off given vector to int and returns the rounded off vector
    Vector3 RoundoffFix(Vector3 T)
    {
        var posx = Mathf.RoundToInt(T.x);
        var posy = Mathf.RoundToInt(T.y);
        var posz = Mathf.RoundToInt(T.z);
        Vector3 S = new Vector3(posx, posy, posz);
        return S;
    }

    private void Update()
    {
        if (temp.magnitude >= 1f)
        {
            SetpopPos();

        }
        Pos1.text = " Prev pos: " + count.ToString();
        Pos2.text = " End pos: " + endpos.ToString();
        C.gameObject.GetComponent<FollowHead>().SetMyEndPos(S_point);
        if (!this.gameObject.GetComponent<MyplayerScript>())
        {
            Vector3 Direction = endpos - this.gameObject.transform.position;
            this.gameObject.transform.Translate(Direction);
        }
        temp = S_point - this.transform.position;
    }
}
