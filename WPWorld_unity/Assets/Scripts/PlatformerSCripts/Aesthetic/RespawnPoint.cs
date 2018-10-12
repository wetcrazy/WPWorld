using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    private bool Interacted;

    [SerializeField]
    private Vector3 InteractedFlagVelocity;

    [SerializeField]
    private AudioClip RespawnSFX;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(Interacted)
        {
            if(Vector3.Distance(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, InteractedFlagVelocity) < 0.1f)
            {
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = InteractedFlagVelocity;
            }
            else
            {
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = Vector3.Lerp(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, InteractedFlagVelocity, Time.deltaTime);
            }
            transform.GetChild(0).GetComponent<Cloth>().randomAcceleration = Vector3.zero;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (Interacted)
            return;
        if(other.tag == "Player")
        {
            Interacted = true;
            other.GetComponent<PlayerMovement>().SetRespawn(this.transform.position);

            if(RespawnSFX != null && GameObject.Find("Sound System") != null)
            {
                GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(RespawnSFX);
            }
        }
    }
}
