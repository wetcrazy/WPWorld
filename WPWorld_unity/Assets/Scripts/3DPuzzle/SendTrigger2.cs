using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTrigger2 : MonoBehaviour
{
    public GameObject Trap;
    public Renderer renderout;
    public float speed;
    // Use this for initialization
    void Start()
    {
        renderout.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (renderout.enabled)
            Trap.transform.Translate(-speed * Time.deltaTime, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        renderout.enabled = true;
        Debug.Log("Surprise.");
    }
}
