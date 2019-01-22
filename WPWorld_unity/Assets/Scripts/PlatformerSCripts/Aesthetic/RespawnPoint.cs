using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    [SerializeField]
    private bool Interacted;


    private Vector3 OrgFlagVelocity;
    private Vector3 OrgFlagRandVelocity;
    [SerializeField]
    private Vector3 InteractedFlagVelocity;

    [SerializeField]
    private string RespawnSFX;

    private GameObject PlayerRef;

    private SoundSystem SoundSystemRef;

	// Use this for initialization
	void Start () {
        SoundSystemRef = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

        PlayerRef = GameObject.FindGameObjectWithTag("Player");

        OrgFlagVelocity = transform.GetChild(0).GetComponent<Cloth>().externalAcceleration;
        OrgFlagRandVelocity = transform.GetChild(0).GetComponent<Cloth>().randomAcceleration;
    }
	
	// Update is called once per frame
	void Update () {
		if(Interacted)
        {
            if(Vector3.Distance(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, InteractedFlagVelocity) < 0.1f)
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = InteractedFlagVelocity;
            else
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = Vector3.Lerp(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, InteractedFlagVelocity, Time.deltaTime);
            transform.GetChild(0).GetComponent<Cloth>().randomAcceleration = Vector3.zero;

            if (PlayerRef.GetComponent<PlayerMovement>().GetRespawn() != transform.position)
                Interacted = false;
        }
        else
        {
            if (Vector3.Distance(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, OrgFlagVelocity) < 0.1f)
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = OrgFlagVelocity;
            else
                transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = Vector3.Lerp(transform.GetChild(0).GetComponent<Cloth>().externalAcceleration, OrgFlagVelocity, Time.deltaTime);
            transform.GetChild(0).GetComponent<Cloth>().randomAcceleration = OrgFlagRandVelocity;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (Interacted)
            return;
        if(other.tag == "Player" && other.GetComponent<TPSLogic>().isMine())
        {
            Interacted = true;
            other.GetComponent<PlayerMovement>().SetRespawn(this.transform.position);

            if (RespawnSFX != "")
                SoundSystemRef.PlaySFX(RespawnSFX);
        }
    }
}
