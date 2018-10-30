using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {
    
    [SerializeField]
    private List<AudioSource> SFX = new List<AudioSource>();
    [SerializeField]
    int MaximumSFXPlayingAtOnce = 10;

    AudioSource BackgroundMusic;
    AudioClip[] AudioSounds;
    AudioSource[] AudioSources;

	// Use this for initialization
	void Start () {
        AudioSounds = Resources.LoadAll<AudioClip>("Audio");
        AudioSources = new AudioSource[MaximumSFXPlayingAtOnce];
        BackgroundMusic.loop = true;
    }
    
    public void PlayBGM(string BGMName)
    {
        foreach (AudioClip audioClip in AudioSounds)
        {
            if (audioClip.name != BGMName)
            {
                continue;
            }

            if(BackgroundMusic.isPlaying)
            {
                BackgroundMusic.Stop();
            }

            BackgroundMusic.clip = audioClip;
            BackgroundMusic.Play();
            break;
        }
    }

    public void PlaySFX(string SFXName)
    {
        foreach (AudioClip audioClip in AudioSounds)
        {
            if(audioClip.name != SFXName)
            {
                continue;
            }

            foreach (AudioSource audioSource in AudioSources)
            {
                if(audioSource.isPlaying)
                {
                    continue;
                }

                audioSource.clip = audioClip;
                audioSource.Play();
                return;
            }
        }
    }
}
