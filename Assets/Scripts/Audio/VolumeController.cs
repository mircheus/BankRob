using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";
    
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

    public void ChangeVolume(float volume)
    {
        _audioMixer.SetFloat(MasterVolume, Mathf.Lerp(-80, 0, volume));
    }
}
