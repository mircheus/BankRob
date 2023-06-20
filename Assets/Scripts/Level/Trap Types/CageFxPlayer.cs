using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageFxPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _crackFx;
    [SerializeField] private AudioSource _audioSource;

    public void PlayCrackFx()
    {
        _crackFx.Play();
    }

    public void PlayCrackFxSound()
    {
        _audioSource.Play();
    }
}
