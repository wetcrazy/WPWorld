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
    private AudioClip CoinSFX;

    [SerializeField]
    private GameObject BounceCoin;

    [SerializeField]
    private AudioClip ItemEnemySFX;

    [SerializeField]
    private GameObject Item;

    [SerializeField]
    private GameObject Enemy;

    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
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
                    GameObject SoundSystemRef = GameObject.Find("Sound System");

                    RenderRef.material = ChangedMaterial;

                    if (GetComponent<SpawnOnCollide>().enabled)
                    {
                        switch (CurrSpawn)
                        {
                            case (SPAWNTYPE.COIN):
                                if (CoinSFX != null && SoundSystemRef != null)
                                    SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(CoinSFX);
                                Instantiate(BounceCoin, this.transform.position, Quaternion.identity);
                                break;
                            case (SPAWNTYPE.ITEM):
                                if (ItemEnemySFX != null && SoundSystemRef != null)
                                    SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(ItemEnemySFX);
                                Instantiate(Item, this.transform.position, Quaternion.identity);
                                break;
                            case (SPAWNTYPE.ENEMY):
                                if (ItemEnemySFX != null && SoundSystemRef != null)
                                    SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(ItemEnemySFX);
                                Instantiate(Enemy, this.transform.position, Quaternion.identity);
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
