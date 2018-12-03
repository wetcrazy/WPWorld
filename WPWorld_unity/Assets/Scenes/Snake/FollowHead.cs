using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowHead : MonoBehaviour {
    public GameObject P;
    public GameObject C;
    private GameObject Parent;
    private GameObject Child;

    
    public Text debugdistance;
    public Text Headpos;
    public Text Bodypos;

    public MyplayerScript huh;
    Vector3 temp;
    // Use this for initialization
    void Start () {
        SetParent(P);
        SetChild(C);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mag = new Vector3();
        var m = huh.S_point;
        
        mag = Parent.transform.position - m;
        temp =  m-transform.position;

        debugdistance.text = " distance:  "+ temp.ToString();
        Bodypos.text = "BodyPos: " + transform.position.ToString();
        Headpos.text = "HeadPos: " + Parent.transform.position.ToString();
        
        // Vector3 offset = new Vector3(1, 0, 0);

        if (mag.magnitude > 0.8f) 
        {
            transform.Translate(temp);
        }
	}



    void SetParent(GameObject P)
    {
        if (P)
        {
            Parent = P;
        }
    }
    void SetChild(GameObject C)
    {
        if (C)
        {
            Child = C;
        }
    }
}
