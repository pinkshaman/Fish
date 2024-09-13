using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundControlUI : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider SFXSoundSlider;
    public Toggle muteAll;

    public AudioSource musicAudioSource;
    public AudioSource SFXAudioSource;

    private float previousMusicVolume;
    private float previousSFXVolume;
    public void Start()
    {
        muteAll.onValueChanged.AddListener(OnToggleValueChanged);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSoundSlider.onValueChanged.AddListener(SetSFXVolume);

        previousMusicVolume = musicVolumeSlider.value;
        previousSFXVolume = SFXSoundSlider.value;

    }
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            previousMusicVolume = musicAudioSource.volume;
            previousSFXVolume = SFXAudioSource.volume;
            musicAudioSource.volume = 0f;
            musicAudioSource.volume = 0f;      
        }
        else
        {
            musicAudioSource.volume = previousMusicVolume;
            SFXAudioSource.volume= previousSFXVolume;
        }
    }
    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        SFXAudioSource.volume = volume;
    }

}
