using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const int MaxMasterVolume = 0;
    private const int MaxMusicVolume = -10;
    private const int ZeroVolume = -80;
    
    [SerializeField] private AudioMixer _audioMixer;

    public void ChangeMasterVolume(float volume)
    {
        _audioMixer.SetFloat(MasterVolume, Mathf.Lerp(ZeroVolume, MaxMasterVolume, volume));
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(ZeroVolume, MaxMusicVolume, volume));
    }
}
