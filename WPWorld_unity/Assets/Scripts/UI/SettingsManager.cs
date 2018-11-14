using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    [SerializeField]
    Slider BGM_slider;
    [SerializeField]
    Slider SFX_slider;

    SoundSystem soundSystem = null;
    bool hasBGMVolumeChange, hasSFXVolumeChange;

    int OriginalBGMVolume, OriginalSFXVolume;
    
    // Use this for initialization
    void Start () {
        soundSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
        gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        gameObject.SetActive(true);
        OriginalBGMVolume = (int)BGM_slider.value;
        OriginalSFXVolume = (int)SFX_slider.value;
    }

    public void hasBGMChanged()
    {
        hasBGMVolumeChange = true;
    }

    public void hasSFXChanged()
    {
        hasSFXVolumeChange = true;
    }

    public void ApplyAndClose()
    {
        if(hasBGMVolumeChange)
        {
            soundSystem.ChangeBGMVolume(BGM_slider.value * 0.01f);
            hasBGMVolumeChange = false;
        }

        if (hasSFXVolumeChange)
        {
            soundSystem.ChangeSFXVolume(SFX_slider.value * 0.01f);
            hasSFXVolumeChange = false;
        }

        gameObject.SetActive(false);
    }

    public void CancelAndClose()
    {
        hasBGMVolumeChange = false;
        hasSFXVolumeChange = false;
        gameObject.SetActive(false);

        BGM_slider.value = OriginalBGMVolume;
        SFX_slider.value = OriginalSFXVolume;
    }
}
