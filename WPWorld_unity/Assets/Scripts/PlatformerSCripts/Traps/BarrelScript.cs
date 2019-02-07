using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour {

    private Rigidbody RigidRef;

    [Header("Collide Settings")]
    [SerializeField]
    private GameObject Debris;
    [SerializeField]
    private float AmountOfDebris;

    [Header("Sound Settings")]
    [SerializeField]
    private string BounceSFX;
    [SerializeField]
    private string DestroySFX;

	// Use this for initialization
	void Start () {
        RigidRef = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killbox")
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy();
            return;
        }

        RigidRef.velocity = Vector3.zero;

        RigidRef.AddForce(collision.contacts[0].normal * 100);
    }

    public void Destroy()
    {
        if(Debris != null)
        {
            for(int i = 0; i < AmountOfDebris; i++)
            {
                GameObject n_Debris = Instantiate(Debris, this.transform);
                Rigidbody RigidRef = n_Debris.GetComponent<Rigidbody>();
                if(RigidRef != null)
                    RigidRef.AddForce(new Vector3(Random.Range(-50, 50) * transform.parent.parent.lossyScale.x,
                                                Random.Range(25, 50) * transform.parent.parent.lossyScale.y,
                                                Random.Range(-50, 50) * transform.parent.parent.lossyScale.z));

                n_Debris.transform.parent = null;
            }
        }
        Destroy(this.gameObject);
    }
}
