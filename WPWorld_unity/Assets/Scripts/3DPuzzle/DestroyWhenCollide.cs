using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollide : MonoBehaviour {


    //[SerializeField]
    //private AudioClip CollectSFX;

    SoundSystem ss;
    // Use this for initialization
    void Start () {
        ss = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();

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
           ss.PlaySFX("you're_already_dead");
            Destroy(gameObject);
        }
    }
}
