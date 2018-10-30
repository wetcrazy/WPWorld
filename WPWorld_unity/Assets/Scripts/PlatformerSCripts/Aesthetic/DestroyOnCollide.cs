using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {

    [SerializeField]
    private int AmountOfDebris;

    [SerializeField]
    private GameObject Debris;

    [SerializeField]
    private string DestroySFX;

    private Renderer RenderRef;
    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();

        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!RenderRef.isVisible)
        {
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            GetComponent<Collider>().enabled = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if(CollidedObject.tag == "Player")
        {
            Debug.Log(CollidedObject.GetComponent<Rigidbody>().velocity);
        }

        if (CollidedObject.tag == "Player" // If Player
            && RenderRef.isVisible // Check if the block is visible
            && !CollidedObject.GetComponent<TPSLogic>().GetGrounded() // Checks if the player is grounded
            && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0) // Checks if the velocity of the player isn't falling
        {
            if (CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y / 2 <= transform.position.y - transform.lossyScale.y / 2
                && Mathf.Abs(CollidedObject.transform.position.x - transform.position.x) < transform.lossyScale.x * 0.5f
                && Mathf.Abs(CollidedObject.transform.position.z - transform.position.z) < transform.lossyScale.z * 0.5f)
            {
                RenderRef.enabled = false;

                if (DestroySFX != "")
                    SoundSystemRef.PlaySFX(DestroySFX);

                for (int i = 0; i < AmountOfDebris; i++)
                {
                    GameObject n_Debris = Instantiate(Debris, this.transform);
                    Rigidbody RigidRef = n_Debris.GetComponent<Rigidbody>();
                    RigidRef.AddForce(new Vector3(Random.Range(-50, 50),
                        Random.Range(25, 50),
                        Random.Range(-50, 50)));
                }

                if(CollidedObject.GetComponent<Rigidbody>().velocity.y > 0)
                {
                    Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                    CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
                }
            }
        }
    }
}
