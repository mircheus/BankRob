using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePerk : Perk
{
    [SerializeField] private ParticleSystem[] _freezeFx;

    private int _counter = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cage cage))
        {
            // _freezeFx[0].transform.position = cage.transform.position;
            // _freezeFx[0].Play();
            PlayFxAt(cage.transform.position);
            cage.GetFreezedBy(this);
        }
        
        if (other.TryGetComponent(out Dynamite dynamite))
        {
            // _freezeFx[1].transform.position = dynamite.transform.position;
            // _freezeFx[1].Play();
            PlayFxAt(dynamite.transform.position);
            dynamite.GetFreezedBy(this);
        }
    }

    private void PlayFxAt(Vector3 trapPosition)
    {
        _freezeFx[_counter].transform.position = trapPosition;
        _freezeFx[_counter].Play();
        _counter++;

        if (_counter == _freezeFx.Length)
        {
            _counter = 0;
        }
    }
}
