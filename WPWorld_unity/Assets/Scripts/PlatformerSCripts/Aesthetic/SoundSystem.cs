using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {
    
    [SerializeField]
    private List<AudioSource> SFX = new List<AudioSource>();
    [SerializeField]
    int MaximumSFXPlayingAtOnce = 10;

    GameObject BackgroundMusic;
    AudioSource BackgroundMusicAudioSource = null;
    [SerializeField]
    AudioClip[] AudioSounds;
    GameObject[] AudioSources;
    // Use this for initialization
    void Start () {
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

    public void PlaySFX(string SFXName)
    {
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
}
