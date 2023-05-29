using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    
    [SerializeField] private AudioMixer _audioMixer;

    public void ToggleSound(bool enabled)
    {
        if (enabled)
        {
            _audioMixer.SetFloat(MasterVolume, 0);
        }
        else
        {
            _audioMixer.SetFloat(MasterVolume, -80);
        }
    }

    public void ChangeMasterVolume(float volume)
    {
        _audioMixer.SetFloat(MasterVolume, Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioMixer.SetFloat(MusicVolume, Mathf.Lerp(-70, 0, volume));
    }
}
