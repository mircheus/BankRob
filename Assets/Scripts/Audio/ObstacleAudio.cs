using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _damageAudioClip;
    [SerializeField] private AudioClip _destroyAudioClip;
    [SerializeField] private Obstacle _obstacle;
    
    [Range(0,1)]
    [SerializeField] private float _customPitch = 1;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _obstacle.Damaged += OnDamaged;
        _obstacle.Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        _obstacle.Damaged -= OnDamaged;
        _obstacle.Destroyed -= OnDestroyed;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDamaged()
    {
        _audioSource.clip = _damageAudioClip;
        _audioSource.pitch = _customPitch;
        _audioSource.Play();
    }

    private void OnDestroyed()
    {
        _audioSource.clip = _destroyAudioClip;
        _audioSource.pitch = _customPitch;
        _audioSource.Play();
    }
}
