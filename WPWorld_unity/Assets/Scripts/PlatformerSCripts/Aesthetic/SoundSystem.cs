using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {
    
    [SerializeField]
    private List<AudioSource> SFX = new List<AudioSource>();
    [SerializeField]
    int MaximumSFXPlayingAtOnce = 10;

    bool isMuted_BGM, isMuted_SFX;

    [SerializeField]
    AudioClip[] AudioSounds;
    GameObject[] AudioSources;

    GameObject BackgroundMusic;
    AudioSource BackgroundMusicAudioSource = null;
    
    // Use this for initialization
    void Start () {
        isMuted_BGM = isMuted_SFX = false;
        AudioSounds = Resources.LoadAll<AudioClip>("Audio");
        AudioSources = new GameObject[MaximumSFXPlayingAtOnce];

        for (int i = 0; i < AudioSources.Length; ++i)
        {
            AudioSources[i] = new GameObject("SFX" + i);
            AudioSources[i].transform.parent = transform;
            AudioSources[i].transform.localPosition = Vector3.zero;
            AudioSources[i].AddComponent<AudioSource>();
        }

        BackgroundMusic = new GameObject("BGM");
        BackgroundMusic.transform.parent = transform;
        BackgroundMusic.transform.localPosition = Vector3.zero;
        BackgroundMusic.AddComponent<AudioSource>();
        BackgroundMusicAudioSource = BackgroundMusic.GetComponent<AudioSource>();
        BackgroundMusicAudioSource.loop = true;
    }

    public void PlayBGM(string BGMName)
    {
        if(isMuted_BGM)
        {
            return;
        }

        foreach (AudioClip audioClip in AudioSounds)
        {
            if (audioClip.name != BGMName)
            {
                continue;
            }

            if(BackgroundMusicAudioSource.isPlaying)
            {
                BackgroundMusicAudioSource.Stop();
            }

            BackgroundMusicAudioSource.clip = audioClip;
            BackgroundMusicAudioSource.Play();
            break;
        }
    }

    public void ChangeBGMVolume(float n_Volume) // Changes the volume of the BGM Object
    {
        BackgroundMusicAudioSource.volume = n_Volume;
    }

    public void PlaySFX(string SFXName)
    {
        if(isMuted_SFX)
        {
            return;
        }

        foreach (AudioClip audioClip in AudioSounds)
        {
            if(audioClip.name != SFXName)
            {
                continue;
            }

            foreach (GameObject audioSourceGameObj in AudioSources)
            {
                AudioSource audioSource = audioSourceGameObj.GetComponent<AudioSource>();

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

    public void ChangeSFXVolume(float n_Volume) // Changes the volumes of all the SFX Objects
    {
        foreach(GameObject audioSourceGameObj in AudioSources)
        {
            AudioSource audioRef = audioSourceGameObj.GetComponent<AudioSource>();

            audioRef.volume = n_Volume;
        }
    }
}
