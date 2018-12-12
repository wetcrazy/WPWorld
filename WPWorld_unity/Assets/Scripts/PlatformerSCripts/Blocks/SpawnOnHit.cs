using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnHit : MonoBehaviour {

    // Spawning Variables
    public GameObject ObjectToSpawn;

    // General Variables
    public Material ChangedMaterial;
    private Material OrgMaterial;
    public int QuantityofSpawns;
    private int OrgQuantity;
    public string HitSFX;
    public string EmptySFX;

    private Vector3 OrgPos;

    // Variables to grab
    private Rigidbody RigidRef;
    private Renderer RenderRef;
    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start()
	{
        OrgPos = transform.position;

        RigidRef = GetComponent<Rigidbody>();
        RenderRef = GetComponent<Renderer>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
	}

	// Update is called once per frame
	void Update()
	{
        if(transform.position.y < OrgPos.y)
        {
            RigidRef.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = OrgPos;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!other.GetComponent<TPSLogic>().GetGrounded()
                && other.GetComponent<Rigidbody>().velocity.y >= 0)
            {
                // Bounce Block
                if (QuantityofSpawns <= 0)
                {
                    if (EmptySFX != "")
                        SoundSystemRef.PlaySFX(EmptySFX);
                    RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    RigidRef.AddForce(transform.up, ForceMode.VelocityChange);
                }
                // Spawn Block
                else
                {
                    if (HitSFX != "")
                        SoundSystemRef.PlaySFX(HitSFX);
                    QuantityofSpawns--;
                    if (QuantityofSpawns <= 0)
                        RenderRef.material = ChangedMaterial;
                    Instantiate(ObjectToSpawn, transform.position, transform.rotation, GameObject.Find("Characters").transform);
                }
            }
        }
    }

    public void Reset()
    {
        
    }
}
