using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnCollide : MonoBehaviour {

    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();

        RenderRef.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerRef = GameObject.FindGameObjectWithTag("Player");

        if(!RenderRef.isVisible)
        {
            if (PlayerRef.transform.position.y < transform.position.y)
            {
                Physics.IgnoreCollision(PlayerRef.GetComponent<Collider>(), GetComponent<Collider>(), false);
            }
            else
            {
                Physics.IgnoreCollision(PlayerRef.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.transform.position.y + collision.gameObject.transform.lossyScale.y / 2
                <= transform.position.y - transform.lossyScale.y / 2)
            {
                if(!RenderRef.isVisible)
                {
                    RenderRef.enabled = true;
                }
            }
        }
    }
}
