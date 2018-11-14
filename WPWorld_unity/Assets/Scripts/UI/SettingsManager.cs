using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    [SerializeField]
    Slider BGM_slider;
    [SerializeField]
    Slider SFX_slider;
    [SerializeField]
    Image BGM_SoundIcon;
    [SerializeField]
    Image SFX_SoundIcon;
    [SerializeField]
    Sprite SoundOnSprite;
    [SerializeField]
    Sprite SoundOffSprite;

    SoundSystem soundSystem = null;
    bool hasBGMVolumeChange, hasSFXVolumeChange;

    int OriginalBGMVolume, OriginalSFXVolume;
    
    // Use this for initialization
    void Start () {
        soundSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundSystem>();
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

        if (BGM_slider.value == 0)
        {
            soundSystem.isMuted_BGM = true;
            BGM_SoundIcon.sprite = SoundOffSprite;
        }
        else if (BGM_SoundIcon.sprite == SoundOffSprite)
        {
            soundSystem.isMuted_BGM = false;
            BGM_SoundIcon.sprite = SoundOnSprite;
        }
    }

    public void hasSFXChanged()
    {
        hasSFXVolumeChange = true;

        if(SFX_slider.value == 0)
        {
            soundSystem.isMuted_SFX = true;
            SFX_SoundIcon.sprite = SoundOffSprite;
        }
        else if (SFX_SoundIcon.sprite == SoundOffSprite)
        {
            soundSystem.isMuted_SFX = false;
            SFX_SoundIcon.sprite = SoundOnSprite;
        }
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
        if(!gameObject.activeSelf)
        {
            return;
        }

        hasBGMVolumeChange = false;
        hasSFXVolumeChange = false;
        gameObject.SetActive(false);

        BGM_slider.value = OriginalBGMVolume;
        SFX_slider.value = OriginalSFXVolume;
    }
}
