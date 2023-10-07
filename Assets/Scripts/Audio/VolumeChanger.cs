using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const string IsSettingsSavedByPlayer = "IsSettingsSavedByPlayer";
    private const int MaxMasterVolume = 0;
    private const int MaxMusicVolume = -10;
    private const int ZeroVolume = -80;
    
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Start()
    {
        SetVolumeSettings();
    }

    public void ChangeMasterVolume(float volume)
    {
        _audioMixer.SetFloat(MasterVolume, Mathf.Lerp(ZeroVolume, MaxMasterVolume, volume));
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(ZeroVolume, MaxMusicVolume, volume));
    }

    public void SaveVolumeSettings()
    {
        // _audioMixer.GetFloat(MasterVolume, out float masterVolume);
        // _audioMixer.GetFloat(MusicVolume, out float musicVolume);
        PlayerPrefs.SetFloat(MasterVolume, _masterVolumeSlider.value);
        PlayerPrefs.SetFloat(MusicVolume, _musicVolumeSlider.value);
        PlayerPrefs.SetInt(IsSettingsSavedByPlayer, 1);
    }

    private void SetVolumeSettings()
    {
        if (PlayerPrefs.GetInt(IsSettingsSavedByPlayer) == 1)
        {
            _masterVolumeSlider.value = PlayerPrefs.GetFloat(MasterVolume);
            _musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolume);
        }
        else
        {
            _masterVolumeSlider.value = 1;
            _musicVolumeSlider.value = 1;
        }
    }
}
