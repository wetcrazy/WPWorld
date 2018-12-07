using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SPAWNTYPE
{
    COIN,
    ITEM,
    ENEMY
}

public class SpawnOnCollide : MonoBehaviour {

    [SerializeField]
    private SPAWNTYPE CurrSpawn;

    private Material OrgMaterial;

    [SerializeField]
    private Material ChangedMaterial;

    [SerializeField]
    private string CoinSFX;

    [SerializeField]
    private GameObject BounceCoin;

    [SerializeField]
    private string ItemEnemySFX;

    [SerializeField]
    private GameObject Item;

    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private int AmountToSpawn = 1;

    private Renderer RenderRef;
    private SoundSystem SoundSystemRef;

    // Reset Variables
    private int OrgAmount;

    // Use this for initialization
    void Start () {
        RenderRef = GetComponent<Renderer>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        OrgMaterial = RenderRef.material;
        OrgAmount = AmountToSpawn;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.tag == "Player")
        {
            if (!CollidedObject.GetComponent<TPSLogic>().GetGrounded() // Grounded Check
                && CollidedObject.transform.localPosition.y + CollidedObject.transform.localScale.y * 0.5f <= transform.localPosition.y - transform.localScale.y * 0.5f // Check if the bottom of the gameobject is colliding with the top of the player
                && Mathf.Abs(CollidedObject.transform.localPosition.x - transform.localPosition.x) < transform.localScale.x * 0.4f // Check if the player is within a certain x range to trigger
                && Mathf.Abs(CollidedObject.transform.localPosition.z - transform.localPosition.z) < transform.localScale.z * 0.4f // Check if the player is within a certain z range to trigger
                )
            {
                if (!RenderRef.material.name.Contains(ChangedMaterial.name))
                {
                    AmountToSpawn--;
                    if(AmountToSpawn == 0)
                    {
                        RenderRef.material = ChangedMaterial;
                    }

                    GameObject ToSpawn;
                    if(CurrSpawn == SPAWNTYPE.COIN)
                    {
                        if (CoinSFX != "")
                            SoundSystemRef.PlaySFX(CoinSFX);
                        ToSpawn = Instantiate(BounceCoin, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        if (ItemEnemySFX != "")
                            SoundSystemRef.PlaySFX(ItemEnemySFX);
                        if (CurrSpawn == SPAWNTYPE.ENEMY)
                            ToSpawn = Instantiate(Enemy, transform.position, transform.rotation);
                        else
                            ToSpawn = Instantiate(Item, transform.position, transform.rotation);
                    }

                    ToSpawn.transform.parent = GameObject.Find("Characters").transform;
                }

                Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                if (VelocityRef.y > 0)
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
            }
        }
    }

    public void Reset()
    {
        RenderRef.material = OrgMaterial;
        AmountToSpawn = OrgAmount;
    }
}
