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

    private Renderer RenderRef;
    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
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
                && Mathf.Abs(CollidedObject.transform.localPosition.x - transform.localPosition.x) < transform.localScale.x * 0.5f // Check if the player is within a certain x range to trigger
                && Mathf.Abs(CollidedObject.transform.localPosition.z - transform.localPosition.z) < transform.localScale.z * 0.5f // Check if the player is within a certain z range to trigger
                && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0 // Check if the player is jumping and not falling
                )
            {
                if (!RenderRef.material.name.Contains(ChangedMaterial.name))
                {
                    RenderRef.material = ChangedMaterial;

                    switch (CurrSpawn)
                    {
                        case (SPAWNTYPE.COIN):
                            if (CoinSFX != "")
                                SoundSystemRef.PlaySFX(CoinSFX);
                            Instantiate(BounceCoin, transform.position, Quaternion.identity);
                            break;
                        case (SPAWNTYPE.ITEM):
                            if (ItemEnemySFX != "")
                                SoundSystemRef.PlaySFX(ItemEnemySFX);
                            Instantiate(Item, transform.position, transform.rotation);
                            break;
                        case (SPAWNTYPE.ENEMY):
                            if (ItemEnemySFX != "")
                                SoundSystemRef.PlaySFX(ItemEnemySFX);
                            Instantiate(Enemy, transform.position, transform.rotation);
                            break;
                    }
                }

                Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                if (VelocityRef.y > 0)
                    VelocityRef.y = -VelocityRef.y * 0.5f;
                CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
            }
        }
    }
}
