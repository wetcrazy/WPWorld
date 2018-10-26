using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollide : MonoBehaviour {


    [SerializeField]
    private AudioClip CollectSFX;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 5, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 

              //  if (CollectSFX != null && GameObject.Find("Sound System") != null)
            GameObject.Find("Sound System").GetComponent<SoundSystem>().PlaySFX(CollectSFX);
            Destroy(gameObject);
        }
    }
}
