using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {

    [SerializeField]
    private GameObject Debris;

    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerRef = GameObject.FindGameObjectWithTag("Player");

        if (!RenderRef.isVisible)
        {
            Physics.IgnoreCollision(PlayerRef.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            Physics.IgnoreCollision(PlayerRef.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && RenderRef.isVisible)
        {
            if (collision.gameObject.transform.position.y + collision.gameObject.transform.lossyScale.y / 2
                <= transform.position.y - transform.lossyScale.y / 2)
            {
                RenderRef.enabled = false;

                for (int i = 0; i < 4; i++)
                {
                    GameObject n_Debris = Instantiate(Debris, this.transform);
                    Rigidbody RigidRef = n_Debris.GetComponent<Rigidbody>();
                    RigidRef.AddForce(new Vector3(Random.Range(-50, 50),
                        Random.Range(25, 50),
                        Random.Range(-50, 50)));
                }
            }
        }
    }
}
