using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnCollide : MonoBehaviour {

    private Renderer RenderRef;

    [SerializeField]
    private string ShowSFX;

    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        RenderRef.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!RenderRef.isVisible && !other.GetComponent<TPSLogic>().GetGrounded())
            {
                if(other.transform.position.y + other.transform.lossyScale.y / 2 < transform.position.y - transform.lossyScale.y / 2
                    && Mathf.Abs(other.transform.position.x - transform.position.x) < transform.lossyScale.x * 0.75f
                    && other.GetComponent<Rigidbody>().velocity.y > 0)
                {
                    GetComponent<Collider>().isTrigger = false;

                    if (ShowSFX != "")
                        SoundSystemRef.PlaySFX(ShowSFX);

                    Vector3 VelocityRef = other.GetComponent<Rigidbody>().velocity;
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                    other.GetComponent<Rigidbody>().velocity = VelocityRef;

                    RenderRef.enabled = true;
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.tag == "Player" && RenderRef.isVisible)
        {
            if (CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y / 2 <= transform.position.y - transform.lossyScale.y / 2
                && Mathf.Abs(CollidedObject.transform.position.x - transform.position.x) < transform.lossyScale.x * 0.75f)
            {
                if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded() && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0)
                {
                    Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                    CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
                }
            }
        }
    }
}
