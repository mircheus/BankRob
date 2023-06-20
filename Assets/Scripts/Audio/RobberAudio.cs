using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pickaxePunch;
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
    
    private void OnEnable()
    {
        _obstacleCrusher.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _obstacleCrusher.Attacked -= OnAttacked;
    }

    private void OnAttacked()
    {
        _audioSource.PlayOneShot(_pickaxePunch);
    }
    
    
}
