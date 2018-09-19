using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnCollide : MonoBehaviour {

    private Renderer RenderRef;

    [SerializeField]
    private AudioClip ShowSFX;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();

        RenderRef.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!RenderRef.isVisible && !other.GetComponent<TPSLogic>().GetGrounded())
            {
                if(other.transform.position.y < transform.position.y && Mathf.Abs(other.transform.position.x - transform.position.x) < transform.lossyScale.x / 2)
                {
                    GetComponent<Collider>().isTrigger = false;

                    if (ShowSFX != null)
                        GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(ShowSFX);

                    Vector3 N_Pos = other.transform.position;

                    N_Pos.y = (transform.position.y - transform.lossyScale.y / 2) - other.transform.lossyScale.y;

                    other.transform.position = N_Pos;
                    RenderRef.enabled = true;
                }
            }
        }
    }
}
