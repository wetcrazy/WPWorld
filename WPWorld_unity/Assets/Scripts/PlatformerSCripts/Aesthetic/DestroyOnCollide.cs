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

        if (CollidedObject.tag == "Player")
        {
            if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded() // Grounded Check
                && CollidedObject.transform.localPosition.y + CollidedObject.transform.localScale.y * 0.5f <= transform.localPosition.y - transform.localScale.y * 0.5f // Check if the bottom of the gameobject is colliding with the top of the player
                && Mathf.Abs(CollidedObject.transform.localPosition.x - transform.localPosition.x) < transform.localScale.x * 0.5f // Check if the player is within a certain x range to trigger
                && Mathf.Abs(CollidedObject.transform.localPosition.z - transform.localPosition.z) < transform.localScale.z * 0.5f // Check if the player is within a certain z range to trigger
                && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0 // Check if the player is jumping and not falling
                )
            {
                RenderRef.enabled = false;

                if (DestroySFX != "")
                    SoundSystemRef.PlaySFX(DestroySFX);

                for (int i = 0; i < AmountOfDebris; i++)
                {
                    GameObject n_Debris = Instantiate(Debris, this.transform);
                    Rigidbody RigidRef = n_Debris.GetComponent<Rigidbody>();
                    RigidRef.AddForce(new Vector3(Random.Range(-50, 50) * transform.parent.parent.lossyScale.x,
                        Random.Range(25, 50) * transform.parent.parent.lossyScale.y,
                        Random.Range(-50, 50) * transform.parent.parent.lossyScale.z));
                }

                Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                if (VelocityRef.y > 0)
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;

                // Checks if there is an enemy to kill on top of the block
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.up, out hit, transform.localScale.y)
                    || Physics.Raycast(transform.position, transform.up + transform.forward * 0.5f, out hit, transform.localScale.y)
                    || Physics.Raycast(transform.position, transform.up - transform.forward * 0.5f, out hit, transform.localScale.y)
                    || Physics.Raycast(transform.position, transform.up + transform.right * 0.5f, out hit, transform.localScale.y)
                    || Physics.Raycast(transform.position, transform.up - transform.right * 0.5f, out hit, transform.localScale.y))
                    // Kill Enemy if there is an enemy on top of the bounce block
                    if (hit.transform.name.Contains("Enemy"))
                    {
                        hit.transform.GetComponent<Enemy>().AirborneDeath();
                        hit.transform.GetComponent<Rigidbody>().AddForce(0, 50 * transform.parent.parent.lossyScale.y, 0);
                    }
            }
        }
    }

    public void Reset()
    {
        RenderRef.enabled = true;
    }
}
