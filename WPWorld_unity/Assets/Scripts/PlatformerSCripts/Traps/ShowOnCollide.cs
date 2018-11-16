using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnCollide : MonoBehaviour {

    private Renderer RenderRef;
    private Collider ColliderRef;

    [SerializeField]
    private string ShowSFX;

    private bool Collided;

    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
        ColliderRef = GetComponent<Collider>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        RenderRef.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (RenderRef.isVisible)
        {
            ColliderRef.isTrigger = false;
            Collided = false;
        }
        else
            ColliderRef.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!other.GetComponent<TPSLogic>().GetGrounded() // Grounded Check
                && other.transform.localPosition.y + other.transform.localScale.y * 0.5f <= transform.localPosition.y - transform.localScale.y * 0.25f // Check if the bottom of the gameobject is colliding with the top of the player
                && Mathf.Abs(other.transform.localPosition.x - transform.localPosition.x) < transform.localScale.x * 0.5f // Check if the player is within a certain x range to trigger
                && Mathf.Abs(other.transform.localPosition.z - transform.localPosition.z) < transform.localScale.z * 0.5f // Check if the player is within a certain x range to trigger
                && other.GetComponent<Rigidbody>().velocity.y > 0 // Check if the player is jumping and not falling
                && !Collided // Prevents sound from being played a second time
                )
            {
                if (ShowSFX != "")
                    SoundSystemRef.PlaySFX(ShowSFX);

                Vector3 VelocityRef = other.GetComponent<Rigidbody>().velocity;
                if (VelocityRef.y > 0)
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                other.GetComponent<Rigidbody>().velocity = VelocityRef;

                Collided = true;
                RenderRef.enabled = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.tag == "Player")
        {
            if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded() // Grounded Check
                && CollidedObject.transform.localPosition.y + CollidedObject.transform.localScale.y * 0.5f <= transform.localPosition.y - transform.localScale.y * 0.5f // Check if the bottom of the gameobject is colliding with the top of the player
                && Mathf.Abs(CollidedObject.transform.localPosition.x - transform.localPosition.x) < transform.localScale.x * 0.5f // Check if the player is within a certain x range to trigger
                && Mathf.Abs(CollidedObject.transform.localPosition.z - transform.localPosition.z) < transform.localScale.z * 0.5f // Check if the player is within a certain z range to trigger
                && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0 // Check if the player is jumping and not falling
                )
            {
                Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                if(VelocityRef.y > 0)
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
            }
        }
    }

    public void Reset()
    {
        Collided = false;
        RenderRef.enabled = false;
    }
}
