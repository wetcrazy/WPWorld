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

        if (CollidedObject.tag == "Player" && !CollidedObject.GetComponent<TPSLogic>().GetGrounded())
        {
            if (CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y / 2 <= transform.position.y - transform.lossyScale.y / 2
                && Mathf.Abs(CollidedObject.transform.position.x - transform.position.x) < transform.lossyScale.x / 2
                && CollidedObject.GetComponent<Rigidbody>().velocity.y > 0)
            {
                if(RenderRef.material.name.Contains(ChangedMaterial.name))
                {
                    if(CollidedObject.GetComponent<Rigidbody>().velocity.y > 0)
                    {
                        Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                        VelocityRef.y = -VelocityRef.y * 0.5f;
                        CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
                    }
                }
                else
                {
                    RenderRef.material = ChangedMaterial;

                    if (GetComponent<SpawnOnCollide>().enabled)
                    {
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

                    if (CollidedObject.GetComponent<Rigidbody>().velocity.y > 0)
                    {
                        Vector3 VelocityRef = CollidedObject.GetComponent<Rigidbody>().velocity;
                        VelocityRef.y = -VelocityRef.y * 0.5f;
                        CollidedObject.GetComponent<Rigidbody>().velocity = VelocityRef;
                    }
                }
            }
        }
    }
}
