using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTrigger2 : MonoBehaviour
{
    public GameObject Trap;
    // public Renderer renderout;  // You dont need this
    public float speed = 1;

    private TrapCollider Traper;

    // Use this for initialization
    void Start()
    {
     Traper = Trap.AddComponent<TrapCollider>();
    }

    // for pushing purposes.
    // Update is called once per frame
    void Update()
    {
        if (Traper.Get_RenderOut() && Traper.gameObject.activeSelf == true)
            Traper.TrapUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Traper.Set_RenderOut(true);
        Debug.Log("Surprise.");
    }

  
}