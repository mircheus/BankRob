using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayDestroyAudio()
    {
        _audioSource.Play();
    }
}
