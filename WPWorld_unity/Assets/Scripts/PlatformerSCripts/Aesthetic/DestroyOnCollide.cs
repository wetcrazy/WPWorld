using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {

    [SerializeField]
    private int AmountOfDebris;

    [SerializeField]
    private GameObject Debris;

    [SerializeField]
    private AudioClip DestroySFX;

    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
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

        if(CollidedObject.tag == "Player" && RenderRef.isVisible)
        {
            if (CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y / 2
                <= transform.position.y - transform.lossyScale.y / 2 && Mathf.Abs(CollidedObject.transform.position.x - transform.position.x) < transform.lossyScale.x * 0.5f)
            {
                if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded() && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0)
                {
                    RenderRef.enabled = false;

                    if (DestroySFX != null && GameObject.Find("Sound System") != null)
                        GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(DestroySFX);

                    for (int i = 0; i < AmountOfDebris; i++)
                    {
                        GameObject n_Debris = Instantiate(Debris, this.transform);
                        Rigidbody RigidRef = n_Debris.GetComponent<Rigidbody>();
                        RigidRef.AddForce(new Vector3(Random.Range(-50, 50),
                            Random.Range(25, 50),
                            Random.Range(-50, 50)));
                    }

                    Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                    CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
                }
            }
        }
    }
}
