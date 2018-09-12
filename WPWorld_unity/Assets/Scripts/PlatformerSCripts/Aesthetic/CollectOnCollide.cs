using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnCollide : MonoBehaviour {

    private Renderer RenderRef;
    [SerializeField]
    private AudioClip CollectSFX;

	// Use this for initialization
	void Start () {
        RenderRef = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(Camera.main.transform,Camera.main.transform.up);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(RenderRef.isVisible)
            {
                RenderRef.enabled = false;

                if(CollectSFX != null)
                    GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(CollectSFX);
            }
        }
    }
}
