using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource.Play();
    }
}
