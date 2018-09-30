using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTrigger2 : MonoBehaviour
{
    public GameObject Trap;
    // public Renderer renderout;  // You dont need this
    public float speed = 1;

    private Renderer Renderout;

    // Use this for initialization
    void Start()
    {      
        Renderout = Trap.GetComponent<Renderer>(); // You call the component from here
        Renderout.enabled = false;
    }

    // for pushing purposes.
    // Update is called once per frame
    void Update()
    {
        if (Renderout.enabled)
            Trap.transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Renderout.enabled = true;
        Debug.Log("Surprise.");
    }

    // 0000000000000000000000000000000
    //        Public Methods
    // 0000000000000000000000000000000

    public void Set_RenderOut(bool _bool)
    {
        Renderout.enabled = _bool;
    }

    public bool Get_RenderOut()
    {
        return Renderout.enabled;
    }
}