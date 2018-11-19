using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePowerUpOnCollide : MonoBehaviour {

    [SerializeField]
    private POWERUPS PowerUpToGive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).Rotate(Vector3.up * 10);
        transform.GetChild(1).right = -(Camera.main.transform.position - transform.localPosition).normalized;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerPowerUp>().SetPowerUp(PowerUpToGive);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject CollidedObject = collision.gameObject;

        if(CollidedObject.tag == "Player")
        {
            CollidedObject.GetComponent<PlayerPowerUp>().SetPowerUp(PowerUpToGive);
            Destroy(this.gameObject);
        }
    }
}
