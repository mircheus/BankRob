using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _buyClick;
    [SerializeField] private AudioClip _defaultButtonClick;
    
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
