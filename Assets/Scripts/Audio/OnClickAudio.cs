using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnClickButton()
    {
        _audioSource.Play();
    }
}
