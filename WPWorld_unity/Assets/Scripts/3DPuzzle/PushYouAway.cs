using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushYouAway : MonoBehaviour {
    Vector3 direction = new Vector3(0, 0, 90);
    float pushspeed = 2.0f;

    //[SerializeField]
    //private AudioClip what;

    SoundSystem ss;
    // Use this for initialization
    void Start () {
        ss = GameObject.Find("SoundSystem").GetComponent<SoundSystem>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
           ss.PlaySFX("trolled");
            Debug.Log("GG");
            collision.transform.Translate(pushspeed,0,0);

        }
    }
}
