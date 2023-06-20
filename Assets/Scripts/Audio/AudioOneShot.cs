using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShot : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayOneShot()
    {
        _audioSource.Play();
    }
}
