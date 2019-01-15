using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public int ID;

    [SerializeField]
    private int AmountOfDebris;

    [SerializeField]
    private GameObject Debris;

    [SerializeField]
    private string DestroySFX;

    private Renderer RenderRef;
    private Collider ColliderRef;
    private SoundSystem SoundSystemRef;

    // Use this for initialization
    void Start()
    {
        RenderRef = GetComponent<Renderer>();
        ColliderRef = GetComponent<Collider>();

        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).name.Contains(Debris.name))
                transform.GetChild(i).GetComponent<Renderer>().enabled = RenderRef.isVisible;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!RenderRef.isVisible)
            return;

        if (other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
        {
            if (!other.GetComponent<TPSLogic>().GetGrounded()
                && other.GetComponent<Rigidbody>().velocity.y >= 0
                )
            {
                //Send event to all players that this block has been destroyed
                object[] content = new object[]
                    {
                        ID
                    };

                ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
                Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_BLOCK_BREAK, content, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                //Perform the event locally
                RenderRef.enabled = false;
                ColliderRef.isTrigger = true;

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

                Vector3 VelocityRef = other.GetComponent<Rigidbody>().velocity;
                if (VelocityRef.y > 0)
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                other.GetComponent<Rigidbody>().velocity = VelocityRef;
                
                // Check for Enemies above the block
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.up, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up + transform.right, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up - transform.right, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up + transform.forward, out hit, transform.lossyScale.y)
                    || Physics.Raycast(transform.position, transform.up - transform.forward, out hit, transform.lossyScale.y)
                    )
                {
                    if (hit.transform.GetComponent<Enemy>())
                    {
                        object[] content02 = new object[]
                               {
                                   //Add id
                               };

                        Photon.Pun.PhotonNetwork.RaiseEvent((byte)EventCodes.EVENT_CODES.PLATFORM_EVENT_ENEMY_DEATH_AIR, content02, Photon.Realtime.RaiseEventOptions.Default, sendOptions);

                        hit.transform.GetComponent<Enemy>().AirDeath();
                    }
                }
            }
        }
    }

    public void Reset()
    {
        RenderRef.enabled = true;
        ColliderRef.isTrigger = false;
    }
}
