using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnHit : MonoBehaviour {

    [Header("ID Settings")]
    public int ID;

    [Space]
    [Header("Spawn Settings")]
    // Spawning Variables
    public GameObject ObjectToSpawn;

    // General Variables
    public Material ChangedMaterial;
    private Material OrgMaterial;
    public int NumberOfSpawns;
    private int OrgQuantity;

    [Space]
    [Header("Sound Settings")]
    // Sound Variables
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

        OrgQuantity = NumberOfSpawns;
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

        if (NumberOfSpawns <= 0)
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
        if(other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
        {
            if (!other.GetComponent<TPSLogic>().GetGrounded()
                && other.GetComponent<Rigidbody>().velocity.y >= 0)
            {
                //Send event to all players that this block has spawned something
                object[] content = new object[]
                    {
                        ID
                    };

                ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
                Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_SPAWNER, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

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
                        Enemy enemyScript = hit.transform.GetComponent<Enemy>();

                        object[] content02 = new object[]
                                  {
                                      enemyScript.ID
                                  };

                        Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_AIR, content02, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                        enemyScript.AirDeath();
                    }
                }

                Spawn();
            }
        }
    }

    public void Spawn()
    {
        // Bounce Block
        if (NumberOfSpawns <= 0)
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
            NumberOfSpawns--;
            Instantiate(ObjectToSpawn, transform.position, transform.rotation, GameObject.Find("Characters").transform);
        }
    }

    public void Reset()
    {
        NumberOfSpawns = OrgQuantity;
    }
}
