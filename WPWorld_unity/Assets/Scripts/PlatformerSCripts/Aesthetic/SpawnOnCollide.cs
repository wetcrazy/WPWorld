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
    private GameObject Enemy;

    private Renderer RenderRef;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.tag == "Player")
        {
            if (CollidedObject.transform.position.y + CollidedObject.transform.lossyScale.y / 2
                <= transform.position.y - transform.lossyScale.y / 2 && Mathf.Abs(CollidedObject.transform.position.x - transform.position.x) < transform.lossyScale.x / 2)
            {
                GameObject SoundSystemRef = GameObject.Find("Sound System");

                RenderRef.material = ChangedMaterial;

                if(GetComponent<SpawnOnCollide>().enabled)
                {
                    switch (CurrSpawn)
                    {
                        case (SPAWNTYPE.COIN):
                            if (CoinSFX != null)
                                SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(CoinSFX);
                            Instantiate(BounceCoin, this.transform.position, Quaternion.identity);
                            break;
                        case (SPAWNTYPE.ITEM):
                            if (ItemEnemySFX != null)
                                SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(ItemEnemySFX);
                            break;
                        case (SPAWNTYPE.ENEMY):
                            if (ItemEnemySFX != null)
                                SoundSystemRef.GetComponent<SoundSystem>().PlaySFX(ItemEnemySFX);
                            break;
                    }
                }

                GetComponent<BounceOnCollide>().enabled = true;
                GetComponent<SpawnOnCollide>().enabled = false;
            }
        }
    }
}
