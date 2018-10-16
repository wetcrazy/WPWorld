using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    [SerializeField]
    private GameObject BGM;
    [SerializeField]
    private List<GameObject> SFX = new List<GameObject>();

    [SerializeField]
    private AudioClip TestClip;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayBGM(TestClip);
            BGM.GetComponent<AudioSource>().Play();
        }
    }

    public void PlayBGM(AudioClip n_BGM)
    {
        BGM.GetComponent<AudioSource>().Stop();
        BGM.GetComponent<AudioSource>().clip = n_BGM;
        BGM.GetComponent<AudioSource>().Play();
    }

    public void PlaySFX(AudioClip n_SFX)
    {
        foreach(GameObject SFXRef in SFX)
        {
            if (SFXRef.GetComponent<AudioSource>().isPlaying)
                continue;
            SFXRef.GetComponent<AudioSource>().clip = n_SFX;
            SFXRef.GetComponent<AudioSource>().Play();
            return;
        }

        Debug.Log("No Audio Source Avaliable To Play!");
    }
}
