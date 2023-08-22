using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOneShot : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayOneShot()
    {
        _audioSource.Play();
    }
}
