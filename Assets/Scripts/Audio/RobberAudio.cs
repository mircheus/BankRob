using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _pickaxePunch;
    [SerializeField] private ObstacleCrusher _obstacleCrusher;
        
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _obstacleCrusher.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _obstacleCrusher.Attacked -= OnAttacked;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnAttacked()
    {
        _audioSource.Play();
        Debug.Log("PickaxePunch");
    }
}
