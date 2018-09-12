using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    [SerializeField]
    private GameObject BGM;
    [SerializeField]
    private List<GameObject> SFX = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySFX(AudioClip n_SFX)
    {
        foreach(GameObject SFXRef in SFX)
        {
            if(!SFXRef.GetComponent<AudioSource>().isPlaying)
            {
                SFXRef.GetComponent<AudioSource>().clip = n_SFX;
                SFXRef.GetComponent<AudioSource>().Play();
                Debug.Log(SFXRef.name);
                break;
            }
        }
    }
}
