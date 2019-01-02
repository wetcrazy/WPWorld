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
    private Collider ColliderRef;
    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start()
	{
        OrgPos = transform.localPosition;

        RigidRef = GetComponent<Rigidbody>();
        RenderRef = GetComponent<Renderer>();
        ColliderRef = GetComponent<Collider>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        OrgQuantity = QuantityofSpawns;
        OrgMaterial = RenderRef.material;

        RigidRef.constraints = RigidbodyConstraints.FreezeAll;
	}

	// Update is called once per frame
	void Update()
	{
        if(transform.position.y < OrgPos.y)
        {
            RigidRef.constraints = RigidbodyConstraints.FreezeAll;
            transform.localPosition = OrgPos;
            ColliderRef.isTrigger = false;
        }

        if (QuantityofSpawns <= 0)
        {
            if (!RenderRef.material.name.Contains(ChangedMaterial.name))
                RenderRef.material = ChangedMaterial;
        }
        else
        {
            if (RenderRef.material != OrgMaterial)
                RenderRef.material = OrgMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!other.GetComponent<TPSLogic>().GetGrounded()
                && other.GetComponent<Rigidbody>().velocity.y >= 0)
            {
                // Check for Enemies above the block
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.up, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up + transform.right, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up - transform.right, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up + transform.forward, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up - transform.forward, out hit, transform.lossyScale.y)
                    )
                {
                    if (hit.transform.name.Contains("Enemy"))
                    {
                        hit.transform.GetComponent<Enemy>().AirDeath();
                    }
                }

                Spawn();
            }
        }
    }

    public void Spawn()
    {
        // Bounce Block
        if (QuantityofSpawns <= 0)
        {
            if (EmptySFX != "")
                SoundSystemRef.PlaySFX(EmptySFX);
            RigidRef.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            RigidRef.AddForce(transform.up * 0.5f, ForceMode.VelocityChange);

            ColliderRef.isTrigger = true;
        }
        // Spawn Block
        else
        {
            if (HitSFX != "")
                SoundSystemRef.PlaySFX(HitSFX);
            QuantityofSpawns--;
            Instantiate(ObjectToSpawn, transform.position, transform.rotation, GameObject.Find("Characters").transform);
        }
    }

    public void Reset()
    {
        QuantityofSpawns = OrgQuantity;
    }
}
